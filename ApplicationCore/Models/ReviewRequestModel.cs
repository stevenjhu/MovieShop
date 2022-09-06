using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class ReviewRequestModel
    {
        public ReviewRequestModel(int movieId, int userId, decimal rating, string reviewText)
        {
            this.MovieId = movieId;
            this.UserId = userId;
            this.Rating = rating;
            this.ReviewText = reviewText;
        }
        
        public int MovieId { get; set; }
        public int UserId { get; set; }
        public decimal Rating { get; set; }
        public string ReviewText { get; set; } = null!;
    }
}
