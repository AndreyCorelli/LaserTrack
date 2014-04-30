using System;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Xml;
using System.Linq;

namespace TargetTracker
{
    /// <summary>
    /// помечает свойства класса, простые или другие классы
    /// если свойство есть класс:
    /// - должен иметь конструктор по умолчанию
    /// списки также могут помечаться атрибутом
    /// если список помечен атрибутом, он (список) должен инициализироваться в
    /// конструкторе по умолчанию
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyXMLTagAttribute : Attribute
    {
        public string Title { get; set; }
        public string DefaultValue { get; set; }
        public string FormatString { get; set; }
        public PropertyXMLTagAttribute()
        {            
        }
        public PropertyXMLTagAttribute(string title)
        {
            Title = title;
        }
        public PropertyXMLTagAttribute(string title, string defaultValue)
        {
            Title = title;
            DefaultValue = defaultValue;
        }
        public PropertyXMLTagAttribute(string title, string defaultValue, string formatString)
        {
            Title = title;
            DefaultValue = defaultValue;
            FormatString = formatString;
        }

        public static bool InitObjectProperties(object o, XmlElement root)
        {
            return InitObjectProperties(o.GetType(), o, root);
        }

        private static bool InitObjectProperties(Type objType, object o, XmlElement root)
        {
            if (root == null) return false;
            var hasProToInit = false;            
            foreach (var pi in objType.GetProperties())
            {
                foreach (var genAttr in pi.GetCustomAttributes(true))
                {
                    if (genAttr is PropertyXMLTagAttribute == false) continue;
                    hasProToInit = true;
                    var atr = (PropertyXMLTagAttribute) genAttr;
                    // найти элемент под корнем
                    var nodeName = string.IsNullOrEmpty(atr.Title) ? pi.Name : atr.Title;
                    var nodes = root.SelectNodes(nodeName);
                    if (nodes == null) continue;
                    if (nodes.Count == 0) continue;
                    
                    var isList = pi.PropertyType.GetInterfaces().Contains(typeof(IList));
                    if (isList)
                    {// все чуть сложней - надо прочитать все узлы и инициализировать набор сущностей
                        // затем воткнуть их в массив или свойство
                        var itemType = pi.PropertyType.GetGenericArguments()[0];
                        var propValue = (IList)pi.GetValue(o, null);
                        foreach (XmlElement nod in nodes)
                        {
                            var newItem = itemType.IsClass 
                                ? itemType.GetConstructor(new Type[0]).Invoke(new object[0])
                                : Activator.CreateInstance(itemType);
                            if (!InitObjectProperties(itemType, newItem, nod))
                                newItem = InitObject(itemType, nod, atr.DefaultValue, atr.FormatString);
                            // воткнуть в массив или в список
                            propValue.Add(newItem);
                        }
                    }
                    else
                    {
                        var node = (XmlElement) nodes[0];
                        // если свойство - класс, чьи поля могут быть помечены атрибутами, попробовать инициализировать их
                        if (o == null) o = objType.GetConstructor(new Type[0]).Invoke(new object[0]);
                        if (InitObjectProperties(pi.PropertyType, pi.GetValue(o, null), node)) continue;
                        // свойство простого типа
                        InitProperty(o, pi, node, atr.DefaultValue, atr.FormatString);
                    }
                }
            }
            return hasProToInit;
        }
        
        public static bool SaveObjectProperties(object o, XmlElement root)
        {
            if (root == null) return false;
            var hasChildren = false;
            foreach (var pi in o.GetType().GetProperties())
            {
                foreach (var genAttr in pi.GetCustomAttributes(true))
                {
                    if (genAttr is PropertyXMLTagAttribute == false) continue;
                    var atr = (PropertyXMLTagAttribute) genAttr;
                    hasChildren = true;
                    
                    var nodeName = string.IsNullOrEmpty(atr.Title) ? pi.Name : atr.Title;
                    var nodeVal = pi.GetValue(o, null);
                    if (nodeVal == null) continue;

                    if (pi.PropertyType.IsGenericType && 
                        pi.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        var listType = typeof(Nullable<>);
                        var genericPropType = listType.MakeGenericType(new [] {
                            Nullable.GetUnderlyingType(pi.PropertyType)});
                        var defaultGenericSpeciman = Activator.CreateInstance(genericPropType);
                        if (defaultGenericSpeciman != null)
                            if (defaultGenericSpeciman.Equals(nodeVal)) continue;
                    }

                    if (nodeVal is IList)
                    {// сохранить каждое значение
                        foreach (var nodeValItem in (IList)nodeVal)
                        {
                            var item = (XmlElement)root.AppendChild(root.OwnerDocument.CreateElement(nodeName));
                            if (!SaveObjectProperties(nodeValItem, item))
                                SaveNodeValue(item, nodeValItem, atr.DefaultValue, atr.FormatString);
                        }
                    }
                    else
                    {// элемент скаляр
                        var item = (XmlElement)root.AppendChild(root.OwnerDocument.CreateElement(nodeName));
                        if (!SaveObjectProperties(nodeVal, item))
                            SaveNodeValue(item, nodeVal, atr.DefaultValue, atr.FormatString);
                    }
                }
            }
            return hasChildren;
        }

        private static void SaveNodeValue(XmlElement item, object val,
            string defValue, string customFormat)
        {            
            var attr = item.Attributes.Append(item.OwnerDocument.CreateAttribute("value"));
            
            if (val == null)
            {
                attr.Value = defValue;
                return;
            }

            var ci = CultureInfo.InvariantCulture;
            var strVal = defValue;
            var valType = val.GetType();

            if (valType == typeof(string)) strVal = (string)val;
            else if (valType == typeof(bool) || valType == typeof(int)) strVal = val.ToString();
            else if (valType == typeof(decimal)) strVal = ((decimal)val).ToString(ci);
            else if (valType == typeof(long)) strVal = ((long)val).ToString(ci);
            else if (valType == typeof(float)) strVal = ((float)val).ToString(ci);
            else if (valType == typeof(double)) strVal = ((double)val).ToString(ci);
            else if (valType == typeof(DateTime))
                strVal = string.IsNullOrEmpty(customFormat)
                             ? ((DateTime)val).ToString(ci)
                             : ((DateTime)val).ToString(customFormat, ci);
            else if (valType == typeof(Point)) strVal = string.Format("{0};{1}", ((Point)val).X, ((Point)val).Y);
            else if (valType == typeof(Size)) strVal = string.Format("{0};{1}", ((Size)val).Width, ((Size)val).Height);
            else if (valType == typeof(Color)) strVal = ((Color)val).ToArgb().ToString();
            else if (valType.IsSubclassOf(typeof(Enum))) strVal = val.ToString();

            attr.Value = strVal;
        }
        
        private static void InitProperty(object o, PropertyInfo pi, XmlElement node, string defaultValue, string formatString)
        {            
            var valStr = node.Attributes["value"].Value;
            if (string.IsNullOrEmpty(valStr)) valStr = defaultValue;
            if (string.IsNullOrEmpty(valStr)) return;
            
            var propType = pi.PropertyType;
            object val = ParseStringValue(valStr, propType, formatString);

            if (val != null) pi.SetValue(o, val, null);
        }

        private static object InitObject(Type objType, XmlElement node, string defaultValue, string formatString)
        {            
            var valStr = node.Attributes["value"].Value;
            if (string.IsNullOrEmpty(valStr)) valStr = defaultValue;
            if (string.IsNullOrEmpty(valStr)) return null;
            object val = ParseStringValue(valStr, objType, formatString);
            return val;
        }

        private static object ParseStringValue(string valStr, Type propType, string formatString)
        {
            var ci = CultureInfo.InvariantCulture;
            object val = null;
            if (propType == typeof(bool)) val = Boolean.Parse(valStr);
            else if (propType == typeof(int)) val = Int32.Parse(valStr);
            else if (propType == typeof(string)) val = valStr;
            else if (propType == typeof(float)) val = float.Parse(valStr, ci);
            else if (propType == typeof(double)) val = double.Parse(valStr, ci);
            else if (propType == typeof(long)) val = long.Parse(valStr, ci);
            else if (propType == typeof(decimal)) val = decimal.Parse(valStr, ci);
            else if (propType == typeof(decimal?)) val = decimal.Parse(valStr, ci);
            else if (propType == typeof(DateTime))
                val = string.IsNullOrEmpty(formatString)
                          ? DateTime.Parse(valStr, ci)
                          : DateTime.ParseExact(valStr, formatString, ci);
            else if (propType == typeof(Color))
                val = Color.FromArgb(int.Parse(valStr));
            else if (propType == typeof(Point))
            {
                var parts = valStr.Split(new[] { '.', ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2) val = new Point(int.Parse(parts[0]), int.Parse(parts[1]));
            }
            else if (propType == typeof(Size))
            {
                var parts = valStr.Split(new[] { '.', ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2) val = new Size(int.Parse(parts[0]), int.Parse(parts[1]));
            }
            else if (propType.IsSubclassOf(typeof(Enum))) val = Enum.Parse(propType, valStr);
            return val;
        }
    }
}
