using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

using Business.Commons;

namespace Business.Models
{
    public static class Gate
    {
        public static GateModel GetGateModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                GateModel model = JsonConvert.DeserializeObject<GateModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to GateModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.Gate_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key GateModel");
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static GateSearchModel GetGateSearchModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                GateSearchModel model = JsonConvert.DeserializeObject<GateSearchModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to GateSearchModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.Gate_Index.HasValue)
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

    public class GateModel
    {
        [Key]
        public Guid? Gate_Index { get; set; }

        public string Gate_Id { get; set; }

        public string Gate_Name { get; set; }

        public Guid? GateType_Index { get; set; }

        public string GateType_Id { get; set; }

        public string GateType_Name { get; set; }

        public Guid? Facility_Index { get; set; }

        public string Facility_Id { get; set; }

        public string Facility_Name { get; set; }

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

    public class GateViewModel
    {
        public List<GateModel> GateModels { get; set; }

        public Pagination Pagination { get; set; }
    }

    public class GateSearchModel : Pagination
    {
        [Key]
        public Guid? Gate_Index { get; set; }

        public Guid? GateType_Index { get; set; }

        public Guid? Facility_Index { get; set; }

        public string Gate_Id { get; set; }

        public string Gate_Name { get; set; }

        public int? IsActive { get; set; }

        public bool IsRemove { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public DateTime? Create_Date_To { get; set; }
    }

}
