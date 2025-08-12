using Microsoft.EntityFrameworkCore;
using VideoGameApi.Models;

namespace VideoGameApi.Data
{
    public class VideoGameDbContext(DbContextOptions<VideoGameDbContext> options) : DbContext(options)
    {
        /// <summary>
        /// Represents the collection of VideoGames in the database.
        /// </summary>
        public DbSet<VideoGame> VideoGames  => Set<VideoGame>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VideoGame>().HasData(

                new VideoGame { Id = "1", Title = "The Legend of Zelda: Breath of the Wild", Platform = "Nintendo Switch", Developer = "Nintendo EPD", Publisher = "Nintendo" },
                new VideoGame { Id = "2", Title = "God of War", Platform = "PlayStation 4", Developer = "Santa Monica Studio", Publisher = "Sony Interactive Entertainment" },
                new VideoGame { Id = "3", Title = "Halo Infinite", Platform = "Xbox Series X/S", Developer = "343 Industries", Publisher = "Xbox Game Studios" },
                new VideoGame { Id = "4", Title = "Super Mario Odyssey", Platform = "Nintendo Switch", Developer = "Nintendo EPD", Publisher = "Nintendo" },
                new VideoGame { Id = "5", Title = "The Witcher 3: Wild Hunt", Platform = "PC, PlayStation 4, Xbox One, Nintendo Switch", Developer = "CD Projekt Red", Publisher = "CD Projekt" },
                new VideoGame { Id = "6", Title = "Final Fantasy VII Remake", Platform = "PlayStation 4", Developer = "Square Enix", Publisher = "Square Enix" },
                new VideoGame { Id = "7", Title = "Animal Crossing: New Horizons", Platform = "Nintendo Switch", Developer = "Nintendo EPD", Publisher = "Nintendo" }

            );

        }

    }

}
