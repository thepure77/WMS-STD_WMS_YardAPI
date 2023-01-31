using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

using Business.Commons;

namespace Business.Models
{
    public static class Dock
    {
        public static DockModel GetDockModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                DockModel model = JsonConvert.DeserializeObject<DockModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to DockModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.Dock_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key DockModel");
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DockSearchModel GetDockSearchModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                DockSearchModel model = JsonConvert.DeserializeObject<DockSearchModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to DockSearchModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.Dock_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key DockSearchModel");
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

    public class DockModel
    {
        [Key]
        public Guid? Dock_Index { get; set; }

        public string Dock_Id { get; set; }

        public string Dock_Name { get; set; }

        public Guid? DockType_Index { get; set; }

        public string DockType_Id { get; set; }

        public string DockType_Name { get; set; }

        public Guid? DockZone_Index { get; set; }

        public string DockZone_Id { get; set; }

        public string DockZone_Name { get; set; }

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

    public class DockViewModel
    {
        public List<DockModel> DockModels { get; set; }

        public Pagination Pagination { get; set; }
    }

    public class DockSearchModel : Pagination
    {
        [Key]
        public Guid? Dock_Index { get; set; }

        public string Dock_Id { get; set; }

        public string Dock_Name { get; set; }

        public Guid? DockType_Index { get; set; }

        public Guid? DockZone_Index { get; set; }

        public int? IsActive { get; set; }

        public bool IsRemove { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public DateTime? Create_Date_To { get; set; }
    }

}
