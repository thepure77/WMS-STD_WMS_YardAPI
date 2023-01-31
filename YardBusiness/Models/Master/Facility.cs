using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

using Business.Commons;

namespace Business.Models
{
    public static class Facility
    {
        public static FacilityModel GetFacilityModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                FacilityModel model = JsonConvert.DeserializeObject<FacilityModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to FacilityModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.Facility_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key FacilityModel");
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static FacilitySearchModel GetFacilitySearchModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                FacilitySearchModel model = JsonConvert.DeserializeObject<FacilitySearchModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to FacilitySearchModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.Facility_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key FacilitySearchModel");
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

    public class FacilityModel
    {
        [Key]
        public Guid? Facility_Index { get; set; }

        public string Facility_Id { get; set; }

        public string Facility_Name { get; set; }

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

    public class FacilityViewModel
    {
        public List<FacilityModel> FacilityModels { get; set; }

        public Pagination Pagination { get; set; }
    }

    public class FacilitySearchModel : Pagination
    {
        [Key]
        public Guid? Facility_Index { get; set; }

        public string Facility_Id { get; set; }

        public string Facility_Name { get; set; }

        public Guid? FacilityType_Index { get; set; }

        public int? IsActive { get; set; }

        public bool IsRemove { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public DateTime? Create_Date_To { get; set; }
    }

}
