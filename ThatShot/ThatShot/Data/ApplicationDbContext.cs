﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ThatShot.Models;

namespace ThatShot.Data
{
    public class ApplicationDbContext : IdentityDbContext<TSUser, TSRole, Guid>
    {
        public DbSet<Picture> Pictures { get; set; }


        public TSUser TSUsers { get; set; }
        public TSRole TSRoles { get; set; }


        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {

        }
    }
}
