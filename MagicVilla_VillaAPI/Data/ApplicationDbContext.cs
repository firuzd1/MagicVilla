﻿using MagicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }
        public DbSet<Villa> Villas {  get; set; }
        public DbSet<VillaNumber> villaNumber { get; set; }
        public DbSet<LocalUser> localUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { }

    }
}
