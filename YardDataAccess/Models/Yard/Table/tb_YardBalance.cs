using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Yard.Table
{
    public class Tb_YardBalance
    {
        [Key]
        public Guid YardBalance_Index { get; set; }

        public Guid Appointment_Index { get; set; }

        public string Appointment_Id { get; set; }

        public Guid AppointmentItem_Index { get; set; }

        public string AppointmentItem_Id { get; set; }

        public Guid DocumentType_Index { get; set; }

        public string DocumentType_Id { get; set; }

        public string DocumentType_Name { get; set; }

        public DateTime? Appointment_Date { get; set; }

        public string Appointment_Time { get; set; }

        public Guid DockQoutaInterval_Index { get; set; }

        public Guid WareHouse_Index { get; set; }

        public string WareHouse_Id { get; set; }

        public string WareHouse_Name { get; set; }

        public Guid Dock_Index { get; set; }

        public string Dock_Id { get; set; }

        public string Dock_Name { get; set; }

        public Guid? Owner_Index { get; set; }

        public string Owner_Id { get; set; }

        public string Owner_Name { get; set; }

        public string Ref_Document_No { get; set; }

        public DateTime? Ref_Document_Date { get; set; }

        public string ContactPerson_Name { get; set; }

        public string ContactPerson_EMail { get; set; }

        public string ContactPerson_Tel { get; set; }

        public string Remark { get; set; }

        public Guid? CheckIn_Index { get; set; }

        public DateTime? CheckIn_Date { get; set; }

        public TimeSpan? CheckIn_Time { get; set; }

        public string CheckIn_Remark { get; set; }

        public string CheckIn_By { get; set; }

        public string CheckIn_Status { get; set; }
        public string GateCheckIn_Status { get; set; }

        public Guid? CheckOut_Index { get; set; }

        public DateTime? CheckOut_Date { get; set; }

        public TimeSpan? CheckOut_Time { get; set; }

        public string CheckOut_Remark { get; set; }

        public string CheckOut_By { get; set; }

        public string CheckOut_Status { get; set; }
        public string GateCheckOut_Status { get; set; }

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

        public Guid? GateCheckOut_Index { get; set; }
        public DateTime? GateCheckOut_Date { get; set; }
        public Guid? GateCheckIn_Index { get; set; }
        public DateTime? GateCheckIn_Date { get; set; }

        public string GateCheckIN_lat { get; set; }
        public string GateCheckIN_long { get; set; }
        public string GateCheckOut_lat { get; set; }
        public string GateCheckOut_long { get; set; }
        public string User_id { get; set; }
        
    }
}
