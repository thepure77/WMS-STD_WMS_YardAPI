using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

using Business.Commons;

namespace Business.Models
{
    public static class DockLocation
    {
        public static DockLocationModel GetDockLocationModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                DockLocationModel model = JsonConvert.DeserializeObject<DockLocationModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to DockLocationModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.DockLocation_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key DockLocationModel");
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DockLocationSearchModel GetDockLocationSearchModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                DockLocationSearchModel model = JsonConvert.DeserializeObject<DockLocationSearchModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to DockLocationSearchModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.DockLocation_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key DockLocationSearchModel");
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

    public class DockLocationModel
    {
        [Key]
        public Guid? DockLocation_Index { get; set; }

        public Guid? Dock_Index { get; set; }

        public string Dock_Id { get; set; }

        public string Dock_Name { get; set; }

        public Guid? Location_Index { get; set; }

        public string Location_Id { get; set; }

        public string Location_Name { get; set; }

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

    public class DockLocationViewModel
    {
        public List<DockLocationModel> DockLocationModels { get; set; }

        public Pagination Pagination { get; set; }
    }

    public class DockLocationSearchModel : Pagination
    {
        [Key]
        public Guid? DockLocation_Index { get; set; }

        public Guid? Dock_Index { get; set; }

        public Guid? Location_Index { get; set; }

        public int? IsActive { get; set; }

        public bool IsRemove { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public DateTime? Create_Date_To { get; set; }
    }

}
