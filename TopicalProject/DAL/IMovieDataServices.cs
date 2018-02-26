using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopicalProject.DAL
{
    interface IMovieDataServices
    {
        List<Movie> Read();
        void Write(List<Movie> movies);
    }
}
