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
        Task CreateGenre(GenreModel genreModel);
        Task UpdateGenre(GenreModel genreModel);
        Task DeleteGenre(string genreId);
    }
}
