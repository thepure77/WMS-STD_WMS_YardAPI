using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

using Business.Commons;

namespace Business.Models
{
    public static class DockZone
    {
        public static DockZoneModel GetDockZoneModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                DockZoneModel model = JsonConvert.DeserializeObject<DockZoneModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to DockZoneModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.DockZone_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key DockZoneModel");
                    }
                }

                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DockZoneSearchModel GetDockZoneSearchModel(string jsonData, bool requiredPrimaryKey = false)
        {
            if (string.IsNullOrEmpty((jsonData ?? string.Empty).Trim()))
            {
                throw new Exception("Invalid JSon : Null");
            }

            try
            {
                DockZoneSearchModel model = JsonConvert.DeserializeObject<DockZoneSearchModel>(jsonData);

                if (model is null)
                {
                    throw new Exception("Invalid JSon : Cannot convert to DockZoneSearchModel");
                }

                if (requiredPrimaryKey)
                {
                    if (!model.DockZone_Index.HasValue)
                    {
                        throw new Exception("Invalid JSon : Required Primary Key DockZoneSearchModel");
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

    public class DockZoneModel
    {
        [Key]
        public Guid? DockZone_Index { get; set; }

        public string DockZone_Id { get; set; }

        public string DockZone_Name { get; set; }

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

    public class DockZoneViewModel
    {
        public List<DockZoneModel> DockZoneModels { get; set; }

        public Pagination Pagination { get; set; }
    }

    public class DockZoneSearchModel : Pagination
    {
        [Key]
        public Guid? DockZone_Index { get; set; }

        public string DockZone_Id { get; set; }

        public string DockZone_Name { get; set; }

        public Guid? Facility_Index { get; set; }

        public int? IsActive { get; set; }

        public bool IsRemove { get; set; }

        public string Create_By { get; set; }

        public DateTime? Create_Date { get; set; }

        public DateTime? Create_Date_To { get; set; }
    }

}
