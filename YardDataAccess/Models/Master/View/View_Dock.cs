using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Master.View
{
    public class View_Dock
    {
        [Key]
        public Guid? Dock_Index { get; set; }

        public string Dock_Id { get; set; }

        public string Dock_Name { get; set; }

        public Guid? DockType_Index { get; set; }

        public string DockType_Id { get; set; }

        public string DockType_Name { get; set; }

        public Guid? DockZone_Index { get; set; }

        public string DockZone_Id { get; set; }

        public string DockZone_Name { get; set; }

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
    }
}
