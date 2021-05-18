using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MultimediaCenter.Models
{
    public class Movie
    {

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Genre { get; set; }
        [Required]
        public float Duration { get; set; }
        [Range(1900, 2022)]
        public int ReleaseYear { get; set; }
        [Required]
        public string Director { get; set; }
        public DateTime DateAdded { get; set; }
        [Range(1, 10)]
        public float Rating { get; set; }
        public bool Watched { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
