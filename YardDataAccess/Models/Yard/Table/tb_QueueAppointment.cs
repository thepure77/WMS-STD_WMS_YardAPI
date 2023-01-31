using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Yard.Table
{
    public partial class tb_QueueAppointment
    {
        [Key]
        public Guid Queue_Index { get; set; }

        public Guid Appointment_index { get; set; }

        public string Appointment_Id { get; set; }
        public string Vehicle_no { get; set; }

        public string Queue_no { get; set; }

        public DateTime? Queue_date { get; set; }

        public Guid? Dock_Index { get; set; }

        public string Dock_Id { get; set; }

        public string Dock_Name { get; set; }
        public string Create_by { get; set; }

        public DateTime? Create_date { get; set; }

        public string Update_by { get; set; }

        public DateTime? Update_date { get; set; }
    }
}
