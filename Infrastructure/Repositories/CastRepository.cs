using ApplicationCore.Contracts.Repository;
using ApplicationCore.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CastRepository : ICastRepository
    {
        private MovieShopDbContext _movieShopDbContext;
        public CastRepository(MovieShopDbContext movieShopDbContext)
        {
            _movieShopDbContext = movieShopDbContext;
        }

        public async Task<Cast> GetById(int id)
        {
            var castDetails = await _movieShopDbContext.Casts
                .Include(m => m.MoviesOfCast)
                .FirstOrDefaultAsync(m=>m.Id==id);
            return castDetails;

        }
    }
}
