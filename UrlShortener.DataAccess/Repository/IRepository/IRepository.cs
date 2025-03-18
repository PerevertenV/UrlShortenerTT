using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.DataAccess.Repository.IRepository
{
    public interface IRepository<T, M> where T : class
    {
        Task<IEnumerable<M>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
        Task<M?> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter = null);

        Task AddAsync(M item);

        Task UpdateAsync(M item);

        Task DeleteAsync(M item);

    }
}
