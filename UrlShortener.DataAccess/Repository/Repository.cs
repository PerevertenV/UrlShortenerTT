using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UrlShortener.DataAccess.Repository.IRepository;

namespace UrlShortener.DataAccess.Repository
{
    internal class Repository<T, M> : IRepository<T, M> where T : class
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        internal DbSet<T> dbSet;

        public Repository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            this.dbSet = _context.Set<T>();
        }
        
        public async Task AddAsync(M item)
        {
            await _context.AddAsync(_mapper.Map<T>(item));

            await _context.SaveChangesAsync();
        }
        
        public async Task DeleteAsync(M item)
        {
            _context.Remove(_mapper.Map<T>(item));

            await _context.SaveChangesAsync();
        }
       
        public async Task<IEnumerable<M>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;

            query = filter != null ? query.Where(filter) : query;

            var list = await query.ToListAsync();

            return _mapper.Map<IEnumerable<M>>(list);
        }
       
        public async Task<M?> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = dbSet;

            query =  filter != null ? query.Where(filter) : query;

            var obj = await query.FirstOrDefaultAsync();

            return _mapper.Map<M?>(obj);
        }
       
        public async Task UpdateAsync(M item)
        {
            _context.Update(_mapper.Map<T>(item));

            await _context.SaveChangesAsync();
        }
    }
}
