using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

using Business.Commons;

namespace Business.Models
{
    public static class DockType
    {
        public static DockTypeModel GetDockTypeModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                DockTypeModel model = JsonConvert.DeserializeObject<DockTypeModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to DockTypeModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.DockType_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key DockTypeModel");
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DockTypeSearchModel GetDockTypeSearchModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                DockTypeSearchModel model = JsonConvert.DeserializeObject<DockTypeSearchModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to DockTypeSearchModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.DockType_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key DockTypeSearchModel");
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

    public class DockTypeModel
    {
        [Key]
        public Guid? DockType_Index { get; set; }

        public string DockType_Id { get; set; }

        public string DockType_Name { get; set; }

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

    public class DockTypeViewModel
    {
        public List<DockTypeModel> DockTypeModels { get; set; }

        public Pagination Pagination { get; set; }
    }

    public class DockTypeSearchModel : Pagination
    {
        [Key]
        public Guid? DockType_Index { get; set; }

        public string DockType_Id { get; set; }

        public string DockType_Name { get; set; }

        public int? IsActive { get; set; }

        public bool IsRemove { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public DateTime? Create_Date_To { get; set; }
    }

}
