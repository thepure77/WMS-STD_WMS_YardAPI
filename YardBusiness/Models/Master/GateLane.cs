using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

using Business.Commons;

namespace Business.Models
{
    public static class GateLane
    {
        public static GateLaneModel GetGateLaneModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                GateLaneModel model = JsonConvert.DeserializeObject<GateLaneModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to GateLaneModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.GateLane_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key GateLaneModel");
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static GateLaneSearchModel GetGateLaneSearchModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                GateLaneSearchModel model = JsonConvert.DeserializeObject<GateLaneSearchModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to GateLaneSearchModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.GateLane_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key GateLaneSearchModel");
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

    public class GateLaneModel
    {
        [Key]
        public Guid? GateLane_Index { get; set; }

        public string GateLane_Id { get; set; }

        public string GateLane_Name { get; set; }

        public Guid? Gate_Index { get; set; }

        public string Gate_Id { get; set; }

        public string Gate_Name { get; set; }

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

    public class GateLaneViewModel
    {
        public List<GateLaneModel> GateLaneModels { get; set; }

        public Pagination Pagination { get; set; }
    }

    public class GateLaneSearchModel : Pagination
    {
        [Key]
        public Guid? GateLane_Index { get; set; }

        public Guid? Gate_Index { get; set; }

        public string GateLane_Id { get; set; }

        public string GateLane_Name { get; set; }

        public int? IsActive { get; set; }

        public bool IsRemove { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public DateTime? Create_Date_To { get; set; }
    }

}
