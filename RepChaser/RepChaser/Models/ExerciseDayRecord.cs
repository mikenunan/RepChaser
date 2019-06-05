using System;

namespace RepChaser.Models
{
    public class ExerciseDayRecord
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Exercise { get; set; }
        public int RepsPerSet { get; set; }
        public int SetsTarget { get; set; }
        public int SetsCompleted { get; set; }
        public int RepsTarget => SetsTarget * RepsPerSet;
        public int RepsCompleted => SetsCompleted * RepsPerSet;
        public int PercentCompleted => SetsCompleted * 100 / SetsTarget;
    }
}