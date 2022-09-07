using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string HashedPassword { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string Salt { get; set; } = null!;
        public bool? IsLocked { get; set; }

        public ICollection<Favorite> FavoritesOfUser { get; set; }
        public ICollection<Review> ReviewsOfUser { get; set; }
        public ICollection<Purchase> PurchasesOfUser  { get; set; }
        public ICollection<UserRole> UserRoleOfUser { get; set; }
    }
}
