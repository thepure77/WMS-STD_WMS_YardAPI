using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Master.View
{
    public class View_DockLocation
    {
        [Key]
        public Guid? DockLocation_Index { get; set; }

        public Guid? Dock_Index { get; set; }

        public string Dock_Id { get; set; }

        public string Dock_Name { get; set; }

        public Guid? Location_Index { get; set; }

        public string Location_Id { get; set; }

        public string Location_Name { get; set; }

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
