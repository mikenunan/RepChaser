using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace RepChaser.Models
{
    public class ExerciseSummaryItem
    {
        public readonly string Id;
        public readonly ObservableCollection<ExerciseDayRecord> DayRecords;
        public readonly int MaxWindowLengthInDays;

        public string Exercise { get; set; }
        public string Description { get; set; }
        public int RepsPerSet { get; set; }
        public int SetsTargetDaily { get; set; }
        public int WindowLengthInDays => DayRecords.Count;
        public int SetsTarget => DayRecords.Aggregate(0, (acc, day) => acc + day.SetsTarget);
        public int SetsCompleted => DayRecords.Aggregate(0, (acc, day) => acc + day.SetsCompleted);
        public int RepsTarget => DayRecords.Aggregate(0, (acc, day) => acc + day.RepsTarget);
        public int RepsCompleted => DayRecords.Aggregate(0, (acc, day) => acc + day.RepsCompleted);
        public int PercentCompleted => RepsCompleted * 100 / RepsTarget;

        public ExerciseSummaryItem(string id, IEnumerable<ExerciseDayRecord> dayRecordsOrderedByDateAndContiguous, int maxWindowLengthInDays = 10)
        {
            Id = id;
            DayRecords = new ObservableCollection<ExerciseDayRecord>(dayRecordsOrderedByDateAndContiguous);
            MaxWindowLengthInDays = maxWindowLengthInDays;
            DateRefresh();
        }

        public void AddSet()
        {
            DateRefresh();
            DayRecords[0].SetTimesAscending.Add(DateTime.Now);
        }

        public void DateRefresh()
        {
            var daysSinceLastRecord = (DateTime.Today - DayRecords.Last().Date).Days;
            var dayRecordsCount = DayRecords.Count;
            var daysToRoll = Math.Max(MaxWindowLengthInDays, Math.Min(daysSinceLastRecord, dayRecordsCount));
            for (var i = 0; i < daysToRoll; i++)
            {
                var dayRecord = new ExerciseDayRecord(GuidFactory.NewGuidString(), DateTime.Today, new List<DateTime>())
                {
                    Exercise = Exercise, Description = Description, RepsPerSet = RepsTarget, SetsTarget = SetsTarget
                };
                DayRecords.Insert(0, dayRecord);
                if (dayRecordsCount < MaxWindowLengthInDays)
                    continue;
                DayRecords.RemoveAt(dayRecordsCount);
            }
        }
    }
}