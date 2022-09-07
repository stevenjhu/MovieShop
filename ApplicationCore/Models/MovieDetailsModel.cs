using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class MovieDetailsModel
    {
        public MovieDetailsModel()
        {
            Genres = new List<GenreModel>();
            Trailers = new List<TrailerModel>();
            Casts = new List<CastModel>();
            Reviews = new List<ReviewModel>();
        }
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Overview { get; set; } = null!;
        public string Tagline { get; set; } = null!;
        public decimal? Budget { get; set; }
        public decimal? Revenue { get; set; }
        public string ImdbUrl { get; set; } = null!;
        public string TmdbUrl { get; set; } = null!;
        public string PosterUrl { get; set; } = null!;
        public string BackdropUrl { get; set; } = null!;
        public string OriginalLanguage { get; set; } = null!;
        public DateTime? ReleaseDate { get; set; }
        public int? RunTime { get; set; }
        public decimal? Price { get; set; }
        public decimal? Rating { get; set; }

        public bool IsMoviePurchased { get; set; }

        public List<GenreModel> Genres { get; set; }
        public List<TrailerModel> Trailers { get; set; }
        public List<CastModel> Casts { get; set; }
        public List<ReviewModel> Reviews { get; set; }
        public PurchaseDetailsModel PurchaseDetails { get; set; }
        public FavoriteModel FavoriteStatus { get; set; }
    }
}
