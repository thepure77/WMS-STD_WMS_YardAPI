using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

using DataAccess.Models.Qcall.Table;
using DataAccess.Models.Utils;
using DataAccess.Models.Yard.View;

namespace DataAccess
{
    public class QcallDbContext : DbContext
    {
        public virtual DbSet<GetValueByColumn> GetValueByColumn { get; set; }

        public virtual DbSet<tbl_qcall> tbl_qcall { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false);

                var configuration = builder.Build();

                var connectionString = configuration.GetConnectionString("Qcall_ConnectionString").ToString();

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
