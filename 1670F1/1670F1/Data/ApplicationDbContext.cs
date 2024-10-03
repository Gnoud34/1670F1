using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using _1670F1.Models;

namespace _1670F1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<_1670F1.Models.Category> Category { get; set; }
        public DbSet<_1670F1.Models.JobPost> JobPost { get; set; }
        public DbSet<_1670F1.Models.Employer> Employer { get; set; }
        public DbSet<_1670F1.Models.JobSeeker> JobSeeker { get; set; }
        public DbSet<_1670F1.Models.Application> Application { get; set; }

        // Override OnModelCreating to define relationships and delete behavior
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>()
                .HasMany(c => c.JobPosts)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            // Modify delete behavior for Application -> JobSeeker relationship
            modelBuilder.Entity<Application>()
                .HasOne(a => a.JobSeeker)
                .WithMany()
                .HasForeignKey(a => a.JobSeekerId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevents cascading delete

            // Modify delete behavior for Application -> JobPost relationship
            modelBuilder.Entity<Application>()
                .HasOne(a => a.JobPost)
                .WithMany()
                .HasForeignKey(a => a.JobPostId)
                .OnDelete(DeleteBehavior.Cascade);  // Keeps cascading delete, or modify if necessary
        }
    }
}
