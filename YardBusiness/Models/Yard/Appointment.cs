using System;
using System.Collections.Generic;
using Newtonsoft.Json;

using Business.Commons;
using static Business.Models.AppointmentSearchModel;

namespace Business.Models
{
    public static class Appointment
    {
        public static AppointmentModel GetAppointmentModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                AppointmentModel model = JsonConvert.DeserializeObject<AppointmentModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to AppointmentModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.Appointment_Index.HasValue)
                    { 
                        throw new Exception("Invalid JSon : Required Primary Key AppointmentModel");
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static AppointmentItemModel GetAppointmentItemModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                AppointmentItemModel model = JsonConvert.DeserializeObject<AppointmentItemModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to AppointmentItemModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.AppointmentItem_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key AppointmentItemModel");
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static AppointmentItemDetailModel GetAppointmentItemDetailModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                AppointmentItemDetailModel model = JsonConvert.DeserializeObject<AppointmentItemDetailModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to AppointmentItemDetailModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.AppointmentItemDetail_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key AppointmentItemDetailModel");
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static AppointmentSearchModel GetAppointmentSearchModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                AppointmentSearchModel model = JsonConvert.DeserializeObject<AppointmentSearchModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to AppointmentSearchModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.Appointment_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key AppointmentSearchModel");
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static AppointmentItemSearchModel GetAppointmentItemSearchModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                AppointmentItemSearchModel model = JsonConvert.DeserializeObject<AppointmentItemSearchModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to AppointmentItemSearchModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.AppointmentItem_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key AppointmentItemSearchModel");
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static AppointmentItemDetailSearchModel GetAppointmentItemDetailSearchModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                AppointmentItemDetailSearchModel model = JsonConvert.DeserializeObject<AppointmentItemDetailSearchModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to AppointmentItemDetailSearchModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.AppointmentItemDetail_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key AppointmentItemDetailSearchModel");
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

    public class AppointmentModel
    {
        public int Seq { get; set; }

        public Guid? Appointment_Index { get; set; }

        public Guid? VehicleType_Index { get; set; }
        public string VehicleType_Name { get; set; }
        public string VehicleType_No { get; set; }

        public string Appointment_Id { get; set; }

        public Guid DocumentType_Index { get; set; }

        public string DocumentType_Id { get; set; }

        public string DocumentType_Name { get; set; }

        public DateTime? Appointment_Date { get; set; }

        public string Appointment_Time { get; set; }

        public Guid WareHouse_Index { get; set; }

        public string WareHouse_Id { get; set; }

        public string WareHouse_Name { get; set; }

        public Guid Dock_Index { get; set; }

        public string Dock_Id { get; set; }

        public string Dock_Name { get; set; }

        public string Ref_Document_No { get; set; }

        public DateTime? Ref_Document_Date { get; set; }

        public Guid? Owner_Index { get; set; }
        public Guid? dockQoutaInterval_Index { get; set; }

        public string Owner_Id { get; set; }

        public string Owner_Name { get; set; }

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

        public int? Document_status { get; set; }
        public int? Is_reassign { get; set; }

        public string Confirm_status { get; set; }

        public bool IsGroup { get; set; }

        public string Remark { get; set; }


        public List<AppointmentItemModel> Items { get; set; } = new List<AppointmentItemModel>();
    }

    public class AppointmentItemModel
    {
        public TimeSpan? CheckIn_Time { get; set; }

        public TimeSpan? CheckOut_Time { get; set; }

        public string CheckIn_Status { get; set; }

        public int Seq { get; set; }

        public Guid? AppointmentItem_Index { get; set; }

        public string AppointmentItem_Id { get; set; }

        public Guid Appointment_Index { get; set; }

        public string Appointment_Id { get; set; }

        public Guid DocumentType_Index { get; set; }

        public string DocumentType_Id { get; set; }

        public string DocumentType_Name { get; set; }

        public DateTime Appointment_Date { get; set; }

        public string Appointment_Time { get; set; }

        public Guid? DockQoutaInterval_Index { get; set; }

        public Guid WareHouse_Index { get; set; }

        public string WareHouse_Id { get; set; }

        public string WareHouse_Name { get; set; }

        public Guid Dock_Index { get; set; }

        public string Dock_Id { get; set; }

        public string Dock_Name { get; set; }

        public string Ref_Document_No { get; set; }

        public DateTime? Ref_Document_Date { get; set; }

        public Guid? Owner_Index { get; set; }

        public string Owner_Id { get; set; }

        public string Owner_Name { get; set; }

        public string ContactPerson_Name { get; set; }

        public string ContactPerson_EMail { get; set; }

        public string ContactPerson_Tel { get; set; }

        public string Remark { get; set; }

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

        public Guid? YardBalance_Index { get; set; }

        public Guid? Group_Index { get; set; }

        public int Status { get; set; }

        public string Status_Desc { get; set; }

        public List<AppointmentItemDetailModel> Details { get; set; } = new List<AppointmentItemDetailModel>();
        public List<DockQoutaIntervalBreakTime> DockQoutaIntervalBreakTime { get; set; } = new List<DockQoutaIntervalBreakTime>();
    }

    public class DockQoutaIntervalBreakTime
    {
        public int? index { get; set; }

        public datarow datarow { get; set; } = new datarow();
    }

    public class datarow
    {
        public Guid? dockQoutaInterval_Index { get; set; }
        public string time_Start { get; set; }
        public string time_End { get; set; }
        public string time { get; set; }
        public int seq { get; set; }
    }

    public class AppointmentItemDetailModel
    {
        public Guid? AppointmentItemDetail_Index { get; set; }

        public Guid? AppointmentItem_Index { get; set; }

        public Guid Appointment_Index { get; set; }

        public string Appointment_Id { get; set; }

        public Guid? VehicleType_Index { get; set; }

        public string VehicleType_Id { get; set; }

        public string VehicleType_Name { get; set; }

        public string Vehicle_No { get; set; }

        public Guid? Driver_Index { get; set; }

        public string Driver_Id { get; set; }

        public string Driver_Name { get; set; }

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

    public class AppointmentQoutaModel
    {
        public Guid WareHouse_Index { get; set; }

        public string WareHouse_Id { get; set; }

        public string WareHouse_Name { get; set; }

        public DateTime Appointment_Date { get; set; }

        public List<AppointmentQoutaDockModel> Items { get; set; } = new List<AppointmentQoutaDockModel>();
    }

    public class AppointmentQoutaDockModel
    { 
        public Guid Dock_Index { get; set; }

        public string Dock_Id { get; set; }

        public string Dock_Name { get; set; }

        public int Interval { get; set; }

        public string Time { get; set; }

        public string Time_End { get; set; }

        public List<AppointmentQoutaIntervalModel> Times { get; set; } = new List<AppointmentQoutaIntervalModel>();
    }

    public class AppointmentQoutaIntervalModel
    {
        public Guid DockQoutaInterval_Index { get; set; }

        public string Time_Start { get; set; }

        public string Time_End { get; set; }

        public string Time { get; set; }

        public int Seq { get; set; }

        public bool IsEnable { get; set; }
    }

    public class AppointmentViewModel : Pagination
    {
        public List<AppointmentModel> AppointmentModels { get; set; } = new List<AppointmentModel>();
    }

    public class AppointmentSearchModel : Pagination
    {
        public AppointmentSearchModel()
        {
            status = new List<statusViewModel>();
            status_cancle = new List<statusViewModel>();
        }
        public Guid? Appointment_Index { get; set; }

        public string vehicleType_Index { get; set; }

        public string Appointment_Id { get; set; }

        public string DatePeriod { get; set; }

        public int? seq { get; set; }

        public Guid? DocumentType_Index { get; set; }

        public string date { get; set; }

        public string date_To { get; set; }

        public DateTime? Appointment_Date { get; set; }

        public DateTime? Appointment_Date_To { get; set; }

        public TimeSpan? Appointment_Time { get; set; }

        public Guid? WareHouse_Index { get; set; }

        public Guid? Dock_Index { get; set; }

        public string Dock_name { get; set; }

        public int? IsActive { get; set; }

        public bool IsRemove { get; set; }

        public string Create_By { get; set; }
        public string Vehicle_No { get; set; }
        public string Document_no { get; set; }
        public string time { get; set; }
        public string planGoodsReceive_Index { get; set; }
        public string ref_Document_No { get; set; }

        public DateTime? Create_Date { get; set; }

        public DateTime? Create_Date_To { get; set; }

        public List<statusViewModel> status { get; set; }
        public List<statusViewModel> status_cancle { get; set; }


    }

    public class ConfirmQ_Model
    {
     
        public Guid? Appointment_Index { get; set; }

        public string vehicleType_Index { get; set; }

        public string Appointment_Id { get; set; }

        public string DatePeriod { get; set; }

        public int? seq { get; set; }

        public Guid? dockQoutaInterval_Index { get; set; }

        public Guid? DocumentType_Index { get; set; }

        public string date { get; set; }

        public string date_To { get; set; }

        public DateTime? Appointment_Date { get; set; }

        public DateTime? Appointment_Date_To { get; set; }

        //public TimeSpan? Appointment_Time { get; set; }

        public Guid? WareHouse_Index { get; set; }

        public Guid? Dock_Index { get; set; }

        public string Dock_name { get; set; }

        public int? IsActive { get; set; }

        public bool IsRemove { get; set; }

        public string Create_By { get; set; }
        public string Vehicle_No { get; set; }
        public string Document_no { get; set; }
        public string time { get; set; }
        public string planGoodsReceive_Index { get; set; }
        public string ref_Document_No { get; set; }

        public DateTime? Create_Date { get; set; }

        public DateTime? Create_Date_To { get; set; }


    }

    public class Model_Q
    {
    
        public Guid? Appointment_Index { get; set; }

        public string vehicleType_Index { get; set; }

        public string Appointment_Id { get; set; }

        public string DatePeriod { get; set; }

        public int? seq { get; set; }

        public Guid? DocumentType_Index { get; set; }

        public string date { get; set; }

        public string date_To { get; set; }

        public DateTime? Appointment_Date { get; set; }

        public DateTime? Appointment_Date_To { get; set; }
        public Guid? DockQoutaInterval_Index { get; set; }


        public string Appointment_Time { get; set; }

        public Guid? WareHouse_Index { get; set; }

        public Guid? Dock_Index { get; set; }

        public string Dock_name { get; set; }

        public int? IsActive { get; set; }

        public bool IsRemove { get; set; }

        public string Create_By { get; set; }
        public string Vehicle_No { get; set; }
        public string Document_no { get; set; }
        public string time { get; set; }
        public string planGoodsReceive_Index { get; set; }
        public string ref_Document_No { get; set; }

        public DateTime? Create_Date { get; set; }

        public DateTime? Create_Date_To { get; set; }

    }



    public class AppointmentItemSearchModel : Pagination
    {
        public Guid? AppointmentItem_Index { get; set; }

        public Guid? Appointment_Index { get; set; }

        public Guid? DockQoutaInterval_Index { get; set; }

        public Guid? DocumentType_Index { get; set; }

        public Guid? Group_Index { get; set; }

        public bool IsRemove { get; set; }

        public int IsActive { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public DateTime? Create_Date_To { get; set; }
    }

    public class AppointmentItemDetailSearchModel : Pagination
    {
        public Guid? AppointmentItemDetail_Index { get; set; }

        public Guid? AppointmentItem_Index { get; set; }

        public Guid? Vehi_Index { get; set; }

        public bool IsRemove { get; set; }

        public int IsActive { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public DateTime? Create_Date_To { get; set; }
    }

    public class AppointmentItemViewModel
    {
        public int Seq { get; set; }

        public Guid Appointment_Index { get; set; }

        public string Appointment_Id { get; set; }

        public Guid AppointmentItem_Index { get; set; }

        public string AppointmentItem_Id { get; set; }

        public Guid DocumentType_Index { get; set; }

        public string DocumentType_Id { get; set; }

        public string DocumentType_Name { get; set; }

        public DateTime Appointment_Date { get; set; }

        public string Appointment_Time { get; set; }

        public Guid DockQoutaInterval_Index { get; set; }

        public Guid WareHouse_Index { get; set; }

        public string WareHouse_Id { get; set; }

        public string WareHouse_Name { get; set; }

        public Guid Dock_Index { get; set; }

        public string Dock_Id { get; set; }

        public string Dock_Name { get; set; }

        public Guid? Owner_Index { get; set; }

        public string Owner_Id { get; set; }

        public string Owner_Name { get; set; }

        public string Ref_Document_No { get; set; }

        public DateTime? Ref_Document_Date { get; set; }

        public Guid? VehicleType_Index { get; set; }

        public string VehicleType_Id { get; set; }

        public string VehicleType_Name { get; set; }

        public string Vehicle_No { get; set; }

        public Guid? Driver_Index { get; set; }

        public string Driver_Id { get; set; }

        public string Driver_Name { get; set; }

        public string ContactPerson_Name { get; set; }

        public string ContactPerson_EMail { get; set; }

        public string ContactPerson_Tel { get; set; }

        public string Remark { get; set; }

        public Guid? CheckIn_Index { get; set; }

        public DateTime? CheckIn_Date { get; set; }

        public TimeSpan? CheckIn_Time { get; set; }

        public string CheckIn_Remark { get; set; }

        public string CheckIn_By { get; set; }

        public string CheckIn_Status { get; set; }

        public Guid? CheckOut_Index { get; set; }

        public DateTime? CheckOut_Date { get; set; }

        public TimeSpan? CheckOut_Time { get; set; }

        public string CheckOut_Remark { get; set; }

        public string CheckOut_By { get; set; }

        public string CheckOut_Status { get; set; }

        public string Activity_Desc { get; set; }

        public int Status { get; set; }

        public string Status_Desc { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }

        public string Cancel_By { get; set; }

        public DateTime? Cancel_Date { get; set; }
    }

    public class BlockModel
    {
 
        public Guid? Appointment_Index { get; set; }

        public string vehicleType_Index { get; set; }

        public string Appointment_Id { get; set; }

        public string DatePeriod { get; set; }

        public int? seq { get; set; }

        public Guid? DocumentType_Index { get; set; }

        public string date { get; set; }

        public string date_To { get; set; }

        public DateTime? Appointment_Date { get; set; }

        public DateTime? Appointment_Date_To { get; set; }

        public string Appointment_Time { get; set; }

        public Guid? WareHouse_Index { get; set; }

        public Guid? Dock_Index { get; set; }

        public string Dock_name { get; set; }

        public int? IsActive { get; set; }

        public bool IsRemove { get; set; }

        public string Create_By { get; set; }
        public string Vehicle_No { get; set; }
        public string Document_no { get; set; }
        public string time { get; set; }
        public string planGoodsReceive_Index { get; set; }
        public string ref_Document_No { get; set; }

        public DateTime? Create_Date { get; set; }

        public DateTime? Create_Date_To { get; set; }
        


    }

    public class AppointmentDockModel
    {
        public Guid Dock_Index { get; set; }

        public string Dock_Id { get; set; }

        public string Dock_Name { get; set; }
    }

    public class AppointmentWithItemModel
    {
        public int Seq { get; set; }

        public Guid Appointment_Index { get; set; }

        public string Appointment_Id { get; set; }

        public Guid AppointmentItem_Index { get; set; }

        public string AppointmentItem_Id { get; set; }

        public Guid DocumentType_Index { get; set; }

        public string DocumentType_Id { get; set; }

        public string DocumentType_Name { get; set; }

        public DateTime Appointment_Date { get; set; }

        public string Appointment_Time { get; set; }

        public Guid DockQoutaInterval_Index { get; set; }

        public Guid WareHouse_Index { get; set; }

        public string WareHouse_Id { get; set; }

        public string WareHouse_Name { get; set; }

        public Guid Dock_Index { get; set; }

        public string Dock_Id { get; set; }

        public string Dock_Name { get; set; }

        public Guid? Owner_Index { get; set; }

        public string Owner_Id { get; set; }

        public string Owner_Name { get; set; }

        public string Ref_Document_No { get; set; }

        public DateTime? Ref_Document_Date { get; set; }

        public Guid? VehicleType_Index { get; set; }

        public string VehicleType_Id { get; set; }

        public string VehicleType_Name { get; set; }

        public string Vehicle_No { get; set; }

        public Guid? Driver_Index { get; set; }

        public string Driver_Id { get; set; }
        public string Gate_Name { get; set; }

        public string Driver_Name { get; set; }

        public string ContactPerson_Name { get; set; }

        public string ContactPerson_EMail { get; set; }

        public string ContactPerson_Tel { get; set; }

        public string Remark { get; set; }

        public Guid? CheckIn_Index { get; set; }

        public DateTime? GateCheckIn_Date { get; set; }
        public DateTime? CheckIn_Date { get; set; }

        public TimeSpan? CheckIn_Time { get; set; }

        public string CheckIn_Remark { get; set; }

        public string GateCheckIn_By { get; set; }
        public string CheckIn_By { get; set; }

        public string CheckIn_Status { get; set; }
        public string GateCheckIn_Status { get; set; }

        public Guid? CheckOut_Index { get; set; }

        public DateTime? GateCheckOut_Date { get; set; }
        public DateTime? CheckOut_Date { get; set; }

        public TimeSpan? CheckOut_Time { get; set; }

        public string CheckOut_Remark { get; set; }

        public string GateCheckOut_By { get; set; }
        public string CheckOut_By { get; set; }

        public string CheckOut_Status { get; set; }
        public string GateCheckOut_Status { get; set; }

        public string Activity_Desc { get; set; }

        public int Status { get; set; }
        public int? Document_status { get; set; }

        public string Status_Desc { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public string Update_By { get; set; }

        public DateTime? Update_Date { get; set; }

        public string Cancel_By { get; set; }

        public DateTime? Cancel_Date { get; set; }

        public string license { get; set; }
        public string runing_Q { get; set; }

        public List<AppointmentItemModel> Items { get; set; } = new List<AppointmentItemModel>();
    }

    public class SortModel
    {
        public string ColId { get; set; }
        public string Sort { get; set; }

        public string PairAsSqlExpression
        {
            get
            {
                return $"{ColId} {Sort}";
            }
        }
    }
}
