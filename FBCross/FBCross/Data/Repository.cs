using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
namespace FBCross.Data
{
    public class Repository<T> where T:new()
    {
        private SQLiteAsyncConnection _database;
        public Repository (SQLiteAsyncConnection database)
        {
            _database = database;
        }


        public Task<List<T>> GetEntitiesAsync()
        {
            return _database.Table<T>().ToListAsync();
        }

        public Task<int> CreateEntityAsync(T entity)
        {
            return _database.InsertAsync(entity);
        }
        public Task<int> UpdateEntityAsync(T entity)
        {
            return _database.UpdateAsync(entity);
        }
        public Task<int> CreateManyAsync(IEnumerable<T> entities)
        {
            return _database.InsertAllAsync(entities);
        }

        public Task<int> RemoveAll()
        {
            return _database.DeleteAllAsync<T>();
        }
    }
}
