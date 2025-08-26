using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using VideoGameApi.Data;
using VideoGameApi.Interfaces;
using VideoGameApi.Models;
using VideoGameApi.Models.DatabaseModels;
using VideoGameApi.Models.Genre;
using VideoGameApi.Models.VideoGame;

namespace VideoGameApi.Services
{
    public class GenreServices : IGenre
    {
        private readonly VideoGameDbContext _context;

        public GenreServices(VideoGameDbContext context)
        {
            _context = context;
        }

        //Get all the genres
        public async Task<List<MdGetGenres>> GetAllGenres()
        {
            var genres = await _context.Set<Genre>()
                .Select(g => new MdGetGenres
                {
                    Id = g.Id,
                    Name = g.Name,
                })
                .ToListAsync();

            return genres;
        }


        //Get genre by id
        public async Task<MdGetGenres> GetGenresById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new MdGetGenres();
            }

            var genre = await _context.Set<Genre>().Where(g => g.Id == id)
                .Select(g => new MdGetGenres
                {
                    Id = g.Id,
                    Name = g.Name,
                })
                .FirstOrDefaultAsync();

            if (genre == null)
            {
                return new MdGetGenres();
            }

            return genre;
        }

        //Get Genres Games 
        public async Task<List<MdGetGenreGames>> GetAllGenreGames()
        {
            var genreGames = await _context.Set<Genre>()
                .Select(g => new MdGetGenreGames
                {
                    GenreId = g.Id,
                    VideoGames = g.VideoGames.Select(v => new MdGetVideoGame
                    {
                        Id = v.Id,
                        Title = v.Title,
                        Platform = v.Platform,
                    }).ToList()                    
                })
                .ToListAsync();

            return genreGames;
        }

        //Get Genre Games 
        public async Task<MdGetGenreGames?> GetGenreGames(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                return new MdGetGenreGames();
            }


            var genre = await _context.Set<Genre>().FindAsync(id);

            if (genre == null)
            {
                return new MdGetGenreGames();
            }

            var genreGames = await _context.Set<Genre>()
                        .Where(g => g.Id == id)
                        .Select(g => new MdGetGenreGames
                        {
                            GenreId = g.Id,
                            VideoGames = g.VideoGames.Select(v => new MdGetVideoGame
                            {
                                Id = v.Id,
                                Title = v.Title,
                                Platform = v.Platform,
                            }).ToList()

                        }).FirstOrDefaultAsync();

            return genreGames;


        }
    
    
        //Create genre
        public async Task<MdResponse> CreateGenre(MdPostGenre genre)
        {
            MdResponse response = new MdResponse() { ResponseCode = 0};

            if(genre == null || string.IsNullOrEmpty(genre.Name))
            {
                response.ResponseMessage = "Name is required";
                return response;
            }

            var payload = new Genre { Id = Guid.NewGuid().ToString(), Name = genre.Name };

            var add = await _context.Set<Genre>().AddAsync(payload);
            var save = await _context.SaveChangesAsync();

            response.ResponseCode = 1;
            response.ResponseMessage = "Genre created successfully";

            return response;

        }

        //Update genre
        public async Task<MdResponse> UpdateGenre(string genreId, MdPostGenre genre)
        {
            MdResponse response = new MdResponse() { ResponseCode = 0 };

            if(string.IsNullOrEmpty(genreId))
            {
                response.ResponseMessage = "Gender id is required";
                return response;
            }

            if (string.IsNullOrEmpty(genre.Name))
            {
                response.ResponseMessage = "Name is required";
                return response;
            }

            //get the genre
            var getGenre = await _context.Set<Genre>().FindAsync(genreId);

            if(getGenre == null)
            {
                response.ResponseMessage = "Invalid genre";
                return response;
            }


            getGenre.Name = genre.Name;

            var save = await _context.SaveChangesAsync();
            response.ResponseCode = 1;
            response.ResponseMessage = "Genre updated successfully";
            return response;

        }
        
        //Delete genre
        public async Task<MdResponse> DeleteGenre(string genreId)
        {
            MdResponse response = new MdResponse() { ResponseCode = 0 };

            if(string.IsNullOrEmpty(genreId))
            {
                response.ResponseMessage = "Gender id is required";
                return response;
            }


            //get the genre
            var getGenre = await _context.Set<Genre>().FindAsync(genreId);

            if(getGenre == null)
            {
                response.ResponseMessage = "Invalid genre";
                return response;
            }


           var del =  _context.Set<Genre>().Remove(getGenre);

            var save = await _context.SaveChangesAsync();
            response.ResponseCode = 1;
            response.ResponseMessage = "Genre deleted successfully";
            return response;

        }

    }
}
