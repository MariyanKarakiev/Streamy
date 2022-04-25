using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streamy.Core.Models
{
    public class SongListViewModel
    {
        public List<SongViewModel> Songs { get; set; } = new List<SongViewModel>();
    }
}
