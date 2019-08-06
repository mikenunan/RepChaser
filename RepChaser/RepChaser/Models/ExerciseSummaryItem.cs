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

        public string Exercise { get; set; }
        public string Description { get; set; }
        public decimal LoadKg { get; set; }
        public int SetsDailyTarget { get; set; }
        public int RepsPerSet { get; set; }
        public int RepsTarget => DayRecords.Aggregate(0, (acc, day) => acc + day.TargetRepsDaily);
        public int RepsCompleted => DayRecords.Aggregate(0, (acc, day) => acc + day.RepsCompleted);
        public decimal FractionCompleted => (decimal)RepsCompleted / RepsTarget;

        public ExerciseSummaryItem(string id, IEnumerable<ExerciseDayRecord> dayRecordsOrderedByDateAndContiguous, int maxWindowLengthInDays = 10)
        {
            Id = id;
            DayRecords = new ObservableCollection<ExerciseDayRecord>(dayRecordsOrderedByDateAndContiguous);
            DateRefresh();
        }

        public void AddSet()
        {
            DateRefresh();
            DayRecords[0].SetTimesAscending.Add(DateTime.Now);
        }

        public void DateRefresh()
        {
            var daysSinceLastRecord = DateTime.Today - (DayRecords.LastOrDefault()?.Date ?? DateTime.MinValue);
            var dayRecordsCount = DayRecords.Count;
            var daysToRoll = Math.Min(daysSinceLastRecord.Days, dayRecordsCount);
            for (var i = 0; i < daysToRoll; i++)
            {
                var dayRecord = new ExerciseDayRecord(GuidFactory.NewGuidString(), DateTime.Today, new List<DateTime>())
                {
                    Exercise = Exercise, Description = Description, RepsPerSet = RepsTarget, SetsDailyTarget = SetsDailyTarget
                };
                DayRecords.Insert(0, dayRecord);
                if (dayRecordsCount < MaxWindowLengthInDays)
                    continue;
                DayRecords.RemoveAt(dayRecordsCount);
            }
        }
    }
}