using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Business.Models
{
    public static class CheckIn
    {
        public static CheckInModel GetCheckInModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                CheckInModel model = JsonConvert.DeserializeObject<CheckInModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to CheckInModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.CheckIn_Index.HasValue)
                    { 
                        throw new Exception("Invalid JSon : Required Primary Key CheckInModel");
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CheckInSearchModel GetCheckInSearchModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                CheckInSearchModel model = JsonConvert.DeserializeObject<CheckInSearchModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to CheckInSearchModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.CheckIn_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key CheckInSearchModel");
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

    public class CheckInModel
    {
        public Guid? CheckIn_Index { get; set; }

        public Guid? AppointmentItem_Index { get; set; }
        public string Appointment_Id { get; set; }

        public DateTime? CheckIn_Date { get; set; }

        public TimeSpan? CheckIn_Time { get; set; }

        public string Remark { get; set; }
        public string GateCheckIN_lat { get; set; }
        public string User_id { get; set; }
        public string GateCheckIN_long { get; set; }
        public string GateCheckIn_Date { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }
    }

    public class CheckInAppointmentModel
    { 
        public DateTime Appointment_Date { get; set; }

        public List<CheckInAppointmentItemModel> Items { get; set; } = new List<CheckInAppointmentItemModel>();
    }

    public class CheckInAppointmentItemModel
    {
        public Guid Appointment_Index { get; set; }

        public string Appointment_Id { get; set; }

        public DateTime Appointment_Date { get; set; }

        public string Appointment_Time { get; set; }

        public string Status { get; set; }

        public List<AppointmentItemModel> Items { get; set; } = new List<AppointmentItemModel>();
    }

    public class CheckInSearchModel
    {
        public Guid? CheckIn_Index { get; set; }

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
