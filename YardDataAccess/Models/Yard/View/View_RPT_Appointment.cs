using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Yard.View
{
    public class View_RPT_Appointment
    {
        [Key]
        public long RowIndex { get; set; }
        public Guid? Appointment_Index { get; set; }
        public string WareHouse_Name { get; set; }
        public string Dock_Name { get; set; }
        public Guid DocumentType_Index { get; set; }
        public string Doccumenttype_name { get; set; }
        public string Appointment_Id { get; set; }
        public string Owner_Id { get; set; }
        public string Owner_Name { get; set; }
        public string Ref_Document_No { get; set; }
        public DateTime Appointment_Date { get; set; }
        public string Appointment_Time { get; set; }
        public string ContactPerson_Name { get; set; }
        public string ContactPerson_Tel { get; set; }
        public string ContactPerson_EMail { get; set; }
        public string VehicleType_Name { get; set; }
        public string Vehicle_No { get; set; }
        public string Driver_Name { get; set; }
        public DateTime Create_Date { get; set; }
        public string remark { get; set; }
        public string asn { get; set; }

    }
}
