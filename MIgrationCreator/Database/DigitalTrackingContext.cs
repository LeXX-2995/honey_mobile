using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace MIgrationCreator.Database
{
    public class DigitalTrackingContext : DbContext
    {
        private readonly string _databasePath;

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
        public DbSet<ReportReturn> ReportReturn { get; set; }
        public DbSet<PalletDataMatrix> PalletDataMatrix { get; set; }
        public DigitalTrackingContext()
        {
            
        }

        public DigitalTrackingContext(string databasePath)
        {
            _databasePath = databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }
    }
}
