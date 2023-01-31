using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Yard.Table
{
    public class Tb_CheckIn
    {
        [Key]
        public Guid CheckIn_Index { get; set; }

        public Guid Appointment_Index { get; set; }

        public string Appointment_Id { get; set; }

        public Guid AppointmentItem_Index { get; set; }

        public string AppointmentItem_Id { get; set; }

        public DateTime Appointment_Date { get; set; }

        public string Appointment_Time { get; set; }

        public DateTime CheckIn_Date { get; set; }

        public TimeSpan CheckIn_Time { get; set; }

        public string Remark { get; set; }

        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

        public int? IsSystem { get; set; }

        public int? Status_Id { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public string Cancel_By { get; set; }

        public DateTime? Cancel_Date { get; set; }
    }
}
