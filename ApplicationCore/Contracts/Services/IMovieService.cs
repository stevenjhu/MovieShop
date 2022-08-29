﻿using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Contracts.Services
{
    public interface IMovieService
    {
        List<MovieCardModel> GetTop30GrossingMovies();
        MovieDetailsModel GetMovieDetails(int movieId);
    }
}
