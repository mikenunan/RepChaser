using RepChaser.Models;
using System;
using Xunit;

namespace RepChaser.Tests.Models
{
    public class ExerciseDayRecordTests
    {
        [Fact]
        public void GivenNewDayRecord_WhenGetSetTimesAscending_ShouldBeEmpty()
        {
            var dayRecord = new ExerciseDayRecord(GuidFactory.NewGuidString(), DateTime.Today, new DateTime[0]);

            var setTimesAscending = dayRecord.SetTimesAscending;

            Assert.Empty(setTimesAscending);
        }

        [Fact]
        public void GivenNewDayRecord_WhenGetAddSet_ShouldHaveSingleRecordWithGivenTime()
        {
            var setDoneTime = new DateTime(2019, 1, 1, 12, 0, 0);
            var dayRecord = new ExerciseDayRecord(GuidFactory.NewGuidString(), setDoneTime.Date, new DateTime[0]);

            dayRecord.AddSet(setDoneTime);

            Assert.Collection(dayRecord.SetTimesAscending, dateTime => Assert.Equal(setDoneTime, dateTime));
        }

        [Fact]
        public void GivenNewDayRecord_WhenGetAddTwoSetsInAscendingOrder_ShouldHaveTwoRecordsInAscendingOrder()
        {
            var setDoneTime1 = new DateTime(2019, 1, 1, 12, 0, 0);
            var setDoneTime2 = new DateTime(2019, 1, 1, 12, 1, 0);
            var dayRecord = new ExerciseDayRecord(GuidFactory.NewGuidString(), setDoneTime1.Date, new DateTime[0]);

            dayRecord.AddSet(setDoneTime1);
            dayRecord.AddSet(setDoneTime2);

            Assert.Collection(dayRecord.SetTimesAscending,
                dateTime => Assert.Equal(setDoneTime1, dateTime), dateTime => Assert.Equal(setDoneTime2, dateTime));
        }

        [Fact]
        public void GivenNewDayRecord_WhenGetSetForWrongDate_ShouldThrow()
        {
            var setDoneTime = new DateTime(2019, 1, 1, 12, 0, 0);
            var dayRecord = new ExerciseDayRecord(GuidFactory.NewGuidString(), setDoneTime.Date, new DateTime[0]);

            var exception = Record.Exception(() => { dayRecord.AddSet(setDoneTime.AddDays(1)); });

            Assert.IsType<InvalidOperationException>(exception);
            Assert.Contains("Caller attempted to add set for wrong date", exception.Message);
        }

        [Fact]
        public void GivenNewDayRecord_WhenGetAddTwoSetsInDescendingOrder_ShouldThrowOnSecondAddSet()
        {
            var setDoneTime1 = new DateTime(2019, 1, 1, 12, 0, 0);
            var setDoneTime2 = new DateTime(2019, 1, 1, 12, 1, 0);
            var dayRecord = new ExerciseDayRecord(GuidFactory.NewGuidString(), setDoneTime1.Date, new DateTime[0]);

            dayRecord.AddSet(setDoneTime2);
            var exception = Record.Exception(() => { dayRecord.AddSet(setDoneTime1); });

            Assert.IsType<InvalidOperationException>(exception);
            Assert.Contains("Caller attempted to add set for a time prior to the previous last set", exception.Message);
        }

        [Fact]
        public void GivenTwoSetsTimesInAscendingOrder_WhenConstructNewDayRecord_ShouldHaveTwoRecordsInAscendingOrder()
        {
            var setDoneTime1 = new DateTime(2019, 1, 1, 12, 0, 0);
            var setDoneTime2 = new DateTime(2019, 1, 1, 12, 1, 0);
            var setTimes = new[] { setDoneTime1, setDoneTime2 };

            var dayRecord = new ExerciseDayRecord(GuidFactory.NewGuidString(), setDoneTime1.Date, setTimes);

            Assert.Collection(dayRecord.SetTimesAscending,
                dateTime => Assert.Equal(setDoneTime1, dateTime), dateTime => Assert.Equal(setDoneTime2, dateTime));
        }


        [Fact]
        public void GivenTwoSetsTimesInDescendingOrder_WhenConstructNewDayRecord_ShouldHaveTwoRecordsInAscendingOrder()
        {
            var setDoneTime1 = new DateTime(2019, 1, 1, 12, 0, 0);
            var setDoneTime2 = new DateTime(2019, 1, 1, 12, 1, 0);
            var setTimes = new[] { setDoneTime2, setDoneTime1 };

            var dayRecord = new ExerciseDayRecord(GuidFactory.NewGuidString(), setDoneTime1.Date, setTimes);

            Assert.Collection(dayRecord.SetTimesAscending,
                dateTime => Assert.Equal(setDoneTime1, dateTime), dateTime => Assert.Equal(setDoneTime2, dateTime));
        }
    }
}
