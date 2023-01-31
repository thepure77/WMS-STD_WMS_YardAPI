using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Master.Table
{
    public partial class ms_GateDock
    {
        [Key]
        public Guid Gate_Index { get; set; }

        public string Gate_Id { get; set; }

        public string Gate_Name { get; set; }

        public Guid Dock_Index { get; set; }

        public string Dock_Id { get; set; }

        public string Dock_Name { get; set; }

        public int? IsActive { get; set; }

        public int? IsSystem { get; set; }

        public int? Status_Id { get; set; }

        [StringLength(200)]
        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        [StringLength(200)]
        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }

        [StringLength(200)]
        public string Cancel_By { get; set; }

        public DateTime? Cancel_Date { get; set; }
    }
}
