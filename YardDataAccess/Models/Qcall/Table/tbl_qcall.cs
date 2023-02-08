using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Qcall.Table
{
    public class tbl_qcall
    {
        [Key]
        public int TransactionID { get; set; }
        public string QNo { get; set; }
        public string DockNo { get; set; }
        public string DockType { get; set; }
        public string LicenseNo { get; set; }
        public string Appointment_Id { get; set; }
        public short Status { get; set; }
        public DateTime UpdateDT { get; set; }
        public string UpdateBy { get; set; }

    }
}
