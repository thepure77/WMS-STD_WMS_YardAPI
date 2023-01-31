using System;
using System.Collections.Generic;
using Newtonsoft.Json;

using Business.Commons;

namespace Business.Models
{
    public static class WareHouseQouta
    {
        public static WareHouseQoutaModel GetWareHouseQoutaModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                WareHouseQoutaModel model = JsonConvert.DeserializeObject<WareHouseQoutaModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to WareHouseQoutaModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.WareHouseQouta_Index.HasValue)
                    { 
                        throw new Exception("Invalid JSon : Required Primary Key WareHouseQoutaModel");
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static WareHouseQoutaSearchModel GetWareHouseQoutaSearchModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                WareHouseQoutaSearchModel model = JsonConvert.DeserializeObject<WareHouseQoutaSearchModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to WareHouseQoutaSearchModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.WareHouseQouta_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key WareHouseQoutaSearchModel");
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

    public class WareHouseQoutaModel
    {
        public Guid? WareHouseQouta_Index { get; set; }

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

    public class WareHouseQoutaWareHouseModel
    {
        public Guid WareHouse_Index { get; set; }

        public string WareHouse_Id { get; set; }

        public string WareHouse_Name { get; set; }
    }

    public class WareHouseQoutaViewModel : Pagination
    {
        public List<WareHouseQoutaModel> WareHouseQoutaModels { get; set; }
    }

    public class WareHouseQoutaSearchModel : Pagination
    {
        public Guid? WareHouseQouta_Index { get; set; }

        public string WareHouseQouta_Id { get; set; }

        public Guid? WareHouse_Index { get; set; }

        public int? IsActive { get; set; }

        public bool IsRemove { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public DateTime? Create_Date_To { get; set; }

        public List<statusViewModel> status { get; set; }
    }

    public class statusViewModel
    {
        public int value { get; set; }
        public string display { get; set; }
        public int seq { get; set; }
    }

}
