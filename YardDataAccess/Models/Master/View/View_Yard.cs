using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.Master.View
{
    public class View_Yard
    {
        [Key]
        public Guid? Yard_Index { get; set; }

        public string Yard_Id { get; set; }

        public string Yard_Name { get; set; }

        public Guid? YardType_Index { get; set; }

        public string YardType_Id { get; set; }

        public string YardType_Name { get; set; }

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
