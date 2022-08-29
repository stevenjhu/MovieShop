using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class MovieShopDbContext : DbContext
    {
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
        {

        }

        // Dbsets are properties of DbContext class

        // Movies Table access
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }

        // override the method called OnModelCreating for Fluent API

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
            modelBuilder.Entity<Cast>(ConfigureCast);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<Favorite>(ConfigureFavorite);
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<Purchase>(ConfigurePurchase);
            modelBuilder.Entity<UserRole>(ConfigureUserRole);
        }

        private void ConfigureUserRole(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(mg => new {mg.RoleId, mg.UserId});
        }

        private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
        {
            builder.HasKey(mg => new {mg.MovieId, mg.UserId });
            builder.Property(m => m.PurchaseDateTime).HasDefaultValueSql("getdate()");
            builder.Property(m => m.PurchaseNumber).HasDefaultValue(new Guid("{00000000-0000-0000-0000-000000000000}"));
            builder.Property(m => m.TotalPrice).HasColumnType("decimal(5,2)").HasDefaultValue(9.9m);
        }

        private void ConfigureReview(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(mg => new {mg.MovieId, mg.UserId });
            builder.Property(m => m.CreateDate).HasDefaultValueSql("getdate()");
            builder.Property(m => m.Rating).HasColumnType("decimal(3,2)").HasDefaultValue(9.9m);
            builder.Property(m => m.ReviewText).HasMaxLength(4096);
        }

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Email).HasMaxLength(256);
            builder.Property(m => m.FirstName).HasMaxLength(128);
            builder.Property(m => m.HashedPassword).HasMaxLength(1024);
            builder.Property(m => m.LastName).HasMaxLength(128);
            builder.Property(m => m.PhoneNumber).HasMaxLength(16);
            builder.Property(m => m.ProfilePictureUrl).HasMaxLength(4096);
            builder.Property(m => m.Salt).HasMaxLength(1024);
        }

        private void ConfigureFavorite(EntityTypeBuilder<Favorite> builder)
        {
            builder.HasKey(mg => new { mg.MovieId, mg.UserId});
        }

        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
        {
            builder.HasKey(mg => new { mg.CastId, mg.MovieId, mg.Character });
        }

        private void ConfigureCast(EntityTypeBuilder<Cast> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Gender).HasMaxLength(4096);
            builder.Property(m => m.Name).HasMaxLength(128);
            builder.Property(m => m.ProfilePath).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(4096);
        }

        private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.ToTable("MovieGenres"); //name the table
            builder.HasKey(mg => new { mg.MovieId, mg.GenreId });
        }

        private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {
            // specify all the Fluent API rules
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).HasMaxLength(256);
            builder.Property(m => m.Overview).HasMaxLength(4096);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            builder.Property(m => m.UpdatedBy).HasMaxLength(4096);
            builder.Property(m => m.CreatedBy).HasMaxLength(4096);

            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.Budget).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.Revenue).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");

            //Fluent API proceeds Data Annotation
            // you want the property in your C# for your business logic not as column in database
            builder.Ignore(m => m.Rating);

            builder.HasIndex(m => m.Title);
            builder.HasIndex(m => m.Price);
            builder.HasIndex(m => m.Revenue);
            builder.HasIndex(m => m.Budget);
        }

    }
}
