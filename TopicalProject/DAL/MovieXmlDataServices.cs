using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using TopicalProject.Models;

namespace TopicalProject.DAL
{
    public class MovieXmlDataServices : IMovieDataServices, IDisposable
    {
        public List<Movie> Read()
        {
            Movies moviesObject;

            string xmlFilePath = HttpContext.Current.Application["dataFilePath"].ToString();
            StreamReader sReader = new StreamReader(xmlFilePath);

            XmlSerializer deserializer = new XmlSerializer(typeof(Movies));

            using (sReader)
            {
                object xmlObject = deserializer.Deserialize(sReader);
                moviesObject = (Movies)xmlObject;
            }

            return moviesObject.movies;
        }

        public void Write(List<Movie> movies)
        {
            string xmlFilePath = HttpContext.Current.Application["dataFilePath"].ToString();
            StreamWriter sWriter = new StreamWriter(xmlFilePath, false);

            XmlSerializer serializer = new XmlSerializer(typeof(List<Movie>), new XmlRootAttribute("Movies"));

            using (sWriter)
            {
                serializer.Serialize(sWriter, movies);
            }
        }

        public void Dispose()
        {

        }
    }
}