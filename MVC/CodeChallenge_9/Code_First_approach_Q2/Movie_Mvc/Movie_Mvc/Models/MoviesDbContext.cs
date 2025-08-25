using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Movie_Mvc.Models
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext() : base("MoviesDB") { } // Connection string name

        public DbSet<Movie> Movies { get; set; }
    }
}