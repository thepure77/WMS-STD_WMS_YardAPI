using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

using Business.Commons;

namespace Business.Models
{
    public static class YardType
    {
        public static YardTypeModel GetYardTypeModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                YardTypeModel model = JsonConvert.DeserializeObject<YardTypeModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to YardTypeModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.YardType_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key YardTypeModel");
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static YardTypeSearchModel GetYardTypeSearchModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                YardTypeSearchModel model = JsonConvert.DeserializeObject<YardTypeSearchModel>(jsonData);

                if (model == null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to YardSearchModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.YardType_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key YardSearchModel");
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

    public class YardTypeModel
    {
        [Key]
        public Guid? YardType_Index { get; set; }

        public string YardType_Id { get; set; }

        public string YardType_Name { get; set; }

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

    public class YardTypeViewModel
    {
        public List<YardTypeModel> YardTypeModels { get; set; }

        public Pagination Pagination { get; set; }
    }

    public class YardTypeSearchModel : Pagination
    {
        [Key]
        public Guid? YardType_Index { get; set; }

        public string YardType_Id { get; set; }

        public string YardType_Name { get; set; }

        public int? IsActive { get; set; }

        public bool IsRemove { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public DateTime? Create_Date_To { get; set; }
    }

}
