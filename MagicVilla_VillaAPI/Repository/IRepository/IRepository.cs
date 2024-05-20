using MagicVilla_VillaAPI.Models;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IRepository <T> 
        where T : class
    {
        Task<List<T>> GetAllAsync(CancellationToken token, Expression<Func<T, bool>>? filter = null);
        Task<T> GetAsync(CancellationToken token, Expression<Func<T, bool>> filter = null, bool tracked = true);
        Task CreateAsync(T entity, CancellationToken token);
        Task RemoveAsync(T entity, CancellationToken token);
        Task SaveAsync(CancellationToken token);
    }
}
