using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

using Business.Commons;

namespace Business.Models
{
    public static class GateType
    {
        public static GateTypeModel GetGateTypeModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                GateTypeModel model = JsonConvert.DeserializeObject<GateTypeModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to GateTypeModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.GateType_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key GateTypeModel");
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static GateTypeSearchModel GetGateTypeSearchModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                GateTypeSearchModel model = JsonConvert.DeserializeObject<GateTypeSearchModel>(jsonData);

                if (model == null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to GateSearchModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.GateType_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key GateSearchModel");
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

    public class GateTypeModel
    {
        [Key]
        public Guid? GateType_Index { get; set; }

        public string GateType_Id { get; set; }

        public string GateType_Name { get; set; }

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

    public class GateTypeViewModel
    {
        public List<GateTypeModel> GateTypeModels { get; set; }

        public Pagination Pagination { get; set; }
    }

    public class GateTypeSearchModel : Pagination
    {
        [Key]
        public Guid? GateType_Index { get; set; }

        public string GateType_Id { get; set; }

        public string GateType_Name { get; set; }

        public int? IsActive { get; set; }

        public bool IsRemove { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public DateTime? Create_Date_To { get; set; }
    }

}
