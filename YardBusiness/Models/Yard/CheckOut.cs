using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Business.Models
{
    public static class CheckOut
    {
        public static CheckOutModel GetCheckOutModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                CheckOutModel model = JsonConvert.DeserializeObject<CheckOutModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to CheckOutModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.CheckOut_Index.HasValue)
                    { 
                        throw new Exception("Invalid JSon : Required Primary Key CheckOutModel");
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CheckOutSearchModel GetCheckOutSearchModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                CheckOutSearchModel model = JsonConvert.DeserializeObject<CheckOutSearchModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to CheckOutSearchModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.CheckOut_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key CheckOutSearchModel");
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class CheckOutModel
    {
        public Guid? CheckOut_Index { get; set; }

        public Guid? YardBalance_Index { get; set; }

        public Guid? AppointmentItem_Index { get; set; }
        public string Appointment_Id { get; set; }

        public DateTime? CheckOut_Date { get; set; }

        public TimeSpan? CheckOut_Time { get; set; }

        public string Remark { get; set; }

        public string Create_By { get; set; }
        public string GateCheckOut_long { get; set; }
        public string GateCheckOut_lat { get; set; }

        public DateTime? Create_Date { get; set; }
    }

    public class CheckOut_ErrorModel
    {

        public string id { get; set; }
 
    }

    public class status_byTMS
    {

        public string tm_no { get; set; }
        public string vehicleType_Id { get; set; }
        public string vehicleType_Name { get; set; }
        public string vehicle_No { get; set; }
        public string driver_Name { get; set; }
        public string vehicleCompany_Id { get; set; }
        public string vehicleCompany_Name { get; set; }
        public string IsAirCon { get; set; }
        public string flagColdRoom { get; set; }

    }

    public class CheckOutAppointmentModel
    {
        public DateTime Appointment_Date { get; set; }

        public List<CheckOutAppointmentItemModel> Items { get; set; } = new List<CheckOutAppointmentItemModel>();
    }

    public class CheckOutAppointmentItemModel
    {
        public Guid Appointment_Index { get; set; }

        public string Appointment_Id { get; set; }

        public DateTime Appointment_Date { get; set; }

        public string Appointment_Time { get; set; }

        public string Status { get; set; }

        public List<AppointmentItemModel> Items { get; set; } = new List<AppointmentItemModel>();
    }


    public class CheckOutSearchModel
    {
        public Guid? CheckOut_Index { get; set; }

        public Guid? YardBalance_Index { get; set; }

        public Guid? Appointment_Index { get; set; }

        public string Appointment_Id { get; set; }

        public Guid? AppointmentItem_Index { get; set; }

        public string AppointmentItem_Id { get; set; }

        public bool IsRemove { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public DateTime? Create_Date_To { get; set; }
    }

}
