﻿using Mago.Services.AuthAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mago.Services.AuthAPI.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser /*IdentityUser*/>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional model configurations can go here
        }
    }
}
