using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Models
{
    public class TransportManifest
    {
        public TransportManifest()
        {
            items = new List<Item>();
        }

        public bool response { get; set; }
        public int? errorItem { get; set; }
        public string message { get; set; }
        public string totalTime { get; set; }
        public string TransportManifest_No { get; set; }
        public string TransportManifest_Index { get; set; }
        public List<Item> items { get; set; }
        
    }

    public class bookTransportManifest
    {
        public bookTransportManifest()
        {
            items = new List<Item>();
        }

        public string response { get; set; }
        public int? errorItem { get; set; }
        public string message { get; set; }
        public string totalTime { get; set; }
        public string TransportManifest_No { get; set; }
        public string TransportManifest_Index { get; set; }
        public List<Item> items { get; set; }

    }



    public class Item
    {
        public Item()
        {
            itemsDO = new List<itemsDO>();
        }
        public string transportManifest_Index { get; set; }
        public string transportManifest_No { get; set; }
        public string result { get; set; }
        public string code { get; set; }
        public string message { get; set; }
        public Guid? release_Index { get; set; }
        public List<itemsDO> itemsDO { get; set; }

    }

    public class itemsDO
    {
        public string TransportOrder_No { get; set; }
        public string Tag_No { get; set; }
        public string Product_Id { get; set; }
        public decimal? Qty { get; set; }
        public string ProductConversion_Name { get; set; }
        public decimal? Width { get; set; }
        public decimal? Length { get; set; }
        public decimal? Height { get; set; }
        public decimal? Volume { get; set; }
        public decimal? Weight_Unit { get; set; }

    }

    public class TransportManifestUpdate
    {
        public string Truckload_no { get; set; }
        public string status { get; set; }
        public Guid? Truckload_Index { get; set; }
        public string expect_Pickup_Time { get; set; }
        public string Driver_Name { get; set; }
        public string Vehicle_no { get; set; }
        public string VehicleType_Id { get; set; }
        public string VehicleType_Name { get; set; }
        public DateTime? expect_Pickup_Date { get; set; }
        public Guid? VehicleType_Index { get; set; }

    }

    public class sendMsgModel
    {
        public string msg { get; set; }
    }
}
