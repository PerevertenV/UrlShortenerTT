using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.DataAccess.Entities;
using UrlShortener.Models;

namespace UrlShortener.DataAccess.Repository.IRepository
{
    public interface IUrlRepository : IRepository<UrlEntity, UrlModel>
    {
    }
}
