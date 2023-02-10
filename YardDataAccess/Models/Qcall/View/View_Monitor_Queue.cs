using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Qcall.View
{
    public class View_Monitor_Queue
    {
        [Key]
        public int TransactionID { get; set; }
        public string QNo { get; set; }
        public string DockNo { get; set; }
        public string DockType { get; set; }
        public string LicenseNo { get; set; }
        public string Appointment_Id { get; set; }
        public string QueueDate { get; set; }
        public string QueueTime { get; set; }
        public string CheckInDate { get; set; }
        public string CheckInTime { get; set; }
        public string StatusText { get; set; }
        public short Status { get; set; }
        public string Remark { get; set; }
        public DateTime UpdateDT { get; set; }
        public string UpdateBy { get; set; }

    }
}
