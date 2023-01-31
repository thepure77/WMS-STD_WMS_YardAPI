using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Yard.Table
{
    public partial class tb_TruckloadBooking
    {
        [Key]
        public Guid TruckloadBooking_index { get; set; }

        public Guid Appointment_Index { get; set; }
        public string Appointment_Id { get; set; }

        public Guid AppointmentItem_Index { get; set; }
        
        public string AppointmentItem_Id { get; set; }

        public Guid DocumentType_Index { get; set; }
        
        public string DocumentType_Id { get; set; }
        
        public string DocumentType_Name { get; set; }

        public DateTime Appointment_Date { get; set; }
        
        public string Appointment_Time { get; set; }
        
        public string Vehicle_No { get; set; }
        
        public string Driver_Name { get; set; }

        public Guid? VehicleType_Index { get; set; }
        
        public string VehicleType_Id { get; set; }
        
        public string VehicleType_Name { get; set; }

        public Guid Dock_Index { get; set; }
        
        public string Dock_Id { get; set; }
        
        public string Dock_Name { get; set; }
        
        public string Ref_Document_No { get; set; }

        public Guid? Ref_Document_Index { get; set; }

        public Guid? TransportManifest_Index { get; set; }

        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

        public int? IsSystem { get; set; }

        public int? Status_Id { get; set; }
        
        public string Create_By { get; set; }

        public DateTime Create_Date { get; set; }

        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }
    }
}
