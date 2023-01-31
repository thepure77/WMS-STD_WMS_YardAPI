using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

using Business.Commons;

namespace Business.Models
{
    public static class FacilityType
    {
        public static FacilityTypeModel GetFacilityTypeModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                FacilityTypeModel model = JsonConvert.DeserializeObject<FacilityTypeModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to FacilityTypeModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.FacilityType_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key FacilityTypeModel");
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static FacilityTypeSearchModel GetFacilityTypeSearchModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                FacilityTypeSearchModel model = JsonConvert.DeserializeObject<FacilityTypeSearchModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to FacilityTypeSearchModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.FacilityType_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key FacilityTypeSearchModel");
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

    public class FacilityTypeModel
    {
        [Key]
        public Guid? FacilityType_Index { get; set; }

        public string FacilityType_Id { get; set; }

        public string FacilityType_Name { get; set; }

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

    public class FacilityTypeViewModel
    {
        public List<FacilityTypeModel> FacilityTypeModels { get; set; }

        public Pagination Pagination { get; set; }
    }

    public class FacilityTypeSearchModel : Pagination
    {
        [Key]
        public Guid? FacilityType_Index { get; set; }

        public string FacilityType_Id { get; set; }

        public string FacilityType_Name { get; set; }

        public int? IsActive { get; set; }

        public bool IsRemove { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public DateTime? Create_Date_To { get; set; }
    }

}
