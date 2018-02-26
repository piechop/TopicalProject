using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TopicalProject.DAL
{
    public class MovieRepository : IMovieRepository,IDisposable
    {
        private List<Movie> _movies;

        public MovieRepository()
        {
            MovieXmlDataServices movieXmlDataService = new MovieXmlDataServices();

            using (movieXmlDataService)
            {
                _movies = movieXmlDataService.Read() as List<Movie>;
            }
        }

        public void Delete(int id)
        {
            var movie = _movies.Where(m => m.ID == id).FirstOrDefault();

            if (movie != null)
            {
                _movies.Remove(movie);
            }

            Save();
        }

        public void Insert(Movie movie)
        {
            movie.ID = NextIdValue();
            _movies.Add(movie);

            Save();
        }

        private int NextIdValue()
        {
            int currentMaxId = _movies.OrderByDescending(m => m.ID).FirstOrDefault().ID;
            return currentMaxId + 1;
        }

        public IEnumerable<Movie> SelectAll()
        {
            return _movies;
        }

        public Movie SelectOne(int id)
        {
            Movie selectedMovie = _movies.Where(m => m.ID == id).FirstOrDefault();

            return selectedMovie;
        }

        public void Update(Movie movie)
        {
            var oldMovie = _movies.Where(m => m.ID == movie.ID).FirstOrDefault();

            if (oldMovie != null)
            {
                _movies.Remove(oldMovie);
                _movies.Add(movie);
            }

            Save();
        }

        public void Save()
        {
            MovieXmlDataServices movieXmlDataService = new MovieXmlDataServices();

            using (movieXmlDataService)
            {
                movieXmlDataService.Write(_movies);
            }
        }

        public void Dispose()
        {
            _movies = null;
        }
    }
}