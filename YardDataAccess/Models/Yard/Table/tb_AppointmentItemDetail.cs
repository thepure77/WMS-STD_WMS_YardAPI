using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Yard.Table
{
    public class Tb_AppointmentItemDetail
    {
        [Key]
        public Guid AppointmentItemDetail_Index { get; set; }

        public Guid AppointmentItem_Index { get; set; }

        public Guid Appointment_Index { get; set; }

        public string Appointment_Id { get; set; }

        public Guid? VehicleType_Index { get; set; }

        public string VehicleType_Id { get; set; }

        public string VehicleType_Name { get; set; }

        public string Vehicle_No { get; set; }

        public Guid? Driver_Index { get; set; }

        public string Driver_Id { get; set; }

        public string Driver_Name { get; set; }

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

        public Guid? TransportManifest_Index { get; set; }
    }
}
