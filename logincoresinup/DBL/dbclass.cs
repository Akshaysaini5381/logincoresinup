using logincoresinup.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace logincoresinup.DBL
{
    public class dbclass:DbContext
    {

        public dbclass(DbContextOptions<dbclass> options) : base(options)
        {

        }

        public DbSet<Employee> employees { get; set; }
        public DbSet<Emplogin> emplogins { get; set; }

    }
}
