using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Yard.Table
{
    public partial class tb_VehicleDock
    {
       
        [Key]
        public Guid VehicleType_Index { get; set; }
        
        public string VehicleType_Id { get; set; }
        
        public string VehicleType_Name { get; set; }
        
        public Guid Dock_Index { get; set; }
        
        public string Dock_Id { get; set; }
        
        public string Dock_Name { get; set; }
    }
}
