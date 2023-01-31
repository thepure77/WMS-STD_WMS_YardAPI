using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Yard.Table
{
    public partial class tb_GateCheckOut
    {
        [Key]
        public Guid GateCheckOut_Index { get; set; }

        
        public Guid Appointment_Index { get; set; }

        
        [StringLength(100)]
        public string Appointment_Id { get; set; }

        
        public Guid AppointmentItem_Index { get; set; }

        
        [StringLength(100)]
        public string AppointmentItem_Id { get; set; }

        
        public DateTime Appointment_Date { get; set; }

        
        [StringLength(50)]
        public string Appointment_Time { get; set; }

        
        public DateTime GateCheckOut_Date { get; set; }

        

        [StringLength(2000)]
        public string Remark { get; set; }

        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

        public int? IsSystem { get; set; }

        public int? Status_Id { get; set; }

        
        [StringLength(200)]
        public string Create_By { get; set; }

       
        public DateTime Create_Date { get; set; }

        [StringLength(200)]
        public string Cancel_By { get; set; }

        public DateTime? Cancel_Date { get; set; }
    }
}
