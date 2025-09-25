using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity_Framework_Day_2_Tasks.Repositories
{
    using Entity_Framework_Day_2_Tasks.Models;

    public interface IMovieRepository
    {
        List<Movie> GetAll();
        Movie GetById(int id);
        void Add(Movie movie);
        void Update(Movie movie);
        void Delete(int id);
    }
}
