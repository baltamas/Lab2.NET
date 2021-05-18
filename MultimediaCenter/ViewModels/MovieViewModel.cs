using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimediaCenter.ViewModels
{
    public class MovieViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public float Duration { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        public DateTime DateAdded { get; set; }
        public float Rating { get; set; }
        public bool Watched { get; set; }
    }
}
