using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Buoi10_Ontap2.Models;

namespace Buoi10_Ontap2.Controllers
{
    public class MoviesController : Controller
    {
        private DbContextMovies db = new DbContextMovies();
        const string DateFormatFull = "dd/MM/yyyy HH:mm:ss";
        // GET: Movies
        public ActionResult Index()
        {
            var movies = db.Movies.Include(m => m.Genres);
            //ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName");
            List<SelectListItem> selectListItems = new SelectList(db.Genres, "GenreId", "GenreName").ToList();
            selectListItems.Insert(0, (new SelectListItem { Text = "All", Value = "0" }));
            ViewBag.GenreId = selectListItems;

            return View(movies.ToList());
        }

        // GET: Movies
        [HttpPost]
        public ActionResult Search(string txtName, int GenreId, DateTime? date)
        {
            var movies = db.Movies.Include(m => m.Genres);
            IQueryable<Movie> movieList = null;
            if (date == null)
            {
                if (GenreId == 0)
                {
                    movieList = movies.Where(m => m.MovieTitle.Contains(txtName));
                }
                else
                {
                    movieList = movies.Where(m => m.MovieTitle.Contains(txtName))
                   .Where(m => m.GenreId == GenreId);

                }
            }
            else
            {
                
                if (GenreId == 0)
                {
                    movieList = movies.Where(m => m.MovieTitle.Contains(txtName))
                        .Where(m => m.ReleaseDate.Value == date.Value);
                }
                else
                {
                    movieList = movies.Where(m => m.MovieTitle.Contains(txtName))
                   .Where(m => m.GenreId == GenreId)
                   .Where(m => m.ReleaseDate.Value == date.Value);
                }

            }


            return View("_MoviePartialView",
                movieList);

            //return View("_MoviePartialView",
            //    movies.Where(m => m.MovieTitle.Contains(txtName)));
        }


        

        // GET: Movies/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MovieId,MovieTitle,ReleaseDate,RunningTime,GenreId")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName", movie.GenreId);
            return View(movie);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName", movie.GenreId);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MovieId,MovieTitle,ReleaseDate,RunningTime,GenreId")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "GenreName", movie.GenreId);
            return View(movie);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
