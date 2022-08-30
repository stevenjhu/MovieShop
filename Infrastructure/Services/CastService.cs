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
        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }

        public async Task<CastDetailsModel> GetCastDetails(int castId)
        {
            var castDetails = await _castRepository.GetById(castId); //type Cast
            var castDetailsModel = new CastDetailsModel
            {
                Id = castDetails.Id,
                Gender = castDetails.Gender,
                Name = castDetails.Name,
                ProfilePath = castDetails.ProfilePath,
                TmdbUrl = castDetails.TmdbUrl,
            };
            foreach (var movieCast in castDetails.MoviesOfCast)
            {
                castDetailsModel.MoviesOfCast.Add(new MovieCardModel
                {
                    Id = movieCast.Movie.Id,
                    Title = movieCast.Movie.Title,
                    PosterUrl = movieCast.Movie.PosterUrl
                });
            }
            return castDetailsModel;
        }
    }
}
