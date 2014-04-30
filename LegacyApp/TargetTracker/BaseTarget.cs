using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Xml;

namespace TargetTracker
{
    public abstract class BaseTarget
    {
        public static Dictionary<string, Type> targetByName = new Dictionary<string, Type> 
        { { "RoundTarget", typeof(RoundTarget) }};

        public string name;

        public string comment;
        
        /// <summary>
        /// расст между парами маркеров по вертикали и горизонтали
        /// </summary>
        public Size size;
        
        /// <param name="worldXmm">координата Х от центра, мм</param>
        /// <param name="worldYmm">координата У от центра, мм (+ вверх от 0)</param>
        public abstract double GetMark(double worldXmm, double worldYmm);

        public virtual void Deserialize(XmlElement node)
        {
            name = node.Attributes["name"].Value;
            comment = node.Attributes["comment"].Value;
            var strSz = node.Attributes["size"].Value.Split(new [] { ';' });
            size = new Size(strSz[0].ToInt(), strSz[1].ToInt());
        }

        public virtual void Serialize(XmlElement node)
        {
            node.Attributes.Append(node.OwnerDocument.CreateAttribute("name")).Value = name;
            node.Attributes.Append(node.OwnerDocument.CreateAttribute("comment")).Value = comment;
            node.Attributes.Append(node.OwnerDocument.CreateAttribute("size")).Value = string.Format("{0};{1}", size.Width, size.Height);
        }
    }

    public class RoundTarget : BaseTarget
    {
        /// <summary>
        /// массив радиусов окружностей по-возрастанию
        /// </summary>
        public decimal[] radius;
        public int[] mark;

        public override double GetMark(double worldXmm, double worldYmm)
        {
            var rad = Math.Sqrt(worldXmm*worldXmm + worldYmm*worldYmm);
            for (var i = 0; i < radius.Length; i++)
            {
                if (rad <= (double)radius[i]) return mark[i];
            }
            return 0;
        }

        public override void Deserialize(XmlElement node)
        {
            base.Deserialize(node);
            var radMark = node.Attributes["markByRad"].Value.ToDecimalArrayUniform();
            var lstRad = new List<decimal>();
            var lstMark = new List<int>();
            for (var i = 0; i < radMark.Length; i += 2)
            {
                lstRad.Add(radMark[i]);
                lstMark.Add((int)radMark[i + 1]);
            }
            radius = lstRad.ToArray();
            mark = lstMark.ToArray();
        }

        public override void Serialize(XmlElement node)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < radius.Length; i++)
            {
                sb.AppendFormat("{0};{1} ", radius[i].ToStringUniform(), mark[i]);
            }
            node.Attributes.Append(node.OwnerDocument.CreateAttribute("markByRad")).Value = sb.ToString();
        }        
    }
}
