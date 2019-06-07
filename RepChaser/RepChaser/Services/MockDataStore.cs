using RepChaser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RepChaser.Services
{
    public class MockDataStore : IDataStore<ExerciseSummaryItem>
    {
        private readonly List<ExerciseSummaryItem> _items = new List<ExerciseSummaryItem>();

        public MockDataStore()
        {
            var mockItems = new List<ExerciseSummaryItem>
            {
                CreateSummaryItem("Push up", 4, 5 * 6),
                CreateSummaryItem("Dowel row", 6, 5 * 6),
                CreateSummaryItem("KB press", 2, 5 * 6),
            };

            foreach (var item in mockItems)
            {
                _items.Add(item);
            }
        }

        private static ExerciseSummaryItem CreateSummaryItem(string exercise, int repsPerSet, int setsTargetDaily)
        {
            ExerciseDayRecord CreateDayRecord(int daysAgo)
            {
                var setTimes = Enumerable.Range(0, daysAgo).Select(n => DateTime.Today.AddHours(n + 1));
                return new ExerciseDayRecord(GuidFactory.NewGuidString(), DateTime.Today.AddDays(-daysAgo), setTimes)
                {
                    Exercise = exercise,
                    Description = $"Description of {exercise}",
                    RepsPerSet = repsPerSet,
                    SetsTarget = setsTargetDaily,
                };
            }

            var dayRecords = (IEnumerable<ExerciseDayRecord>)new[]
            {
                CreateDayRecord(1),
                CreateDayRecord(2),
                CreateDayRecord(4),
            };

            return new ExerciseSummaryItem(GuidFactory.NewGuidString(), dayRecords)
                { Exercise = exercise, RepsPerSet = repsPerSet, SetsTargetDaily = setsTargetDaily };
        }

        public async Task<bool> AddItemAsync(ExerciseSummaryItem item)
        {
            _items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(ExerciseSummaryItem item)
        {
            var oldItem = _items.FirstOrDefault(arg => arg.Id == item.Id);
            _items.Remove(oldItem);
            _items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = _items.FirstOrDefault(arg => arg.Id == id);
            _items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<ExerciseSummaryItem> GetItemAsync(string id)
        {
            return await Task.FromResult(_items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<ExerciseSummaryItem>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(_items);
        }
    }
}