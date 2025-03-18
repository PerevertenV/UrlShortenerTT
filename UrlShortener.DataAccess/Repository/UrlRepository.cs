using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.DataAccess.Entities;
using UrlShortener.DataAccess.Repository.IRepository;
using UrlShortener.Models;

namespace UrlShortener.DataAccess.Repository
{
    internal class UrlRepository: Repository<UrlEntity, UrlModel>, IUrlRepository
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        public UrlRepository(AppDbContext db, IMapper mapper) : base(db, mapper)
        {
            _db = db;
            _mapper = mapper;
        }
    }
}
