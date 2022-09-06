using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class FavoriteRequestModel
    {
        public FavoriteRequestModel(int movieId, int userId)
        {
            this.MovieId = movieId;
            this.UserId = userId;
        }
        public int MovieId { get; set; }
        public int UserId { get; set; }

    }
}
