using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamy.Core.Models
{
    public class SongListViewModel
    {
        public SongListViewModel()
        {
            Songs = new List<SongViewModel>();
        }
        public List<SongViewModel> Songs { get; set; }
    }
}
