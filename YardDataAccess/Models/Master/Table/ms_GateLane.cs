using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Master.Table
{
    public class Ms_GateLane
    {
        [Key]
        public Guid GateLane_Index { get; set; }

        public string GateLane_Id { get; set; }

        public string GateLane_Name { get; set; }

        public Guid? Gate_Index { get; set; }

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
