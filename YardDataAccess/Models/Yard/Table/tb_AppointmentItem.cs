using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Yard.Table
{
    public class Tb_AppointmentItem
    {
        [Key]
        public Guid AppointmentItem_Index { get; set; }

        public string AppointmentItem_Id { get; set; }

        public Guid Appointment_Index { get; set; }

        public string Appointment_Id { get; set; }

        public Guid DocumentType_Index { get; set; }

        public string DocumentType_Id { get; set; }

        public string DocumentType_Name { get; set; }

        public DateTime Appointment_Date { get; set; }

        public string Appointment_Time { get; set; }

        public Guid WareHouse_Index { get; set; }

        public string WareHouse_Id { get; set; }

        public string WareHouse_Name { get; set; }

        public Guid DockQoutaInterval_Index { get; set; }

        public Guid Dock_Index { get; set; }

        public string Dock_Id { get; set; }

        public string Dock_Name { get; set; }

        public string Ref_Document_No { get; set; }

        public DateTime? Ref_Document_Date { get; set; }

        public Guid? Owner_Index { get; set; }

        public string Owner_Id { get; set; }

        public string Owner_Name { get; set; }
  
        public string ContactPerson_Name { get; set; }

        public string ContactPerson_EMail { get; set; }

        public string ContactPerson_Tel { get; set; }

        public string Remark { get; set; }

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

        public Guid? Group_Index { get; set; }
    }
}
