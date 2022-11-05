using Blazor.Diagrams.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
namespace ModelX.Data
{


public class AppDbContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<RbacUser> RbacUsers { get; set; }
        public DbSet<DatabaseTableColumn> DatabaseTableColumns { get; set; }
        public DbSet<DatabaseTable> DatabaseTables { get; set; }

        public DbSet<SystemErView> SystemErViews { get; set; }
        public DbSet<SystemErViewRelation> SystemErViewRelations { get; set; }

        //public string DbPath { get; }

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            //var folder = Environment.SpecialFolder.LocalApplicationData;
            //var path = Environment.GetFolderPath(folder);
            //DbPath = System.IO.Path.Join(path, "blogging.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlite($"Data Source={DbPath}");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DatabaseTable>().HasNoKey();
            modelBuilder.Entity<DatabaseTableColumn>().HasNoKey();
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; } = new();
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
