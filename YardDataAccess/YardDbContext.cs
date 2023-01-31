using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

using DataAccess.Models.Yard.Table;
using DataAccess.Models.Utils;
using DataAccess.Models.Yard.View;

namespace DataAccess
{
    public class YardDbContext : DbContext
    {
        public virtual DbSet<GetValueByColumn> GetValueByColumn { get; set; }

        public virtual DbSet<Ms_WareHouseQouta> Ms_WareHouseQouta { get; set; }
        public virtual DbSet<Ms_DocumentTypeNumber> Ms_DocumentTypeNumber { get; set; }
        public virtual DbSet<Tb_AppointmentItemDetail> Tb_AppointmentItemDetail { get; set; }


        public virtual DbSet<Ms_DockQouta> Ms_DockQouta { get; set; }

        public virtual DbSet<Ms_DockQoutaInterval> Ms_DockQoutaInterval { get; set; }

        public virtual DbSet<Tb_Appointment> Tb_Appointment { get; set; }

        public virtual DbSet<Tb_AppointmentItem> Tb_AppointmentItem { get; set; }
        public virtual DbSet<tb_BlockAppointmentTime> tb_BlockAppointmentTime { get; set; }

        public virtual DbSet<Tb_CheckIn> Tb_CheckIn { get; set; }

        public virtual DbSet<Tb_CheckOut> Tb_CheckOut { get; set; }

        public virtual DbSet<Tb_YardBalance> Tb_YardBalance { get; set; }

        public virtual DbSet<Tb_YardMovement> Tb_YardMovement { get; set; }
        public virtual DbSet<tb_VehicleDock> tb_VehicleDock { get; set; }
        public virtual DbSet<tb_GateCheckIn> tb_GateCheckIn { get; set; }
        public virtual DbSet<tb_GateCheckOut> tb_GateCheckOut { get; set; }
        public virtual DbSet<View_RPT_Appointment> View_RPT_Appointment { get; set; }
        public virtual DbSet<tb_QueueAppointment> tb_QueueAppointment { get; set; }
        public virtual DbSet<tb_TruckloadBooking> tb_TruckloadBooking { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false);

                var configuration = builder.Build();

                var connectionString = configuration.GetConnectionString("Yard_ConnectionString").ToString();

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
