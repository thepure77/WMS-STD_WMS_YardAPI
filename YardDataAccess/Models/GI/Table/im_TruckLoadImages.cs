using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.GI.Table
{
    public partial class im_TruckLoadImages
    {
        [Key]
        public Guid TruckLoadImage_Index { get; set; }

        public Guid TruckLoad_Index { get; set; }


        public string TruckLoad_No { get; set; }

        public int? Document_Status { get; set; }


        public string ImageUrl { get; set; }


        public string ImageType { get; set; }


        public string Remark { get; set; }


        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }


        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }


        public string Cancel_By { get; set; }

        public DateTime? Cancel_Date { get; set; }
    }
}
