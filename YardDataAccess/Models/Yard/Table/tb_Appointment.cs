using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Yard.Table
{
    public class Tb_Appointment
    {
        [Key]
        public Guid Appointment_Index { get; set; }

        public string Appointment_Id { get; set; }

        public Guid DocumentType_Index { get; set; }

        public string DocumentType_Id { get; set; }

        public string DocumentType_Name { get; set; }

        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

        public int? IsSystem { get; set; }

        public int? Status_Id { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }

        public string Cancel_By { get; set; }

        public DateTime? Cancel_Date { get; set; }

        public int? Document_status { get; set; }
        public int? In_queue { get; set; }
        public int? Car_Inspection { get; set; }
        public string Remark { get; set; }
        public Guid? GateCheckIn_Index { get; set; }
        public Guid? GateCheckOut_Index { get; set; }
        public string Ref_document_No { get; set; }
        public Guid? Ref_planGoodsReceive_Index { get; set; }
        public string Runing_Q { get; set; }
        public string User_carInspection { get; set; }

    }
}
