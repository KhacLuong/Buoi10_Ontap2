using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Buoi10_Ontap2.Models
{
    public class DbContextMovies: DbContext
    {
        public DbContextMovies() : base("name=Dbconnection")
        {

        }
        public DbSet<Genre> Genres { get; set; }

        public DbSet<Movie> Movies { get; set; }
    }
}