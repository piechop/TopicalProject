using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TopicalProject.DAL;

namespace TopicalProject.Controllers
{
    public class MovieController : Controller
    {
        // GET: Brewery
        [HttpGet]
        public ActionResult Index(string sortOrder, int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            MovieRepository movieRepository = new MovieRepository();
            ViewBag.Years = ListOfYears();

            IEnumerable<Movie> movies;
            using (movieRepository)
            {
                movies = movieRepository.SelectAll() as IList<Movie>;
            }

            switch (sortOrder)
            {
                case "Title":
                    movies = movies.OrderBy(m => m.Title);
                    break;
                case "Year":
                    movies = movies.OrderBy(m => m.Release.Year).Reverse();
                    break;
                case "Director":
                    movies = movies.OrderBy(m => m.Director);
                    break;
                default:
                    movies = movies.OrderBy(m => m.Title);
                    break;
            }

            //movies = movies.ToPagedList(pageNumber, pageSize);

            return View(movies);
        }

        [HttpPost]
        public ActionResult Index(string searchCriteria, int yearFilter, int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            MovieRepository movieRepository = new MovieRepository();
            ViewBag.Years = ListOfYears();

            IEnumerable<Movie> movies;
            using (movieRepository)
            {
                movies = movieRepository.SelectAll() as IList<Movie>;
            }

            if (searchCriteria != null)
            {
                movies = movies.Where(m => m.Title.ToUpper().Contains(searchCriteria.ToUpper()));
            }

            if (yearFilter >= 0)
            {
                movies = movies.Where(m => m.Release.Year == yearFilter);
            }

            //movies = movies.ToPagedList(pageNumber, pageSize);

            return View(movies);
        }

        // GET: Brewery/Details/5
        public ActionResult Details(int id)
        {
            MovieRepository movieRepository = new MovieRepository();
            Movie movie = new Movie();

            using (movieRepository)
            {
                movie = movieRepository.SelectOne(id);
            }
            return View(movie);
        }

        // GET: Brewery/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Brewery/Create
        [HttpPost]
        public ActionResult Create(Movie movie)
        {
            try
            {
                MovieRepository movieRepository = new MovieRepository();
                using (movieRepository)
                {
                    movieRepository.Insert(movie);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Brewery/Edit/5
        public ActionResult Edit(int id)
        {
            MovieRepository movieRepository = new MovieRepository();
            Movie movie = new Movie();

            using (movieRepository)
            {
                movie = movieRepository.SelectOne(id);
            }

            return View(movie);
        }

        // POST: Brewery/Edit/5
        [HttpPost]
        public ActionResult Edit(Movie movie)
        {
            try
            {
                MovieRepository movieRepository = new MovieRepository();

                using (movieRepository)
                {
                    movieRepository.Update(movie);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Brewery/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Brewery/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [NonAction]
        private IEnumerable<int> ListOfYears()
        {
            MovieRepository movieRepository = new MovieRepository();

            IEnumerable<Movie> movies;
            using (movieRepository)
            {
                movies = movieRepository.SelectAll() as IList<Movie>;
            }

            var years = movies.Select(m => m.Release.Year).Distinct().OrderBy(x => x);

            return years;
        }
    }
}
