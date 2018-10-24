using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ThatShot.Models
{
    public class ThatShotContext : DbContext
    {
        public ThatShotContext (DbContextOptions<ThatShotContext> options)
            : base(options)
        {
        }

        public DbSet<ThatShot.Models.Picture> Picture { get; set; }
    }
}
