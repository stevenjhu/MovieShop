using ApplicationCore.Contracts.Repository;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;
        private readonly IMovieRepository _movieRepository;
        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }

        public async Task<CastDetailsModel> GetCastDetails(int castId)
        {
            var castDetails = await _castRepository.GetById(castId);
            var castDetailsModel = new CastDetailsModel
            {
                Id = castDetails.Id,
                Gender = castDetails.Gender,
                Name = castDetails.Name,
                ProfilePath = castDetails.ProfilePath,
                TmdbUrl = castDetails.TmdbUrl,
            };
            foreach (var movie in castDetailsModel.MoviesOfCast)
            {
                castDetailsModel.MoviesOfCast.Add(new MovieCardModel
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    PosterUrl = movie.PosterUrl
                });
            }
            return castDetailsModel;
        }
    }
}
