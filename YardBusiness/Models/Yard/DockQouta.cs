using System;
using System.Collections.Generic;
using Newtonsoft.Json;

using Business.Commons;

namespace Business.Models
{
    public static class DockQouta
    {
        public static DockQoutaModel GetDockQoutaModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                DockQoutaModel model = JsonConvert.DeserializeObject<DockQoutaModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to DockQoutaModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.DockQouta_Index.HasValue)
                    { 
                        throw new Exception("Invalid JSon : Required Primary Key DockQoutaModel");
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DockQoutaSearchModel GetDockQoutaSearchModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                DockQoutaSearchModel model = JsonConvert.DeserializeObject<DockQoutaSearchModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to DockQoutaSearchModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.DockQouta_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key DockQoutaSearchModel");
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

    public class DockQoutaModel
    {
        public int Seq { get; set; }

        public Guid? DockQouta_Index { get; set; }

        public string DockQouta_Id { get; set; }

        public Guid WareHouse_Index { get; set; }

        public string WareHouse_Id { get; set; }

        public string WareHouse_Name { get; set; }

        public Guid? First_Dock_Index { get; set; }

        public string First_Dock_Id { get; set; }

        public string First_Dock_Name { get; set; }

        public DateTime? DockQouta_Date { get; set; }

        public DateTime? DockQouta_Date_End { get; set; }

        public int Interval { get; set; }

        public string checkIn_Limit_Before { get; set; }

        public string checkIn_Limit_After { get; set; }


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

        public List<DockQoutaDockModel> Docks { get; set; } = new List<DockQoutaDockModel>();

        public List<DockQoutaIntervalTime> DockQoutaIntervalTime { get; set; } = new List<DockQoutaIntervalTime>();

        public List<DockQoutaIntervalTime> DockQoutaIntervalBreakTime { get; set; } = new List<DockQoutaIntervalTime>();
    }

    public class DockQoutaIntervalTime
    {
        public string Time_Start { get; set; }

        public string Time_End { get; set; }

        public string Time { get; set; }

        public int Time_Id { get; set; }
    }

    public class DockQoutaDockModel
    {
        public Guid Dock_Index { get; set; }

        public string Dock_Id { get; set; }

        public string Dock_Name { get; set; }
        public string Name { get; set; }
    }

    public class DockQoutaViewModel : Pagination
    {

        public List<DockQoutaModel> DockQoutaModels { get; set; } = new List<DockQoutaModel>();
    }

    public class DockQoutaSearchModel : Pagination
    {
        public Guid? DockQouta_Index { get; set; }

        public string DockQouta_Id { get; set; }

        public Guid? WareHouse_Index { get; set; }

        public int Interval { get; set; }

        public int? IsActive { get; set; }

        public bool IsRemove { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public DateTime? Create_Date_To { get; set; }

        public List<statusViewModelDock> status { get; set; }

        public List<Guid> ListDockIndex { get; set; } = new List<Guid>();

    }
    public class statusViewModelDock
    {
        public int value { get; set; }
        public string display { get; set; }
        public int seq { get; set; }
    }
}
