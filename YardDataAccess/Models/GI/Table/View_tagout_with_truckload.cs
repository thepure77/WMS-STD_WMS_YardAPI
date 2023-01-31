using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models.GI.Table
{
    public partial class View_tagout_with_truckload
    {
        [Key]
        public long? RowIndex { get; set; }
        
        public Guid TruckLoad_Index { get; set; }
        
        public string TruckLoad_No { get; set; }
        
        public Guid PlanGoodsIssue_Index { get; set; }
        
        public string LineNum { get; set; }

        public Guid? Product_Index { get; set; }
        
        public string Product_Id { get; set; }
        
        public string Product_Name { get; set; }
        
        public decimal? QTY { get; set; }
        
        public decimal? TotalQty { get; set; }
        
        public string ProductConversion_Name { get; set; }
        
        public decimal? Weight { get; set; }
        
        public decimal? Width { get; set; }
        
        public decimal? Length { get; set; }
        
        public decimal? Height { get; set; }
        
        public decimal? Volume { get; set; }

        public int? Document_Status { get; set; }
        
        public string PlanGoodsIssue_No { get; set; }
        
        public string TagOut_No { get; set; }
    }
}
