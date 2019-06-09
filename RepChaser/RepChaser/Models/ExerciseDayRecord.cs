﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace RepChaser.Models
{
    public class ExerciseDayRecord
    {
        public readonly string Id;
        public readonly DateTime Date;
        public readonly List<DateTime> SetTimesAscending;

        public string Exercise { get; set; }
        public string Description{ get; set; }
        public decimal LoadKg { get; set; }
        public int SetsDailyTarget { get; set; }
        public int RepsPerSet { get; set; }
        public int SetsCompleted => SetTimesAscending.Count;
        public int TargetRepsDaily => SetsDailyTarget * RepsPerSet;
        public int RepsCompleted => SetsCompleted * RepsPerSet;
        public decimal FractionCompleted => (decimal)SetsCompleted / SetsDailyTarget;

        public ExerciseDayRecord(string id, DateTime date, IEnumerable<DateTime> setTimes)
        {
            Id = id;
            Date = date;
            SetTimesAscending = setTimes.ToList();
            SetTimesAscending.Sort();
        }

        public void AddSet(DateTime setDoneTime)
        {
            var setDate = setDoneTime.Date;
            var recordDate = Date.Date;
            if (setDate != recordDate)
                throw new InvalidOperationException($"Caller attempted to add set for wrong date (gave {setDate}, should match {recordDate})");
            if (SetTimesAscending.Count > 0)
            {
                var currentLast = SetTimesAscending.Last();
                if (setDoneTime < currentLast)
                    throw new InvalidOperationException($"Caller attempted to add set for a time prior to the previous last set ({setDoneTime} vs {currentLast}");
            } 
            SetTimesAscending.Add(setDoneTime);
        }
    }
}