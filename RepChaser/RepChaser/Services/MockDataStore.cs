﻿using RepChaser.Models;
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
                return new ExerciseDayRecord
                {
                    Date = DateTime.Today.AddDays(-daysAgo),
                    Exercise = exercise,
                    Id = GuidFactory.NewGuidString(),
                    RepsPerSet = repsPerSet,
                    SetsTarget = setsTargetDaily,
                    SetsCompleted = daysAgo
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
            var oldItem = _items.Where((ExerciseSummaryItem arg) => arg.Id == item.Id).FirstOrDefault();
            _items.Remove(oldItem);
            _items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = _items.Where((ExerciseSummaryItem arg) => arg.Id == id).FirstOrDefault();
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