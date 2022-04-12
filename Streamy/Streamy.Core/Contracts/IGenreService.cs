using Streamy.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamy.Core.Services
{
    public interface IGenreService
    {
        GenreListViewModel GetAllGenres();
        Task<GenreViewModel> GetByIdAsync(short id);
        Task CreateGenre(GenreViewModel genreModel);
        void UpdateGenre(GenreViewModel genreModel);
        Task DeleteGenre(short id);
        Task<GenreViewModel> GetGenreWithDetails(short id);
    }
}
