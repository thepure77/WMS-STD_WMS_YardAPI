using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

using DataAccess.Models.GR.Table;

namespace DataAccess
{
    public class GRDbContext : DbContext
    {
        public virtual DbSet<IM_PlanGoodsReceive> IM_PlanGoodsReceive { get; set; }
        public virtual DbSet<IM_PlanGoodsReceiveItem> IM_PlanGoodsReceiveItem { get; set; }
        public virtual DbSet<IM_GoodsReceive> IM_GoodsReceive { get; set; }
        public virtual DbSet<IM_GoodsReceiveItem> IM_GoodsReceiveItem { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false);

                var configuration = builder.Build();

                var connectionString = configuration.GetConnectionString("Inbound_ConnectionString").ToString();

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
