using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace TargetTracker
{
    public class TargetStorage
    {
        private string storageFilePath = string.Format("{0}\\targets.xml", ExecutablePath.ExecPath);
        private static TargetStorage instance;
        public static TargetStorage Instance
        {
            get { return instance ?? (instance = new TargetStorage()); }
        }
        private TargetStorage()
        {            
        }

        public List<BaseTarget> LoadTargets()
        {
            if (!File.Exists(storageFilePath)) return new List<BaseTarget>();
            try
            {
                var doc = new XmlDocument();
                doc.Load(storageFilePath);
                var targets = new List<BaseTarget>();
                foreach (XmlElement node in doc.DocumentElement)
                {
                    var nodeType = node.Name;
                    var targetClass = BaseTarget.targetByName[nodeType];
                    var target = (BaseTarget) targetClass.GetConstructor(new Type[0]).Invoke(null);
                    target.Deserialize(node);
                    targets.Add(target);
                }
                return targets;
            }
            catch (Exception ex)
            {
                Logger.Error("Ошибка загрузки базы мишеней", ex);
                return new List<BaseTarget>();
            }
        }
    }
}
