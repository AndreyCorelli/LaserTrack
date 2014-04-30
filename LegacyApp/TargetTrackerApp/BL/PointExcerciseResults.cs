using System;
using System.Collections.Generic;
using System.Drawing;

namespace TargetTrackerApp.BL
{
    /// <summary>
    /// выбранная мишень, старт-окончание
    /// </summary>
    class TargetSelectionPeriod
    {
        public int Target;
        public DateTime Start, End;
    }

    struct PointCoordsByTime
    {
        public DateTime Time;
        public Point? Coords;
    }

    /// <summary>
    /// хранит данные: какие мишени были выбраны в какие моменты времени,
    /// координаты точек указателя по камерам
    /// </summary>
    class PointExcerciseResults
    {
        public readonly List<TargetSelectionPeriod> Targets = new List<TargetSelectionPeriod>();

        public void SelectTarget(int targetIndex)
        {
            Targets.Add(new TargetSelectionPeriod { Target = targetIndex, Start = DateTime.Now });
        }

        public void CompleteTarget()
        {
            Targets[Targets.Count - 1].End = DateTime.Now;
        }
    }

    class PointExcerciseIterationResults
    {
        /// <summary>
        /// наведение на цель, удержание до завершения упражнения
        /// </summary>
        public bool wasPointed;
        /// <summary>
        /// фальстарт (точка в мишени до старта упражнения)
        /// </summary>
        public bool falstart;
        /// <summary>
        /// мс до наведения на цель
        /// </summary>
        public int milsTillPoint;
        /// <summary>
        /// средний балл с момента старта до завершения упражнения
        /// </summary>
        public double avgPoints;
        /// <summary>
        /// сумма очков за время старта
        /// </summary>
        public double sumPoints;

        public override string ToString()
        {
            if (falstart) return "фальстарт";
            if (!wasPointed) return "промах";
            return string.Format("Наведение за {0}мс, ср. балл {1:f2}, сумм балл {2}", 
                milsTillPoint, avgPoints, sumPoints);
        }
    }
}
