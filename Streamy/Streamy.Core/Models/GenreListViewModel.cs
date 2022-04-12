using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamy.Core.Models
{
    public class GenreListViewModel
    {
        public GenreListViewModel()
        {
            Genres = new List<GenreViewModel>();
        }
        public List<GenreViewModel> Genres { get; set; }
        public string Name { get; set; }
    }
}
