using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Entity_Framework_Day_3_CodeFirst.Models
{
    public class TheatreContext : DbContext
    {
        
        public DbSet<Movie> Movies { get; set; }

       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=TheatreCodeFirstDB;Trusted_Connection=True;Encrypt=False");
        }
    }
}
