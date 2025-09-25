using Entity_Framework_Day_2_Tasks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Entity_Framework_Day_2_Tasks.Models;

namespace Entity_Framework_Day_2_Tasks.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly TheatreContext _context;

        public MovieRepository(TheatreContext context)
        {
            _context = context;
        }

        public List<Movie> GetAll()
        {
            return _context.Movies.AsNoTracking().ToList();
        }

        public Movie GetById(int id)
        {
            return _context.Movies.Find(id);
        }

        public void Add(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
        }

        public void Update(Movie movie)
        {
            _context.Movies.Update(movie);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var movie = GetById(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                _context.SaveChanges();
            }
        }
    }
}
