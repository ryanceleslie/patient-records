using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces;

public interface IRepository<T> where T : class, IAggregateRoot
{
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T> GetByIdAsync<Tid>(Tid id) where Tid : notnull;

    // I prefer to use different collection types for these kind of properties as the usages
    // of the collection should determine which type to use. .NET collections are notorious
    // with people misusing them or overlooking performance issues. They quickly become a
    // bottleneck for applications
    Task<List<T>> GetAllAsync();
}
