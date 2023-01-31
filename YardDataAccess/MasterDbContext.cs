using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

using DataAccess.Models.Master.Table;
using DataAccess.Models.Master.View;
using DataAccess.Models.Utils;

namespace DataAccess
{
    public class MasterDbContext : DbContext
    {
        public virtual DbSet<GetValueByColumn> GetValueByColumn { get; set; }

        public virtual DbSet<Sy_AutoNumber> Sy_AutoNumber { get; set; }

        public virtual DbSet<Ms_DocumentType> Ms_DocumentType { get; set; }

        public virtual DbSet<Ms_FacilityType> Ms_FacilityType { get; set; }

        public virtual DbSet<Ms_Facility> Ms_Facility { get; set; }

        public virtual DbSet<Ms_Dock> Ms_Dock { get; set; }

        public virtual DbSet<Ms_DockType> Ms_DockType { get; set; }

        public virtual DbSet<Ms_DockZone> Ms_DockZone { get; set; }

        public virtual DbSet<Ms_DockLocation> Ms_DockLocation { get; set; }

        public virtual DbSet<Ms_GateType> Ms_GateType { get; set; }

        public virtual DbSet<Ms_Gate> Ms_Gate { get; set; }

        public virtual DbSet<Ms_GateLane> Ms_GateLane { get; set; }

        public virtual DbSet<Ms_Location> Ms_Location { get; set; }

        public virtual DbSet<Ms_Yard> Ms_Yard { get; set; }

        public virtual DbSet<Ms_YardType> Ms_YardType { get; set; }

        public virtual DbSet<Ms_Warehouse> Ms_WareHouse { get; set; }

        public virtual DbSet<View_Dock> View_Dock { get; set; }

        public virtual DbSet<View_DockZone> View_DockZone { get; set; }

        public virtual DbSet<View_DockLocation> View_DockLocation { get; set; }

        public virtual DbSet<View_Gate> View_Gate { get; set; }

        public virtual DbSet<View_GateLane> View_GateLane { get; set; }

        public virtual DbSet<View_Facility> View_Facility { get; set; }

        public virtual DbSet<View_Yard> View_Yard { get; set; }
        public virtual DbSet<ms_GateDock> ms_GateDock { get; set; }
        public virtual DbSet<ms_VehicleType> ms_VehicleType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false);

                var configuration = builder.Build();

                var connectionString = configuration.GetConnectionString("Master_ConnectionString").ToString();

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
