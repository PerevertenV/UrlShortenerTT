using AutoMapper;
using UrlShortener.DataAccess.Repository.IRepository;

namespace UrlShortener.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;

        public IUserRepository User { get; private set; }
        public IUrlRepository Url { get; private set; }

        public UnitOfWork(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

            User = new UserRepository(_db, _mapper);
            Url = new UrlRepository(_db, _mapper);   
        }
    }
}
