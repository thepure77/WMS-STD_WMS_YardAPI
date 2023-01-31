using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Yard.Table
{
    public partial class tb_BlockAppointmentTime
    {
        [Key]
        public Guid Block_index { get; set; }

        public Guid Appointment_Index { get; set; }

        public Guid AppointmentItem_Index { get; set; }

        public string Appointment_Id { get; set; }

        public string AppointmentItem_Id { get; set; }

        public DateTime Appointment_Date { get; set; }

        public string Appointment_Time { get; set; }

        public Guid DockQoutaInterval_Index { get; set; }

        public string Interval_Start { get; set; }

        public string Interval_End { get; set; }

        public int Seq { get; set; }

        public Guid Dock_Index { get; set; }

        public string Dock_Id { get; set; }

        public string Dock_Name { get; set; }

        public string Create_By { get; set; }

        public DateTime Create_Date { get; set; }

        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }
        
        public string Cancel_By { get; set; }
        
        public DateTime? Cancel_Date { get; set; }
    }
}
