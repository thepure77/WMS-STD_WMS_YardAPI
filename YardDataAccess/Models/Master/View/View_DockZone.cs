using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Master.View
{
    public class View_DockZone
    {
        [Key]
        public Guid? DockZone_Index { get; set; }

        public string DockZone_Id { get; set; }

        public string DockZone_Name { get; set; }

        public Guid? Facility_Index { get; set; }

        public string Facility_Id { get; set; }

        public string Facility_Name { get; set; }

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
