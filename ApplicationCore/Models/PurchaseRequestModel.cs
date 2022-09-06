using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class PurchaseRequestModel
    {
        public PurchaseRequestModel(int movieId, int userId)
        {
            MovieId = movieId;
            UserId = userId;
        }

        public int MovieId { get; set; }
        public int UserId { get; set; }
    }
}
