using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Yard.Table
{
    public class Ms_DockQouta
    {
        [Key]
        public Guid DockQouta_Index { get; set; }

        public string DockQouta_Id { get; set; }

        public Guid WareHouse_Index { get; set; }

        public string WareHouse_Id { get; set; }

        public string WareHouse_Name { get; set; }

        public DateTime? DockQouta_Date { get; set; }

        public DateTime? DockQouta_Date_End { get; set; }

        public int Interval { get; set; }

        public string DockQouta_Time { get; set; }

        public string DockQouta_Time_End { get; set; }

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

        public string checkIn_Limit_Before { get; set; }

        public string checkIn_Limit_After { get; set; }
    }
}
