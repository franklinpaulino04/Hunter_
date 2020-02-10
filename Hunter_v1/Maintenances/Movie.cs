using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maintenances
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int Genero { get; set; }
        public DateTime Release_date { get; set; }
        public string Photo { get; set; }
        public List<Movie_Items> Items { get; set; }
        public int Hidden { get; set; }
    }
}
