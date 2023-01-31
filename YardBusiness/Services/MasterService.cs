using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

using DataAccess;
using DataAccess.Models.Master.Table;
using DataAccess.Models.Master.View;

using Business.Commons;
using Business.Models;

namespace Business.Services
{
    public class MasterService
    {
        private readonly MasterDbContext db;

        public MasterService()
        {
            db = new MasterDbContext();
        }

        public MasterService(MasterDbContext db)
        {
            this.db = db;
        }

        #region + List +
        public DockViewModel ListDock(string jsonData)
        {
            try
            {
                DockSearchModel model = Dock.GetDockSearchModel(jsonData);

                List<View_Dock> Docks = db.View_Dock.Where(
                        s => ModelCondition.NullableEqual(s.IsActive, model.IsActive) &&
                             ModelCondition.StringLike(s.Dock_Id, model.Dock_Id) &&
                             ModelCondition.StringLike(s.Dock_Name, model.Dock_Name) &&
                             ModelCondition.NullableEqual(s.DockType_Index, model.DockType_Index) &&
                             ModelCondition.NullableEqual(s.DockZone_Index, model.DockZone_Index) &&
                             ModelCondition.StringLike(s.Create_By, model.Create_By) &&
                             ModelCondition.DateTimeBetween(s.Create_Date, model.Create_Date, model.Create_Date_To, true)
                    ).ToList();

                DockViewModel result = new DockViewModel()
                {
                    DockModels = JsonConvert.DeserializeObject<List<DockModel>>(JsonConvert.SerializeObject(Docks)),
                    Pagination = new Pagination() { TotalRow = Docks.Count, CurrentPage = model.CurrentPage, PerPage = model.PerPage }
                };

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DockTypeViewModel ListDockType(string jsonData)
        {
            try
            {
                DockTypeSearchModel model = DockType.GetDockTypeSearchModel(jsonData);

                List<Ms_DockType> DockTypes = db.Ms_DockType.Where(
                        s => ModelCondition.NullableEqual(s.IsActive, model.IsActive) &&
                             ModelCondition.StringLike(s.DockType_Id, model.DockType_Id) &&
                             ModelCondition.StringLike(s.DockType_Name, model.DockType_Name) &&
                             ModelCondition.StringLike(s.Create_By, model.Create_By) &&
                             ModelCondition.DateTimeBetween(s.Create_Date, model.Create_Date, model.Create_Date_To, true)
                    ).ToList();

                DockTypeViewModel result = new DockTypeViewModel()
                {
                    DockTypeModels = JsonConvert.DeserializeObject<List<DockTypeModel>>(JsonConvert.SerializeObject(DockTypes)),
                    Pagination = new Pagination() { TotalRow = DockTypes.Count, CurrentPage = model.CurrentPage, PerPage = model.PerPage }
                };

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DockZoneViewModel ListDockZone(string jsonData)
        {
            try
            {
                DockZoneSearchModel model = DockZone.GetDockZoneSearchModel(jsonData);

                List<Ms_DockZone> DockZones = db.Ms_DockZone.Where(
                        s => ModelCondition.NullableEqual(s.IsActive, model.IsActive) &&
                             ModelCondition.StringLike(s.DockZone_Id, model.DockZone_Id) &&
                             ModelCondition.StringLike(s.DockZone_Name, model.DockZone_Name) &&
                             ModelCondition.NullableEqual(s.Facility_Index, model.Facility_Index) &&
                             ModelCondition.StringLike(s.Create_By, model.Create_By) &&
                             ModelCondition.DateTimeBetween(s.Create_Date, model.Create_Date, model.Create_Date_To, true)
                    ).ToList();

                DockZoneViewModel result = new DockZoneViewModel()
                {
                    DockZoneModels = JsonConvert.DeserializeObject<List<DockZoneModel>>(JsonConvert.SerializeObject(DockZones)),
                    Pagination = new Pagination() { TotalRow = DockZones.Count, CurrentPage = model.CurrentPage, PerPage = model.PerPage }
                };

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DockLocationViewModel ListDockLocation(string jsonData)
        {
            try
            {
                DockLocationSearchModel model = DockLocation.GetDockLocationSearchModel(jsonData);

                List<Ms_DockLocation> DockLocations = db.Ms_DockLocation.Where(
                        s => ModelCondition.NullableEqual(s.IsActive, model.IsActive) &&
                             ModelCondition.NullableEqual(s.Dock_Index, model.Dock_Index) &&
                             ModelCondition.NullableEqual(s.Location_Index, model.Location_Index) &&
                             ModelCondition.StringLike(s.Create_By, model.Create_By) &&
                             ModelCondition.DateTimeBetween(s.Create_Date, model.Create_Date, model.Create_Date_To, true)
                    ).ToList();

                DockLocationViewModel result = new DockLocationViewModel()
                {
                    DockLocationModels = JsonConvert.DeserializeObject<List<DockLocationModel>>(JsonConvert.SerializeObject(DockLocations)),
                    Pagination = new Pagination() { TotalRow = DockLocations.Count, CurrentPage = model.CurrentPage, PerPage = model.PerPage }
                };

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FacilityViewModel ListFacility(string jsonData)
        {
            try
            {
                FacilitySearchModel model = Facility.GetFacilitySearchModel(jsonData);

                List<View_Facility> Facilitys = db.View_Facility.Where(
                        s => ModelCondition.NullableEqual(s.IsActive, model.IsActive) &&
                             ModelCondition.StringLike(s.Facility_Id, model.Facility_Id) &&
                             ModelCondition.StringLike(s.Facility_Name, model.Facility_Name) &&
                             ModelCondition.NullableEqual(s.FacilityType_Index, model.FacilityType_Index) &&
                             ModelCondition.StringLike(s.Create_By, model.Create_By) &&
                             ModelCondition.DateTimeBetween(s.Create_Date, model.Create_Date, model.Create_Date_To, true)
                    ).ToList();

                FacilityViewModel result = new FacilityViewModel()
                {
                    FacilityModels = JsonConvert.DeserializeObject<List<FacilityModel>>(JsonConvert.SerializeObject(Facilitys)),
                    Pagination = new Pagination() { TotalRow = Facilitys.Count, CurrentPage = model.CurrentPage, PerPage = model.PerPage }
                };

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FacilityTypeViewModel ListFacilityType(string jsonData)
        {
            try
            {
                FacilityTypeSearchModel model = FacilityType.GetFacilityTypeSearchModel(jsonData);

                List<Ms_FacilityType> FacilityTypes = db.Ms_FacilityType.Where(
                        s => ModelCondition.NullableEqual(s.IsActive, model.IsActive) &&
                             ModelCondition.StringLike(s.FacilityType_Id, model.FacilityType_Id) &&
                             ModelCondition.StringLike(s.FacilityType_Name, model.FacilityType_Name) &&
                             ModelCondition.StringLike(s.Create_By, model.Create_By) &&
                             ModelCondition.DateTimeBetween(s.Create_Date, model.Create_Date, model.Create_Date_To, true)
                    ).ToList();

                FacilityTypeViewModel result = new FacilityTypeViewModel()
                {
                    FacilityTypeModels = JsonConvert.DeserializeObject<List<FacilityTypeModel>>(JsonConvert.SerializeObject(FacilityTypes)),
                    Pagination = new Pagination() { TotalRow = FacilityTypes.Count, CurrentPage = model.CurrentPage, PerPage = model.PerPage }
                };

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GateViewModel ListGate(string jsonData)
        {
            try
            {
                GateSearchModel model = Gate.GetGateSearchModel(jsonData);

                List<View_Gate> gates = db.View_Gate.Where(
                        s => ModelCondition.NullableEqual(s.IsActive, model.IsActive) &&
                             ModelCondition.StringLike(s.Gate_Id, model.Gate_Id) &&
                             ModelCondition.StringLike(s.Gate_Name, model.Gate_Name) &&
                             ModelCondition.NullableEqual(s.GateType_Index, model.GateType_Index) &&
                             ModelCondition.NullableEqual(s.Facility_Index, model.Facility_Index) &&
                             ModelCondition.StringLike(s.Create_By, model.Create_By) &&
                             ModelCondition.DateTimeBetween(s.Create_Date, model.Create_Date, model.Create_Date_To, true)
                    ).ToList();

                GateViewModel result = new GateViewModel() {
                    GateModels = JsonConvert.DeserializeObject<List<GateModel>>(JsonConvert.SerializeObject(gates)),
                    Pagination = new Pagination() { TotalRow = gates.Count, CurrentPage = model.CurrentPage, PerPage = model.PerPage }
                };

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GateTypeViewModel ListGateType(string jsonData)
        {
            try
            {
                GateTypeSearchModel model = GateType.GetGateTypeSearchModel(jsonData);

                List<Ms_GateType> gateTypes = db.Ms_GateType.Where(
                        s => ModelCondition.NullableEqual(s.IsActive, model.IsActive) &&
                             ModelCondition.StringLike(s.GateType_Id, model.GateType_Id) &&
                             ModelCondition.StringLike(s.GateType_Name, model.GateType_Name) &&
                             ModelCondition.StringLike(s.Create_By, model.Create_By) &&
                             ModelCondition.DateTimeBetween(s.Create_Date, model.Create_Date, model.Create_Date_To, true)
                    ).ToList();

                GateTypeViewModel result = new GateTypeViewModel()
                {
                    GateTypeModels = JsonConvert.DeserializeObject<List<GateTypeModel>>(JsonConvert.SerializeObject(gateTypes)),
                    Pagination = new Pagination() { TotalRow = gateTypes.Count, CurrentPage = model.CurrentPage, PerPage = model.PerPage }
                };

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GateLaneViewModel ListGateLane(string jsonData)
        {
            try
            {
                GateLaneSearchModel model = GateLane.GetGateLaneSearchModel(jsonData);

                List<View_GateLane> GateLanes = db.View_GateLane.Where(
                        s => ModelCondition.NullableEqual(s.IsActive, model.IsActive) &&
                             ModelCondition.StringLike(s.GateLane_Id, model.GateLane_Id) &&
                             ModelCondition.StringLike(s.GateLane_Name, model.GateLane_Name) &&
                             ModelCondition.NullableEqual(s.Gate_Index, model.Gate_Index) &&
                             ModelCondition.StringLike(s.Create_By, model.Create_By) &&
                             ModelCondition.DateTimeBetween(s.Create_Date, model.Create_Date, model.Create_Date_To, true)
                    ).ToList();

                GateLaneViewModel result = new GateLaneViewModel()
                {
                    GateLaneModels = JsonConvert.DeserializeObject<List<GateLaneModel>>(JsonConvert.SerializeObject(GateLanes)),
                    Pagination = new Pagination() { TotalRow = GateLanes.Count, CurrentPage = model.CurrentPage, PerPage = model.PerPage }
                };

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public YardViewModel ListYard(string jsonData)
        {
            try
            {
                YardSearchModel model = Yard.GetYardSearchModel(jsonData);

                List<View_Yard> Yards = db.View_Yard.Where(
                        s => ModelCondition.NullableEqual(s.IsActive, model.IsActive) &&
                             ModelCondition.StringLike(s.Yard_Id, model.Yard_Id) &&
                             ModelCondition.StringLike(s.Yard_Name, model.Yard_Name) &&
                             ModelCondition.NullableEqual(s.YardType_Index, model.YardType_Index) &&
                             ModelCondition.StringLike(s.Create_By, model.Create_By) &&
                             ModelCondition.DateTimeBetween(s.Create_Date, model.Create_Date, model.Create_Date_To, true)
                    ).ToList();

                YardViewModel result = new YardViewModel()
                {
                    YardModels = JsonConvert.DeserializeObject<List<YardModel>>(JsonConvert.SerializeObject(Yards)),
                    Pagination = new Pagination() { TotalRow = Yards.Count, CurrentPage = model.CurrentPage, PerPage = model.PerPage }
                };

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public YardTypeViewModel ListYardType(string jsonData)
        {
            try
            {
                YardTypeSearchModel model = YardType.GetYardTypeSearchModel(jsonData);

                List<Ms_YardType> YardTypes = db.Ms_YardType.Where(
                        s => ModelCondition.NullableEqual(s.IsActive, model.IsActive) &&
                             ModelCondition.StringLike(s.YardType_Id, model.YardType_Id) &&
                             ModelCondition.StringLike(s.YardType_Name, model.YardType_Name) &&
                             ModelCondition.StringLike(s.Create_By, model.Create_By) &&
                             ModelCondition.DateTimeBetween(s.Create_Date, model.Create_Date, model.Create_Date_To, true)
                    ).ToList();

                YardTypeViewModel result = new YardTypeViewModel()
                {
                    YardTypeModels = JsonConvert.DeserializeObject<List<YardTypeModel>>(JsonConvert.SerializeObject(YardTypes)),
                    Pagination = new Pagination() { TotalRow = YardTypes.Count, CurrentPage = model.CurrentPage, PerPage = model.PerPage }
                };

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region + Get +
        public DockModel GetDock(string jsonData)
        {
            try
            {
                DockSearchModel model = Dock.GetDockSearchModel(jsonData, true);
                View_Dock result = db.View_Dock.Find(model.Dock_Index);

                if (result is null)
                {
                    throw new Exception("Dock not found");
                }

                return JsonConvert.DeserializeObject<DockModel>(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DockTypeModel GetDockType(string jsonData)
        {
            try
            {
                DockTypeSearchModel model = DockType.GetDockTypeSearchModel(jsonData, true);
                Ms_DockType result = db.Ms_DockType.Find(model.DockType_Index);

                if (result is null)
                {
                    throw new Exception("DockType not found");
                }

                return JsonConvert.DeserializeObject<DockTypeModel>(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DockZoneModel GetDockZone(string jsonData)
        {
            try
            {
                DockZoneSearchModel model = DockZone.GetDockZoneSearchModel(jsonData, true);
                Ms_DockZone result = db.Ms_DockZone.Find(model.DockZone_Index);

                if (result is null)
                {
                    throw new Exception("DockZone not found");
                }

                return JsonConvert.DeserializeObject<DockZoneModel>(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DockLocationModel GetDockLocation(string jsonData)
        {
            try
            {
                DockLocationSearchModel model = DockLocation.GetDockLocationSearchModel(jsonData, true);
                Ms_DockLocation result = db.Ms_DockLocation.Find(model.DockLocation_Index);

                if (result is null)
                {
                    throw new Exception("DockLocation not found");
                }

                return JsonConvert.DeserializeObject<DockLocationModel>(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GateModel GetGate(string jsonData)
        {
            try
            {
                GateSearchModel model = Gate.GetGateSearchModel(jsonData, true);
                View_Gate result = db.View_Gate.Find(model.Gate_Index);

                if (result is null)
                {
                    throw new Exception("Gate not found");
                }

                return JsonConvert.DeserializeObject<GateModel>(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GateTypeModel GetGateType(string jsonData)
        {
            try
            {
                GateTypeSearchModel model = GateType.GetGateTypeSearchModel(jsonData, true);
                Ms_GateType result = db.Ms_GateType.Find(model.GateType_Index);

                if (result is null)
                {
                    throw new Exception("Gate not found");
                }

                return JsonConvert.DeserializeObject<GateTypeModel>(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GateLaneModel GetGateLane(string jsonData)
        {
            try
            {
                GateLaneSearchModel model = GateLane.GetGateLaneSearchModel(jsonData, true);
                View_GateLane result = db.View_GateLane.Find(model.GateLane_Index);

                if (result is null)
                {
                    throw new Exception("GateLane not found");
                }

                return JsonConvert.DeserializeObject<GateLaneModel>(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FacilityModel GetFacility(string jsonData)
        {
            try
            {
                FacilitySearchModel model = Facility.GetFacilitySearchModel(jsonData, true);
                View_Facility result = db.View_Facility.Find(model.Facility_Index);

                if (result is null)
                {
                    throw new Exception("Facility not found");
                }

                return JsonConvert.DeserializeObject<FacilityModel>(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FacilityTypeModel GetFacilityType(string jsonData)
        {
            try
            {
                FacilityTypeSearchModel model = FacilityType.GetFacilityTypeSearchModel(jsonData, true);
                Ms_FacilityType result = db.Ms_FacilityType.Find(model.FacilityType_Index);

                if (result is null)
                {
                    throw new Exception("Facility not found");
                }

                return JsonConvert.DeserializeObject<FacilityTypeModel>(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public YardModel GetYard(string jsonData)
        {
            try
            {
                YardSearchModel model = Yard.GetYardSearchModel(jsonData, true);
                View_Yard result = db.View_Yard.Find(model.Yard_Index);

                if (result is null)
                {
                    throw new Exception("Yard not found");
                }

                return JsonConvert.DeserializeObject<YardModel>(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public YardTypeModel GetYardType(string jsonData)
        {
            try
            {
                YardTypeSearchModel model = YardType.GetYardTypeSearchModel(jsonData, true);
                Ms_YardType result = db.Ms_YardType.Find(model.YardType_Index);

                if (result is null)
                {
                    throw new Exception("Yard not found");
                }

                return JsonConvert.DeserializeObject<YardTypeModel>(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region + Save +
        public Guid SaveDock(string jsonData)
        {
            try
            {
                DockModel model = Dock.GetDockModel(jsonData);
                Ms_Dock dock = db.Ms_Dock.Find(model.Dock_Index);

                Guid DockIndex;
                string userBy;
                DateTime userDate = DateTime.Now;

                if (dock is null)
                {
                    DockIndex = Guid.NewGuid();
                    userBy = model.Create_By;

                    Ms_Dock newDock = new Ms_Dock
                    {
                        Dock_Index = DockIndex,
                        Dock_Id = "Dock_Id".GenAutonumber(),
                        Dock_Name = model.Dock_Name,
                        DockType_Index = model.DockType_Index,
                        DockZone_Index = model.DockZone_Index,
                        IsActive = model.IsActive,
                        IsDelete = 0,
                        IsSystem = 0,
                        Status_Id = 0,
                        Create_By = userBy,
                        Create_Date = userDate
                    };

                    db.Ms_Dock.Add(newDock);
                }
                else
                {
                    DockIndex = dock.Dock_Index;
                    userBy = model.Update_By;

                    dock.Dock_Id = model.Dock_Id;
                    dock.Dock_Name = model.Dock_Name;
                    dock.DockType_Index = model.DockType_Index;
                    dock.DockZone_Index = model.DockZone_Index;
                    dock.IsActive = model.IsActive;
                    dock.Update_By = userBy;
                    dock.Update_Date = userDate;
                }

                var myTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    myTransaction.Commit();
                }
                catch (Exception saveEx)
                {
                    myTransaction.Rollback();
                    throw saveEx;
                }

                return DockIndex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Guid SaveDockType(string jsonData)
        {
            try
            {
                DockTypeModel model = DockType.GetDockTypeModel(jsonData);
                Ms_DockType dockType = db.Ms_DockType.Find(model.DockType_Index);

                Guid DockTypeIndex;
                string userBy;
                DateTime userDate = DateTime.Now;

                if (dockType is null)
                {
                    DockTypeIndex = Guid.NewGuid();
                    userBy = model.Create_By;

                    Ms_DockType newDockType = new Ms_DockType
                    {
                        DockType_Index = DockTypeIndex,
                        DockType_Id = "DockType_Id".GenAutonumber(),
                        DockType_Name = model.DockType_Name,
                        IsActive = model.IsActive,
                        IsDelete = 0,
                        IsSystem = 0,
                        Status_Id = 0,
                        Create_By = userBy,
                        Create_Date = userDate
                    };

                    db.Ms_DockType.Add(newDockType);
                }
                else
                {
                    DockTypeIndex = dockType.DockType_Index;
                    userBy = model.Update_By;

                    dockType.DockType_Id = model.DockType_Id;
                    dockType.DockType_Name = model.DockType_Name;
                    dockType.IsActive = model.IsActive;
                    dockType.Update_By = userBy;
                    dockType.Update_Date = userDate;
                }

                var myTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    myTransaction.Commit();
                }
                catch (Exception saveEx)
                {
                    myTransaction.Rollback();
                    throw saveEx;
                }

                return DockTypeIndex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Guid SaveDockZone(string jsonData)
        {
            try
            {
                DockZoneModel model = DockZone.GetDockZoneModel(jsonData);
                Ms_DockZone dockZone = db.Ms_DockZone.Find(model.DockZone_Index);

                Guid DockZoneIndex;
                string userBy;
                DateTime userDate = DateTime.Now;

                if (dockZone is null)
                {
                    DockZoneIndex = Guid.NewGuid();
                    userBy = model.Create_By;

                    Ms_DockZone newDockZone = new Ms_DockZone
                    {
                        DockZone_Index = DockZoneIndex,
                        DockZone_Id = "DockZone_Id".GenAutonumber(),
                        DockZone_Name = model.DockZone_Name,
                        Facility_Index = model.Facility_Index,
                        IsActive = model.IsActive,
                        IsDelete = 0,
                        IsSystem = 0,
                        Status_Id = 0,
                        Create_By = userBy,
                        Create_Date = userDate
                    };

                    db.Ms_DockZone.Add(newDockZone);
                }
                else
                {
                    DockZoneIndex = dockZone.DockZone_Index;
                    userBy = model.Update_By;

                    dockZone.DockZone_Id = model.DockZone_Id;
                    dockZone.DockZone_Name = model.DockZone_Name;
                    dockZone.IsActive = model.IsActive;
                    dockZone.Update_By = userBy;
                    dockZone.Update_Date = userDate;
                }

                var myTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    myTransaction.Commit();
                }
                catch (Exception saveEx)
                {
                    myTransaction.Rollback();
                    throw saveEx;
                }

                return DockZoneIndex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Guid SaveDockLocation(string jsonData)
        {
            try
            {
                DockLocationModel model = DockLocation.GetDockLocationModel(jsonData);
                Ms_DockLocation dockLocation = db.Ms_DockLocation.Find(model.DockLocation_Index);

                Guid DockLocationIndex;
                string userBy;
                DateTime userDate = DateTime.Now;

                if (dockLocation is null)
                {
                    DockLocationIndex = Guid.NewGuid();
                    userBy = model.Create_By;

                    Ms_DockLocation newDockLocation = new Ms_DockLocation
                    {
                        DockLocation_Index = DockLocationIndex,
                        Dock_Index = model.Dock_Index,
                        Location_Index = model.Location_Index,
                        IsActive = model.IsActive,
                        IsDelete = 0,
                        IsSystem = 0,
                        Status_Id = 0,
                        Create_By = userBy,
                        Create_Date = userDate
                    };

                    db.Ms_DockLocation.Add(newDockLocation);
                }
                else
                {
                    DockLocationIndex = dockLocation.DockLocation_Index;
                    userBy = model.Update_By;

                    dockLocation.Dock_Index = model.Dock_Index;
                    dockLocation.Location_Index = model.Location_Index;
                    dockLocation.IsActive = model.IsActive;
                    dockLocation.Update_By = userBy;
                    dockLocation.Update_Date = userDate;
                }

                var myTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    myTransaction.Commit();
                }
                catch (Exception saveEx)
                {
                    myTransaction.Rollback();
                    throw saveEx;
                }

                return DockLocationIndex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Guid SaveGate(string jsonData)
        {
            try
            {
                GateModel model = Gate.GetGateModel(jsonData);
                Ms_Gate gate = db.Ms_Gate.Find(model.Gate_Index);

                Guid gateIndex;
                string userBy;
                DateTime userDate = DateTime.Now;

                if (gate is null)
                {
                    gateIndex = Guid.NewGuid();
                    userBy = model.Create_By;

                    Ms_Gate newGate = new Ms_Gate
                    {
                        Gate_Index = gateIndex,
                        Gate_Id = "Gate_Id".GenAutonumber(),
                        Gate_Name = model.Gate_Name,
                        GateType_Index = model.GateType_Index,
                        Facility_Index = model.Facility_Index,
                        IsActive = model.IsActive,
                        IsDelete = 0,
                        IsSystem = 0,
                        Status_Id = 0,
                        Create_By = userBy,
                        Create_Date = userDate
                    };
  
                    db.Ms_Gate.Add(newGate);
                }
                else
                {
                    gateIndex = gate.Gate_Index;
                    userBy = model.Update_By;

                    gate.Gate_Id = model.Gate_Id;
                    gate.Gate_Name = model.Gate_Name;
                    gate.GateType_Index = model.GateType_Index;
                    gate.Facility_Index = model.Facility_Index;
                    gate.IsActive = model.IsActive;
                    gate.Update_By = userBy;
                    gate.Update_Date = userDate;
                }

                var myTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    myTransaction.Commit();
                }
                catch (Exception saveEx)
                {
                    myTransaction.Rollback();
                    throw saveEx;
                }

                return gateIndex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Guid SaveGateType(string jsonData)
        {
            try
            {
                GateTypeModel model = GateType.GetGateTypeModel(jsonData);
                Ms_GateType gateType = db.Ms_GateType.Find(model.GateType_Index);

                Guid gateTypeIndex;
                string userBy;
                DateTime userDate = DateTime.Now;

                if (gateType is null)
                {
                    gateTypeIndex = Guid.NewGuid();
                    userBy = model.Create_By;

                    Ms_GateType newGateType = new Ms_GateType
                    {
                        GateType_Index = gateTypeIndex,
                        GateType_Id = "GateType_Id".GenAutonumber(),
                        GateType_Name = model.GateType_Name,
                        IsActive = model.IsActive,
                        IsDelete = 0,
                        IsSystem = 0,
                        Status_Id = 0,
                        Create_By = userBy,
                        Create_Date = userDate
                    };

                    db.Ms_GateType.Add(newGateType);
                }
                else
                {
                    gateTypeIndex = gateType.GateType_Index;
                    userBy = model.Update_By;

                    gateType.GateType_Id = model.GateType_Id;
                    gateType.GateType_Name = model.GateType_Name;
                    gateType.IsActive = model.IsActive;
                    gateType.Update_By = userBy;
                    gateType.Update_Date = userDate;
                }

                var myTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    myTransaction.Commit();
                }
                catch (Exception saveEx)
                {
                    myTransaction.Rollback();
                    throw saveEx;
                }

                return gateTypeIndex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Guid SaveGateLane(string jsonData)
        {
            try
            {
                GateLaneModel model = GateLane.GetGateLaneModel(jsonData);
                Ms_GateLane gatelane = db.Ms_GateLane.Find(model.GateLane_Index);

                Guid GateLaneIndex;
                string userBy;
                DateTime userDate = DateTime.Now;

                if (gatelane is null)
                {
                    GateLaneIndex = Guid.NewGuid();
                    userBy = model.Create_By;

                    Ms_GateLane newGateLane = new Ms_GateLane
                    {
                        GateLane_Index = GateLaneIndex,
                        GateLane_Id = "GateLane_Id".GenAutonumber(),
                        GateLane_Name = model.GateLane_Name,
                        Gate_Index = model.Gate_Index,
                        IsActive = model.IsActive,
                        IsDelete = 0,
                        IsSystem = 0,
                        Status_Id = 0,
                        Create_By = userBy,
                        Create_Date = userDate
                    };

                    db.Ms_GateLane.Add(newGateLane);
                }
                else
                {
                    GateLaneIndex = gatelane.GateLane_Index;
                    userBy = model.Update_By;

                    gatelane.GateLane_Id = model.GateLane_Id;
                    gatelane.GateLane_Name = model.GateLane_Name;
                    gatelane.Gate_Index = model.Gate_Index;
                    gatelane.IsActive = model.IsActive;
                    gatelane.Update_By = userBy;
                    gatelane.Update_Date = userDate;
                }

                var myTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    myTransaction.Commit();
                }
                catch (Exception saveEx)
                {
                    myTransaction.Rollback();
                    throw saveEx;
                }

                return GateLaneIndex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Guid SaveFacility(string jsonData)
        {
            try
            {
                FacilityModel model = Facility.GetFacilityModel(jsonData);
                Ms_Facility facility = db.Ms_Facility.Find(model.Facility_Index);

                Guid FacilityIndex;
                string userBy;
                DateTime userDate = DateTime.Now;

                if (facility is null)
                {
                    FacilityIndex = Guid.NewGuid();
                    userBy = model.Create_By;

                    Ms_Facility newFacility = new Ms_Facility
                    {
                        Facility_Index = FacilityIndex,
                        Facility_Id = "Facility_Id".GenAutonumber(),
                        Facility_Name = model.Facility_Name,
                        FacilityType_Index = model.FacilityType_Index,
                        IsActive = model.IsActive,
                        IsDelete = 0,
                        IsSystem = 0,
                        Status_Id = 0,
                        Create_By = userBy,
                        Create_Date = userDate
                    };

                    db.Ms_Facility.Add(newFacility);
                }
                else
                {
                    FacilityIndex = facility.Facility_Index;
                    userBy = model.Update_By;

                    facility.Facility_Id = model.Facility_Id;
                    facility.Facility_Name = model.Facility_Name;
                    facility.FacilityType_Index = model.FacilityType_Index;
                    facility.IsActive = model.IsActive;
                    facility.Update_By = userBy;
                    facility.Update_Date = userDate;
                }

                var myTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    myTransaction.Commit();
                }
                catch (Exception saveEx)
                {
                    myTransaction.Rollback();
                    throw saveEx;
                }

                return FacilityIndex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Guid SaveFacilityType(string jsonData)
        {
            try
            {
                FacilityTypeModel model = FacilityType.GetFacilityTypeModel(jsonData);
                Ms_FacilityType facilityType = db.Ms_FacilityType.Find(model.FacilityType_Index);

                Guid FacilityTypeIndex;
                string userBy;
                DateTime userDate = DateTime.Now;

                if (facilityType is null)
                {
                    FacilityTypeIndex = Guid.NewGuid();
                    userBy = model.Create_By;

                    Ms_FacilityType newFacilityType = new Ms_FacilityType
                    {
                        FacilityType_Index = FacilityTypeIndex,
                        FacilityType_Id = "FacilityType_Id".GenAutonumber(),
                        FacilityType_Name = model.FacilityType_Name,
                        IsActive = model.IsActive,
                        IsDelete = 0,
                        IsSystem = 0,
                        Status_Id = 0,
                        Create_By = userBy,
                        Create_Date = userDate
                    };

                    db.Ms_FacilityType.Add(newFacilityType);
                }
                else
                {
                    FacilityTypeIndex = facilityType.FacilityType_Index;
                    userBy = model.Update_By;

                    facilityType.FacilityType_Id = model.FacilityType_Id;
                    facilityType.FacilityType_Name = model.FacilityType_Name;
                    facilityType.IsActive = model.IsActive;
                    facilityType.Update_By = userBy;
                    facilityType.Update_Date = userDate;
                }

                var myTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    myTransaction.Commit();
                }
                catch (Exception saveEx)
                {
                    myTransaction.Rollback();
                    throw saveEx;
                }

                return FacilityTypeIndex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Guid SaveYard(string jsonData)
        {
            try
            {
                YardModel model = Yard.GetYardModel(jsonData);
                Ms_Yard yard = db.Ms_Yard.Find(model.Yard_Index);

                Guid YardIndex;
                string userBy;
                DateTime userDate = DateTime.Now;

                if (yard is null)
                {
                    YardIndex = Guid.NewGuid();
                    userBy = model.Create_By;

                    Ms_Yard newYard = new Ms_Yard
                    {
                        Yard_Index = YardIndex,
                        Yard_Id = "Yard_Id".GenAutonumber(),
                        Yard_Name = model.Yard_Name,
                        YardType_Index = model.YardType_Index,
                        IsActive = model.IsActive,
                        IsDelete = 0,
                        IsSystem = 0,
                        Status_Id = 0,
                        Create_By = userBy,
                        Create_Date = userDate
                    };

                    db.Ms_Yard.Add(newYard);
                }
                else
                {
                    YardIndex = yard.Yard_Index;
                    userBy = model.Update_By;

                    yard.Yard_Id = model.Yard_Id;
                    yard.Yard_Name = model.Yard_Name;
                    yard.YardType_Index = model.YardType_Index;
                    yard.IsActive = model.IsActive;
                    yard.Update_By = userBy;
                    yard.Update_Date = userDate;
                }

                var myTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    myTransaction.Commit();
                }
                catch (Exception saveEx)
                {
                    myTransaction.Rollback();
                    throw saveEx;
                }

                return YardIndex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Guid SaveYardType(string jsonData)
        {
            try
            {
                YardTypeModel model = YardType.GetYardTypeModel(jsonData);
                Ms_YardType yardtype = db.Ms_YardType.Find(model.YardType_Index);

                Guid YardTypeIndex;
                string userBy;
                DateTime userDate = DateTime.Now;

                if (yardtype is null)
                {
                    YardTypeIndex = Guid.NewGuid();
                    userBy = model.Create_By;

                    Ms_YardType newYardType = new Ms_YardType
                    {
                        YardType_Index = YardTypeIndex,
                        YardType_Id = "YardType_Id".GenAutonumber(),
                        YardType_Name = model.YardType_Name,
                        IsActive = model.IsActive,
                        IsDelete = 0,
                        IsSystem = 0,
                        Status_Id = 0,
                        Create_By = userBy,
                        Create_Date = userDate
                    };

                    db.Ms_YardType.Add(newYardType);
                }
                else
                {
                    YardTypeIndex = yardtype.YardType_Index;
                    userBy = model.Update_By;

                    yardtype.YardType_Id = model.YardType_Id;
                    yardtype.YardType_Name = model.YardType_Name;
                    yardtype.IsActive = model.IsActive;
                    yardtype.Update_By = userBy;
                    yardtype.Update_Date = userDate;
                }

                var myTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    myTransaction.Commit();
                }
                catch (Exception saveEx)
                {
                    myTransaction.Rollback();
                    throw saveEx;
                }

                return YardTypeIndex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region + Delete +
        public bool DeleteDock(string jsonData)
        {
            try
            {
                DockSearchModel model = Dock.GetDockSearchModel(jsonData, true);
                Ms_Dock dock = db.Ms_Dock.Find(model.Dock_Index);

                if (dock is null)
                {
                    throw new Exception("Dock not found");
                }

                if (model.IsRemove)
                {
                    db.Ms_Dock.Remove(dock);
                }
                else
                {
                    dock.IsDelete = 1;
                    dock.Status_Id = -1;
                }

                var MyTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    MyTransaction.Commit();
                }
                catch (Exception saveEx)
                {
                    MyTransaction.Rollback();
                    throw saveEx;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteDockType(string jsonData)
        {
            try
            {
                DockTypeSearchModel model = DockType.GetDockTypeSearchModel(jsonData, true);
                Ms_DockType dockType = db.Ms_DockType.Find(model.DockType_Index);

                if (dockType is null)
                {
                    throw new Exception("DockType not found");
                }

                if (model.IsRemove)
                {
                    db.Ms_DockType.Remove(dockType);
                }
                else
                {
                    dockType.IsDelete = 1;
                    dockType.Status_Id = -1;
                }

                var MyTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    MyTransaction.Commit();
                }
                catch (Exception saveEx)
                {
                    MyTransaction.Rollback();
                    throw saveEx;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteDockZone(string jsonData)
        {
            try
            {
                DockZoneSearchModel model = DockZone.GetDockZoneSearchModel(jsonData, true);
                Ms_DockZone dockZone = db.Ms_DockZone.Find(model.DockZone_Index);

                if (dockZone is null)
                {
                    throw new Exception("DockZone not found");
                }

                if (model.IsRemove)
                {
                    db.Ms_DockZone.Remove(dockZone);
                }
                else
                {
                    dockZone.IsDelete = 1;
                    dockZone.Status_Id = -1;
                }

                var MyTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    MyTransaction.Commit();
                }
                catch (Exception saveEx)
                {
                    MyTransaction.Rollback();
                    throw saveEx;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteDockLocation(string jsonData)
        {
            try
            {
                DockLocationSearchModel model = DockLocation.GetDockLocationSearchModel(jsonData, true);
                Ms_DockLocation dockLocation = db.Ms_DockLocation.Find(model.DockLocation_Index);

                if (dockLocation is null)
                {
                    throw new Exception("DockLocation not found");
                }

                if (model.IsRemove)
                {
                    db.Ms_DockLocation.Remove(dockLocation);
                }
                else
                {
                    dockLocation.IsDelete = 1;
                    dockLocation.Status_Id = -1;
                }

                var MyTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    MyTransaction.Commit();
                }
                catch (Exception saveEx)
                {
                    MyTransaction.Rollback();
                    throw saveEx;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteGate(string jsonData)
        {
            try
            {
                GateSearchModel model = Gate.GetGateSearchModel(jsonData, true);
                Ms_Gate gate = db.Ms_Gate.Find(model.Gate_Index);

                if (gate is null)
                {
                    throw new Exception("Gate not found");
                }

                if (model.IsRemove)
                {
                    db.Ms_Gate.Remove(gate);
                }
                else
                {
                    gate.IsDelete = 1;
                    gate.Status_Id = -1;
                }
                

                var MyTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    MyTransaction.Commit();
                }
                catch (Exception saveEx)
                {
                    MyTransaction.Rollback();
                    throw saveEx;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteGateType(string jsonData)
        {
            try
            {
                GateTypeSearchModel model = GateType.GetGateTypeSearchModel(jsonData, true);
                Ms_GateType gateType = db.Ms_GateType.Find(model.GateType_Index);

                if (gateType is null)
                {
                    throw new Exception("GateType not found");
                }

                if (model.IsRemove)
                {
                    db.Ms_GateType.Remove(gateType);
                }
                else
                {
                    gateType.IsDelete = 1;
                    gateType.Status_Id = -1;
                }

                var MyTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    MyTransaction.Commit();
                }
                catch (Exception saveEx)
                {
                    MyTransaction.Rollback();
                    throw saveEx;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteGateLane(string jsonData)
        {
            try
            {
                GateLaneSearchModel model = GateLane.GetGateLaneSearchModel(jsonData, true);
                Ms_GateLane gatelane = db.Ms_GateLane.Find(model.GateLane_Index);

                if (gatelane is null)
                {
                    throw new Exception("GateLane not found");
                }

                if (model.IsRemove)
                {
                    db.Ms_GateLane.Remove(gatelane);
                }
                else
                {
                    gatelane.IsDelete = 1;
                    gatelane.Status_Id = -1;
                }

                var MyTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    MyTransaction.Commit();
                }
                catch (Exception saveEx)
                {
                    MyTransaction.Rollback();
                    throw saveEx;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteFacility(string jsonData)
        {
            try
            {
                FacilitySearchModel model = Facility.GetFacilitySearchModel(jsonData, true);
                Ms_Facility facility = db.Ms_Facility.Find(model.Facility_Index);

                if (facility is null)
                {
                    throw new Exception("Facility not found");
                }

                if (model.IsRemove)
                {
                    db.Ms_Facility.Remove(facility);
                }
                else
                {
                    facility.IsDelete = 1;
                    facility.Status_Id = -1;
                }

                var MyTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    MyTransaction.Commit();
                }
                catch (Exception saveEx)
                {
                    MyTransaction.Rollback();
                    throw saveEx;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteFacilityType(string jsonData)
        {
            try
            {
                FacilityTypeSearchModel model = FacilityType.GetFacilityTypeSearchModel(jsonData, true);
                Ms_FacilityType facilityType = db.Ms_FacilityType.Find(model.FacilityType_Index);

                if (facilityType is null)
                {
                    throw new Exception("FacilityType not found");
                }

                if (model.IsRemove)
                {
                    db.Ms_FacilityType.Remove(facilityType);
                }
                else
                {
                    facilityType.IsDelete = 1;
                    facilityType.Status_Id = -1;
                }

                var MyTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    MyTransaction.Commit();
                }
                catch (Exception saveEx)
                {
                    MyTransaction.Rollback();
                    throw saveEx;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteYard(string jsonData)
        {
            try
            {
                YardSearchModel model = Yard.GetYardSearchModel(jsonData, true);
                Ms_Yard yard = db.Ms_Yard.Find(model.Yard_Index);

                if (yard is null)
                {
                    throw new Exception("Yard not found");
                }

                if (model.IsRemove)
                {
                    db.Ms_Yard.Remove(yard);
                }
                else
                {
                    yard.IsDelete = 1;
                    yard.Status_Id = -1;
                }

                var MyTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    MyTransaction.Commit();
                }
                catch (Exception saveEx)
                {
                    MyTransaction.Rollback();
                    throw saveEx;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteYardType(string jsonData)
        {
            try
            {
                YardTypeSearchModel model = YardType.GetYardTypeSearchModel(jsonData, true);
                Ms_YardType yardtype = db.Ms_YardType.Find(model.YardType_Index);

                if (yardtype is null)
                {
                    throw new Exception("YardType not found");
                }

                if (model.IsRemove)
                {
                    db.Ms_YardType.Remove(yardtype);
                }
                else
                {
                    yardtype.IsDelete = 1;
                    yardtype.Status_Id = -1;
                }

                var MyTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    MyTransaction.Commit();
                }
                catch (Exception saveEx)
                {
                    MyTransaction.Rollback();
                    throw saveEx;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }

}
