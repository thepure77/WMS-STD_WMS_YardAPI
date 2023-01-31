using DataAccess.Models.GI.Table;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace DataAccess
{
    public class GIDbContext : DbContext
    {

        public virtual DbSet<im_TruckLoad> im_TruckLoad { get; set; }
        public virtual DbSet<im_TruckLoadImages> im_TruckLoadImages { get; set; }
        public virtual DbSet<im_TruckLoadItem> im_TruckLoadItem { get; set; }
        public virtual DbSet<View_tagout_with_truckload> View_tagout_with_truckload { get; set; }
        public virtual DbSet<View_CheckReport_Status> View_CheckReport_Status { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false);

                var configuration = builder.Build();

                var connectionString = configuration.GetConnectionString("Outbound_ConnectionString").ToString();

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
