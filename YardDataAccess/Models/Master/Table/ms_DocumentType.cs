using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models.Master.Table
{

    public partial class Ms_DocumentType
    {
        [Key]
        public Guid DocumentType_Index { get; set; }

        public Guid? Process_Index { get; set; }

        [StringLength(50)]
        public string DocumentType_Id { get; set; }

        [StringLength(200)]
        public string DocumentType_Name { get; set; }

        [StringLength(200)]
        public string Format_Text { get; set; }

        [StringLength(200)]
        public string Format_Date { get; set; }

        [StringLength(200)]
        public string Format_Running { get; set; }

        [StringLength(200)]
        public string Format_Document { get; set; }

        public int? IsResetByYear { get; set; }

        public int? IsResetByMonth { get; set; }

        public int? IsResetByDay { get; set; }

        public int? IsActive { get; set; }

        public int? IsDelete { get; set; }

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
