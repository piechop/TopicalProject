using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicalProject.DAL
{
    interface IMovieRepository
    {
        IEnumerable<Movie> SelectAll();
        Movie SelectOne(int id);
        void Insert(Movie movie);
        void Update(Movie movie);
        void Delete(int id);
    }
}
