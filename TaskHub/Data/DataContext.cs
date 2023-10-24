﻿using Microsoft.EntityFrameworkCore;
using TaskHub.Models;

namespace TaskHub.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<ProjectTasks> ProjectTasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProject>()
                .HasKey(up => new { up.UserId, up.ProjectId });

            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.User)
                .WithMany(u => u.UserProjects)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserProject>()
                .HasOne(up => up.Project)
                .WithMany(p => p.Team)
                .HasForeignKey(u => u.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<User>()
                .HasMany(u => u.AssignedTasks)
                .WithOne(pt => pt.User)
                .HasForeignKey(pt => pt.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Remove ON DELETE CASCADE
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.ProjectTasks)
                .WithMany(pt => pt.Comments)
                .HasForeignKey(c => c.ProjectTasksId)
                .OnDelete(DeleteBehavior.Restrict); // Specify ON DELETE NO ACTION

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Specify ON DELETE NO ACTION
        }

    }
}