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

        public CastDetailsModel GetCastDetails(int id)
        {
            var castDetails = _castRepository.GetById(id);
            var castDetailsModel = new CastDetailsModel
            {
                Id = castDetails.Id,
                Gender = castDetails.Gender,
                Name = castDetails.Name,
                ProfilePath = castDetails.ProfilePath,
                TmdbUrl = castDetails.TmdbUrl,
            };
            foreach (var cast in castDetails.MoviesOfCast)
            {
                castDetailsModel.MoviesOfCast.Add(new MovieCastModel
                {
                    MovieId = cast.MovieId,
                    CastId = cast.CastId
                });
            }
            return castDetailsModel;
        }
    }
}
