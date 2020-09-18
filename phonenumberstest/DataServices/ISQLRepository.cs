using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace phonenumberstest.DataServices
{
    public interface ISQLRepository
    {
        Task CreateTableAsync<T>()
            where T : new();
   
        Task<List<T>> GetAll<T>()
            where T : new();

        Task<int> SaveAll<T>(IEnumerable objects)
            where T : new();
    }
}