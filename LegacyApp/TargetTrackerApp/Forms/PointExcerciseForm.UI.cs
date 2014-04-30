using System;
using System.IO;
using System.Xml;
using TargetTracker;

namespace TargetTrackerApp.Forms
{
    public partial class PointExcerciseForm
    {
        private readonly Random randomGenr = new Random(DateTime.Now.Millisecond);

        #region Настройки
        private int iterationCount;
        private int[] timeoutBeforeFirst;
        private int[] timeoutBetweenIters;
        private int[] timeToHold;
        private double minScore;
        private bool randomCamera;
        private bool checkFalstart;
        #endregion

        #region Status Label
        private delegate void PassTextDel(string text);
        private void SetStatusLabelUnsafe(string text)
        {
            lblStatus.Text = text;
        }
        private void SetStatusLabelSafe(string text)
        {
            BeginInvoke(new PassTextDel(SetStatusLabelUnsafe), text);
        }
        #endregion

        private void InitSettings()
        {
            iterationCount = tbItersCount.Text.ToInt();

            timeoutBeforeFirst = tbStartDelay.Text.ToIntArrayUniform();
            if (timeoutBeforeFirst.Length == 1)
                timeoutBeforeFirst = new[] { timeoutBeforeFirst[0], timeoutBeforeFirst[0] };

            timeoutBetweenIters = tbInterBetweenIters.Text.ToIntArrayUniform();
            if (timeoutBetweenIters.Length == 1)
                timeoutBetweenIters = new[] { timeoutBetweenIters[0], timeoutBetweenIters[0] };

            timeToHold = tbTimeToHold.Text.ToIntArrayUniform();
            if (timeToHold.Length == 1)
                timeToHold = new[] { timeToHold[0], timeToHold[0] };

            minScore = tbMinScore.Text.ToDoubleUniform();
            randomCamera = cbRandomCamera.Checked;
            checkFalstart = cbCheckFalstart.Checked;
        }

        private void LoadSettings()
        {
            var path = ExecutablePath.ExecPath + "\\settings.xml";
            if (!File.Exists(path)) return;
            var doc = new XmlDocument();
            doc.Load(path);
            if (doc.DocumentElement == null) return;
            var pointingExcNodes = doc.DocumentElement.GetElementsByTagName("pointing");
            if (pointingExcNodes.Count == 0) return;
            var nodeSets = (XmlElement)pointingExcNodes[0];
            
            if (nodeSets.Attributes["iterationCount"] != null)
                tbItersCount.Text = nodeSets.Attributes["iterationCount"].Value;
            if (nodeSets.Attributes["timeoutBeforeFirst"] != null)
                tbStartDelay.Text = nodeSets.Attributes["timeoutBeforeFirst"].Value;
            if (nodeSets.Attributes["timeoutBetweenIters"] != null)
                tbInterBetweenIters.Text = nodeSets.Attributes["timeoutBetweenIters"].Value;
            if (nodeSets.Attributes["timeToHold"] != null)
                tbTimeToHold.Text = nodeSets.Attributes["timeToHold"].Value;
            if (nodeSets.Attributes["minScore"] != null)
                tbMinScore.Text = nodeSets.Attributes["minScore"].Value;
            if (nodeSets.Attributes["randomCamera"] != null)
                cbRandomCamera.Checked = nodeSets.Attributes["randomCamera"].Value.ToBool();
            if (nodeSets.Attributes["checkFalstart"] != null)
                cbCheckFalstart.Checked = nodeSets.Attributes["checkFalstart"].Value.ToBool();
        }

        private void SaveSettings()
        {
            var path = ExecutablePath.ExecPath + "\\settings.xml";
            var doc = new XmlDocument();
            XmlElement nodeSets = null;
            if (File.Exists(path))
            {
                doc.Load(path);
                if (doc.DocumentElement != null)
                {
                    var pointingExcNodes = doc.DocumentElement.GetElementsByTagName("pointing");
                    if (pointingExcNodes.Count > 0)
                    {
                        doc.DocumentElement.RemoveChild(pointingExcNodes[0]);                        
                    }
                }            
            }
            if (doc.DocumentElement == null)
                doc.AppendChild(doc.CreateElement("settings"));
            nodeSets = (XmlElement)doc.DocumentElement.AppendChild(doc.CreateElement("pointing"));
            nodeSets.Attributes.Append(doc.CreateAttribute("iterationCount")).Value = tbItersCount.Text;
            nodeSets.Attributes.Append(doc.CreateAttribute("timeoutBeforeFirst")).Value = tbStartDelay.Text;
            nodeSets.Attributes.Append(doc.CreateAttribute("timeoutBetweenIters")).Value = tbInterBetweenIters.Text;
            nodeSets.Attributes.Append(doc.CreateAttribute("timeToHold")).Value = tbTimeToHold.Text;
            nodeSets.Attributes.Append(doc.CreateAttribute("minScore")).Value = tbMinScore.Text;
            nodeSets.Attributes.Append(doc.CreateAttribute("randomCamera")).Value = cbRandomCamera.Checked.ToString();
            nodeSets.Attributes.Append(doc.CreateAttribute("checkFalstart")).Value = cbCheckFalstart.Checked.ToString();
            doc.Save(path);
        }
    
        private void CalculateNextStart()
        {
            nextStart = DateTime.Now.AddSeconds(curIteration == 0 ? 
                GetValueInInterval(timeoutBeforeFirst) : GetValueInInterval(timeoutBetweenIters));
            nextEnd = nextStart.AddSeconds(GetValueInInterval(timeToHold));
        }

        private int GetValueInInterval(int []inter)
        {
            if (inter[0] == inter[1]) return inter[0];
            return randomGenr.Next(inter[0], inter[1]);
        }
    }
}