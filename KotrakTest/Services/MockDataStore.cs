using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KotrakTest.Models;

namespace KotrakTest
{
    public class MockDataStore : IDataStore<Person>
    {
        private readonly List<Person> items;

        public MockDataStore()
        {
            items = new List<Person>();
            List<Person> _items = new List<Person>();
            for (int i = 0; i < 7; i++)
            {
                _items.Add(new Contractor(Guid.NewGuid().ToString(), "Name " + i, "Surname " + i, "Company Name " + i));
            }

            items.AddRange(_items);
        }

        public async Task<bool> AddItemAsync(Person item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Person item)
        {
            var _item = items.Where((Person arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var _item = items.Where((Person arg) => arg.Id == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Person> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Person>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
