using System;
using System.ComponentModel.DataAnnotations;

namespace Movie_Mvc.Models
{
    public class Movie
    {
        [Key]
        public int Mid { get; set; }

        [Required]
        public string Moviename { get; set; }

        [Required]
        public string DirectorName { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateofRelease { get; set; }
    }
}