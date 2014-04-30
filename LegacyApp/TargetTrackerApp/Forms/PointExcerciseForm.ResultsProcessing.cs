using System;
using System.Collections.Generic;
using System.Text;
using TargetTrackerApp.BL;

namespace TargetTrackerApp.Forms
{
    /// <summary>
    /// обработка результатов упражнения
    /// </summary>
    public partial class PointExcerciseForm
    {
        private List<PointExcerciseIterationResults> ProcessResults()
        {
            var rstList = new List<PointExcerciseIterationResults>();

            var curIndicies = new int[snapshots.Count];
            foreach (var camEvent in results.Targets)
            {
                if (camEvent.End == default(DateTime)) break; // упражнение незакончено
                var camera = camEvent.Target;
                var snapDescr = snapshots[camera];
                
                // перейти на начало обработки мишени
                var coordsList = spotCoordsByTime[camera];
                var excrStartIndex = -1;
                for (var i = curIndicies[camera]; i < coordsList.Count; i++)
                {
                    if (coordsList[i].Time < camEvent.Start) continue;
                    if (coordsList[i].Time >= camEvent.End) break;
                    excrStartIndex = i;                    
                    break;
                }                
                if (excrStartIndex < 0) break;
                curIndicies[camera] = excrStartIndex;

                // определить фальстарт
                var falstart = false;
                if (checkFalstart)
                {
                    var prevIndex = excrStartIndex - 1;
                    if (prevIndex >= 0)
                    {
                        var coords = coordsList[prevIndex].Coords;
                        if (coords.HasValue)
                            falstart = snapDescr.GetMarkByScreenCoords(coords.Value) > 0;
                    }
                }
                var rst = new PointExcerciseIterationResults {falstart = falstart};
                if (rst.falstart)
                {
                    rstList.Add(rst);
                    continue;
                }
                // время (индекс) конца итерации
                var endIndex = coordsList.Count - 1;
                for (var j = excrStartIndex; j < coordsList.Count; j++)
                {
                    // ! final iteration
                    if (coordsList[j].Time < camEvent.End) continue;
                    endIndex = j - 1;
                    break;
                }
                if (endIndex <= excrStartIndex) break;
                // определить время до наведения
                double sumPoints = 0;
                var pointTimeStamps = 0;
                DateTime? pointStart = null;
                for (var j = endIndex; j >= excrStartIndex; j--)
                {
                    if (coordsList[j].Coords == null) break;
                    var score = snapDescr.GetMarkByScreenCoords(coordsList[j].Coords.Value);
                    if (score < minScore) break;
                    sumPoints += score;
                    pointTimeStamps++;
                    pointStart = coordsList[j].Time;
                }

                rst.wasPointed = pointStart.HasValue;
                rst.milsTillPoint = !pointStart.HasValue ? 0 : (int)(pointStart.Value - 
                    camEvent.Start).TotalMilliseconds;
                rst.avgPoints = pointTimeStamps == 0 ? 0 : sumPoints/pointTimeStamps;
                rst.sumPoints = sumPoints;
                rstList.Add(rst);
            }// foreach (camEvent ... цикл по итерациям упражнения
            
            return rstList;
        }
    
        private string GetResultsString(List<PointExcerciseIterationResults> rstList)
        {
            var sb = new StringBuilder();
            if (results.Targets.Count == 0) return "-";
            sb.AppendLine(string.Format("Старт обработки мишени: {0: dd.MMM HH:mm:ss}", 
                results.Targets[0].Start));
            sb.AppendLine(string.Format("Повторов: {0}", iterationCount));
            for (var i = 0; i < rstList.Count; i++)
            {
                sb.AppendLine(string.Format("[{0}] {1}", i + 1, rstList[i]));
            }
            return sb.ToString();
        }
    }
}