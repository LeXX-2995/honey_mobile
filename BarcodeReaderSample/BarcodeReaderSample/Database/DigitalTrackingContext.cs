using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Android.App;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace BarcodeReaderSample.Database
{
    public partial class DigitalTrackingContext : DbContext
    {
        private readonly string _databaseFileName = "dtrack13.db";

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Pallet> Pallets { get; set; }
        public DbSet<Box> Boxes { get; set; }
        public DbSet<DataMatrix> DataMatrix { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<CodesMapping> CodesMappings { get; set; }
        public DbSet<Setting> Setting { get; set; }
        public DbSet<OrderCodeMapping> OrderCodeMappings { get; set; }
        public DbSet<ReportReturn> ReportReturns { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), _databaseFileName);
            optionsBuilder.UseSqlite($"Filename={path}");
        }
    }
}
