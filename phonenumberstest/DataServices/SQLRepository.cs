using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace phonenumberstest.DataServices
{
    public class SQLRepository : ISQLRepository
    {
        private readonly SQLiteAsyncConnection sQLiteAsyncConnection;

        public SQLRepository(string dbPath)
        {
            sQLiteAsyncConnection = new SQLiteAsyncConnection(dbPath);
        }

        public Task CreateTableAsync<T>()
            where T : new()
        {
            return sQLiteAsyncConnection.CreateTableAsync<T>();
        }

        public Task<List<T>> GetAll<T>()
            where T : new()
        {
            return sQLiteAsyncConnection.Table<T>().ToListAsync();
        }

        public Task<int> SaveAll<T>(IEnumerable objects)
            where T : new()
        {
            return sQLiteAsyncConnection.UpdateAllAsync(objects);
        }
    }
}
