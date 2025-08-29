using Microsoft.EntityFrameworkCore;
using VideoGameApi.Models.DatabaseModels;
using VideoGameApi.Models.User;

namespace VideoGameApi.Data
{
    public class VideoGameDbContext(DbContextOptions<VideoGameDbContext> options) : DbContext(options)
    {
        /// <summary>
        /// Represents the collection of VideoGames in the database.
        /// </summary>
        /// 
        public DbSet<User> Users => Set<User>();
        public DbSet<VideoGame> VideoGames  => Set<VideoGame>();

        public DbSet<VideoGameDetails> VideoGameDetails => Set<VideoGameDetails>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VideoGame>().HasData(

                new VideoGame { Id = "1", Title = "The Legend of Zelda: Breath of the Wild", Platform = "Nintendo Switch"},
                new VideoGame { Id = "2", Title = "God of War", Platform = "PlayStation 4" },
                new VideoGame { Id = "3", Title = "Halo Infinite", Platform = "Xbox Series X/S"},
                new VideoGame { Id = "4", Title = "Super Mario Odyssey", Platform = "Nintendo Switch"},
                new VideoGame { Id = "5", Title = "The Witcher 3: Wild Hunt", Platform = "PC, PlayStation 4, Xbox One, Nintendo Switch"},
                new VideoGame { Id = "6", Title = "Final Fantasy VII Remake", Platform = "PlayStation 4"},
                new VideoGame { Id = "7", Title = "Animal Crossing: New Horizons", Platform = "Nintendo Switch"}

            );

        }

    }

}
