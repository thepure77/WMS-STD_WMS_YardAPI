using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Yard.Table
{
    public class Ms_WareHouseQouta
    {
        [Key]
        public Guid WareHouseQouta_Index { get; set; }

        public string WareHouseQouta_Id { get; set; }

        public Guid WareHouse_Index { get; set; }

        public string WareHouse_Id { get; set; }

        public string WareHouse_Name { get; set; }

        public int Display_Date { get; set; }

        public int Pre_Booking { get; set; }

        public int CheckIn_Limit_Before { get; set; }

        public int CheckIn_Limit_After { get; set; }

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
