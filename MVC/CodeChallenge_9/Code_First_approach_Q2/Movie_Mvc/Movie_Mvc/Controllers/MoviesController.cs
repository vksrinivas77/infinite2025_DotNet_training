using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movie_Mvc.Models;
using Movie_Mvc.Repositories;

namespace Movie_Mvc.Controllers
{
    public class MoviesController : Controller
    {
        private IMovieRepository repo = new MovieRepository();

        // READ: List all movies
        public ActionResult Index()
        {
            var movies = repo.GetAll();
            return View(movies);
        }

        // CREATE: Show form
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                repo.Add(movie);
                repo.Save();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // EDIT
        public ActionResult Edit(int id)
        {
            var movie = repo.GetById(id);
            if (movie == null) return HttpNotFound();
            return View(movie);
        }

        [HttpPost]
        public ActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                repo.Update(movie);
                repo.Save();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // DELETE
        public ActionResult Delete(int id)
        {
            var movie = repo.GetById(id);
            if (movie == null) return HttpNotFound();
            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            repo.Delete(id);
            repo.Save();
            return RedirectToAction("Index");
        }

        // CUSTOM: Find by Year
        public ActionResult ByYear(int year)
        {
            var movies = repo.GetByYear(year);
            return View("Index", movies); // Reuse Index view
        }

        // CUSTOM: Find by Director
        public ActionResult ByDirector(string directorName)
        {
            var movies = repo.GetByDirector(directorName);
            return View("Index", movies); // Reuse Index view
        }
    }
}