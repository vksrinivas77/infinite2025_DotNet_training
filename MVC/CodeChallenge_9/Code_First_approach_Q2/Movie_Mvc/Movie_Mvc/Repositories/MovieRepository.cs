using Movie_Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie_Mvc.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private MoviesDbContext context = new MoviesDbContext();

        public IEnumerable<Movie> GetAll()
        {
            return context.Movies.ToList();
        }

        public Movie GetById(int id)
        {
            return context.Movies.Find(id);
        }

        public void Add(Movie movie)
        {
            context.Movies.Add(movie);
        }

        public void Update(Movie movie)
        {
            context.Entry(movie).State = System.Data.Entity.EntityState.Modified;
        }

        public void Delete(int id)
        {
            var movie = context.Movies.Find(id);
            if (movie != null)
                context.Movies.Remove(movie);
        }

        public IEnumerable<Movie> GetByYear(int year)
        {
            return context.Movies.Where(m => m.DateofRelease.Year == year).ToList();
        }

        public IEnumerable<Movie> GetByDirector(string directorName)
        {
            return context.Movies.Where(m => m.DirectorName == directorName).ToList();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}