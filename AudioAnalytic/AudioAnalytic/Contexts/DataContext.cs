using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioAnalytic.Entities
{
    public class DataContext : DbContext
    {
        //    public DataContext(DbContextOptions<DataContext> options) : base(options)
        //    {
        //    }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=CoividDB;Persist Security Info=True;User ID=sa;Password=abcdef;MultipleActiveResultSets=true");
           // optionsBuilder.UseSqlite("Data Source=datas/di_bi.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          //  modelBuilder.Entity<AudioDetail>().ToTable("AudioDetail");

            modelBuilder.Entity<PublicTrain>().ToTable("PublicTrain");
            modelBuilder.Entity<PublicTest>().ToTable("PublicTest");

            modelBuilder.Entity<FileFeature>().ToTable("FileFeature");
        }

        public DbSet<PublicTrain> PublicTrains { get; set; }
        public DbSet<PublicTest> PublicTests { get; set; }
        public DbSet<FileFeature> FileFeatures { get; set; }
    }
}
