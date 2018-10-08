using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ThatShot.Models;

namespace ThatShot.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        public DbSet<Genre> Genres { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}
