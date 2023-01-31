using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

using Business.Commons;

namespace Business.Models
{
    public static class Yard
    {
        public static YardModel GetYardModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                YardModel model = JsonConvert.DeserializeObject<YardModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to YardModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.Yard_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key YardModel");
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static YardSearchModel GetYardSearchModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                YardSearchModel model = JsonConvert.DeserializeObject<YardSearchModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to YardSearchModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.Yard_Index.HasValue)
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

    public class YardModel
    {
        [Key]
        public Guid? Yard_Index { get; set; }

        public string Yard_Id { get; set; }

        public string Yard_Name { get; set; }

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

    public class YardViewModel
    {
        public List<YardModel> YardModels { get; set; }

        public Pagination Pagination { get; set; }
    }

    public class YardSearchModel : Pagination
    {
        [Key]
        public Guid? Yard_Index { get; set; }

        public Guid? YardType_Index { get; set; }

        public string Yard_Id { get; set; }

        public string Yard_Name { get; set; }

        public int? IsActive { get; set; }

        public bool IsRemove { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public DateTime? Create_Date_To { get; set; }
    }

}
