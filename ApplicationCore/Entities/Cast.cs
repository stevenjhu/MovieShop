using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    public class Cast
    {
        public int Id { get; set; }
        public string Gender { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string ProfilePath { get; set; } = null!;
        public string TmdbUrl { get; set; } = null!;
        public ICollection<MovieCast> MoviesOfCast { get; set; }

    }
}
