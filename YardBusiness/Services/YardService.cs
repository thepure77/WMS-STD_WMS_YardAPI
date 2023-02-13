using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

using DataAccess;
using DataAccess.Models.Master.Table;
using DataAccess.Models.Yard.Table;

using Business.Models;
using Business.Reports;
using AspNetCore.Reporting;
using System.Text;
using Business.Commons;
using System.Drawing;
using System.Configuration;
using System.Net;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.SqlClient;
using DataAccess.Models.GI.Table;

namespace Business.Services
{
    public class YardService
    {
        private readonly YardDbContext db;
        private readonly MasterDbContext dbMS;
        private readonly GRDbContext dbGR;
        private readonly GIDbContext dbGI;
        private readonly QcallDbContext dbQcall;
        private string urlFireBase = ConfigurationManager1.AppSetting["firebase:real_time_url"];
        private string authFireBaseRealtimebase = ConfigurationManager1.AppSetting["firebase:real_time_auth"];
        private string urlFireBaseFCM = ConfigurationManager1.AppSetting["firebase:fcm_url"];
        private string authFireBaseFCM = ConfigurationManager1.AppSetting["firebase:fcm_auth"];

        #region + route +
        private string routeBKK = ConfigurationManager1.AppSetting["ConfigAuto:routeBKK"];
        private string routeCentral = ConfigurationManager1.AppSetting["ConfigAuto:routeCentral"];
        private string routeUPC = ConfigurationManager1.AppSetting["ConfigAuto:routeUPC"];
        private string SubrouteBKK_A = ConfigurationManager1.AppSetting["ConfigAuto:SubrouteBKK_A"];
        private string SubrouteBKK_B = ConfigurationManager1.AppSetting["ConfigAuto:SubrouteBKK_B"];
        private string SubrouteBKK_C = ConfigurationManager1.AppSetting["ConfigAuto:SubrouteBKK_C"];
        private string SubrouteBKK_D = ConfigurationManager1.AppSetting["ConfigAuto:SubrouteBKK_D"];
        private string SubrouteCentral_01 = ConfigurationManager1.AppSetting["ConfigAuto:SubrouteCentral_01"];
        private string SubrouteCentral_02 = ConfigurationManager1.AppSetting["ConfigAuto:SubrouteCentral_02"];
        private string SubrouteCentral_03 = ConfigurationManager1.AppSetting["ConfigAuto:SubrouteCentral_03"];
        private string SubrouteCentral_04 = ConfigurationManager1.AppSetting["ConfigAuto:SubrouteCentral_04"];
        private string SubrouteCentral_05 = ConfigurationManager1.AppSetting["ConfigAuto:SubrouteCentral_05"];
        private string SubrouteUPC_01 = ConfigurationManager1.AppSetting["ConfigAuto:SubrouteUPC_01"];
        private string SubrouteUPC_02 = ConfigurationManager1.AppSetting["ConfigAuto:SubrouteUPC_02"];
        private string SubrouteUPC_03 = ConfigurationManager1.AppSetting["ConfigAuto:SubrouteUPC_03"];
        private string SubrouteUPC_04 = ConfigurationManager1.AppSetting["ConfigAuto:SubrouteUPC_04"];
        private string Time_SubrouteBKK_A = ConfigurationManager1.AppSetting["ConfigAuto:Time_SubrouteBKK_A"];
        private string Time_SubrouteBKK_B = ConfigurationManager1.AppSetting["ConfigAuto:Time_SubrouteBKK_B"];
        private string Time_SubrouteBKK_C = ConfigurationManager1.AppSetting["ConfigAuto:Time_SubrouteBKK_C"];
        private string Time_SubrouteBKK_D = ConfigurationManager1.AppSetting["ConfigAuto:Time_SubrouteBKK_D"];
        private string Time_SubrouteCentral_01 = ConfigurationManager1.AppSetting["ConfigAuto:Time_SubrouteCentral_01"];
        private string Time_SubrouteCentral_02 = ConfigurationManager1.AppSetting["ConfigAuto:Time_SubrouteCentral_02"];
        private string Time_SubrouteCentral_03 = ConfigurationManager1.AppSetting["ConfigAuto:Time_SubrouteCentral_03"];
        private string Time_SubrouteCentral_04 = ConfigurationManager1.AppSetting["ConfigAuto:Time_SubrouteCentral_04"];
        private string Time_SubrouteCentral_05 = ConfigurationManager1.AppSetting["ConfigAuto:Time_SubrouteCentral_05"];
        private string Time_SubrouteUPC_01 = ConfigurationManager1.AppSetting["ConfigAuto:Time_SubrouteUPC_01"];
        private string Time_SubrouteUPC_02 = ConfigurationManager1.AppSetting["ConfigAuto:Time_SubrouteUPC_02"];
        private string Time_SubrouteUPC_03 = ConfigurationManager1.AppSetting["ConfigAuto:Time_SubrouteUPC_03"];
        private string Time_SubrouteUPC_04 = ConfigurationManager1.AppSetting["ConfigAuto:Time_SubrouteUPC_04"];

        private string latti = ConfigurationManager1.AppSetting["ConfigAuto:lat"];
        private string longti = ConfigurationManager1.AppSetting["ConfigAuto:long"];

        #endregion

        #region + YardService +
        public YardService()
        {
            db = new YardDbContext();
            dbMS = new MasterDbContext();
            dbGR = new GRDbContext();
            dbGI = new GIDbContext();
            dbQcall = new QcallDbContext();
        }

        public YardService(YardDbContext db, MasterDbContext dbMS, GRDbContext dbGR, GIDbContext dbGI)
        {
            this.db = db;
            this.dbMS = dbMS;
            this.dbGR = dbGR;
            this.dbGI = dbGI;
            this.dbQcall = dbQcall;
        }

        public YardService(IConfiguration configuration)
        {

        }
        #endregion

        #region + WareHouseQouta +

        #region + List +
        public List<WareHouseQoutaWareHouseModel> ListWareHouseForWareHouseQouta()
        {
            try
            {
                List<Ms_WareHouseQouta> wareHouseQouta = db.Ms_WareHouseQouta.Where(w => w.IsDelete == 0).ToList();
                List<Guid> listWareHouseIndex = wareHouseQouta.GroupBy(g => g.WareHouse_Index).Select(s => s.Key).ToList();

                MasterDbContext dbMs = new MasterDbContext();
                List<Ms_Warehouse> wareHouse = dbMs.Ms_WareHouse.Where(w =>
                    (w.IsActive == 1 || w.IsActive == 0 && w.IsDelete == 0) &&
                    !listWareHouseIndex.Contains(w.Warehouse_Index)
                ).ToList();

                List<WareHouseQoutaWareHouseModel> result = new List<WareHouseQoutaWareHouseModel>();
                wareHouse.ForEach(e => result.Add(new WareHouseQoutaWareHouseModel() { WareHouse_Index = e.Warehouse_Index, WareHouse_Id = e.Warehouse_Id, WareHouse_Name = e.Warehouse_Name }));

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WareHouseQoutaViewModel ListWareHouseQouta(string jsonData)
        {
            try
            {
                WareHouseQoutaSearchModel model = WareHouseQouta.GetWareHouseQoutaSearchModel(jsonData);

                List<Ms_WareHouseQouta> wareHouseQouta = db.Ms_WareHouseQouta.Where(
                        s => s.IsDelete == 0 &&
                             s.IsActive.IsEquals(model.IsActive) &&
                             s.WareHouseQouta_Id.Like(model.WareHouseQouta_Id) &&
                             s.WareHouse_Index.IsEquals(model.WareHouse_Index) &&
                             s.Create_By.Like(model.Create_By)
                    //&& s.Create_Date.DateBetweenCondition(model.Create_Date, model.Create_Date_To)
                    ).ToList();

                var statusModels = new List<int?>();
                if (model.status != null)
                {
                    if (model.status.Count > 0)
                    {
                        foreach (var item in model.status)
                        {
                            statusModels.Add(item.value);
                        }
                        wareHouseQouta = wareHouseQouta.Where(c => statusModels.Contains(c.IsActive)).ToList();

                    }

                }
                WareHouseQoutaViewModel result = new WareHouseQoutaViewModel()
                {
                    WareHouseQoutaModels = JsonConvert.DeserializeObject<List<WareHouseQoutaModel>>(JsonConvert.SerializeObject(wareHouseQouta)),
                    TotalRow = wareHouseQouta.Count,
                    CurrentPage = model.CurrentPage,
                    PerPage = model.PerPage
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
        public Ms_WareHouseQouta GetWareHouseQouta(string jsonData)
        {
            try
            {
                WareHouseQoutaSearchModel model = WareHouseQouta.GetWareHouseQoutaSearchModel(jsonData, true);

                Ms_WareHouseQouta result = db.Ms_WareHouseQouta.Find(model.WareHouseQouta_Index);

                if (result is null)
                {
                    throw new Exception("WareHouseQouta not found");
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region + Save +
        public Guid SaveWareHouseQouta(string jsonData)
        {
            try
            {
                WareHouseQoutaModel model = WareHouseQouta.GetWareHouseQoutaModel(jsonData);
                Ms_WareHouseQouta wareHouseQouta = db.Ms_WareHouseQouta.Find(model.WareHouseQouta_Index);

                Guid wareHouseQoutaIndex;
                string userBy;
                DateTime userDate = DateTime.Now;

                if (wareHouseQouta is null)
                {
                    wareHouseQoutaIndex = Guid.NewGuid();
                    userBy = model.Create_By;

                    Ms_WareHouseQouta newWareHouseQouta = new Ms_WareHouseQouta
                    {
                        WareHouseQouta_Index = wareHouseQoutaIndex,
                        WareHouseQouta_Id = "WareHouseQouta_Id".GenAutonumber(),
                        WareHouse_Index = model.WareHouse_Index,
                        WareHouse_Id = model.WareHouse_Id,
                        WareHouse_Name = model.WareHouse_Name,

                        Display_Date = model.Display_Date,
                        Pre_Booking = model.Pre_Booking,
                        CheckIn_Limit_Before = model.CheckIn_Limit_Before,
                        CheckIn_Limit_After = model.CheckIn_Limit_After,

                        IsActive = model.IsActive,
                        IsDelete = 0,
                        IsSystem = 0,
                        Status_Id = 0,
                        Create_By = userBy,
                        Create_Date = userDate
                    };

                    db.Ms_WareHouseQouta.Add(newWareHouseQouta);
                }
                else
                {
                    wareHouseQoutaIndex = wareHouseQouta.WareHouseQouta_Index;
                    userBy = model.Update_By;

                    wareHouseQouta.WareHouseQouta_Id = model.WareHouseQouta_Id;
                    wareHouseQouta.Display_Date = model.Display_Date;
                    wareHouseQouta.Pre_Booking = model.Pre_Booking;
                    wareHouseQouta.CheckIn_Limit_Before = model.CheckIn_Limit_Before;
                    wareHouseQouta.CheckIn_Limit_After = model.CheckIn_Limit_After;
                    wareHouseQouta.IsActive = model.IsActive;
                    wareHouseQouta.Update_By = userBy;
                    wareHouseQouta.Update_Date = userDate;
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

                return wareHouseQoutaIndex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region + Delete +
        public bool DeleteWareHouseQouta(string jsonData)
        {
            try
            {
                WareHouseQoutaSearchModel model = WareHouseQouta.GetWareHouseQoutaSearchModel(jsonData, true);
                Ms_WareHouseQouta wareHouseQouta = db.Ms_WareHouseQouta.Find(model.WareHouseQouta_Index);

                if (wareHouseQouta is null)
                {
                    throw new Exception("WareHouseQouta not found");
                }

                if (model.IsRemove)
                {
                    db.Ms_WareHouseQouta.Remove(wareHouseQouta);
                }
                else
                {
                    wareHouseQouta.IsDelete = 1;
                    wareHouseQouta.Status_Id = -1;
                    wareHouseQouta.Cancel_By = model.Create_By;
                    wareHouseQouta.Cancel_Date = DateTime.Now;
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

        #endregion

        #region + DockQouta +

        #region + List +
        public List<WareHouseQoutaWareHouseModel> ListWareHouseForDockQouta()
        {
            try
            {
                List<Ms_WareHouseQouta> wareHouseQouta = db.Ms_WareHouseQouta.Where(w => w.IsActive == 1 && w.IsDelete == 0).ToList();

                List<WareHouseQoutaWareHouseModel> result = new List<WareHouseQoutaWareHouseModel>();
                wareHouseQouta.ForEach(e => result.Add(new WareHouseQoutaWareHouseModel() { WareHouse_Index = e.WareHouse_Index, WareHouse_Id = e.WareHouse_Id, WareHouse_Name = e.WareHouse_Name }));

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AppointmentDockModel> ListDockForAppointment()
        {
            try
            {
                List<Ms_DockQoutaInterval> dockQoutaInterval = db.Ms_DockQoutaInterval.Where(w => w.IsActive == 1 && w.IsDelete == 0).ToList();

                List<AppointmentDockModel> result = new List<AppointmentDockModel>();
                dockQoutaInterval.GroupBy(g =>
                    new { g.Dock_Index, g.Dock_Id, g.Dock_Name }).Select(s =>
                        new { s.Key.Dock_Index, s.Key.Dock_Id, s.Key.Dock_Name }).OrderBy(o => o.Dock_Name).ToList().ForEach(e =>
                            result.Add(new AppointmentDockModel() { Dock_Index = e.Dock_Index, Dock_Id = e.Dock_Id, Dock_Name = e.Dock_Name }));

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AppointmentWithItemModel> ListTimeForQueue()
        {
            try
            {
                var Time_Appoint = db.Tb_AppointmentItem.Where(w => w.IsActive == 1 && w.IsDelete == 0).ToList();

                List<AppointmentWithItemModel> result = new List<AppointmentWithItemModel>();
                Time_Appoint.GroupBy(g =>
                    new { g.Appointment_Time, g.Appointment_Date }).Select(s =>
                         new { s.Key.Appointment_Time, s.Key.Appointment_Date }).OrderBy(o => o.Appointment_Time).ToList().ForEach(e =>
                              result.Add(new AppointmentWithItemModel() { Appointment_Time = e.Appointment_Time, Appointment_Date = e.Appointment_Date }));

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DockQoutaDockModel> ListDockForDockQouta()
        {
            try
            {
                List<Ms_DockQoutaInterval> dockQoutaInterval = db.Ms_DockQoutaInterval.Where(w => w.IsActive == 1 && w.IsDelete == 0).ToList();
                List<Guid> listDockIndex = dockQoutaInterval.GroupBy(g => g.Dock_Index).Select(s => s.Key).ToList();

                MasterDbContext dbMs = new MasterDbContext();
                List<Ms_Dock> dock = dbMs.Ms_Dock.Where(
                    w => w.IsActive == 1 &&
                         w.IsDelete == 0 &&
                         !listDockIndex.Contains(w.Dock_Index)
                ).OrderBy(o => o.Dock_Name).ToList();

                List<DockQoutaDockModel> result = new List<DockQoutaDockModel>();
                dock.ForEach(e => result.Add(new DockQoutaDockModel() { Dock_Index = e.Dock_Index, Dock_Id = e.Dock_Id, Dock_Name = e.Dock_Name }));

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DockQoutaDockModel> ListDockForSearchDockQouta()
        {
            try
            {
                List<Ms_DockQoutaInterval> dockQoutaInterval = db.Ms_DockQoutaInterval.Where(w => w.IsActive == 1 && w.IsDelete == 0).ToList();

                List<DockQoutaDockModel> result = new List<DockQoutaDockModel>();
                dockQoutaInterval.Select(s => new { s.Dock_Index, s.Dock_Id, s.Dock_Name }).Distinct().
                    OrderBy(o => o.Dock_Name).ToList().ForEach(e =>
                        result.Add(new DockQoutaDockModel() { Dock_Index = e.Dock_Index, Dock_Id = e.Dock_Id, Dock_Name = e.Dock_Name }));

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DockQoutaIntervalTime> ListDockQoutaIntervalTime(string jsonData)
        {
            try
            {
                DockQoutaSearchModel model = DockQouta.GetDockQoutaSearchModel(jsonData);
                if (model.Interval < 0 || model.Interval > 240) { throw new Exception("Invalid Parameter : Interval Time must between 0 - 60"); }

                List<DockQoutaIntervalTime> result = new List<DockQoutaIntervalTime>();

                TimeSpan interval = new TimeSpan(0, model.Interval - 1, 0);
                TimeSpan adjust = new TimeSpan(0, 1, 0);
                TimeSpan time = new TimeSpan(0, 0, 0);

                string start, end;
                int id = 0;
                while (time.Days < 1)
                {
                    start = time.ToString(@"hh\:mm");

                    //if (start == "10:00" || start == "17:00" || start == "05:00" || start == "22:00")
                    //{
                    //    TimeSpan time_brake = new TimeSpan(1, -1, 0);
                    //    time = time.Add(time_brake);
                    //}
                    //else
                    //{
                    time = time.Add(interval);
                    //}

                    if (time.Days == 1)
                    {
                        time = new TimeSpan(24, 59, 0);
                    }

                    end = time.ToString(@"hh\:mm");
                    result.Add(new DockQoutaIntervalTime() { Time_Id = ++id, Time_Start = start, Time_End = end, Time = start + " - " + end });

                    time = time.Add(adjust);
                }

                result.OrderBy(o => o.Time_Id);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //try
            //{
            //    DockQoutaSearchModel model = DockQouta.GetDockQoutaSearchModel(jsonData);
            //    if (model.Interval < 0 || model.Interval > 240) { throw new Exception("Invalid Parameter : Interval Time must between 0 - 60"); }

            //    List<DockQoutaIntervalTime> result = new List<DockQoutaIntervalTime>();

            //    TimeSpan interval = new TimeSpan(0, model.Interval - 1, 0);
            //    TimeSpan adjust = new TimeSpan(0, 1, 0);
            //    TimeSpan time = new TimeSpan(0, 0, 0);

            //    string start, end;
            //    int id = 0;
            //    while (time.Days < 1)
            //    {
            //        start = time.ToString(@"hh\:mm");

            //        if (start == "12:00" || start == "17:00")
            //        {
            //            TimeSpan time_brake = new TimeSpan(1, -1, 0);
            //            time = time.Add(time_brake);
            //        }
            //        else
            //        {
            //            time = time.Add(interval);
            //        }

            //        if (time.Days == 1)
            //        {
            //            time = new TimeSpan(23, 59, 0);
            //        }

            //        end = time.ToString(@"hh\:mm");
            //        result.Add(new DockQoutaIntervalTime() { Time_Id = ++id, Time_Start = start, Time_End = end, Time = start + " - " + end });

            //        time = time.Add(adjust);
            //    }

            //    result.OrderBy(o => o.Time_Id);
            //    return result;
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        public DockQoutaViewModel ListDockQouta(string jsonData)
        {
            try
            {
                DockQoutaSearchModel model = DockQouta.GetDockQoutaSearchModel(jsonData);

                List<Guid> listDockQoutaIndex = new List<Guid>();
                if ((model.ListDockIndex?.Count ?? 0) > 0)
                {
                    listDockQoutaIndex = db.Ms_DockQoutaInterval.Where(
                        w => w.IsActive == 1 &&
                                w.IsDelete == 0 &&
                                model.ListDockIndex.Contains(w.Dock_Index)
                    ).ToList().GroupBy(g => g.DockQouta_Index).Select(s => s.Key).ToList();
                }

                List<Ms_DockQouta> dockQouta = db.Ms_DockQouta.Where(
                    w => w.IsDelete == 0 &&
                         w.IsActive.IsEquals(model.IsActive) &&
                         //w.DockQouta_Id.Like(model.DockQouta_Id) &&
                         w.WareHouse_Index.IsEquals(model.WareHouse_Index) &&
                         w.Create_By.Like(model.Create_By) &&
                         w.Create_Date.DateBetweenCondition(model.Create_Date, model.Create_Date_To) &&
                         w.DockQouta_Index.IsContains(listDockQoutaIndex)
                ).ToList();

                var statusModels = new List<int?>();
                if (model.status != null)
                {
                    if (model.status.Count > 0)
                    {
                        foreach (var item in model.status)
                        {
                            statusModels.Add(item.value);
                        }
                    }
                    dockQouta = dockQouta.Where(c => statusModels.Contains(c.IsActive)).ToList();

                }

                DockQoutaViewModel result = new DockQoutaViewModel()
                {
                    TotalRow = dockQouta.Count,
                    CurrentPage = model.CurrentPage,
                    PerPage = model.PerPage
                };

                dockQouta.ForEach(e =>
                {

                    if (model.DockQouta_Id != null)
                    {
                        Ms_DockQoutaInterval interval = db.Ms_DockQoutaInterval.FirstOrDefault(w => w.DockQouta_Index == e.DockQouta_Index && w.Dock_Id == model.DockQouta_Id);
                        if (interval != null)
                        {
                            DockQoutaModel dock = JsonConvert.DeserializeObject<DockQoutaModel>(JsonConvert.SerializeObject(e));
                            dock.First_Dock_Index = interval?.Dock_Index;
                            dock.First_Dock_Id = interval?.Dock_Id;
                            dock.First_Dock_Name = interval?.Dock_Name;
                            result.DockQoutaModels.Add(dock);
                        }
                    }
                    else
                    {
                        Ms_DockQoutaInterval interval = db.Ms_DockQoutaInterval.FirstOrDefault(w => w.DockQouta_Index == e.DockQouta_Index);
                        DockQoutaModel dock = JsonConvert.DeserializeObject<DockQoutaModel>(JsonConvert.SerializeObject(e));
                        dock.First_Dock_Index = interval?.Dock_Index;
                        dock.First_Dock_Id = interval?.Dock_Id;
                        dock.First_Dock_Name = interval?.Dock_Name;
                        result.DockQoutaModels.Add(dock);
                    }



                });

                List<DockQoutaModel> resultOrderBy = new List<DockQoutaModel>();
                int seq = 0;
                result.DockQoutaModels.OrderBy(o => o.First_Dock_Name).ToList().ForEach(e =>
                { e.Seq = ++seq; resultOrderBy.Add(e); });

                result.DockQoutaModels = resultOrderBy;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region + Get +
        public DockQoutaModel GetDockQouta(string jsonData)
        {
            try
            {
                DockQoutaSearchModel model = DockQouta.GetDockQoutaSearchModel(jsonData, true);

                Ms_DockQouta dockQouta = db.Ms_DockQouta.Find(model.DockQouta_Index);

                if (dockQouta is null)
                {
                    throw new Exception("DockQouta not found");
                }

                List<Ms_DockQoutaInterval> dockQoutaIntervals = db.Ms_DockQoutaInterval.Where(w => w.DockQouta_Index == model.DockQouta_Index.Value).ToList();
                List<DockQoutaIntervalTime> listDockQoutaIntervalTime = new List<DockQoutaIntervalTime>();

                DockQoutaModel result = JsonConvert.DeserializeObject<DockQoutaModel>(JsonConvert.SerializeObject(dockQouta));
                dockQoutaIntervals.GroupBy(g => new { g.Dock_Index, g.Dock_Id, g.Dock_Name }).Select(s =>
                    new { s.Key.Dock_Index, s.Key.Dock_Id, s.Key.Dock_Name }).ToList().ForEach(e =>
                    {
                        result.Docks.Add(new DockQoutaDockModel() { Dock_Index = e.Dock_Index, Dock_Id = e.Dock_Id, Dock_Name = e.Dock_Name });
                    });

                listDockQoutaIntervalTime.Clear();
                dockQoutaIntervals.GroupBy(g => new { g.Interval_Start, g.Interval_End, g.Seq }).Select(s =>
                    new { s.Key.Interval_Start, s.Key.Interval_End, s.Key.Seq }).ToList().ForEach(e => listDockQoutaIntervalTime.Add(
                        new DockQoutaIntervalTime() { Time_Id = e.Seq, Time_Start = e.Interval_Start, Time_End = e.Interval_End, Time = e.Interval_Start + " - " + e.Interval_End }));
                result.DockQoutaIntervalTime.AddRange(listDockQoutaIntervalTime.OrderBy(o => o.Time_Id).ToList());

                listDockQoutaIntervalTime.Clear();
                dockQoutaIntervals.Where(w => w.IsBreakTime == true).GroupBy(g => new { g.Interval_Start, g.Interval_End, g.Seq }).Select(s =>
                    new { s.Key.Interval_Start, s.Key.Interval_End, s.Key.Seq }).ToList().ForEach(e => listDockQoutaIntervalTime.Add(
                        new DockQoutaIntervalTime() { Time_Id = e.Seq, Time_Start = e.Interval_Start, Time_End = e.Interval_End, Time = e.Interval_Start + " - " + e.Interval_End }));
                result.DockQoutaIntervalBreakTime.AddRange(listDockQoutaIntervalTime.OrderBy(o => o.Time_Id).ToList());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region + Save +
        public Guid SaveDockQouta(string jsonData)
        {
            try
            {
                DockQoutaModel model = DockQouta.GetDockQoutaModel(jsonData);
                Ms_DockQouta dockQouta = db.Ms_DockQouta.Find(model.DockQouta_Index);

                Guid dockQoutaIndex;
                string userBy;
                DateTime userDate = DateTime.Now;
                List<Ms_DockQoutaInterval> dockInterval;

                if (dockQouta is null)
                {
                    dockQoutaIndex = Guid.NewGuid();
                    userBy = model.Create_By;

                    Ms_DockQouta newDockQouta = new Ms_DockQouta
                    {
                        DockQouta_Index = dockQoutaIndex,
                        DockQouta_Id = "DockQouta_Id".GenAutonumber(),

                        WareHouse_Index = model.WareHouse_Index,
                        WareHouse_Id = model.WareHouse_Id,
                        WareHouse_Name = model.WareHouse_Name,

                        DockQouta_Date = model.DockQouta_Date,
                        DockQouta_Date_End = model.DockQouta_Date_End,
                        Interval = model.Interval,
                        DockQouta_Time = model.DockQouta_Time,
                        DockQouta_Time_End = model.DockQouta_Time_End,
                        checkIn_Limit_Before = model.checkIn_Limit_Before,
                        checkIn_Limit_After = model.checkIn_Limit_After,

                        IsActive = model.IsActive,
                        IsDelete = 0,
                        IsSystem = 0,
                        Status_Id = 0,
                        Create_By = userBy,
                        Create_Date = userDate
                    };

                    db.Ms_DockQouta.Add(newDockQouta);
                }
                else
                {
                    dockQoutaIndex = dockQouta.DockQouta_Index;
                    userBy = model.Update_By;
                    dockInterval = db.Ms_DockQoutaInterval.Where(w => w.DockQouta_Index == dockQoutaIndex).ToList();
                    db.Ms_DockQoutaInterval.RemoveRange(dockInterval);

                    dockQouta.WareHouse_Index = model.WareHouse_Index;
                    dockQouta.WareHouse_Id = model.WareHouse_Id;
                    dockQouta.WareHouse_Name = model.WareHouse_Name;

                    dockQouta.DockQouta_Date = model.DockQouta_Date;
                    dockQouta.DockQouta_Date_End = model.DockQouta_Date_End;
                    dockQouta.Interval = model.Interval;
                    dockQouta.DockQouta_Time = model.DockQouta_Time;
                    dockQouta.DockQouta_Time_End = model.DockQouta_Time_End;
                    dockQouta.checkIn_Limit_Before = model.checkIn_Limit_Before;
                    dockQouta.checkIn_Limit_After = model.checkIn_Limit_After;

                    dockQouta.IsActive = model.IsActive;
                    dockQouta.Update_By = userBy;
                    dockQouta.Update_Date = userDate;
                }

                dockInterval = new List<Ms_DockQoutaInterval>();

                foreach (DockQoutaDockModel dock in model.Docks)
                {
                    foreach (DockQoutaIntervalTime interval in model.DockQoutaIntervalTime)
                    {
                        db.Ms_DockQoutaInterval.Add(new Ms_DockQoutaInterval()
                        {
                            DockQoutaInterval_Index = Guid.NewGuid(),
                            DockQouta_Index = dockQoutaIndex,

                            Dock_Index = dock.Dock_Index,
                            Dock_Id = dock.Dock_Id,
                            Dock_Name = dock.Dock_Name,

                            Interval_Start = interval.Time_Start ?? "00:00",
                            Interval_End = interval.Time_End ?? "23:59",
                            Seq = interval.Time_Id,
                            IsBreakTime = model.DockQoutaIntervalBreakTime?.Any(s => s.Time_Id == interval.Time_Id) ?? false,
                            checkIn_Limit_Before = model.checkIn_Limit_Before,
                            checkIn_Limit_After = model.checkIn_Limit_After,

                            IsActive = model.IsActive,
                            IsDelete = 0,
                            IsSystem = 0,
                            Status_Id = 0,
                            Create_By = userBy,
                            Create_Date = userDate
                        });
                    }
                }
                db.Ms_DockQoutaInterval.AddRange(dockInterval);

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

                return dockQoutaIndex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region + Delete +
        public bool DeleteDockQouta(string jsonData)
        {
            try
            {
                DockQoutaSearchModel model = DockQouta.GetDockQoutaSearchModel(jsonData, true);
                Ms_DockQouta dockQouta = db.Ms_DockQouta.Find(model.DockQouta_Index);

                if (dockQouta is null)
                {
                    throw new Exception("DockQouta not found");
                }

                List<Ms_DockQoutaInterval> dockQoutaIntervals = db.Ms_DockQoutaInterval.Where(w => w.DockQouta_Index == model.DockQouta_Index.Value).ToList();

                if (model.IsRemove)
                {
                    db.Ms_DockQouta.Remove(dockQouta);

                    if ((dockQoutaIntervals?.Count ?? 0) > 0)
                    {
                        db.Ms_DockQoutaInterval.RemoveRange(dockQoutaIntervals);
                    }
                }
                else
                {
                    dockQouta.IsDelete = 1;
                    dockQouta.Status_Id = -1;
                    dockQouta.Cancel_By = model.Create_By;
                    dockQouta.Cancel_Date = DateTime.Now;

                    if ((dockQoutaIntervals?.Count ?? 0) > 0)
                    {
                        dockQoutaIntervals.ForEach(e => { e.IsDelete = 1; e.Status_Id = -1; e.Cancel_By = model.Create_By; e.Cancel_Date = DateTime.Now; });
                    }
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

        #endregion

        #region + Appointment +

        #region + List +

        #region ListWareHouseForAppointment
        public List<WareHouseQoutaWareHouseModel> ListWareHouseForAppointment()
        {
            try
            {
                List<Ms_WareHouseQouta> wareHouseQouta = db.Ms_WareHouseQouta.Where(w => w.IsActive == 1 && w.IsDelete == 0).ToList();
                List<Guid> listWareHouseQoutaIndex = wareHouseQouta.Select(s => s.WareHouse_Index).ToList();
                List<Ms_DockQouta> dockQouta = db.Ms_DockQouta.Where(w => w.IsActive == 1 && w.IsDelete == 0 && w.WareHouse_Index.IsContains(listWareHouseQoutaIndex)).ToList();

                List<WareHouseQoutaWareHouseModel> result = new List<WareHouseQoutaWareHouseModel>();
                dockQouta.GroupBy(g => new { g.WareHouse_Index, g.WareHouse_Id, g.WareHouse_Name }).Select(s =>
                    new { s.Key.WareHouse_Index, s.Key.WareHouse_Id, s.Key.WareHouse_Name }).ToList().ForEach(e => result.Add(
                        new WareHouseQoutaWareHouseModel() { WareHouse_Index = e.WareHouse_Index, WareHouse_Id = e.WareHouse_Id, WareHouse_Name = e.WareHouse_Name }));

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ListDocumentTypeForAppointment
        public List<DocumentType> ListDocumentTypeForAppointment()
        {
            try
            {
                Guid processIndex = Guid.Parse("F62293EB-1AEB-4F92-89F5-EC6D3C5A20D3");
                SearchDocumentTypeInClauseViewModel documentType_model = new SearchDocumentTypeInClauseViewModel() { Process_Index = processIndex };
                ActionResultSearchDocumentTypeModel result_api = Utils.SendDataApi<ActionResultSearchDocumentTypeModel>(new AppSettingConfig().GetUrl("SearchDocumentTypeInClause"), JsonConvert.SerializeObject(documentType_model));
                if ((result_api?.ItemsDocumentType?.Count ?? 0) == 0) { throw new Exception("DocumentType not found"); }
                List<DocumentType> documenttype = new List<DocumentType>();
                foreach (var item in result_api.ItemsDocumentType)
                {
                    if (item.DocumentType_Index == Guid.Parse("6A6D5A1C-3998-4C2B-B44A-330A78BA6335")) { continue; }
                    else { documenttype.Add(item); }
                }
                return documenttype;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ListDockQouataForAppointment
        public List<AppointmentQoutaModel> ListDockQouataForAppointment(string jsonData)
        {
            try
            {
                AppointmentSearchModel model = Appointment.GetAppointmentSearchModel(jsonData);
                if (!model.WareHouse_Index.HasValue) { throw new Exception("WareHouse Index not found"); }
                if (!model.Appointment_Date.HasValue) { throw new Exception("Appointment Date not found"); }
                if (model.Appointment_Date < DateTime.Now.TrimTime()) { throw new Exception("The previous date could not be found."); }

                Ms_WareHouseQouta wareHouseQouta = db.Ms_WareHouseQouta.FirstOrDefault(
                    w => w.IsActive == 1 &&
                         w.IsDelete == 0 &&
                         w.WareHouse_Index == model.WareHouse_Index.Value);
                if (wareHouseQouta is null) { throw new Exception("WareHouseQouta not found"); }

                List<AppointmentQoutaModel> result = new List<AppointmentQoutaModel>();
                AppointmentQoutaModel qouta;

                List<Guid> listDockQoutaIndex, listDockQoutaIntervalIndex;
                List<Ms_DockQouta> dockQouta;
                List<Ms_DockQoutaInterval> dockIntervalQouta, dockIntervalQoutaFilter;
                List<Tb_AppointmentItem> appointmentItem;
                List<tb_BlockAppointmentTime> blockAppointmentTimes;

                DateTime date = model.Appointment_Date.Value.TrimTime();
                TimeSpan bookingTime = model.Appointment_Time ?? DateTime.Now.TimeOfDay;
                TimeSpan addTime = new TimeSpan(wareHouseQouta.Pre_Booking > 0 ? wareHouseQouta.Pre_Booking : 0, 0, 0);
                TimeSpan prebookingTime = new TimeSpan(date.Ticks) + bookingTime.Add(addTime);
                TimeSpan dockTimeStart, dockTimeEnd, intervalTime, currentTime;

                var getdock = new List<Guid>();
                if (model.DocumentType_Index == Guid.Parse("7f139afb-b712-4562-911e-0ed654c6f7af"))
                {
                    getdock = dbMS.Ms_Dock.Where(c => c.DockType_Index == Guid.Parse("CDF0F0D4-F72A-4419-A58E-0F93EE4304BC") && c.IsActive == 1).GroupBy(c => c.Dock_Index).Select(c => c.Key).ToList();
                }
                else if (model.DocumentType_Index == Guid.Parse("C392D865-8E69-4985-B72F-2421EBE8BCDB"))
                {
                    getdock = dbMS.Ms_Dock.Where(c => c.DockType_Index == Guid.Parse("BDB6CC26-B144-4E44-BC3F-F8E78E0E97AE") && c.IsActive == 1).GroupBy(c => c.Dock_Index).Select(c => c.Key).ToList();
                }
                else if (model.DocumentType_Index == Guid.Parse("13091631-5829-4341-BCB4-272B0ED854D7"))
                {
                    getdock = dbMS.Ms_Dock.Where(c => c.DockType_Index == Guid.Parse("8DC27B53-6F51-41BC-824F-6ABAD656284C") && c.IsActive == 1).GroupBy(c => c.Dock_Index).Select(c => c.Key).ToList();
                }


                bool isEnalble;
                int displayDate = wareHouseQouta.Display_Date; //wareHouseQouta.Display_Date;                           
                for (int i = 0; i < displayDate; i++)
                {
                    qouta = new AppointmentQoutaModel() { WareHouse_Index = wareHouseQouta.WareHouse_Index, WareHouse_Id = wareHouseQouta.WareHouse_Id, WareHouse_Name = wareHouseQouta.WareHouse_Name, Appointment_Date = date };
                    result.Add(qouta);

                    dockQouta = db.Ms_DockQouta.Where(
                        w => w.IsActive == 1 &&
                             w.IsDelete == 0 &&
                             w.WareHouse_Index == model.WareHouse_Index.Value &&
                             date.IsBetween(w.DockQouta_Date, w.DockQouta_Date_End, true)
                    ).ToList();

                    if ((dockQouta?.Count ?? 0) > 0)
                    {
                        listDockQoutaIndex = dockQouta.Select(s => s.DockQouta_Index).ToList();
                        dockIntervalQouta = db.Ms_DockQoutaInterval.Where(w => w.Dock_Index.IsEquals(model.Dock_Index) && listDockQoutaIndex.Contains(w.DockQouta_Index) && getdock.Contains(w.Dock_Index))
                            .OrderBy(c => (int.Parse(c.Dock_Id))).ToList();
                        if (model.vehicleType_Index != null)
                        {
                            List<Guid> Vehicle_Dock = db.tb_VehicleDock.Where(c => c.VehicleType_Index == Guid.Parse(model.vehicleType_Index)).Select(s => s.Dock_Index).ToList();
                            dockIntervalQouta = dockIntervalQouta.Where(c => Vehicle_Dock.Contains(c.Dock_Index)).ToList();
                        }
                        if ((dockIntervalQouta?.Count ?? 0) > 0)
                        {
                            listDockQoutaIntervalIndex = dockIntervalQouta.Select(s => s.DockQoutaInterval_Index).ToList();
                            appointmentItem = db.Tb_AppointmentItem.Where(w => w.Appointment_Date == date && listDockQoutaIntervalIndex.Contains(w.DockQoutaInterval_Index) && w.IsActive == 1 && w.IsDelete == 0).OrderBy(c => int.Parse(c.Dock_Id)).ToList();
                            blockAppointmentTimes = db.tb_BlockAppointmentTime.Where(w => w.Appointment_Date == date && listDockQoutaIntervalIndex.Contains(w.DockQoutaInterval_Index)).ToList();

                            (from dock in dockQouta
                             join interval in dockIntervalQouta
                             on dock.DockQouta_Index equals interval.DockQouta_Index
                             group new { dock, interval }
                             by new
                             {
                                 Time = dock.DockQouta_Time,
                                 Time_End = dock.DockQouta_Time_End,
                                 dock.Interval,
                                 interval.Dock_Index,
                                 interval.Dock_Id,
                                 interval.Dock_Name
                             } into dockGroup
                             orderby int.Parse(dockGroup.Key.Dock_Id)
                             select dockGroup).ToList().ForEach(e => qouta.Items.Add(
                                 new AppointmentQoutaDockModel()
                                 {
                                     Dock_Index = e.Key.Dock_Index,
                                     Dock_Id = e.Key.Dock_Id,
                                     Dock_Name = e.Key.Dock_Name,
                                     Interval = e.Key.Interval,
                                     Time = e.Key.Time,
                                     Time_End = e.Key.Time_End
                                 }));

                            foreach (AppointmentQoutaDockModel dock in qouta.Items)
                            {
                                dockIntervalQoutaFilter = dockIntervalQouta.Where(w => w.Dock_Index == dock.Dock_Index).OrderBy(c => int.Parse(c.Dock_Id)).ThenBy(o => o.Seq).ToList();
                                foreach (Ms_DockQoutaInterval interval in dockIntervalQoutaFilter)
                                {

                                    dockTimeStart = TimeSpan.Parse(dock.Time + ":00");
                                    dockTimeEnd = TimeSpan.Parse(dock.Time_End + ":00");
                                    intervalTime = TimeSpan.Parse(interval.Interval_Start + ":00");
                                    currentTime = new TimeSpan(date.Ticks + intervalTime.Ticks);
                                    var a = !appointmentItem.Any(w => w.DockQoutaInterval_Index == interval.DockQoutaInterval_Index);
                                    if (date < DateTime.Now)
                                    {
                                        isEnalble = (intervalTime >= dockTimeStart)
                                        //&& (intervalTime <= dockTimeEnd)
                                        && (currentTime > prebookingTime)
                                        && !interval.IsBreakTime
                                        && !appointmentItem.Any(w => w.DockQoutaInterval_Index == interval.DockQoutaInterval_Index)
                                        && !blockAppointmentTimes.Any(w => w.DockQoutaInterval_Index == interval.DockQoutaInterval_Index);
                                    }
                                    else
                                    {
                                        isEnalble = !interval.IsBreakTime
                                            && !appointmentItem.Any(w => w.DockQoutaInterval_Index == interval.DockQoutaInterval_Index)
                                            && !blockAppointmentTimes.Any(w => w.DockQoutaInterval_Index == interval.DockQoutaInterval_Index);
                                    }


                                    dock.Times.Add(new AppointmentQoutaIntervalModel()
                                    {
                                        DockQoutaInterval_Index = interval.DockQoutaInterval_Index,
                                        Seq = interval.Seq,
                                        Time_Start = interval.Interval_Start,
                                        Time_End = interval.Interval_End,
                                        Time = interval.Interval_Start + " - " + interval.Interval_End,
                                        IsEnable = isEnalble
                                    });
                                }
                            }
                        }
                    }

                    date = date.AddDays(1);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ListDockQouataForAppointmentreAssign
        public List<AppointmentQoutaModel> ListDockQouataForAppointmentreAssign(string jsonData)
        {
            try
            {
                AppointmentSearchModel model = Appointment.GetAppointmentSearchModel(jsonData);
                if (!model.WareHouse_Index.HasValue) { throw new Exception("WareHouse Index not found"); }
                if (!model.Appointment_Date.HasValue) { throw new Exception("Appointment Date not found"); }
                if (model.Appointment_Date < DateTime.Now.TrimTime()) { throw new Exception("The previous date could not be found."); }

                Ms_WareHouseQouta wareHouseQouta = db.Ms_WareHouseQouta.FirstOrDefault(
                    w => w.IsActive == 1 &&
                         w.IsDelete == 0 &&
                         w.WareHouse_Index == model.WareHouse_Index.Value);
                if (wareHouseQouta is null) { throw new Exception("WareHouseQouta not found"); }

                List<AppointmentQoutaModel> result = new List<AppointmentQoutaModel>();
                AppointmentQoutaModel qouta;

                List<Guid> listDockQoutaIndex, listDockQoutaIntervalIndex;
                List<Ms_DockQouta> dockQouta;
                List<Ms_DockQoutaInterval> dockIntervalQouta, dockIntervalQoutaFilter;
                List<Tb_AppointmentItem> appointmentItem;
                List<tb_BlockAppointmentTime> blockAppointmentTimes;

                DateTime date = model.Appointment_Date.Value.TrimTime();
                TimeSpan bookingTime = model.Appointment_Time ?? DateTime.Now.TimeOfDay;
                TimeSpan addTime = new TimeSpan(wareHouseQouta.Pre_Booking > 0 ? wareHouseQouta.Pre_Booking : 0, 0, 0);
                TimeSpan prebookingTime = new TimeSpan(date.Ticks) + bookingTime.Add(addTime);
                TimeSpan dockTimeStart, dockTimeEnd, intervalTime, currentTime;
                Guid Docktemp;
                if (model.DocumentType_Index == Guid.Parse("c392d865-8e69-4985-b72f-2421ebe8bcdb"))
                {
                    Docktemp = Guid.Parse("c754528c-d800-42f2-89ad-a5d269f8147d");
                }
                else if (model.DocumentType_Index == Guid.Parse("7f139afb-b712-4562-911e-0ed654c6f7af"))
                {
                    Docktemp = Guid.Parse("807f51c6-e2a5-4e41-b56d-021fb56c993e");
                }
                else if (model.DocumentType_Index == Guid.Parse("13091631-5829-4341-BCB4-272B0ED854D7"))
                {
                    Docktemp = Guid.Parse("c2c199b5-7de0-44d3-a69c-b499c10fea34");
                }


                bool isEnalble;
                int displayDate = 1; //wareHouseQouta.Display_Date;                           
                for (int i = 0; i < displayDate; i++)
                {
                    qouta = new AppointmentQoutaModel() { WareHouse_Index = wareHouseQouta.WareHouse_Index, WareHouse_Id = wareHouseQouta.WareHouse_Id, WareHouse_Name = wareHouseQouta.WareHouse_Name, Appointment_Date = date };
                    result.Add(qouta);

                    dockQouta = db.Ms_DockQouta.Where(
                        w => w.IsActive == 1 &&
                             w.IsDelete == 0 &&
                             w.WareHouse_Index == model.WareHouse_Index.Value &&
                             date.IsBetween(w.DockQouta_Date, w.DockQouta_Date_End, true)
                    ).ToList();

                    if ((dockQouta?.Count ?? 0) > 0)
                    {
                        listDockQoutaIndex = dockQouta.Select(s => s.DockQouta_Index).ToList();
                        dockIntervalQouta = db.Ms_DockQoutaInterval.Where(w => w.Dock_Index.IsEquals(Docktemp) && listDockQoutaIndex.Contains(w.DockQouta_Index)).ToList();
                        if (model.vehicleType_Index != null)
                        {
                            List<Guid> Vehicle_Dock = db.tb_VehicleDock.Where(c => c.VehicleType_Index == Guid.Parse(model.vehicleType_Index)).Select(s => s.Dock_Index).ToList();
                            dockIntervalQouta = dockIntervalQouta.Where(c => Vehicle_Dock.Contains(c.Dock_Index)).ToList();
                        }
                        if ((dockIntervalQouta?.Count ?? 0) > 0)
                        {
                            listDockQoutaIntervalIndex = dockIntervalQouta.Select(s => s.DockQoutaInterval_Index).ToList();
                            appointmentItem = db.Tb_AppointmentItem.Where(w => w.Appointment_Date == date && listDockQoutaIntervalIndex.Contains(w.DockQoutaInterval_Index) && w.IsActive == 1 && w.IsDelete == 0).ToList();
                            blockAppointmentTimes = db.tb_BlockAppointmentTime.Where(w => w.Appointment_Date == date && listDockQoutaIntervalIndex.Contains(w.DockQoutaInterval_Index)).ToList();

                            (from dock in dockQouta
                             join interval in dockIntervalQouta
                             on dock.DockQouta_Index equals interval.DockQouta_Index
                             group new { dock, interval }
                             by new
                             {
                                 Time = dock.DockQouta_Time,
                                 Time_End = dock.DockQouta_Time_End,
                                 dock.Interval,
                                 interval.Dock_Index,
                                 interval.Dock_Id,
                                 interval.Dock_Name
                             } into dockGroup
                             orderby dockGroup.Key.Dock_Id
                             select dockGroup).ToList().ForEach(e => qouta.Items.Add(
                                 new AppointmentQoutaDockModel()
                                 {
                                     Dock_Index = e.Key.Dock_Index,
                                     Dock_Id = e.Key.Dock_Id,
                                     Dock_Name = e.Key.Dock_Name,
                                     Interval = e.Key.Interval,
                                     Time = e.Key.Time,
                                     Time_End = e.Key.Time_End
                                 }));

                            foreach (AppointmentQoutaDockModel dock in qouta.Items)
                            {
                                dockIntervalQoutaFilter = dockIntervalQouta.Where(w => w.Dock_Index == dock.Dock_Index).OrderBy(o => o.Seq).ToList();
                                foreach (Ms_DockQoutaInterval interval in dockIntervalQoutaFilter)
                                {

                                    dockTimeStart = TimeSpan.Parse(dock.Time + ":00");
                                    dockTimeEnd = TimeSpan.Parse(dock.Time_End + ":00");
                                    intervalTime = TimeSpan.Parse(interval.Interval_Start + ":00");
                                    currentTime = new TimeSpan(date.Ticks + intervalTime.Ticks);
                                    var a = !appointmentItem.Any(w => w.DockQoutaInterval_Index == interval.DockQoutaInterval_Index);
                                    if (date < DateTime.Now)
                                    {
                                        isEnalble = (intervalTime >= dockTimeStart)
                                        //&& (intervalTime <= dockTimeEnd)
                                        && (currentTime > prebookingTime)
                                        && !interval.IsBreakTime
                                        && !appointmentItem.Any(w => w.DockQoutaInterval_Index == interval.DockQoutaInterval_Index)
                                        && !blockAppointmentTimes.Any(w => w.DockQoutaInterval_Index == interval.DockQoutaInterval_Index);
                                    }
                                    else
                                    {
                                        isEnalble = !interval.IsBreakTime
                                            && !appointmentItem.Any(w => w.DockQoutaInterval_Index == interval.DockQoutaInterval_Index)
                                            && !blockAppointmentTimes.Any(w => w.DockQoutaInterval_Index == interval.DockQoutaInterval_Index);
                                    }


                                    dock.Times.Add(new AppointmentQoutaIntervalModel()
                                    {
                                        DockQoutaInterval_Index = interval.DockQoutaInterval_Index,
                                        Seq = interval.Seq,
                                        Time_Start = interval.Interval_Start,
                                        Time_End = interval.Interval_End,
                                        Time = interval.Interval_Start + " - " + interval.Interval_End,
                                        IsEnable = isEnalble
                                    });
                                }
                            }
                        }
                    }

                    date = date.AddDays(1);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ListDockQouataForAppointmentreAssign
        public List<AppointmentQoutaModel> ListDockForAppointmentreAssign(string jsonData)
        {
            try
            {
                AppointmentSearchModel model = Appointment.GetAppointmentSearchModel(jsonData);
                if (!model.Appointment_Date.HasValue) { throw new Exception("Appointment Date not found"); }
                if (model.Appointment_Date < DateTime.Now.TrimTime()) { throw new Exception("The previous date could not be found."); }

                Ms_WareHouseQouta wareHouseQouta = db.Ms_WareHouseQouta.FirstOrDefault(
                    w => w.IsActive == 1 &&
                         w.IsDelete == 0 &&
                         w.WareHouse_Index == model.WareHouse_Index.Value);
                if (wareHouseQouta is null) { throw new Exception("WareHouseQouta not found"); }

                List<AppointmentQoutaModel> result = new List<AppointmentQoutaModel>();
                AppointmentQoutaModel qouta;

                List<Guid> listDockQoutaIndex, listDockQoutaIntervalIndex;
                List<Ms_DockQouta> dockQouta;
                List<Ms_DockQoutaInterval> dockIntervalQouta, dockIntervalQoutaFilter;
                List<Tb_AppointmentItem> appointmentItem;
                List<tb_BlockAppointmentTime> blockAppointmentTimes;

                DateTime date = model.Appointment_Date.Value.TrimTime();
                TimeSpan bookingTime = model.Appointment_Time ?? DateTime.Now.TimeOfDay;
                TimeSpan addTime = new TimeSpan(wareHouseQouta.Pre_Booking > 0 ? wareHouseQouta.Pre_Booking : 0, 0, 0);
                TimeSpan prebookingTime = new TimeSpan(date.Ticks) + bookingTime.Add(addTime);
                TimeSpan dockTimeStart, dockTimeEnd, intervalTime, currentTime;

                var getdock = new List<Guid>();
                if (model.DocumentType_Index == Guid.Parse("7f139afb-b712-4562-911e-0ed654c6f7af"))
                {
                    getdock = dbMS.Ms_Dock.Where(c => c.DockType_Index == Guid.Parse("CDF0F0D4-F72A-4419-A58E-0F93EE4304BC") && c.IsActive == 1).GroupBy(c => c.Dock_Index).Select(c => c.Key).ToList();
                }
                else if (model.DocumentType_Index == Guid.Parse("C392D865-8E69-4985-B72F-2421EBE8BCDB"))
                {
                    getdock = dbMS.Ms_Dock.Where(c => c.DockType_Index == Guid.Parse("BDB6CC26-B144-4E44-BC3F-F8E78E0E97AE") && c.IsActive == 1).GroupBy(c => c.Dock_Index).Select(c => c.Key).ToList();
                }
                else if (model.DocumentType_Index == Guid.Parse("13091631-5829-4341-BCB4-272B0ED854D7"))
                {
                    getdock = dbMS.Ms_Dock.Where(c => c.DockType_Index == Guid.Parse("8DC27B53-6F51-41BC-824F-6ABAD656284C") && c.IsActive == 1).GroupBy(c => c.Dock_Index).Select(c => c.Key).ToList();
                }


                bool isEnalble;
                int displayDate = 1; //wareHouseQouta.Display_Date;                           
                for (int i = 0; i < displayDate; i++)
                {
                    qouta = new AppointmentQoutaModel() { WareHouse_Index = wareHouseQouta.WareHouse_Index, WareHouse_Id = wareHouseQouta.WareHouse_Id, WareHouse_Name = wareHouseQouta.WareHouse_Name, Appointment_Date = date };
                    result.Add(qouta);

                    dockQouta = db.Ms_DockQouta.Where(
                        w => w.IsActive == 1 &&
                             w.IsDelete == 0 &&
                             w.WareHouse_Index == model.WareHouse_Index.Value &&
                             date.IsBetween(w.DockQouta_Date, w.DockQouta_Date_End, true)
                    ).ToList();

                    if ((dockQouta?.Count ?? 0) > 0)
                    {
                        listDockQoutaIndex = dockQouta.Select(s => s.DockQouta_Index).ToList();
                        dockIntervalQouta = db.Ms_DockQoutaInterval.Where(w => getdock.Contains(w.Dock_Index) && listDockQoutaIndex.Contains(w.DockQouta_Index) && w.Seq == model.seq).ToList();
                        if (model.vehicleType_Index != null)
                        {
                            List<Guid> Vehicle_Dock = db.tb_VehicleDock.Where(c => c.VehicleType_Index == Guid.Parse(model.vehicleType_Index)).Select(s => s.Dock_Index).ToList();
                            dockIntervalQouta = dockIntervalQouta.Where(c => Vehicle_Dock.Contains(c.Dock_Index)).ToList();
                        }
                        if ((dockIntervalQouta?.Count ?? 0) > 0)
                        {
                            listDockQoutaIntervalIndex = dockIntervalQouta.Select(s => s.DockQoutaInterval_Index).ToList();
                            appointmentItem = db.Tb_AppointmentItem.Where(w => w.Appointment_Date == date && listDockQoutaIntervalIndex.Contains(w.DockQoutaInterval_Index) && w.IsActive == 1 && w.IsDelete == 0).ToList();
                            blockAppointmentTimes = db.tb_BlockAppointmentTime.Where(w => w.Appointment_Date == date && listDockQoutaIntervalIndex.Contains(w.DockQoutaInterval_Index)).ToList();

                            (from dock in dockQouta
                             join interval in dockIntervalQouta
                             on dock.DockQouta_Index equals interval.DockQouta_Index
                             group new { dock, interval }
                             by new
                             {
                                 Time = dock.DockQouta_Time,
                                 Time_End = dock.DockQouta_Time_End,
                                 dock.Interval,
                                 interval.Dock_Index,
                                 interval.Dock_Id,
                                 interval.Dock_Name
                             } into dockGroup
                             orderby dockGroup.Key.Dock_Id
                             select dockGroup).ToList().ForEach(e => qouta.Items.Add(
                                 new AppointmentQoutaDockModel()
                                 {
                                     Dock_Index = e.Key.Dock_Index,
                                     Dock_Id = e.Key.Dock_Id,
                                     Dock_Name = e.Key.Dock_Name,
                                     Interval = e.Key.Interval,
                                     Time = e.Key.Time,
                                     Time_End = e.Key.Time_End
                                 }));

                            foreach (AppointmentQoutaDockModel dock in qouta.Items)
                            {
                                dockIntervalQoutaFilter = dockIntervalQouta.Where(w => w.Dock_Index == dock.Dock_Index).OrderBy(o => o.Seq).ToList();
                                foreach (Ms_DockQoutaInterval interval in dockIntervalQoutaFilter)
                                {

                                    dockTimeStart = TimeSpan.Parse(dock.Time + ":00");
                                    dockTimeEnd = TimeSpan.Parse(dock.Time_End + ":00");
                                    intervalTime = TimeSpan.Parse(interval.Interval_Start + ":00");
                                    currentTime = new TimeSpan(date.Ticks + intervalTime.Ticks);
                                    var a = !appointmentItem.Any(w => w.DockQoutaInterval_Index == interval.DockQoutaInterval_Index);
                                    if (date < DateTime.Now)
                                    {
                                        isEnalble = (intervalTime >= dockTimeStart)
                                        //&& (intervalTime <= dockTimeEnd)
                                        && (currentTime > prebookingTime)
                                        && !interval.IsBreakTime
                                        && !appointmentItem.Any(w => w.DockQoutaInterval_Index == interval.DockQoutaInterval_Index)
                                        && !blockAppointmentTimes.Any(w => w.DockQoutaInterval_Index == interval.DockQoutaInterval_Index);
                                    }
                                    else
                                    {
                                        isEnalble = !interval.IsBreakTime
                                            && !appointmentItem.Any(w => w.DockQoutaInterval_Index == interval.DockQoutaInterval_Index)
                                            && !blockAppointmentTimes.Any(w => w.DockQoutaInterval_Index == interval.DockQoutaInterval_Index);
                                    }


                                    dock.Times.Add(new AppointmentQoutaIntervalModel()
                                    {
                                        DockQoutaInterval_Index = interval.DockQoutaInterval_Index,
                                        Seq = interval.Seq,
                                        Time_Start = interval.Interval_Start,
                                        Time_End = interval.Interval_End,
                                        Time = interval.Interval_Start + " - " + interval.Interval_End,
                                        IsEnable = isEnalble
                                    });
                                }
                            }
                        }
                    }

                    date = date.AddDays(1);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ListDockForAppointmentQ
        public List<AppointmentQoutaModel> ListDockForAppointmentQ(string jsonData)
        {
            try
            {
                Model_Q model = JsonConvert.DeserializeObject<Model_Q>(jsonData);
                if (!model.Appointment_Date.HasValue) { throw new Exception("Appointment Date not found"); }
                if (model.Appointment_Date < DateTime.Now.TrimTime()) { throw new Exception("The previous date could not be found."); }

                Ms_WareHouseQouta wareHouseQouta = db.Ms_WareHouseQouta.FirstOrDefault(
                    w => w.IsActive == 1 &&
                         w.IsDelete == 0 &&
                         w.WareHouse_Index == Guid.Parse("b0ad4e8f-a7b1-4952-bac7-1a9482baba79"));
                if (wareHouseQouta is null) { throw new Exception("WareHouseQouta not found"); }

                List<AppointmentQoutaModel> result = new List<AppointmentQoutaModel>();
                AppointmentQoutaModel qouta;

                List<Guid> listDockQoutaIndex, listDockQoutaIntervalIndex;
                List<Ms_DockQouta> dockQouta;
                List<Ms_DockQoutaInterval> dockIntervalQouta, dockIntervalQoutaFilter;
                List<Tb_AppointmentItem> appointmentItem;
                List<tb_BlockAppointmentTime> blockAppointmentTimes;


                DateTime date = model.Appointment_Date.Value.TrimTime();
                TimeSpan bookingTime = DateTime.Now.TimeOfDay;
                TimeSpan addTime = new TimeSpan(wareHouseQouta.Pre_Booking > 0 ? wareHouseQouta.Pre_Booking : 0, 0, 0);
                TimeSpan prebookingTime = new TimeSpan(date.Ticks) + bookingTime.Add(addTime);
                TimeSpan dockTimeStart, dockTimeEnd, intervalTime, currentTime;

                var getdock = new List<Guid>();
                if (model.DocumentType_Index == Guid.Parse("7f139afb-b712-4562-911e-0ed654c6f7af"))
                {
                    getdock = dbMS.Ms_Dock.Where(c => c.DockType_Index == Guid.Parse("CDF0F0D4-F72A-4419-A58E-0F93EE4304BC") && c.Dock_use == 0 && c.IsActive == 1).GroupBy(c => c.Dock_Index).Select(c => c.Key).ToList();
                }
                else if (model.DocumentType_Index == Guid.Parse("C392D865-8E69-4985-B72F-2421EBE8BCDB"))
                {
                    getdock = dbMS.Ms_Dock.Where(c => c.DockType_Index == Guid.Parse("BDB6CC26-B144-4E44-BC3F-F8E78E0E97AE") && c.Dock_use == 0 && c.IsActive == 1).GroupBy(c => c.Dock_Index).Select(c => c.Key).ToList();
                }
                else if (model.DocumentType_Index == Guid.Parse("13091631-5829-4341-BCB4-272B0ED854D7"))
                {
                    getdock = dbMS.Ms_Dock.Where(c => c.DockType_Index == Guid.Parse("8DC27B53-6F51-41BC-824F-6ABAD656284C") && c.Dock_use == 0 && c.IsActive == 1).GroupBy(c => c.Dock_Index).Select(c => c.Key).ToList();
                }
                Ms_DockQoutaInterval dockQoutaInterval = db.Ms_DockQoutaInterval.FirstOrDefault(c => c.DockQoutaInterval_Index == model.DockQoutaInterval_Index);

                bool isEnalble;
                int displayDate = 1; //wareHouseQouta.Display_Date;                           
                for (int i = 0; i < displayDate; i++)
                {
                    qouta = new AppointmentQoutaModel() { WareHouse_Index = wareHouseQouta.WareHouse_Index, WareHouse_Id = wareHouseQouta.WareHouse_Id, WareHouse_Name = wareHouseQouta.WareHouse_Name, Appointment_Date = date };
                    result.Add(qouta);

                    dockQouta = db.Ms_DockQouta.Where(
                        w => w.IsActive == 1 &&
                             w.IsDelete == 0 &&
                             w.WareHouse_Index == Guid.Parse("b0ad4e8f-a7b1-4952-bac7-1a9482baba79") &&
                             date.IsBetween(w.DockQouta_Date, w.DockQouta_Date_End, true)
                    ).ToList();

                    if ((dockQouta?.Count ?? 0) > 0)
                    {
                        listDockQoutaIndex = dockQouta.Select(s => s.DockQouta_Index).ToList();
                        dockIntervalQouta = db.Ms_DockQoutaInterval.Where(w => getdock.Contains(w.Dock_Index) && listDockQoutaIndex.Contains(w.DockQouta_Index) && w.Seq == dockQoutaInterval.Seq).ToList();
                        if (model.vehicleType_Index != null)
                        {
                            List<Guid> Vehicle_Dock = db.tb_VehicleDock.Where(c => c.VehicleType_Index == Guid.Parse(model.vehicleType_Index)).Select(s => s.Dock_Index).ToList();
                            dockIntervalQouta = dockIntervalQouta.Where(c => Vehicle_Dock.Contains(c.Dock_Index)).ToList();
                        }
                        if ((dockIntervalQouta?.Count ?? 0) > 0)
                        {
                            listDockQoutaIntervalIndex = dockIntervalQouta.Select(s => s.DockQoutaInterval_Index).ToList();
                            appointmentItem = db.Tb_AppointmentItem.Where(w => w.Appointment_Date == date && listDockQoutaIntervalIndex.Contains(w.DockQoutaInterval_Index) && w.IsActive == 1 && w.IsDelete == 0).ToList();
                            blockAppointmentTimes = db.tb_BlockAppointmentTime.Where(w => w.Appointment_Date == date && listDockQoutaIntervalIndex.Contains(w.DockQoutaInterval_Index)).ToList();

                            (from dock in dockQouta
                             join interval in dockIntervalQouta
                             on dock.DockQouta_Index equals interval.DockQouta_Index
                             group new { dock, interval }
                             by new
                             {
                                 Time = dock.DockQouta_Time,
                                 Time_End = dock.DockQouta_Time_End,
                                 dock.Interval,
                                 interval.Dock_Index,
                                 interval.Dock_Id,
                                 interval.Dock_Name
                             } into dockGroup
                             orderby dockGroup.Key.Dock_Id
                             select dockGroup).ToList().ForEach(e => qouta.Items.Add(
                                 new AppointmentQoutaDockModel()
                                 {
                                     Dock_Index = e.Key.Dock_Index,
                                     Dock_Id = e.Key.Dock_Id,
                                     Dock_Name = e.Key.Dock_Name,
                                     Interval = e.Key.Interval,
                                     Time = e.Key.Time,
                                     Time_End = e.Key.Time_End
                                 }));

                            foreach (AppointmentQoutaDockModel dock in qouta.Items)
                            {
                                dockIntervalQoutaFilter = dockIntervalQouta.Where(w => w.Dock_Index == dock.Dock_Index).OrderBy(o => o.Seq).ToList();
                                foreach (Ms_DockQoutaInterval interval in dockIntervalQoutaFilter)
                                {

                                    dockTimeStart = TimeSpan.Parse(dock.Time + ":00");
                                    dockTimeEnd = TimeSpan.Parse(dock.Time_End + ":00");
                                    intervalTime = TimeSpan.Parse(interval.Interval_Start + ":00");
                                    currentTime = new TimeSpan(date.Ticks + intervalTime.Ticks);
                                    //if (date < DateTime.Now)
                                    //{
                                    //    isEnalble = (intervalTime >= dockTimeStart)
                                    //    && (intervalTime <= dockTimeEnd)
                                    //    && (currentTime > prebookingTime)
                                    //    && !interval.IsBreakTime
                                    //    && !appointmentItem.Any(w => w.DockQoutaInterval_Index == interval.DockQoutaInterval_Index)
                                    //    && !blockAppointmentTimes.Any(w => w.DockQoutaInterval_Index == interval.DockQoutaInterval_Index);
                                    //}
                                    //else
                                    //{
                                    isEnalble = !interval.IsBreakTime
                                        && !appointmentItem.Any(w => w.DockQoutaInterval_Index == interval.DockQoutaInterval_Index)
                                        && !blockAppointmentTimes.Any(w => w.DockQoutaInterval_Index == interval.DockQoutaInterval_Index);
                                    //}


                                    dock.Times.Add(new AppointmentQoutaIntervalModel()
                                    {
                                        DockQoutaInterval_Index = interval.DockQoutaInterval_Index,
                                        Seq = interval.Seq,
                                        Time_Start = interval.Interval_Start,
                                        Time_End = interval.Interval_End,
                                        Time = interval.Interval_Start + " - " + interval.Interval_End,
                                        IsEnable = isEnalble
                                    });
                                }
                            }
                        }
                    }

                    date = date.AddDays(1);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ListBlockTime
        public List<AppointmentQoutaModel> ListBlockTime(string jsonData)
        {
            try
            {
                BlockModel model = JsonConvert.DeserializeObject<BlockModel>(jsonData);

                if (!model.WareHouse_Index.HasValue) { throw new Exception("WareHouse Index not found"); }
                if (!model.Appointment_Date.HasValue) { throw new Exception("Appointment Date not found"); }
                if (model.Appointment_Date < DateTime.Now.TrimTime()) { throw new Exception("The previous date could not be found."); }

                Ms_WareHouseQouta wareHouseQouta = db.Ms_WareHouseQouta.FirstOrDefault(
                    w => w.IsActive == 1 &&
                         w.IsDelete == 0 &&
                         w.WareHouse_Index == model.WareHouse_Index.Value);
                if (wareHouseQouta is null) { throw new Exception("WareHouseQouta not found"); }

                List<AppointmentQoutaModel> result = new List<AppointmentQoutaModel>();
                AppointmentQoutaModel qouta;

                List<Guid> listDockQoutaIndex, listDockQoutaIntervalIndex;
                List<Ms_DockQouta> dockQouta;
                List<Ms_DockQoutaInterval> dockIntervalQouta, dockIntervalQoutaFilter;
                List<Tb_AppointmentItem> appointmentItem;
                List<tb_BlockAppointmentTime> blockAppointmentTimes;

                DateTime date = model.Appointment_Date.Value.TrimTime();
                TimeSpan bookingTime = DateTime.Now.TimeOfDay;
                TimeSpan addTime = new TimeSpan(wareHouseQouta.Pre_Booking > 0 ? wareHouseQouta.Pre_Booking : 0, 0, 0);
                TimeSpan prebookingTime = new TimeSpan(date.Ticks) + bookingTime.Add(addTime);
                TimeSpan dockTimeStart, dockTimeEnd, intervalTime, currentTime;

                Guid Docktemp;

                if (model.Dock_Index != null)
                {
                    Docktemp = model.Dock_Index.GetValueOrDefault();
                }

                bool isEnalble;
                int displayDate = 1; //wareHouseQouta.Display_Date;                           
                for (int i = 0; i < displayDate; i++)
                {
                    qouta = new AppointmentQoutaModel() { WareHouse_Index = wareHouseQouta.WareHouse_Index, WareHouse_Id = wareHouseQouta.WareHouse_Id, WareHouse_Name = wareHouseQouta.WareHouse_Name, Appointment_Date = date };
                    result.Add(qouta);

                    dockQouta = db.Ms_DockQouta.Where(
                        w => w.IsActive == 1 &&
                             w.IsDelete == 0 &&
                             w.WareHouse_Index == model.WareHouse_Index.Value &&
                             date.IsBetween(w.DockQouta_Date, w.DockQouta_Date_End, true)
                    ).ToList();

                    if ((dockQouta?.Count ?? 0) > 0)
                    {
                        listDockQoutaIndex = dockQouta.Select(s => s.DockQouta_Index).ToList();
                        dockIntervalQouta = db.Ms_DockQoutaInterval.Where(w => w.Dock_Index.IsEquals(Docktemp) && listDockQoutaIndex.Contains(w.DockQouta_Index)).ToList();
                        if (model.vehicleType_Index != null)
                        {
                            List<Guid> Vehicle_Dock = db.tb_VehicleDock.Where(c => c.VehicleType_Index == Guid.Parse(model.vehicleType_Index)).Select(s => s.Dock_Index).ToList();
                            dockIntervalQouta = dockIntervalQouta.Where(c => Vehicle_Dock.Contains(c.Dock_Index)).ToList();
                        }
                        if ((dockIntervalQouta?.Count ?? 0) > 0)
                        {
                            listDockQoutaIntervalIndex = dockIntervalQouta.Select(s => s.DockQoutaInterval_Index).ToList();
                            appointmentItem = db.Tb_AppointmentItem.Where(w => w.Appointment_Date == date && listDockQoutaIntervalIndex.Contains(w.DockQoutaInterval_Index) && w.IsActive == 1 && w.IsDelete == 0).ToList();
                            blockAppointmentTimes = db.tb_BlockAppointmentTime.Where(w => w.Appointment_Date == date && listDockQoutaIntervalIndex.Contains(w.DockQoutaInterval_Index)).ToList();

                            (from dock in dockQouta
                             join interval in dockIntervalQouta
                             on dock.DockQouta_Index equals interval.DockQouta_Index
                             group new { dock, interval }
                             by new
                             {
                                 Time = dock.DockQouta_Time,
                                 Time_End = dock.DockQouta_Time_End,
                                 dock.Interval,
                                 interval.Dock_Index,
                                 interval.Dock_Id,
                                 interval.Dock_Name
                             } into dockGroup
                             orderby dockGroup.Key.Dock_Id
                             select dockGroup).ToList().ForEach(e => qouta.Items.Add(
                                 new AppointmentQoutaDockModel()
                                 {
                                     Dock_Index = e.Key.Dock_Index,
                                     Dock_Id = e.Key.Dock_Id,
                                     Dock_Name = e.Key.Dock_Name,
                                     Interval = e.Key.Interval,
                                     Time = e.Key.Time,
                                     Time_End = e.Key.Time_End
                                 }));

                            foreach (AppointmentQoutaDockModel dock in qouta.Items)
                            {
                                dockIntervalQoutaFilter = dockIntervalQouta.Where(w => w.Dock_Index == dock.Dock_Index).OrderBy(o => o.Seq).ToList();
                                foreach (Ms_DockQoutaInterval interval in dockIntervalQoutaFilter)
                                {

                                    dockTimeStart = TimeSpan.Parse(dock.Time + ":00");
                                    dockTimeEnd = TimeSpan.Parse(dock.Time_End + ":00");
                                    intervalTime = TimeSpan.Parse(interval.Interval_Start + ":00");
                                    currentTime = new TimeSpan(date.Ticks + intervalTime.Ticks);
                                    var a = !appointmentItem.Any(w => w.DockQoutaInterval_Index == interval.DockQoutaInterval_Index);
                                    if (date < DateTime.Now)
                                    {
                                        isEnalble = (intervalTime >= dockTimeStart)
                                        //&& (intervalTime <= dockTimeEnd)
                                        && (currentTime > prebookingTime)
                                        && !interval.IsBreakTime
                                        && !appointmentItem.Any(w => w.DockQoutaInterval_Index == interval.DockQoutaInterval_Index)
                                        && !blockAppointmentTimes.Any(w => w.DockQoutaInterval_Index == interval.DockQoutaInterval_Index);
                                    }
                                    else
                                    {
                                        isEnalble = !interval.IsBreakTime
                                            && !appointmentItem.Any(w => w.DockQoutaInterval_Index == interval.DockQoutaInterval_Index)
                                            && !blockAppointmentTimes.Any(w => w.DockQoutaInterval_Index == interval.DockQoutaInterval_Index);
                                    }


                                    dock.Times.Add(new AppointmentQoutaIntervalModel()
                                    {
                                        DockQoutaInterval_Index = interval.DockQoutaInterval_Index,
                                        Seq = interval.Seq,
                                        Time_Start = interval.Interval_Start,
                                        Time_End = interval.Interval_End,
                                        Time = interval.Interval_Start + " - " + interval.Interval_End,
                                        IsEnable = isEnalble
                                    });
                                }
                            }
                        }
                    }

                    date = date.AddDays(1);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ListAppointment
        public AppointmentViewModel ListAppointment(string jsonData)
        {
            try
            {
                AppointmentSearchModel model = Appointment.GetAppointmentSearchModel(jsonData);
                var appoint = (from am in db.Tb_Appointment.AsQueryable()
                               join ami in db.Tb_AppointmentItem.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.Dock_Index.IsEquals(model.Dock_Index)) on am.Appointment_Index equals ami.Appointment_Index
                               select new
                               {
                                   am.Appointment_Index
                                   ,
                                   am.Appointment_Id
                                   ,
                                   am.DocumentType_Index
                                   ,
                                   am.DocumentType_Id
                                   ,
                                   am.DocumentType_Name
                                   ,
                                   am.Create_By
                                   ,
                                   am.Update_By
                                   ,
                                   am.Cancel_By
                                   ,
                                   am.Update_Date
                                   ,
                                   am.Create_Date
                                   ,
                                   am.Document_status
                                   ,
                                   ami.Appointment_Date
                                   ,
                                   ami.Appointment_Time
                                   ,
                                   ami.Dock_Name
                                   ,
                                   ami.Ref_Document_No
                                   ,
                                   ami.WareHouse_Index
                               }).GroupBy(c => new
                               {
                                   c.Appointment_Index
                                   ,
                                   c.Appointment_Id
                                   ,
                                   c.DocumentType_Index
                                   ,
                                   c.DocumentType_Id
                                   ,
                                   c.DocumentType_Name
                                   ,
                                   c.Create_By
                                   ,
                                   c.Update_By
                                   ,
                                   c.Cancel_By
                                   ,
                                   c.Create_Date
                                   ,
                                   c.Update_Date
                                   ,
                                   c.Document_status
                                   ,
                                   c.Appointment_Date
                                   ,
                                   c.Appointment_Time
                                   ,
                                   c.Ref_Document_No
                                   ,
                                   c.WareHouse_Index
                               }).Select(c => new
                               {
                                   c.Key.Appointment_Index
                                   ,
                                   c.Key.Appointment_Id
                                   ,
                                   c.Key.DocumentType_Index
                                   ,
                                   c.Key.DocumentType_Id
                                   ,
                                   c.Key.DocumentType_Name
                                   ,
                                   c.Key.Create_By
                                   ,
                                   c.Key.Update_By
                                   ,
                                   c.Key.Cancel_By
                                   ,
                                   c.Key.Create_Date
                                   ,
                                   c.Key.Update_Date
                                   ,
                                   c.Key.Document_status
                                   ,
                                   c.Key.Appointment_Date
                                   ,
                                   Time = DateTime.Parse(c.Key.Appointment_Time.Split('-')[0].Trim())
                                   ,
                                   c.Key.Appointment_Time
                                   ,
                                   Dock = string.Join(" , ", c.Select(w => w.Dock_Name))
                                   ,
                                   c.Key.Ref_Document_No
                                   ,
                                   c.Key.WareHouse_Index
                               }).OrderByDescending(c => c.Appointment_Date).ThenByDescending(c => c.Appointment_Time).ToList();

                #region Appointment_Id
                if (!string.IsNullOrEmpty(model.Appointment_Id))
                {
                    appoint = appoint.Where(c => c.Appointment_Id == model.Appointment_Id).ToList();
                }
                #endregion

                #region Appointment_Date
                if (model.date != null && model.date_To != null)
                {
                    //appoint = appoint.Where(w => w.Appointment_Date.DateBetweenCondition(model.Appointment_Date, model.Appointment_Date_To, true)).ToList();

                    var dateStart = model.date.toBetweenDate();
                    var dateEnd = model.date_To.toBetweenDate();
                    appoint = appoint.Where(c => c.Appointment_Date >= dateStart.start && c.Appointment_Date <= dateEnd.end).ToList();
                }
                else
                {
                    appoint = appoint.Where(w => w.Appointment_Date.DateBetweenCondition(DateTime.Now.TrimTime(), DateTime.Now.TrimTime().AddHours(23), true)).ToList();
                }
                #endregion

                #region ref_Document_No
                if (!string.IsNullOrEmpty(model.ref_Document_No))
                {
                    appoint = appoint.Where(c => c.Ref_Document_No == model.ref_Document_No).ToList();
                }
                #endregion

                var statusModels = new List<int?>();

                #region Status
                if (model.status.Count > 0)
                {
                    foreach (var status in model.status)
                    {
                        if (status.value != 2)
                        {
                            statusModels.Add(0);
                        }
                        else
                        {
                            statusModels.Add(1);
                            statusModels.Add(2);
                            statusModels.Add(3);
                            statusModels.Add(4);
                            statusModels.Add(5);
                        }
                    }
                }
                if (model.status_cancle.Count > 0)
                {
                    foreach (var status in model.status_cancle)
                    {
                        if (status.value == -1)
                        {
                            statusModels.Add(-1);
                        }
                        else
                        {
                            statusModels.Add(1);
                            statusModels.Add(2);
                            statusModels.Add(3);
                            statusModels.Add(4);
                            statusModels.Add(5);
                        }
                    }
                }

                if (statusModels.Count > 0)
                {
                    appoint = appoint.Where(c => statusModels.Contains(c.Document_status)).ToList();
                }
                #endregion

                //var statusModels = new List<int?>();
                var statusModels_InAndOut = new List<int?>();
                AppointmentViewModel result = new AppointmentViewModel();

                foreach (var app in appoint.OrderByDescending(c => c.Appointment_Date).ThenByDescending(x => (x.Time.TimeOfDay.TotalHours <= 10 ? 24.0 : 0.0) + x.Time.TimeOfDay.TotalHours).ThenByDescending(c => c.Dock))
                {
                    var car = db.Tb_AppointmentItemDetail.FirstOrDefault(c => c.Appointment_Index == app.Appointment_Index);
                    var Dock = db.Tb_AppointmentItem.Where(c => c.Appointment_Index == app.Appointment_Index).OrderBy(c => c.AppointmentItem_Id).FirstOrDefault();
                    Tb_YardBalance yardBalance = db.Tb_YardBalance.Where(c => c.Appointment_Index == app.Appointment_Index).FirstOrDefault();



                    AppointmentModel resultitem = new AppointmentModel();
                    resultitem.Appointment_Index = app.Appointment_Index;
                    resultitem.Appointment_Id = app.Appointment_Id;
                    resultitem.DocumentType_Index = app.DocumentType_Index;
                    resultitem.DocumentType_Id = app.DocumentType_Id;
                    resultitem.DocumentType_Name = app.DocumentType_Name;
                    resultitem.Create_By = app.Create_By;
                    resultitem.Create_By = app.Create_By;
                    resultitem.Update_By = app.Update_By;
                    resultitem.Cancel_By = app.Cancel_By;
                    resultitem.Create_Date = app.Create_Date;
                    resultitem.Update_Date = app.Update_Date;
                    resultitem.Document_status = app.Document_status;
                    if (yardBalance == null)
                    {
                        resultitem.Is_reassign = 0;
                    }
                    else
                    {
                        resultitem.Is_reassign = 1;
                    }

                    resultitem.Confirm_status = ApproveAppointment_status(app.Appointment_Index, app.Document_status);
                    resultitem.Appointment_Date = app.Appointment_Date;
                    resultitem.Appointment_Time = app.Appointment_Time;
                    resultitem.Dock_Index = Dock.Dock_Index;
                    resultitem.Dock_Id = Dock.Dock_Id;
                    resultitem.Dock_Name = app.Dock;
                    resultitem.Ref_Document_No = app.Ref_Document_No;
                    resultitem.WareHouse_Index = app.WareHouse_Index;
                    if (car != null)
                    {
                        resultitem.VehicleType_Name = car.VehicleType_Name;
                        resultitem.VehicleType_No = car.Vehicle_No;
                    }
                    result.AppointmentModels.Add(resultitem);
                }


                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region OLD
        //public AppointmentViewModel ListAppointment(string jsonData)
        //{
        //    try
        //    {
        //        AppointmentSearchModel model = Appointment.GetAppointmentSearchModel(jsonData);

        //        var appointmentItem = db.Tb_AppointmentItem.Where(
        //            w => w.IsActive == 1 &&
        //                 w.IsDelete == 0 &&
        //                 w.WareHouse_Index.IsEquals(model.WareHouse_Index) &&
        //                 w.Dock_Index.IsEquals(model.Dock_Index) &&
        //                 w.Appointment_Id.Like(model.Appointment_Id) &&
        //                 w.DocumentType_Index.IsEquals(model.DocumentType_Index)
        //        );
        //        if (model.Appointment_Date != null && model.Appointment_Date_To != null)
        //        {
        //            appointmentItem = appointmentItem.Where(w => w.Appointment_Date.DateBetweenCondition(model.Appointment_Date, model.Appointment_Date_To, true));
        //        }
        //        else
        //        {
        //            appointmentItem = appointmentItem.Where(w => w.Appointment_Date.DateBetweenCondition(DateTime.Now.TrimTime(), DateTime.Now.TrimTime().AddHours(23), true));
        //        }
        //        if (!string.IsNullOrEmpty(model.ref_Document_No))
        //        {
        //            appointmentItem = appointmentItem.Where(c => c.Ref_Document_No == model.ref_Document_No);
        //        }



        //        List<Tb_AppointmentItem> appointmentItemList = appointmentItem.ToList().GroupBy(g => g.Appointment_Index).Select(s => s.OrderBy(o => o.Appointment_Index).ThenBy(o => o.Appointment_Time).First()).ToList();


        //        List<Guid> listAppointmentIndex = appointmentItemList.Select(s => s.Appointment_Index).ToList();
        //        List<Tb_Appointment> appointmentQ = db.Tb_Appointment.Where(
        //            w => w.IsDelete == 0 &&
        //                 w.IsActive.IsEquals(model.IsActive) &&
        //                 w.Create_By.Like(model.Create_By) &&
        //                 w.Create_Date.DateBetweenCondition(model.Create_Date, model.Create_Date_To) &&
        //                 listAppointmentIndex.Contains(w.Appointment_Index)).ToList();

        //        if (model.status.Count > 0)
        //        {
        //            foreach (var status in model.status)
        //            {
        //                if (status.value != 2)
        //                {
        //                    appointmentQ = appointmentQ.Where(c => c.Document_status == status.value).ToList();
        //                }
        //                else
        //                {
        //                    appointmentQ = appointmentQ.Where(c => c.Document_status != 0).ToList();
        //                }
        //            }
        //        }

        //        var statusModels = new List<int?>();
        //        var statusModels_InAndOut = new List<int?>();


        //        AppointmentViewModel result = new AppointmentViewModel()
        //        {
        //            TotalRow = appointmentQ.Count,
        //            CurrentPage = model.CurrentPage,
        //            PerPage = model.PerPage
        //        };

        //        Tb_AppointmentItem filter;
        //        AppointmentModel item;

        //        foreach (Tb_Appointment app in appointmentQ)
        //        {
        //            item = JsonConvert.DeserializeObject<AppointmentModel>(JsonConvert.SerializeObject(app));
        //            filter = appointmentItem.FirstOrDefault(w => w.Appointment_Index == app.Appointment_Index);
        //            var dock = db.Tb_AppointmentItem.Where(c => !(c.Dock_Name == null || c.Dock_Name == string.Empty) && c.Appointment_Index == app.Appointment_Index).GroupBy(g => g.Dock_Name).ToList();
        //            if (filter == null) { continue; }
        //            var apmid = db.Tb_AppointmentItemDetail.FirstOrDefault(c => c.Appointment_Index == filter.Appointment_Index);
        //            if (apmid != null)
        //            {
        //                item.VehicleType_No = apmid.Vehicle_No;
        //            }


        //            item.Appointment_Date = filter.Appointment_Date;
        //            item.Appointment_Time = filter.Appointment_Time;
        //            item.WareHouse_Index = filter.WareHouse_Index;
        //            item.WareHouse_Id = filter.WareHouse_Id;
        //            item.WareHouse_Name = filter.WareHouse_Name;
        //            item.Dock_Index = filter.Dock_Index;
        //            item.Dock_Id = filter.Dock_Id;
        //            item.Dock_Name = string.Join(" , ", dock.Select(s => s.Key));
        //            item.Ref_Document_No = filter.Ref_Document_No;
        //            item.Ref_Document_Date = filter.Ref_Document_Date;
        //            item.Owner_Index = filter.Owner_Index;
        //            item.Owner_Id = filter.Owner_Id;
        //            item.Owner_Name = filter.Owner_Name;
        //            item.Document_status = item.Document_status;
        //            item.Confirm_status = ApproveAppointment_status(item.Appointment_Index, item.Document_status);
        //            item.VehicleType_Name = item.VehicleType_Name;
        //            result.AppointmentModels.Add(item);
        //        }


        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        #endregion

        #endregion

        #endregion

        #region + Get +

        #region  GetAppointment
        public AppointmentModel GetAppointment(string jsonData)
        {
            try
            {
                AppointmentSearchModel model = Appointment.GetAppointmentSearchModel(jsonData, true);
                Tb_Appointment appointment = db.Tb_Appointment.Find(model.Appointment_Index);

                if (appointment is null)
                {
                    throw new Exception("Appointment not found");
                }

                List<Tb_AppointmentItem> appointmentItems = db.Tb_AppointmentItem.Where(w => w.Appointment_Index == model.Appointment_Index.Value && w.IsActive == 1 && w.IsDelete == 0).ToList();

                AppointmentModel result = new AppointmentModel()
                {
                    Appointment_Index = appointment.Appointment_Index,
                    Appointment_Id = appointment.Appointment_Id,
                    DocumentType_Index = appointment.DocumentType_Index,
                    DocumentType_Id = appointment.DocumentType_Id,
                    DocumentType_Name = appointment.DocumentType_Name,
                    IsActive = appointment.IsActive,
                    IsDelete = appointment.IsDelete,
                    IsSystem = appointment.IsSystem,
                    Status_Id = appointment.Status_Id,
                    Create_By = appointment.Create_By,
                    Create_Date = appointment.Create_Date,
                    Update_By = appointment.Update_By,
                    Update_Date = appointment.Update_Date,
                    Cancel_By = appointment.Cancel_By,
                    Cancel_Date = appointment.Cancel_Date,
                    Document_status = appointment.Document_status,
                };

                appointmentItems.ForEach(e => { result.Items.Add(JsonConvert.DeserializeObject<AppointmentItemModel>(JsonConvert.SerializeObject(e))); });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  GetAppointmentGroup
        public AppointmentModel GetAppointmentGroup(string jsonData)
        {
            try
            {
                AppointmentSearchModel model = Appointment.GetAppointmentSearchModel(jsonData, true);
                Tb_Appointment appointment = db.Tb_Appointment.Find(model.Appointment_Index);

                if (appointment is null)
                {
                    throw new Exception("Appointment not found");
                }

                List<Tb_AppointmentItem> appointmentItems = db.Tb_AppointmentItem.Where(w => w.Appointment_Index == model.Appointment_Index.Value && w.IsActive == 1 && w.IsDelete == 0).ToList();
                List<Ms_DockQoutaInterval> dockIntervals = db.Ms_DockQoutaInterval.Where(w => appointmentItems.Select(s => s.DockQoutaInterval_Index).Distinct().Contains(w.DockQoutaInterval_Index)).ToList();

                AppointmentModel result = new AppointmentModel()
                {
                    Appointment_Index = appointment.Appointment_Index,
                    Appointment_Id = appointment.Appointment_Id,
                    DocumentType_Index = appointment.DocumentType_Index,
                    DocumentType_Id = appointment.DocumentType_Id,
                    DocumentType_Name = appointment.DocumentType_Name,
                    IsActive = appointment.IsActive,
                    IsDelete = appointment.IsDelete,
                    IsSystem = appointment.IsSystem,
                    Status_Id = appointment.Status_Id,
                    Create_By = appointment.Create_By,
                    Create_Date = appointment.Create_Date,
                    Update_By = appointment.Update_By,
                    Update_Date = appointment.Update_Date,
                    Cancel_By = appointment.Cancel_By,
                    Cancel_Date = appointment.Cancel_Date,
                    Document_status = appointment.Document_status,
                };

                List<Guid?> listGroupIndex = appointmentItems.Select(s => s.Group_Index).Distinct().ToList();

                List<Tb_AppointmentItem> itemGroup;
                int seq;
                foreach (Guid? groupIndex in listGroupIndex)
                {
                    itemGroup = appointmentItems.Where(w => w.Group_Index == groupIndex).ToList();
                    seq = 0;
                    var Dock = itemGroup.Where(c => !(c.Dock_Name == null || c.Dock_Name == string.Empty)).GroupBy(g => g.Dock_Name).ToList();
                    (from item in itemGroup
                     join interval in dockIntervals
                     on item.DockQoutaInterval_Index equals interval.DockQoutaInterval_Index
                     group new { item, interval }
                     by new
                     {
                         item.Appointment_Date
                         //,item.Dock_Index,
                         //item.Dock_Id,
                         //item.Dock_Name
                     } into appointmentGroup
                     orderby appointmentGroup.Key.Appointment_Date
                     //, appointmentGroup.Key.Dock_Id
                     select new
                     {
                         Group_Index = groupIndex,
                         //appointmentGroup.Key.Dock_Index,
                         //appointmentGroup.Key.Dock_Id,
                         //appointmentGroup.Key.Dock_Name,
                         appointmentGroup.Key.Appointment_Date,
                         Appointment_Time = appointmentGroup.Min(m => m.interval.Interval_Start) + " - " +
                                            appointmentGroup.Max(m => m.interval.Interval_End),
                         AppointmentItem = appointmentGroup.OrderByDescending(o => o.item.Update_Date ?? o.item.Create_Date).First().item
                     }).ToList().ForEach(e =>
                     {
                         seq++;

                         result.Items.Add(new AppointmentItemModel()
                         {

                             Seq = seq,
                             Group_Index = e.Group_Index,

                             DockQoutaInterval_Index = null,
                             YardBalance_Index = null,
                             AppointmentItem_Index = e.AppointmentItem.AppointmentItem_Index,
                             AppointmentItem_Id = null,

                             Appointment_Index = e.AppointmentItem.Appointment_Index,
                             Appointment_Id = e.AppointmentItem.Appointment_Id,
                             Appointment_Date = e.Appointment_Date,
                             Appointment_Time = e.Appointment_Time,

                             Ref_Document_No = e.AppointmentItem.Ref_Document_No,
                             Ref_Document_Date = e.AppointmentItem.Ref_Document_Date,
                             ContactPerson_Name = e.AppointmentItem.ContactPerson_Name,
                             ContactPerson_EMail = e.AppointmentItem.ContactPerson_EMail,
                             ContactPerson_Tel = e.AppointmentItem.ContactPerson_Tel,

                             //Dock_Index = e.Dock_Index,
                             //Dock_Id = e.Dock_Id,
                             Dock_Name = string.Join(" , ", Dock.Select(s => s.Key)),

                             Owner_Index = e.AppointmentItem.Owner_Index,
                             Owner_Id = e.AppointmentItem.Owner_Id,
                             Owner_Name = e.AppointmentItem.Owner_Name,

                             WareHouse_Index = e.AppointmentItem.WareHouse_Index,
                             WareHouse_Id = e.AppointmentItem.WareHouse_Id,
                             WareHouse_Name = e.AppointmentItem.WareHouse_Name,

                             DocumentType_Index = e.AppointmentItem.DocumentType_Index,
                             DocumentType_Id = e.AppointmentItem.DocumentType_Id,
                             DocumentType_Name = e.AppointmentItem.DocumentType_Name,

                             Remark = e.AppointmentItem.Remark,
                             IsActive = e.AppointmentItem.IsActive,
                             IsDelete = e.AppointmentItem.IsDelete,
                             IsSystem = e.AppointmentItem.IsSystem,
                             Status_Id = e.AppointmentItem.Status_Id,

                             Create_By = e.AppointmentItem.Update_By ?? e.AppointmentItem.Create_By,
                             Create_Date = e.AppointmentItem.Update_Date ?? e.AppointmentItem.Create_Date
                         });
                     });
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetAppointmentItem
        public AppointmentItemModel GetAppointmentItem(string jsonData)
        {
            try
            {
                AppointmentItemSearchModel model = Appointment.GetAppointmentItemSearchModel(jsonData, true);

                Tb_AppointmentItem appointmentItem = db.Tb_AppointmentItem.Find(model.AppointmentItem_Index);

                if (appointmentItem is null) { throw new Exception("AppointmentItem not found"); }

                List<Tb_AppointmentItemDetail> details = db.Tb_AppointmentItemDetail.Where(w => w.AppointmentItem_Index == appointmentItem.AppointmentItem_Index).ToList();

                AppointmentItemModel result = JsonConvert.DeserializeObject<AppointmentItemModel>(JsonConvert.SerializeObject(appointmentItem));
                details.ForEach(e => { result.Details.Add(JsonConvert.DeserializeObject<AppointmentItemDetailModel>(JsonConvert.SerializeObject(e))); });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetAppointmentItemGroup
        public AppointmentItemModel GetAppointmentItemGroup(string jsonData)
        {
            try
            {
                AppointmentItemSearchModel model = Appointment.GetAppointmentItemSearchModel(jsonData);
                if (!model.Group_Index.HasValue) { throw new Exception("GroupIndex not found"); }

                Tb_AppointmentItem appointmentItem = db.Tb_AppointmentItem.FirstOrDefault(w => w.Group_Index == model.Group_Index.Value);

                if (appointmentItem is null) { throw new Exception("AppointmentItem not found"); }

                List<Tb_AppointmentItemDetail> details = db.Tb_AppointmentItemDetail.Where(w => w.AppointmentItem_Index == appointmentItem.AppointmentItem_Index && w.IsActive == 1 && w.IsDelete == 0).ToList();
                List<tb_BlockAppointmentTime> blockAppointmentTimes = db.tb_BlockAppointmentTime.Where(w => w.AppointmentItem_Index == appointmentItem.AppointmentItem_Index).ToList();

                AppointmentItemModel result = new AppointmentItemModel()
                {
                    Seq = 1,
                    Group_Index = appointmentItem.Group_Index,

                    DockQoutaInterval_Index = null,
                    YardBalance_Index = null,
                    AppointmentItem_Index = null,
                    AppointmentItem_Id = null,

                    Appointment_Index = appointmentItem.Appointment_Index,
                    Appointment_Id = appointmentItem.Appointment_Id,
                    Appointment_Date = appointmentItem.Appointment_Date,
                    Appointment_Time = appointmentItem.Appointment_Time,

                    Ref_Document_No = appointmentItem.Ref_Document_No,
                    Ref_Document_Date = appointmentItem.Ref_Document_Date,
                    ContactPerson_Name = appointmentItem.ContactPerson_Name,
                    ContactPerson_EMail = appointmentItem.ContactPerson_EMail,
                    ContactPerson_Tel = appointmentItem.ContactPerson_Tel,

                    Dock_Index = appointmentItem.Dock_Index,
                    Dock_Id = appointmentItem.Dock_Id,
                    Dock_Name = appointmentItem.Dock_Name,

                    Owner_Index = appointmentItem.Owner_Index,
                    Owner_Id = appointmentItem.Owner_Id,
                    Owner_Name = appointmentItem.Owner_Name,

                    WareHouse_Index = appointmentItem.WareHouse_Index,
                    WareHouse_Id = appointmentItem.WareHouse_Id,
                    WareHouse_Name = appointmentItem.WareHouse_Name,

                    DocumentType_Index = appointmentItem.DocumentType_Index,
                    DocumentType_Id = appointmentItem.DocumentType_Id,
                    DocumentType_Name = appointmentItem.DocumentType_Name,

                    Remark = appointmentItem.Remark,
                    IsActive = appointmentItem.IsActive,
                    IsDelete = appointmentItem.IsDelete,
                    IsSystem = appointmentItem.IsSystem,
                    Status_Id = appointmentItem.Status_Id,

                    Create_By = appointmentItem.Update_By ?? appointmentItem.Create_By,
                    Create_Date = appointmentItem.Update_Date ?? appointmentItem.Create_Date
                };

                details.ForEach(e =>
                {
                    result.Details.Add(new AppointmentItemDetailModel()
                    {
                        AppointmentItemDetail_Index = null,
                        AppointmentItem_Index = null,
                        Appointment_Index = appointmentItem.Appointment_Index,
                        Appointment_Id = appointmentItem.Appointment_Id,
                        VehicleType_Index = e.VehicleType_Index,
                        VehicleType_Id = e.VehicleType_Id,
                        VehicleType_Name = e.VehicleType_Name,
                        Vehicle_No = e.Vehicle_No,
                        Driver_Index = e.Driver_Index,
                        Driver_Id = e.Driver_Id,
                        Driver_Name = e.Driver_Name,

                        IsActive = e.IsActive,
                        IsDelete = e.IsDelete,
                        IsSystem = e.IsSystem,
                        Status_Id = e.Status_Id,
                        Create_By = e.Update_By ?? e.Create_By,
                        Create_Date = e.Update_Date ?? e.Create_Date
                    });
                });
                int runing = 1;
                foreach (var item in blockAppointmentTimes.OrderBy(c => c.Seq))
                {
                    datarow data = new datarow();
                    DockQoutaIntervalBreakTime dockQoutaIntervalBreak = new DockQoutaIntervalBreakTime();
                    data.dockQoutaInterval_Index = item.DockQoutaInterval_Index;
                    data.time_Start = item.Interval_Start;
                    data.time_End = item.Interval_End;
                    data.time = item.Interval_Start + " - " + item.Interval_End;
                    data.seq = item.Seq;

                    dockQoutaIntervalBreak.datarow = data;
                    dockQoutaIntervalBreak.index = runing;

                    result.DockQoutaIntervalBreakTime.Add(dockQoutaIntervalBreak);
                    runing++;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #region Approve

        #region + Check_Approve +
        public string ApproveAppointment_status(Guid? Appointment, int? document_status)
        {
            try
            {
                if (document_status == 0)
                {
                    List<Tb_AppointmentItem> item = db.Tb_AppointmentItem.Where(c => c.Appointment_Index == Appointment && c.Ref_Document_No == null && c.Ref_Document_Date == null && c.Owner_Index == null && c.IsActive == 1).ToList();
                    if (item.Count > 0) { return "Incomplete"; }
                    List<Tb_AppointmentItem> itemCheck = db.Tb_AppointmentItem.Where(c => c.Appointment_Index == Appointment && c.Ref_Document_No != null && c.Ref_Document_Date != null && c.Owner_Index != null && c.IsActive == 1).ToList();
                    foreach (Tb_AppointmentItem checkIttem in itemCheck)
                    {
                        bool itemdetail = db.Tb_AppointmentItemDetail.Any(c => c.AppointmentItem_Index == checkIttem.AppointmentItem_Index && c.IsActive == 1);
                        if (!itemdetail)
                        {
                            return "Incomplete";
                        }

                    }
                    return "Pending";
                }
                else
                {
                    return "Success";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region + Confirm Approve +
        public bool Confirm_Approve(string jsonData)
        {
            try
            {
                AppointmentSearchModel model = Appointment.GetAppointmentSearchModel(jsonData);
                string check = ApproveAppointment_status(model.Appointment_Index, 0);
                string userBy = model.Create_By;
                DateTime userDate = DateTime.Now;
                if (check != "Incomplete")
                {
                    Tb_Appointment appointment = db.Tb_Appointment.Find(model.Appointment_Index);
                    appointment.Document_status = 5;
                    appointment.Update_By = userBy;
                    appointment.Update_Date = userDate;

                    model.DocumentType_Index = appointment.DocumentType_Index;

                    if (!string.IsNullOrEmpty(model.planGoodsReceive_Index))
                    {
                        var result_api = Utils.SendDataApi<bool>(new AppSettingConfig().GetUrl("confirmStatus"), JsonConvert.SerializeObject(model));
                        if (!result_api)
                        {
                            return false;
                        }
                        else
                        {
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
                        }
                    }
                    else
                    {
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
                    }

                }
                else { return false; }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #region + Save +

        #region SaveAppointment
        public Guid SaveAppointment(string jsonData)
        {
            try
            {
                AppointmentModel model = Appointment.GetAppointmentModel(jsonData);
                if ((model.Items?.Count ?? 0) == 0) { throw new Exception("AppointmentItem not found"); }

                Tb_Appointment appointment = db.Tb_Appointment.Find(model.Appointment_Index);

                Guid appointmentIndex;
                string appointmentId, appointmentItemId, userBy;
                DateTime userDate = DateTime.Now;

                if (appointment is null)
                {
                    appointmentIndex = Guid.NewGuid();
                    appointmentId = GetDocumentNumber(model.DocumentType_Index, DateTime.Now);
                    userBy = model.Create_By;

                    Tb_Appointment newAppointment = new Tb_Appointment
                    {
                        Appointment_Index = appointmentIndex,
                        Appointment_Id = appointmentId,

                        DocumentType_Index = model.DocumentType_Index,
                        DocumentType_Id = model.DocumentType_Id,
                        DocumentType_Name = model.DocumentType_Name,

                        IsActive = model.IsActive ?? 1,
                        IsDelete = 0,
                        IsSystem = 0,
                        Status_Id = 0,
                        Create_By = userBy,
                        Create_Date = userDate,
                        Document_status = 0
                    };

                    db.Tb_Appointment.Add(newAppointment);
                }
                else
                {
                    appointmentIndex = appointment.Appointment_Index;
                    appointmentId = appointment.Appointment_Id;
                    userBy = model.Update_By;

                    appointment.IsActive = model.IsActive;
                    appointment.Update_By = userBy;
                    appointment.Update_Date = userDate;
                }

                Guid groupIndex = Guid.NewGuid();

                Tb_AppointmentItem appointmentItem, newAppointmentItem;
                foreach (AppointmentItemModel item in model.Items)
                {
                    if (!model.IsGroup)
                    {
                        groupIndex = Guid.NewGuid();
                    }

                    appointmentItemId = appointmentId + "-" + model.Items.IndexOf(item).ToString();
                    appointmentItem = db.Tb_AppointmentItem.Find(item.AppointmentItem_Index);
                    if (appointmentItem is null)
                    {
                        newAppointmentItem = new Tb_AppointmentItem
                        {
                            AppointmentItem_Index = Guid.NewGuid(),
                            AppointmentItem_Id = appointmentItemId,
                            Appointment_Index = appointmentIndex,
                            Appointment_Id = appointmentId,

                            DocumentType_Index = model.DocumentType_Index,
                            DocumentType_Id = model.DocumentType_Id,
                            DocumentType_Name = model.DocumentType_Name,

                            Appointment_Date = item.Appointment_Date,
                            Appointment_Time = item.Appointment_Time,

                            DockQoutaInterval_Index = item.DockQoutaInterval_Index.Value,
                            WareHouse_Index = item.WareHouse_Index,
                            WareHouse_Id = item.WareHouse_Id,
                            WareHouse_Name = item.WareHouse_Name,

                            Group_Index = groupIndex,

                            Dock_Index = item.Dock_Index,
                            Dock_Id = item.Dock_Id,
                            Dock_Name = item.Dock_Name,

                            IsActive = model.IsActive ?? 1,
                            IsDelete = 0,
                            IsSystem = 0,
                            Status_Id = 0,
                            Create_By = userBy,
                            Create_Date = userDate
                        };

                        db.Tb_AppointmentItem.Add(newAppointmentItem);

                        if (model.VehicleType_Index == Guid.Parse("73D42B62-92F1-44D0-85F3-A413DCE7CD84"))
                        {
                            List<Guid> index_Dock18 = new List<Guid>
                            {
                                Guid.Parse("FDCADA40-A6E7-4B6A-A822-C1C95534C564"),
                                Guid.Parse("406AE16D-DEA0-45A2-89EA-753052CD0811")
                            };
                            List<Guid> index_Dock21 = new List<Guid>
                            {
                                Guid.Parse("0E79E1E0-51E4-4357-A529-1C71D5D0F452"),
                                Guid.Parse("202FA423-573C-4FEA-B7A8-E8039675B365")
                            };
                            if (item.Dock_Index == Guid.Parse("9AB5DAD5-7331-41AD-9E90-635EC507CE93"))
                            {
                                foreach (var itemDock in index_Dock18)
                                {
                                    var dock = dbMS.Ms_Dock.FirstOrDefault(c => c.Dock_Index == itemDock);
                                    var time = item.Appointment_Time.Split('-');
                                    var DQI = db.Ms_DockQoutaInterval.FirstOrDefault(c => c.Dock_Index == itemDock && c.Interval_Start == time[0].Trim() && c.Interval_End == time[1].Trim());
                                    newAppointmentItem = new Tb_AppointmentItem
                                    {
                                        AppointmentItem_Index = Guid.NewGuid(),
                                        AppointmentItem_Id = appointmentItemId,
                                        Appointment_Index = appointmentIndex,
                                        Appointment_Id = appointmentId,

                                        DocumentType_Index = model.DocumentType_Index,
                                        DocumentType_Id = model.DocumentType_Id,
                                        DocumentType_Name = model.DocumentType_Name,

                                        Appointment_Date = item.Appointment_Date,
                                        Appointment_Time = item.Appointment_Time,

                                        DockQoutaInterval_Index = DQI.DockQoutaInterval_Index,
                                        WareHouse_Index = item.WareHouse_Index,
                                        WareHouse_Id = item.WareHouse_Id,
                                        WareHouse_Name = item.WareHouse_Name,

                                        Group_Index = groupIndex,

                                        Dock_Index = dock.Dock_Index,
                                        Dock_Id = dock.Dock_Id,
                                        Dock_Name = dock.Dock_Name,

                                        IsActive = model.IsActive ?? 1,
                                        IsDelete = 0,
                                        IsSystem = 0,
                                        Status_Id = 0,
                                        Create_By = userBy,
                                        Create_Date = userDate
                                    };

                                    db.Tb_AppointmentItem.Add(newAppointmentItem);
                                }

                            }
                            else if (item.Dock_Index == Guid.Parse("43E4B9C5-9428-4235-AF04-B541686D83B7"))
                            {
                                foreach (var itemDock in index_Dock21)
                                {
                                    var dock = dbMS.Ms_Dock.FirstOrDefault(c => c.Dock_Index == itemDock);
                                    var time = item.Appointment_Time.Split('-');
                                    var DQI = db.Ms_DockQoutaInterval.FirstOrDefault(c => c.Dock_Index == itemDock && c.Interval_Start == time[0] && c.Interval_End == time[1]);
                                    newAppointmentItem = new Tb_AppointmentItem
                                    {
                                        AppointmentItem_Index = Guid.NewGuid(),
                                        AppointmentItem_Id = appointmentItemId,
                                        Appointment_Index = appointmentIndex,
                                        Appointment_Id = appointmentId,

                                        DocumentType_Index = model.DocumentType_Index,
                                        DocumentType_Id = model.DocumentType_Id,
                                        DocumentType_Name = model.DocumentType_Name,

                                        Appointment_Date = item.Appointment_Date,
                                        Appointment_Time = item.Appointment_Time,

                                        DockQoutaInterval_Index = DQI.DockQoutaInterval_Index,
                                        WareHouse_Index = item.WareHouse_Index,
                                        WareHouse_Id = item.WareHouse_Id,
                                        WareHouse_Name = item.WareHouse_Name,

                                        Group_Index = groupIndex,

                                        Dock_Index = dock.Dock_Index,
                                        Dock_Id = dock.Dock_Id,
                                        Dock_Name = dock.Dock_Name,

                                        IsActive = model.IsActive ?? 1,
                                        IsDelete = 0,
                                        IsSystem = 0,
                                        Status_Id = 0,
                                        Create_By = userBy,
                                        Create_Date = userDate
                                    };

                                    db.Tb_AppointmentItem.Add(newAppointmentItem);

                                }
                            }
                        }
                    }
                    else
                    {
                        //appointmentItem.Ref_Document_No = item.Ref_Document_No;
                        //appointmentItem.Ref_Document_Date = item.Ref_Document_Date;
                        //appointmentItem.VehicleType_Index = item.VehicleType_Index;
                        //appointmentItem.VehicleType_Id = item.VehicleType_Id;
                        //appointmentItem.VehicleType_Name = item.VehicleType_Name;
                        //appointmentItem.Vehicle_No = item.Vehicle_No;
                        //appointmentItem.Driver_Index = item.Driver_Index;
                        //appointmentItem.Driver_Id = item.Driver_Id;
                        //appointmentItem.Driver_Name = item.Driver_Name;
                        //appointmentItem.ContactPerson_Name = item.ContactPerson_Name;
                        //appointmentItem.ContactPerson_EMail = item.ContactPerson_EMail;
                        //appointmentItem.ContactPerson_Tel = item.ContactPerson_Tel;
                        //appointmentItem.Remark = item.Remark;

                        //appointmentItem.IsActive = item.IsActive;
                        //appointmentItem.Update_By = userBy;
                        //appointmentItem.Update_Date = userDate;
                    }
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

                return appointmentIndex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveAppointmentItem
        public Tb_AppointmentItem SaveAppointmentItem(string jsonData)
        {
            try
            {
                AppointmentItemModel model = Appointment.GetAppointmentItemModel(jsonData);
                Tb_AppointmentItem appointmentItem = db.Tb_AppointmentItem.Find(model.AppointmentItem_Index);

                string userBy = model.Create_By;
                DateTime userDate = DateTime.Now;

                if (appointmentItem is null) { throw new Exception("AppointmentItem not found"); }
                if (model.Ref_Document_No is null) { throw new Exception("Document No not found"); }
                if (model.Ref_Document_Date is null) { throw new Exception("Document Date not found"); }
                if (model.Owner_Index is null) { throw new Exception("Owner not found"); }
                if (model.Owner_Id is null) { throw new Exception("Owner not found"); }
                if (model.Owner_Name is null) { throw new Exception("Owner not found"); }

                appointmentItem.Ref_Document_No = model.Ref_Document_No;
                appointmentItem.Ref_Document_Date = model.Ref_Document_Date;
                appointmentItem.Owner_Index = model.Owner_Index;
                appointmentItem.Owner_Id = model.Owner_Id;
                appointmentItem.Owner_Name = model.Owner_Name;
                appointmentItem.ContactPerson_Name = model.ContactPerson_Name;
                appointmentItem.ContactPerson_EMail = model.ContactPerson_EMail;
                appointmentItem.ContactPerson_Tel = model.ContactPerson_Tel;
                appointmentItem.Remark = model.Remark;

                appointmentItem.IsActive = model.IsActive ?? 1;
                appointmentItem.Update_By = userBy;
                appointmentItem.Update_Date = userDate;

                var Vehicle = db.tb_VehicleDock.FirstOrDefault(c => c.Dock_Index == appointmentItem.Dock_Index);

                Tb_AppointmentItemDetail appointmentItemDetail;
                foreach (AppointmentItemDetailModel detail in model.Details)
                {
                    //if (detail.VehicleType_Name is null) { throw new Exception("VehicleType not found"); }
                    //if (detail.Vehicle_No is null) { throw new Exception("Vehicle No not found"); }


                    appointmentItemDetail = db.Tb_AppointmentItemDetail.Find(detail.AppointmentItemDetail_Index);
                    if (appointmentItemDetail is null)
                    {
                        appointmentItemDetail = new Tb_AppointmentItemDetail()
                        {
                            AppointmentItemDetail_Index = Guid.NewGuid(),
                            AppointmentItem_Index = appointmentItem.AppointmentItem_Index,
                            Appointment_Index = appointmentItem.Appointment_Index,
                            Appointment_Id = appointmentItem.Appointment_Id,
                            VehicleType_Index = Vehicle.VehicleType_Index,
                            VehicleType_Id = Vehicle.VehicleType_Id,
                            VehicleType_Name = Vehicle.VehicleType_Name,
                            Vehicle_No = detail.Vehicle_No,
                            Driver_Index = detail.Driver_Index,
                            Driver_Id = detail.Driver_Id,
                            Driver_Name = detail.Driver_Name,

                            IsActive = detail.IsActive ?? 1,
                            IsDelete = 0,
                            IsSystem = 0,
                            Status_Id = 0,
                            Create_By = userBy,
                            Create_Date = userDate
                        };

                        db.Tb_AppointmentItemDetail.Add(appointmentItemDetail);
                    }
                    else
                    {
                        appointmentItemDetail.VehicleType_Index = detail.VehicleType_Index;
                        appointmentItemDetail.VehicleType_Id = detail.VehicleType_Id;
                        appointmentItemDetail.VehicleType_Name = detail.VehicleType_Name;
                        appointmentItemDetail.Vehicle_No = detail.Vehicle_No;
                        appointmentItemDetail.Driver_Index = detail.Driver_Index;
                        appointmentItemDetail.Driver_Id = detail.Driver_Id;
                        appointmentItemDetail.Driver_Name = detail.Driver_Name;

                        appointmentItemDetail.IsActive = detail.IsActive ?? 1;
                        appointmentItemDetail.Update_By = userBy;
                        appointmentItemDetail.Update_Date = userDate;
                    }
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

                return appointmentItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SaveAppointmentItemGroup
        public Result SaveAppointmentItemGroup(string jsonData)
        {
            Logtxt LoggingService = new Logtxt();
            Result result = new Result();
            try
            {
                AppointmentItemModel model = Appointment.GetAppointmentItemModel(jsonData);
                if (!model.Group_Index.HasValue) { throw new Exception("GroupIndex not found"); }
                if (model.Ref_Document_No is null) { throw new Exception("Document No not found"); }
                if (model.Ref_Document_Date is null) { throw new Exception("Document Date not found"); }
                if (model.Owner_Index is null) { throw new Exception("Owner not found"); }
                if (model.Owner_Id is null) { throw new Exception("Owner not found"); }
                if (model.Owner_Name is null) { throw new Exception("Owner not found"); }

                LoggingService.DataLogLines("SaveAppointmentItemGroup", "SaveAppointmentItemGroup" + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "Start" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                LoggingService.DataLogLines("SaveAppointmentItemGroup", "SaveAppointmentItemGroup" + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "Group_Index" + model.Group_Index + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                List<Tb_AppointmentItem> appointmentItems = db.Tb_AppointmentItem.Where(w => w.Group_Index == model.Group_Index).ToList();
                LoggingService.DataLogLines("SaveAppointmentItemGroup", "SaveAppointmentItemGroup" + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "appointmentItems" + appointmentItems.Count() + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                if ((appointmentItems?.Count ?? 0) == 0) { throw new Exception("AppointmentItem not found"); }

                string userBy = model.Create_By;
                DateTime userDate = DateTime.Now;

                List<Tb_AppointmentItemDetail> newAppointmentItemDetails, delAppointmentItemDetails;
                List<tb_BlockAppointmentTime> newBlockAppointmentTime;
                foreach (Tb_AppointmentItem appointmentItem in appointmentItems)
                {
                    appointmentItem.Ref_Document_No = model.Ref_Document_No;
                    appointmentItem.Ref_Document_Date = model.Ref_Document_Date;
                    appointmentItem.Owner_Index = model.Owner_Index;
                    appointmentItem.Owner_Id = model.Owner_Id;
                    appointmentItem.Owner_Name = model.Owner_Name;
                    appointmentItem.ContactPerson_Name = model.ContactPerson_Name;
                    appointmentItem.ContactPerson_EMail = model.ContactPerson_EMail;
                    appointmentItem.ContactPerson_Tel = model.ContactPerson_Tel;
                    appointmentItem.Remark = model.Remark;

                    appointmentItem.IsActive = model.IsActive ?? 1;
                    appointmentItem.Update_By = userBy;
                    appointmentItem.Update_Date = userDate;

                    delAppointmentItemDetails = db.Tb_AppointmentItemDetail.Where(w => w.AppointmentItem_Index == appointmentItem.AppointmentItem_Index).ToList();
                    if ((delAppointmentItemDetails?.Count ?? 0) > 0)
                    {
                        db.Tb_AppointmentItemDetail.RemoveRange(delAppointmentItemDetails);
                    }
                    LoggingService.DataLogLines("SaveAppointmentItemGroup", "SaveAppointmentItemGroup" + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "appointmentItem.Dock_Index" + appointmentItem.Dock_Index + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                    var Vehicle = db.tb_VehicleDock.FirstOrDefault(c => c.Dock_Index == appointmentItem.Dock_Index);
                    LoggingService.DataLogLines("SaveAppointmentItemGroup", "SaveAppointmentItemGroup" + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "appointmentItem.Dock_Index" + JsonConvert.SerializeObject(Vehicle) + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                    if ((model.Details?.Count ?? 0) > 0)
                    {
                        newAppointmentItemDetails = new List<Tb_AppointmentItemDetail>();
                        foreach (AppointmentItemDetailModel detail in model.Details)
                        {

                            newAppointmentItemDetails.Add(new Tb_AppointmentItemDetail()
                            {
                                AppointmentItemDetail_Index = Guid.NewGuid(),
                                AppointmentItem_Index = appointmentItem.AppointmentItem_Index,
                                Appointment_Index = appointmentItem.Appointment_Index,
                                Appointment_Id = appointmentItem.Appointment_Id,
                                VehicleType_Index = Vehicle.VehicleType_Index,
                                VehicleType_Id = Vehicle.VehicleType_Id,
                                VehicleType_Name = Vehicle.VehicleType_Name,
                                Vehicle_No = detail.Vehicle_No,
                                Driver_Index = detail.Driver_Index,
                                Driver_Id = detail.Driver_Id,
                                Driver_Name = detail.Driver_Name,

                                IsActive = detail.IsActive ?? 1,
                                IsDelete = 0,
                                IsSystem = 0,
                                Status_Id = 0,
                                Create_By = userBy,
                                Create_Date = userDate
                            });
                        }

                        db.Tb_AppointmentItemDetail.AddRange(newAppointmentItemDetails);
                    }
                    LoggingService.DataLogLines("SaveAppointmentItemGroup", "SaveAppointmentItemGroup" + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "S.1" + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                    if ((model.DockQoutaIntervalBreakTime?.Count ?? 0) > 0)
                    {
                        Ms_DockQoutaInterval dockQoutaInterval = db.Ms_DockQoutaInterval.FirstOrDefault(c => c.DockQoutaInterval_Index == appointmentItem.DockQoutaInterval_Index);
                        int runing = 0;
                        for (int i = dockQoutaInterval.Seq; i < dockQoutaInterval.Seq + model.DockQoutaIntervalBreakTime.Count; i++)
                        {
                            if (model.DockQoutaIntervalBreakTime[runing].datarow.seq != dockQoutaInterval.Seq + 1 + runing)
                            {
                                result.ResultIsUse = false;
                                result.ResultMsg = "กรุณาเลือกช่วงเวลาต่อเนื่องกัน";
                                return result;
                            }
                            runing++;
                        }

                        List<tb_BlockAppointmentTime> blockAppointmentTimes = db.tb_BlockAppointmentTime.Where(c => c.AppointmentItem_Index == appointmentItem.AppointmentItem_Index).ToList();
                        if ((blockAppointmentTimes?.Count ?? 0) > 0)
                        {
                            db.tb_BlockAppointmentTime.RemoveRange(blockAppointmentTimes);
                        }

                        newBlockAppointmentTime = new List<tb_BlockAppointmentTime>();
                        LoggingService.DataLogLines("SaveAppointmentItemGroup", "SaveAppointmentItemGroup" + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "S.2" + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                        foreach (var item in model.DockQoutaIntervalBreakTime)
                        {

                            newBlockAppointmentTime.Add(new tb_BlockAppointmentTime()
                            {
                                Block_index = Guid.NewGuid(),
                                Appointment_Index = appointmentItem.Appointment_Index,
                                AppointmentItem_Index = appointmentItem.AppointmentItem_Index,
                                Appointment_Id = appointmentItem.Appointment_Id,
                                AppointmentItem_Id = appointmentItem.AppointmentItem_Id,
                                Appointment_Date = appointmentItem.Appointment_Date,
                                Appointment_Time = item.datarow.time,
                                DockQoutaInterval_Index = item.datarow.dockQoutaInterval_Index.GetValueOrDefault(),
                                Interval_Start = item.datarow.time_Start,
                                Interval_End = item.datarow.time_End,
                                Seq = item.datarow.seq,
                                Dock_Index = appointmentItem.Dock_Index,
                                Dock_Id = appointmentItem.Dock_Id,
                                Dock_Name = appointmentItem.Dock_Name,
                                Create_By = userBy,
                                Create_Date = DateTime.Now
                            });
                        }
                        db.tb_BlockAppointmentTime.AddRange(newBlockAppointmentTime);
                    }
                    LoggingService.DataLogLines("SaveAppointmentItemGroup", "SaveAppointmentItemGroup" + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "S.3" + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                    if (model.DocumentType_Index != Guid.Parse("c392d865-8e69-4985-b72f-2421ebe8bcdb"))
                    {
                        var ref_planGR = dbGR.IM_PlanGoodsReceive.FirstOrDefault(c => c.PlanGoodsReceive_No == appointmentItem.Ref_Document_No);
                        if (ref_planGR != null)
                        {
                            ref_planGR.Dock_Name = appointmentItem.Dock_Name;
                            ref_planGR.Dock_Id = appointmentItem.Dock_Id;
                            ref_planGR.Dock_Index = appointmentItem.Dock_Index;
                            ref_planGR.Appointment_id = appointmentItem.Appointment_Id;
                        }

                    }

                }

                var myTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    LoggingService.DataLogLines("SaveAppointmentItemGroup", "SaveAppointmentItemGroup" + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "SSS" + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                    db.SaveChanges();
                    myTransaction.Commit();
                }
                catch (Exception saveEx)
                {
                    LoggingService.DataLogLines("SaveAppointmentItemGroup", "SaveAppointmentItemGroup" + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "SSS : EX" + JsonConvert.SerializeObject(saveEx) + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                    myTransaction.Rollback();
                    result.ResultIsUse = false;
                    result.ResultMsg = saveEx.Message;
                    return result;
                }
                var myTransactionGR = dbGR.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    LoggingService.DataLogLines("SaveAppointmentItemGroup", "SaveAppointmentItemGroup" + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "AAA" + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                    dbGR.SaveChanges();
                    myTransactionGR.Commit();
                }
                catch (Exception saveEx)
                {
                    LoggingService.DataLogLines("SaveAppointmentItemGroup", "SaveAppointmentItemGroup" + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "AAA : EX" + JsonConvert.SerializeObject(saveEx) + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                    myTransactionGR.Rollback();
                    result.ResultIsUse = false;
                    result.ResultMsg = saveEx.Message;
                    return result;
                }
                result.ResultIsUse = true;
                return result;
            }
            catch (Exception ex)
            {
                LoggingService.DataLogLines("SaveAppointmentItemGroup", "SaveAppointmentItemGroup" + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "XXX : EX" + JsonConvert.SerializeObject(ex) + "_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                throw ex;
            }
        }
        #endregion

        #region SaveAppointmentItemDetail
        public Tb_AppointmentItemDetail SaveAppointmentItemDetail(string jsonData)
        {
            try
            {
                AppointmentItemDetailModel model = Appointment.GetAppointmentItemDetailModel(jsonData);
                Tb_AppointmentItemDetail appointmentItemDetail = db.Tb_AppointmentItemDetail.Find(model.AppointmentItemDetail_Index);

                string userBy = model.Create_By;
                DateTime userDate = DateTime.Now;

                if (appointmentItemDetail is null)
                {
                    appointmentItemDetail = new Tb_AppointmentItemDetail()
                    {
                        AppointmentItemDetail_Index = Guid.NewGuid(),
                        AppointmentItem_Index = model.AppointmentItem_Index.Value,
                        Appointment_Index = model.Appointment_Index,
                        Appointment_Id = model.Appointment_Id,
                        VehicleType_Index = model.VehicleType_Index,
                        VehicleType_Id = model.VehicleType_Id,
                        VehicleType_Name = model.VehicleType_Name,
                        Vehicle_No = model.Vehicle_No,
                        Driver_Index = model.Driver_Index,
                        Driver_Id = model.Driver_Id,
                        Driver_Name = model.Driver_Name,

                        IsActive = model.IsActive ?? 1,
                        IsDelete = 0,
                        IsSystem = 0,
                        Status_Id = 0,
                        Create_By = userBy,
                        Create_Date = userDate
                    };

                    db.Tb_AppointmentItemDetail.Add(appointmentItemDetail);
                }
                else
                {
                    appointmentItemDetail.VehicleType_Index = model.VehicleType_Index;
                    appointmentItemDetail.VehicleType_Id = model.VehicleType_Id;
                    appointmentItemDetail.VehicleType_Name = model.VehicleType_Name;
                    appointmentItemDetail.Vehicle_No = model.Vehicle_No;
                    appointmentItemDetail.Driver_Index = model.Driver_Index;
                    appointmentItemDetail.Driver_Id = model.Driver_Id;
                    appointmentItemDetail.Driver_Name = model.Driver_Name;

                    appointmentItemDetail.IsActive = model.IsActive;
                    appointmentItemDetail.Update_By = userBy;
                    appointmentItemDetail.Update_Date = userDate;
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

                return appointmentItemDetail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region  SaveAppointment Auto out
        public string SaveAppointment_Auto_out()
        {
            Logtxt LoggingService = new Logtxt();
            try
            {
                LoggingService.DataLogLines("SaveAppointment_Auto_out", "SaveAppointment_Auto_out" + DateTime.Now.ToString("yyyy-MM-dd"), "Start Booking" + DateTime.Now.ToString("yyyy-MM-dd-HHmm"));
                string mess = "";
                bool save = true;
                #region config
                List<Guid> listdock;
                string Time = "";
                List<Ms_DockQoutaInterval> gettime = new List<Ms_DockQoutaInterval>();
                List<DockInteval> gettime_new = new List<DockInteval>();

                Ms_DockQoutaInterval get_timecan_Use = new Ms_DockQoutaInterval();
                DataAccess.Models.GI.Table.im_TruckLoadItem getitem_TruckLoad = new DataAccess.Models.GI.Table.im_TruckLoadItem();
                ms_VehicleType gettype_Vehicle = new ms_VehicleType();


                List<DataAccess.Models.GI.Table.im_TruckLoad> TruckLoad = dbGI.im_TruckLoad.Where(c => (c.Is_Booking == null || c.Is_Booking == 0) && c.UDF_1 != "NB").OrderBy(c => c.TruckLoad_No).ToList();
                if (TruckLoad.Count <= 0)
                {
                    mess = "Can not find dock";
                    save = false;
                }
                else
                {
                    if (save)
                    {
                        List<Ms_Dock> get_dock = new List<Ms_Dock>();

                        foreach (DataAccess.Models.GI.Table.im_TruckLoad get_TruckLoad in TruckLoad)
                        {
                            Guid Documenttype__index;
                            string Documenttype_id;
                            string Documenttype_name;
                            if (get_TruckLoad.DocumentRef_No6 == "1")
                            {
                                get_dock = dbMS.Ms_Dock.Where(c => c.DockType_Index == Guid.Parse("8DC27B53-6F51-41BC-824F-6ABAD656284C") && c.IsActive == 1).ToList();
                                Documenttype__index = Guid.Parse("13091631-5829-4341-BCB4-272B0ED854D7");
                                Documenttype_id = "FROZEN01";
                                Documenttype_name = "รับสินค้าห้องเย็น-OutBound";

                            }
                            else
                            {
                                get_dock = dbMS.Ms_Dock.Where(c => c.DockType_Index == Guid.Parse("BDB6CC26-B144-4E44-BC3F-F8E78E0E97AE") && c.IsActive == 1).ToList();
                                Documenttype__index = Guid.Parse("C392D865-8E69-4985-B72F-2421EBE8BCDB");
                                Documenttype_id = "OUT01";
                                Documenttype_name = "รับสินค้า-OutBound";
                            }



                            LoggingService.DataLogLines("SaveAppointment_Auto_out", "SaveAppointment_Auto_out" + DateTime.Now.ToString("yyyy-MM-dd"), "Shipment No : " + get_TruckLoad.TruckLoad_No);
                            save = true;
                            mess = "";
                            Time = get_TruckLoad.DocumentRef_No5;

                            if (get_dock.Count() > 0)
                            {
                                listdock = get_dock.Select(c => c.Dock_Index).ToList();
                                gettime = db.Ms_DockQoutaInterval.Where(c => listdock.Contains(c.Dock_Index)).ToList();
                                gettime_new = (from DQT in db.Ms_DockQoutaInterval.Where(c => listdock.Contains(c.Dock_Index) && c.IsBreakTime == false)
                                               join D in get_dock on DQT.Dock_Index equals D.Dock_Index
                                               select new DockInteval
                                               {
                                                   DockQoutaInterval_Index = DQT.DockQoutaInterval_Index,
                                                   Interval_Start = DQT.Interval_Start,
                                                   Interval_End = DQT.Interval_End,
                                                   Dock_Index = D.Dock_Index,
                                                   Dock_Id = D.Dock_Id,
                                                   Dock_Name = D.Dock_Name,
                                                   seq = D.seq
                                               }).ToList();
                            }
                            else
                            {
                                mess = "Can not find dock";
                                save = false;
                            }

                            if (get_TruckLoad != null)
                            {

                                getitem_TruckLoad = dbGI.im_TruckLoadItem.FirstOrDefault(c => c.TruckLoad_Index == get_TruckLoad.TruckLoad_Index);
                                if (getitem_TruckLoad == null)
                                {
                                    mess = "This TruckLoad didn't Ref_planGI ";
                                    save = false;
                                }
                                if (!string.IsNullOrEmpty(Time))
                                {
                                    DateTime date_Booking = get_TruckLoad.Expect_Pickup_Date.Value.TrimTime().AddHours(double.Parse(Time));
                                    if (DateTime.Now > date_Booking)
                                    {
                                        mess = "เกินเวลาจอง Dock";
                                        save = false;
                                    }
                                    string Time_S, Time_E;
                                    if (get_TruckLoad.DocumentRef_No6 == "1")
                                    {
                                        Time_S = Time + ":00";
                                        Time_E = Time == "23" ? "00:59" : ((int.Parse(Time) + 1).ToString().Length > 1 ? (int.Parse(Time) + 1).ToString() : "0" + (int.Parse(Time) + 1).ToString()) + ":59";
                                    }
                                    else
                                    {
                                        Time_S = Time + ":00";
                                        Time_E = Time == "23" ? "00:59" : ((int.Parse(Time) + 1).ToString().Length > 1 ? (int.Parse(Time) + 1).ToString() : "0" + (int.Parse(Time) + 1).ToString()) + ":59";
                                        //Time_E = ((int.Parse(Time) + 1).ToString().Length > 1 ? (int.Parse(Time) + 1).ToString() : "0" + (int.Parse(Time) + 1).ToString()) + ":59";
                                    }




                                    tb_TruckloadBooking truckloadBooking = db.tb_TruckloadBooking.Where(c => c.Ref_Document_No == get_TruckLoad.TruckLoad_No && c.IsActive == 1).FirstOrDefault();
                                    if (truckloadBooking != null)
                                    {
                                        Tb_AppointmentItem appointmentItem = db.Tb_AppointmentItem.FirstOrDefault(c => c.Appointment_Date == get_TruckLoad.Expect_Pickup_Date.Value.TrimTime() && c.Dock_Index == truckloadBooking.Dock_Index && c.Appointment_Time == (Time_S + " - " + Time_E) && c.IsActive == 1 && c.IsDelete == 0);

                                        if (appointmentItem == null)
                                        {
                                            get_timecan_Use = db.Ms_DockQoutaInterval.Where(c => c.Dock_Index == truckloadBooking.Dock_Index && c.Interval_Start == (Time_S) && c.Interval_End == (Time_E)).FirstOrDefault();
                                            if (get_timecan_Use == null)
                                            {
                                                mess = "ประตูในช่วงเวลาที่กำหนดไม่ว่าง";
                                                save = false;
                                            }
                                            else
                                            {
                                                LoggingService.DataLogLines("SaveAppointment_Auto_out", "SaveAppointment_Auto_out" + DateTime.Now.ToString("yyyy-MM-dd"), "Used same Dock : " + truckloadBooking.Dock_Name);
                                            }
                                        }
                                        else
                                        {
                                            LoggingService.DataLogLines("SaveAppointment_Auto_out", "SaveAppointment_Auto_out" + DateTime.Now.ToString("yyyy-MM-dd"), "Old Dock Already Used: " + truckloadBooking.Dock_Name);
                                            List<Guid> finduse_time = db.Tb_AppointmentItem.Where(c => c.Appointment_Date == get_TruckLoad.Expect_Pickup_Date.Value.TrimTime() && c.IsActive == 1 && c.IsDelete == 0).Select(c => c.DockQoutaInterval_Index).ToList();
                                            DockInteval gettime_frist = gettime_new.OrderBy(c => c.seq).FirstOrDefault(c => !finduse_time.Contains(c.DockQoutaInterval_Index) && c.Interval_Start == (Time_S) && c.Interval_End == (Time_E));
                                            if (gettime_frist != null)
                                            {
                                                get_timecan_Use = db.Ms_DockQoutaInterval.Where(c => c.DockQoutaInterval_Index == gettime_frist.DockQoutaInterval_Index).FirstOrDefault();
                                                if (get_timecan_Use == null)
                                                {
                                                    mess = "ประตูในช่วงเวลาที่กำหนดไม่ว่าง";
                                                    save = false;
                                                }
                                            }
                                            else
                                            {
                                                mess = "เวลาที่ส่งมา ไม่ได้ถูก Config ไว้ในระบบ";
                                                save = false;
                                            }
                                        }
                                        truckloadBooking.IsActive = 0;
                                        truckloadBooking.Update_By = "System Yard";
                                        truckloadBooking.Update_Date = DateTime.Now;
                                    }
                                    else
                                    {
                                        List<Guid> finduse_time = db.Tb_AppointmentItem.Where(c => c.Appointment_Date == get_TruckLoad.Expect_Pickup_Date.Value.TrimTime() && c.IsActive == 1 && c.IsDelete == 0).Select(c => c.DockQoutaInterval_Index).ToList();
                                        DockInteval gettime_frist = gettime_new.OrderBy(c => c.seq).FirstOrDefault(c => !finduse_time.Contains(c.DockQoutaInterval_Index) && c.Interval_Start == (Time_S) && c.Interval_End == (Time_E));
                                        if (gettime_frist != null)
                                        {
                                            get_timecan_Use = db.Ms_DockQoutaInterval.Where(c => c.DockQoutaInterval_Index == gettime_frist.DockQoutaInterval_Index).FirstOrDefault();
                                            if (get_timecan_Use == null)
                                            {
                                                mess = "ประตูในช่วงเวลาที่กำหนดไม่ว่าง";
                                                save = false;
                                            }
                                        }
                                        else
                                        {
                                            mess = "เวลาที่ส่งมา ไม่ได้ถูก Config ไว้ในระบบ";
                                            save = false;
                                        }
                                    }

                                }
                                else
                                {
                                    mess = "This Route Time didn't Config";
                                    save = false;
                                }
                                gettype_Vehicle = dbMS.ms_VehicleType.FirstOrDefault(c => c.VehicleType_Id == get_TruckLoad.VehicleType_Id);
                                if (gettype_Vehicle == null)
                                {
                                    mess = "ประเภทรถที่ส่งไม่ถูกกำหนด การจอง";
                                    save = false;
                                }
                            }
                            else
                            {
                                mess = "Can not find TruckLoad No";
                                save = false;

                            }
                            #endregion

                            if (save)
                            {
                                string appointmentId = GetDocumentNumber(Documenttype__index, DateTime.Now);


                                Tb_Appointment newAppointment = new Tb_Appointment
                                {
                                    Appointment_Index = Guid.NewGuid(),
                                    Appointment_Id = appointmentId,
                                    DocumentType_Index = Documenttype__index,
                                    DocumentType_Id = Documenttype_id,
                                    DocumentType_Name = Documenttype_name,
                                    IsActive = 1,
                                    IsDelete = 0,
                                    IsSystem = 0,
                                    Status_Id = 0,
                                    Create_By = "System Yard",
                                    Create_Date = DateTime.Now,
                                    Document_status = 5
                                };

                                Tb_AppointmentItem newAppointmentItem = new Tb_AppointmentItem
                                {
                                    AppointmentItem_Index = Guid.NewGuid(),
                                    Appointment_Index = newAppointment.Appointment_Index,
                                    AppointmentItem_Id = appointmentId + "-0",
                                    Appointment_Id = newAppointment.Appointment_Id,
                                    DocumentType_Index = newAppointment.DocumentType_Index,
                                    DocumentType_Id = newAppointment.DocumentType_Id,
                                    DocumentType_Name = newAppointment.DocumentType_Name,
                                    Appointment_Date = get_TruckLoad.Expect_Pickup_Date.Value,
                                    Appointment_Time = get_timecan_Use.Interval_Start + " - " + get_timecan_Use.Interval_End,
                                    DockQoutaInterval_Index = get_timecan_Use.DockQoutaInterval_Index,
                                    WareHouse_Index = Guid.Parse("B0AD4E8F-A7B1-4952-BAC7-1A9482BABA79"),
                                    WareHouse_Id = "DC7",
                                    WareHouse_Name = "Amazon วังน้อย",
                                    Group_Index = Guid.NewGuid(),
                                    Dock_Index = get_timecan_Use.Dock_Index,
                                    Dock_Id = get_timecan_Use.Dock_Id,
                                    Dock_Name = get_timecan_Use.Dock_Name,
                                    Ref_Document_No = get_TruckLoad.TruckLoad_No,

                                    IsActive = 1,
                                    IsDelete = 0,
                                    IsSystem = 0,
                                    Status_Id = 0,
                                    Create_By = "System Yard",
                                    Create_Date = DateTime.Now
                                };

                                Tb_AppointmentItemDetail appointmentItemDetail = new Tb_AppointmentItemDetail()
                                {
                                    AppointmentItemDetail_Index = Guid.NewGuid(),
                                    AppointmentItem_Index = newAppointmentItem.AppointmentItem_Index,
                                    Appointment_Index = newAppointment.Appointment_Index,
                                    Appointment_Id = newAppointment.Appointment_Id,
                                    VehicleType_Index = gettype_Vehicle.VehicleType_Index,
                                    VehicleType_Id = gettype_Vehicle.VehicleType_Id,
                                    VehicleType_Name = gettype_Vehicle.VehicleType_Name,
                                    Vehicle_No = get_TruckLoad.Vehicle_Registration,
                                    Driver_Name = get_TruckLoad.DocumentRef_No1,
                                    TransportManifest_Index = Guid.Parse(get_TruckLoad.DocumentRef_No4),

                                    IsActive = 1,
                                    IsDelete = 0,
                                    IsSystem = 0,
                                    Status_Id = 0,
                                    Create_By = "System Yard",
                                    Create_Date = DateTime.Now
                                };

                                tb_TruckloadBooking truckloadBooking = new tb_TruckloadBooking()
                                {
                                    TruckloadBooking_index = Guid.NewGuid(),
                                    Appointment_Index = newAppointment.Appointment_Index,
                                    Appointment_Id = newAppointment.Appointment_Id,
                                    AppointmentItem_Index = newAppointmentItem.AppointmentItem_Index,
                                    AppointmentItem_Id = newAppointmentItem.AppointmentItem_Id,
                                    DocumentType_Index = newAppointmentItem.DocumentType_Index,
                                    DocumentType_Id = newAppointmentItem.DocumentType_Id,
                                    DocumentType_Name = newAppointmentItem.DocumentType_Name,
                                    Appointment_Date = get_TruckLoad.Expect_Pickup_Date.Value,
                                    Appointment_Time = newAppointmentItem.Appointment_Time,
                                    Vehicle_No = appointmentItemDetail.Vehicle_No,
                                    Driver_Name = appointmentItemDetail.Driver_Name,
                                    VehicleType_Index = appointmentItemDetail.VehicleType_Index,
                                    VehicleType_Id = appointmentItemDetail.VehicleType_Id,
                                    VehicleType_Name = appointmentItemDetail.VehicleType_Name,
                                    Dock_Index = newAppointmentItem.Dock_Index,
                                    Dock_Id = newAppointmentItem.Dock_Id,
                                    Dock_Name = newAppointmentItem.Dock_Name,
                                    Ref_Document_No = get_TruckLoad.TruckLoad_No,
                                    Ref_Document_Index = get_TruckLoad.TruckLoad_Index,
                                    TransportManifest_Index = Guid.Parse(get_TruckLoad.DocumentRef_No4),
                                    IsActive = 1,
                                    IsDelete = 0,
                                    IsSystem = 0,
                                    Status_Id = 0,
                                    Create_By = "System Yard",
                                    Create_Date = DateTime.Now
                                };

                                db.Tb_Appointment.Add(newAppointment);
                                db.Tb_AppointmentItem.Add(newAppointmentItem);
                                db.Tb_AppointmentItemDetail.Add(appointmentItemDetail);
                                db.tb_TruckloadBooking.Add(truckloadBooking);

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

                                var status = new
                                {
                                    TransportManifest_No = get_TruckLoad.TruckLoad_No,
                                    Dock_Name = newAppointmentItem.Dock_Name,
                                    Dock_Time = newAppointmentItem.Appointment_Time,
                                    Dock_Status = "จอง Booking สำเร็จ"
                                };
                                LoggingService.DataLogLines("SaveAppointment_Auto_out", "SaveAppointment_Auto_out" + DateTime.Now.ToString("yyyy-MM-dd"), "Send TMS : " + JsonConvert.SerializeObject(status));
                                try
                                {
                                    var result_api = Utils.SendDataApi<bookTransportManifest>(new AppSettingConfig().GetUrl("Status_Booking"), JsonConvert.SerializeObject(status));
                                    LoggingService.DataLogLines("SaveAppointment_Auto_out", "SaveAppointment_Auto_out" + DateTime.Now.ToString("yyyy-MM-dd"), "Mess : " + JsonConvert.SerializeObject(result_api));
                                }
                                catch (Exception ex)
                                {
                                    LoggingService.DataLogLines("SaveAppointment_Auto_out", "SaveAppointment_Auto_out" + DateTime.Now.ToString("yyyy-MM-dd"), "Mess Error: " + JsonConvert.SerializeObject(ex));
                                }


                            }
                            if (mess == "")
                            {

                                get_TruckLoad.Is_Booking = 1;
                                get_TruckLoad.Document_Remark = "สำเร็จ";
                                LoggingService.DataLogLines("SaveAppointment_Auto_out", "SaveAppointment_Auto_out" + DateTime.Now.ToString("yyyy-MM-dd"), "Save Outbound type: " + get_TruckLoad.Document_Remark);
                            }
                            else
                            {
                                if (mess != "Can not find TruckLoad No")
                                {
                                    get_TruckLoad.Is_Booking = -2;
                                    get_TruckLoad.Document_Remark = mess;
                                    LoggingService.DataLogLines("SaveAppointment_Auto_out", "SaveAppointment_Auto_out" + DateTime.Now.ToString("yyyy-MM-dd"), "Save Outbound type: " + get_TruckLoad.Document_Remark);

                                    var status = new
                                    {
                                        TransportManifest_No = get_TruckLoad.TruckLoad_No,
                                        Dock_Name = mess,
                                        Dock_Time = "",
                                        Dock_Status = -1
                                    };
                                    LoggingService.DataLogLines("SaveAppointment_Auto_out", "SaveAppointment_Auto_out" + DateTime.Now.ToString("yyyy-MM-dd"), "Send TMS : " + JsonConvert.SerializeObject(status));
                                    try
                                    {
                                        var result_api = Utils.SendDataApi<bookTransportManifest>(new AppSettingConfig().GetUrl("Status_Booking"), JsonConvert.SerializeObject(status));
                                        LoggingService.DataLogLines("SaveAppointment_Auto_out", "SaveAppointment_Auto_out" + DateTime.Now.ToString("yyyy-MM-dd"), "Mess : " + JsonConvert.SerializeObject(result_api));
                                    }
                                    catch (Exception ex)
                                    {
                                        LoggingService.DataLogLines("SaveAppointment_Auto_out", "SaveAppointment_Auto_out" + DateTime.Now.ToString("yyyy-MM-dd"), "Mess Error: " + JsonConvert.SerializeObject(ex));
                                    }

                                }

                            }
                            var myTransactionGI = dbGI.Database.BeginTransaction(IsolationLevel.Serializable);
                            try
                            {
                                dbGI.SaveChanges();
                                myTransactionGI.Commit();
                                LoggingService.DataLogLines("SaveAppointment_Auto_out", "SaveAppointment_Auto_out" + DateTime.Now.ToString("yyyy-MM-dd"), "Save Outbound : true");
                            }
                            catch (Exception saveEx)
                            {
                                LoggingService.DataLogLines("SaveAppointment_Auto_out", "SaveAppointment_Auto_out" + DateTime.Now.ToString("yyyy-MM-dd"), "Save Outbound Error: " + JsonConvert.SerializeObject(saveEx));
                                myTransactionGI.Rollback();
                                throw saveEx;
                            }
                        }
                    }
                }


                mess = "S";
                return mess;
            }
            catch (Exception ex)
            {
                LoggingService.DataLogLines("SaveAppointment_Auto_out", "SaveAppointment_Auto_out" + DateTime.Now.ToString("yyyy-MM-dd"), "Save Outbound Error: " + JsonConvert.SerializeObject(ex));
                throw ex;
            }
        }
        #endregion

        #region Save ReAssign
        public Result SaveAppointment_ReAssign(string jsonData)
        {
            Result result = new Result();
            Logtxt LoggingService = new Logtxt();
            try
            {
                LoggingService.DataLogLines("ReAssign", "ReAssign" + DateTime.Now.ToString("yyyy-MM-dd"), "Start ReAssign" + DateTime.Now.ToString("yyyy-MM-dd-HHmm"));
                AppointmentModel model = Appointment.GetAppointmentModel(jsonData);
                string mess = "";
                bool save = false;
                bool check = false;

                bool type;
                string truckload = "";
                string dock = "";
                string time = "";

                var get_appointment = db.Tb_Appointment.FirstOrDefault(c => c.Appointment_Index == model.Appointment_Index && c.Document_status != 0);

                if (get_appointment != null)
                {

                    var appointmentdelete = db.Tb_Appointment.FirstOrDefault(c => c.Appointment_Index == model.Appointment_Index && c.Document_status == -1);
                    if (appointmentdelete != null)
                    {

                        result.ResultIsUse = false;
                        result.ResultMsg = "ไม่สามารถ ReAssign ได้เนื่องจาก Appointment ที่ทำการได้ถูก ReAssign แล้ว กรุณาทำการค้นหาข้อมูลใหม่อีกครั้ง";
                        return result;

                    }

                    Tb_YardBalance get_yardBalance = db.Tb_YardBalance.FirstOrDefault(c => c.Appointment_Index == get_appointment.Appointment_Index);

                    if (get_yardBalance != null)
                    {

                        result.ResultIsUse = false;
                        result.ResultMsg = "ไม่สามารถ ReAssign ได้เนื่องจาก Appointment ถูก check in Gate แล้ว";
                        return result;

                    }
                    Tb_AppointmentItem appointmentItem = db.Tb_AppointmentItem.FirstOrDefault(c => c.Appointment_Index == model.Appointment_Index);
                    var checkstatus = dbGI.View_CheckReport_Status.Where(c => c.TruckLoad_No == appointmentItem.Ref_Document_No && c.Document_StatusDocktoStg == null).ToList();
                    if (checkstatus.Count() > 0)
                    {
                        result.ResultIsUse = false;
                        result.ResultMsg = "shipment กำลังถูก wave กรุณารอให้ wave เสร็จ จึงแก้ไข shipment ได้";
                        return result;
                    }
                    else
                    {
                        result.ResultIsUse = true;
                    }
                    type = get_appointment.DocumentType_Index == Guid.Parse("c392d865-8e69-4985-b72f-2421ebe8bcdb") || get_appointment.DocumentType_Index == Guid.Parse("13091631-5829-4341-BCB4-272B0ED854D7") ? true : false;
                    LoggingService.DataLogLines("ReAssign", "ReAssign" + DateTime.Now.ToString("yyyy-MM-dd"), jsonData);
                    LoggingService.DataLogLines("ReAssign", "ReAssign" + DateTime.Now.ToString("yyyy-MM-dd"), "Appointment_Id : " + model.Appointment_Id);

                    string appointmentId = GetDocumentNumber(get_appointment.DocumentType_Index, DateTime.Now);

                    type = get_appointment.DocumentType_Index == Guid.Parse("C392D865-8E69-4985-B72F-2421EBE8BCDB") || get_appointment.DocumentType_Index == Guid.Parse("13091631-5829-4341-BCB4-272B0ED854D7") ? true : false;

                    Tb_Appointment newAppointment = new Tb_Appointment
                    {
                        Appointment_Index = Guid.NewGuid(),
                        Appointment_Id = appointmentId,
                        DocumentType_Index = get_appointment.DocumentType_Index,
                        DocumentType_Id = get_appointment.DocumentType_Id,
                        DocumentType_Name = get_appointment.DocumentType_Name,
                        IsActive = 1,
                        IsDelete = 0,
                        IsSystem = 0,
                        Status_Id = 0,
                        Create_By = model.Create_By,
                        Create_Date = DateTime.Now,
                        Document_status = 5
                    };
                    var new_time = db.Ms_DockQoutaInterval.FirstOrDefault(c => c.DockQoutaInterval_Index == model.dockQoutaInterval_Index);
                    if (new_time != null)
                    {
                        var get_appointmentitem = db.Tb_AppointmentItem.FirstOrDefault(c => c.Appointment_Index == model.Appointment_Index);

                        LoggingService.DataLogLines("ReAssign", "ReAssign" + DateTime.Now.ToString("yyyy-MM-dd"), "Truckload No : " + get_appointmentitem.Ref_Document_No);
                        LoggingService.DataLogLines("ReAssign", "ReAssign" + DateTime.Now.ToString("yyyy-MM-dd"), "Dock : " + get_appointmentitem.Dock_Name + " To :" + new_time.Dock_Name);
                        LoggingService.DataLogLines("ReAssign", "ReAssign" + DateTime.Now.ToString("yyyy-MM-dd"), "Time : " + get_appointmentitem.Appointment_Time + " To :" + new_time.Interval_Start + " - " + new_time.Interval_End);

                        if (get_appointmentitem != null)
                        {
                            Tb_AppointmentItem newAppointmentItem = new Tb_AppointmentItem
                            {
                                AppointmentItem_Index = Guid.NewGuid(),
                                Appointment_Index = newAppointment.Appointment_Index,
                                AppointmentItem_Id = appointmentId + "-0",
                                Appointment_Id = newAppointment.Appointment_Id,
                                DocumentType_Index = newAppointment.DocumentType_Index,
                                DocumentType_Id = newAppointment.DocumentType_Id,
                                DocumentType_Name = newAppointment.DocumentType_Name,
                                Appointment_Date = get_appointmentitem.Appointment_Date,
                                Appointment_Time = new_time.Interval_Start + " - " + new_time.Interval_End,
                                DockQoutaInterval_Index = new_time.DockQoutaInterval_Index,
                                WareHouse_Index = Guid.Parse("B0AD4E8F-A7B1-4952-BAC7-1A9482BABA79"),
                                WareHouse_Id = "DC7",
                                WareHouse_Name = "Amazon วังน้อย",
                                Group_Index = Guid.NewGuid(),
                                Dock_Index = new_time.Dock_Index,
                                Dock_Id = new_time.Dock_Id,
                                Dock_Name = new_time.Dock_Name,
                                Ref_Document_No = get_appointmentitem.Ref_Document_No,

                                IsActive = 1,
                                IsDelete = 0,
                                IsSystem = 0,
                                Status_Id = 0,
                                Create_By = model.Create_By,
                                Create_Date = DateTime.Now
                            };
                            db.Tb_AppointmentItem.Add(newAppointmentItem);

                            if (model.DocumentType_Index != Guid.Parse("c392d865-8e69-4985-b72f-2421ebe8bcdb") && get_appointment.DocumentType_Index != Guid.Parse("13091631-5829-4341-BCB4-272B0ED854D7"))
                            {
                                var ref_planGR = dbGR.IM_PlanGoodsReceive.FirstOrDefault(c => c.PlanGoodsReceive_No == newAppointmentItem.Ref_Document_No);
                                ref_planGR.Dock_Name = newAppointmentItem.Dock_Name;
                                ref_planGR.Dock_Id = newAppointmentItem.Dock_Id;
                                ref_planGR.Dock_Index = newAppointmentItem.Dock_Index;
                                ref_planGR.Appointment_id = newAppointmentItem.Appointment_Id;
                            }

                            var get_appointmentitemDetail = db.Tb_AppointmentItemDetail.FirstOrDefault(c => c.AppointmentItem_Index == get_appointmentitem.AppointmentItem_Index);
                            if (get_appointmentitemDetail != null)
                            {
                                Tb_AppointmentItemDetail appointmentItemDetail = new Tb_AppointmentItemDetail()
                                {
                                    AppointmentItemDetail_Index = Guid.NewGuid(),
                                    AppointmentItem_Index = newAppointmentItem.AppointmentItem_Index,
                                    Appointment_Index = newAppointment.Appointment_Index,
                                    Appointment_Id = newAppointment.Appointment_Id,
                                    VehicleType_Index = get_appointmentitemDetail.VehicleType_Index,
                                    VehicleType_Id = get_appointmentitemDetail.VehicleType_Id,
                                    VehicleType_Name = get_appointmentitemDetail.VehicleType_Name,
                                    Vehicle_No = get_appointmentitemDetail.Vehicle_No,
                                    Driver_Name = get_appointmentitemDetail.Driver_Name,
                                    TransportManifest_Index = get_appointmentitemDetail.TransportManifest_Index,

                                    IsActive = 1,
                                    IsDelete = 0,
                                    IsSystem = 0,
                                    Status_Id = 0,
                                    Create_By = model.Create_By,
                                    Create_Date = DateTime.Now
                                };
                                db.Tb_AppointmentItemDetail.Add(appointmentItemDetail);

                                tb_TruckloadBooking OLD_truckloadBooking = db.tb_TruckloadBooking.FirstOrDefault(c => c.Ref_Document_No == newAppointmentItem.Ref_Document_No && c.IsActive == 1);
                                if (OLD_truckloadBooking != null)
                                {
                                    tb_TruckloadBooking truckloadBooking = new tb_TruckloadBooking()
                                    {
                                        TruckloadBooking_index = Guid.NewGuid(),
                                        Appointment_Index = newAppointment.Appointment_Index,
                                        Appointment_Id = newAppointment.Appointment_Id,
                                        AppointmentItem_Index = newAppointmentItem.AppointmentItem_Index,
                                        AppointmentItem_Id = newAppointmentItem.AppointmentItem_Id,
                                        DocumentType_Index = newAppointmentItem.DocumentType_Index,
                                        DocumentType_Id = newAppointmentItem.DocumentType_Id,
                                        DocumentType_Name = newAppointmentItem.DocumentType_Name,
                                        Appointment_Date = newAppointmentItem.Appointment_Date,
                                        Appointment_Time = newAppointmentItem.Appointment_Time,
                                        Vehicle_No = appointmentItemDetail.Vehicle_No,
                                        Driver_Name = appointmentItemDetail.Driver_Name,
                                        VehicleType_Index = appointmentItemDetail.VehicleType_Index,
                                        VehicleType_Id = appointmentItemDetail.VehicleType_Id,
                                        VehicleType_Name = appointmentItemDetail.VehicleType_Name,
                                        Dock_Index = newAppointmentItem.Dock_Index,
                                        Dock_Id = newAppointmentItem.Dock_Id,
                                        Dock_Name = newAppointmentItem.Dock_Name,
                                        Ref_Document_No = newAppointmentItem.Ref_Document_No,
                                        Ref_Document_Index = OLD_truckloadBooking.Ref_Document_Index,
                                        TransportManifest_Index = get_appointmentitemDetail.TransportManifest_Index,
                                        IsActive = 1,
                                        IsDelete = 0,
                                        IsSystem = 0,
                                        Status_Id = 0,
                                        Create_By = model.Create_By,
                                        Create_Date = DateTime.Now
                                    };

                                    db.tb_TruckloadBooking.Add(truckloadBooking);

                                    OLD_truckloadBooking.IsActive = 0;
                                    OLD_truckloadBooking.Update_By = model.Create_By;
                                    OLD_truckloadBooking.Update_Date = DateTime.Now;
                                }


                                save = true;

                            }



                            if (save)
                            {

                                get_appointment.Document_status = -1;
                                get_appointment.IsActive = 0;
                                get_appointment.IsDelete = 1;
                                get_appointment.Cancel_By = model.Create_By;
                                get_appointment.Cancel_Date = DateTime.Now;


                                get_appointmentitem.IsActive = 0;
                                get_appointmentitem.IsDelete = 1;
                                get_appointmentitem.Cancel_By = model.Create_By;
                                get_appointmentitem.Cancel_Date = DateTime.Now;

                                //db.Tb_Appointment.Remove(get_appointment);
                                //db.Tb_AppointmentItem.RemoveRange(get_appointmentitem);
                                //db.Tb_AppointmentItemDetail.RemoveRange(get_appointmentitemDetail);
                                if (get_yardBalance != null)
                                {
                                    db.Tb_YardBalance.RemoveRange(get_yardBalance);
                                }

                                truckload = get_appointmentitem.Ref_Document_No;
                                dock = newAppointmentItem.Dock_Name;
                                time = newAppointmentItem.Appointment_Time;

                            }
                        }


                    }
                    db.Tb_Appointment.Add(newAppointment);



                    var myTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                    try
                    {
                        db.SaveChanges();
                        myTransaction.Commit();
                        check = true;
                        LoggingService.DataLogLines("ReAssign", "ReAssign" + DateTime.Now.ToString("yyyy-MM-dd"), "Success ");

                    }
                    catch (Exception saveEx)
                    {
                        myTransaction.Rollback();
                        throw saveEx;
                    }

                }
                else
                {
                    result.ResultIsUse = false;
                    result.ResultMsg = "ไม่สามารถ ReAssign ได้เนื่องจากเอกสารไม่ได้ถูกยืนยัน";
                    return result;
                }

                if (check && type)
                {
                    var status = new
                    {
                        TransportManifest_No = truckload,
                        Dock_Name = dock,
                        Dock_Time = time,
                        Dock_Status = "จอง Booking สำเร็จ"
                    };
                    LoggingService.DataLogLines("ReAssign", "ReAssign" + DateTime.Now.ToString("yyyy-MM-dd"), "Send TMS");
                    LoggingService.DataLogLines("ReAssign", "ReAssign" + DateTime.Now.ToString("yyyy-MM-dd"), JsonConvert.SerializeObject(status));
                    var result_api = Utils.SendDataApi<TransportManifest>(new AppSettingConfig().GetUrl("Status_Booking"), JsonConvert.SerializeObject(status));
                }

                result.ResultIsUse = true;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Update Booking by Truckload
        public Result UpdatebookingByTruckload(TransportManifestUpdate data)
        {
            var result = new Result();
            try
            {
                bool updateDock = false;
                Logtxt LoggingService = new Logtxt();
                LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Start Update Booking" + DateTime.Now.ToString("yyyy-MM-dd-HHmm"));
                var get_update = db.Tb_AppointmentItem.Where(c => c.Ref_Document_No == data.Truckload_no && c.IsActive == 1 && c.IsDelete == 0).FirstOrDefault();

                if (get_update != null)
                {
                    Ms_DockQoutaInterval get_timecan_Use = new Ms_DockQoutaInterval();
                    if (data.expect_Pickup_Date != null)
                    {
                        LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Appointment Date :" + get_update.Appointment_Date + " To " + data.expect_Pickup_Date.GetValueOrDefault().TrimTime());
                        if (get_update.Appointment_Date != data.expect_Pickup_Date.GetValueOrDefault().TrimTime())
                        {
                            LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Reject : ไม่สามารถเปลี่ยนวันที่การ Booking ได้");
                            result.ResultMsg = "ไม่สามารถเปลี่ยนวันที่การ Booking ได้";
                            result.ResultIsUse = false;
                            return result;
                        }
                    }
                    Guid Dock_type;
                    if (get_update.DocumentType_Index == Guid.Parse("13091631-5829-4341-BCB4-272B0ED854D7"))
                    {
                        Dock_type = Guid.Parse("8DC27B53-6F51-41BC-824F-6ABAD656284C");
                    }
                    else
                    {
                        Dock_type = Guid.Parse("BDB6CC26-B144-4E44-BC3F-F8E78E0E97AE");
                    }

                    List<Ms_Dock> get_dock = dbMS.Ms_Dock.Where(c => c.DockType_Index == Dock_type && c.IsActive == 1).ToList();
                    var gettime = db.Ms_DockQoutaInterval.Where(c => get_dock.Select(x => x.Dock_Index).Contains(c.Dock_Index)).ToList();
                    var gettime_new = (from DQT in db.Ms_DockQoutaInterval.Where(c => get_dock.Select(x => x.Dock_Index).Contains(c.Dock_Index) && c.IsBreakTime == false)
                                       join D in get_dock on DQT.Dock_Index equals D.Dock_Index
                                       select new DockInteval
                                       {
                                           DockQoutaInterval_Index = DQT.DockQoutaInterval_Index,
                                           Interval_Start = DQT.Interval_Start,
                                           Interval_End = DQT.Interval_End,
                                           Dock_Index = D.Dock_Index,
                                           Dock_Id = D.Dock_Id,
                                           Dock_Name = D.Dock_Name,
                                           seq = D.seq
                                       }).ToList();

                    if (!string.IsNullOrEmpty(data.expect_Pickup_Time))
                    {
                        LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Time TMS : " + data.expect_Pickup_Time + ":00");
                        DateTime date_Booking = get_update.Appointment_Date.AddHours(double.Parse(data.expect_Pickup_Time));
                        if (DateTime.Now > date_Booking)
                        {
                            LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Reject : เกินเวลาจอง Dock");
                            result.ResultMsg = "เกินเวลาจอง Dock";
                            result.ResultIsUse = false;
                            return result;
                        }
                    }
                    else
                    {
                        LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Reject : This Route Time didn't Config");
                        result.ResultIsUse = false;
                        result.ResultMsg = "This Route Time didn't Config";
                        return result;
                    }

                    var get_updateDetail = db.Tb_AppointmentItemDetail.FirstOrDefault(c => c.AppointmentItem_Index == get_update.AppointmentItem_Index);
                    if (get_updateDetail != null)
                    {
                        string Time_S, Time_E;
                        Time_S = data.expect_Pickup_Time + ":00";
                        if (get_update.DocumentType_Index == Guid.Parse("13091631-5829-4341-BCB4-272B0ED854D7"))
                        {
                            Time_E = data.expect_Pickup_Time == "23" ? "00:59" : ((int.Parse(data.expect_Pickup_Time) + 1).ToString().Length > 1 ? (int.Parse(data.expect_Pickup_Time) + 1).ToString() : "0" + (int.Parse(data.expect_Pickup_Time) + 1).ToString()) + ":59";
                        }
                        else
                        {
                            //Time_E = data.expect_Pickup_Time == "23" ? data.expect_Pickup_Time + ":59" : ((int.Parse(data.expect_Pickup_Time) + 1).ToString().Length > 1 ? (int.Parse(data.expect_Pickup_Time) + 1).ToString() : "0" + (int.Parse(data.expect_Pickup_Time) + 1).ToString()) + ":59";
                            Time_E = data.expect_Pickup_Time == "23" ? "00:59" : ((int.Parse(data.expect_Pickup_Time) + 1).ToString().Length > 1 ? (int.Parse(data.expect_Pickup_Time) + 1).ToString() : "0" + (int.Parse(data.expect_Pickup_Time) + 1).ToString()) + ":59";
                        }




                        LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Appointment ID : " + get_update.Appointment_Id);
                        LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "get_update.Appointment_Time : " + get_update.Appointment_Time);
                        LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Time_S : " + Time_S);
                        LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Time_E : " + Time_E);
                        if (get_update.Appointment_Time != (Time_S + " - " + Time_E))
                        {
                            tb_TruckloadBooking truckloadBooking = db.tb_TruckloadBooking.Where(c => c.Ref_Document_No == data.Truckload_no && c.IsActive == 1).FirstOrDefault();

                            if (truckloadBooking != null)
                            {
                                LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "truckloadBooking : " + JsonConvert.SerializeObject(truckloadBooking));
                                Tb_AppointmentItem appointmentItem = db.Tb_AppointmentItem.FirstOrDefault(c => c.Appointment_Date == data.expect_Pickup_Date.GetValueOrDefault().TrimTime() && c.Dock_Index == truckloadBooking.Dock_Index && c.Appointment_Time == (Time_S + " - " + Time_E) && c.IsActive == 1 && c.IsDelete == 0);

                                if (appointmentItem == null)
                                {
                                    LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "appointmentItem : " + JsonConvert.SerializeObject(appointmentItem));
                                    get_timecan_Use = db.Ms_DockQoutaInterval.Where(c => c.Dock_Index == truckloadBooking.Dock_Index && c.Interval_Start == (Time_S) && c.Interval_End == (Time_E)).FirstOrDefault();
                                    if (get_timecan_Use == null)
                                    {
                                        result.ResultIsUse = false;
                                        result.ResultMsg = "ประตูในช่วงเวลาที่กำหนดไม่ว่าง 1";
                                        return result;
                                    }
                                    else
                                    {
                                        LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Used same Dock : " + truckloadBooking.Dock_Name);
                                    }
                                }
                                else
                                {
                                    LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Old Dock Already Used: " + truckloadBooking.Dock_Name);
                                    List<Guid> finduse_time = db.Tb_AppointmentItem.Where(c => c.Appointment_Date == get_update.Appointment_Date.TrimTime() && c.IsActive == 1 && c.IsDelete == 0).Select(c => c.DockQoutaInterval_Index).ToList();
                                    DockInteval gettime_frist = gettime_new.OrderBy(c => c.seq).FirstOrDefault(c => !finduse_time.Contains(c.DockQoutaInterval_Index) && c.Interval_Start == (Time_S) && c.Interval_End == (Time_E));
                                    if (gettime_frist != null)
                                    {
                                        get_timecan_Use = db.Ms_DockQoutaInterval.Where(c => c.DockQoutaInterval_Index == gettime_frist.DockQoutaInterval_Index).FirstOrDefault();
                                        if (get_timecan_Use == null)
                                        {
                                            result.ResultIsUse = false;
                                            result.ResultMsg = "ประตูในช่วงเวลาที่กำหนดไม่ว่าง 2";
                                            return result;
                                        }
                                    }
                                    else
                                    {
                                        result.ResultIsUse = false;
                                        result.ResultMsg = "เวลาที่ส่งมา ไม่ได้ถูก Config ไว้ในระบบ";
                                        return result;
                                    }
                                }
                                truckloadBooking.IsActive = 0;
                                truckloadBooking.Update_By = "OMS System";
                                truckloadBooking.Update_Date = DateTime.Now;
                            }
                            else
                            {
                                List<Guid> finduse_time = db.Tb_AppointmentItem.Where(c => c.Appointment_Date == data.expect_Pickup_Date.GetValueOrDefault().TrimTime() && c.IsActive == 1 && c.IsDelete == 0).Select(c => c.DockQoutaInterval_Index).ToList();
                                DockInteval gettime_frist = gettime_new.OrderBy(c => c.seq).FirstOrDefault(c => !finduse_time.Contains(c.DockQoutaInterval_Index) && c.Interval_Start == (Time_S) && c.Interval_End == (Time_E));
                                if (gettime_frist != null)
                                {
                                    get_timecan_Use = db.Ms_DockQoutaInterval.Where(c => c.DockQoutaInterval_Index == gettime_frist.DockQoutaInterval_Index).FirstOrDefault();
                                    if (get_timecan_Use == null)
                                    {
                                        result.ResultIsUse = false;
                                        result.ResultMsg = "ประตูในช่วงเวลาที่กำหนดไม่ว่าง 3";
                                        return result;
                                    }
                                }
                                else
                                {
                                    result.ResultIsUse = false;
                                    result.ResultMsg = "เวลาที่ส่งมา ไม่ได้ถูก Config ไว้ในระบบ";
                                    return result;
                                }
                            }

                            LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Update Dock => Dock : " + get_update.Dock_Name + " To " + get_timecan_Use.Dock_Name);
                            LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Update Dock => Time : " + get_update.Appointment_Time + " To " + get_timecan_Use.Interval_Start + " - " + get_timecan_Use.Interval_End);
                            updateDock = true;

                            string appointmentId = GetDocumentNumber(get_update.DocumentType_Index, DateTime.Now);


                            Tb_Appointment newAppointment = new Tb_Appointment
                            {
                                Appointment_Index = Guid.NewGuid(),
                                Appointment_Id = appointmentId,
                                DocumentType_Index = get_update.DocumentType_Index,
                                DocumentType_Id = get_update.DocumentType_Id,
                                DocumentType_Name = get_update.DocumentType_Name,
                                IsActive = 1,
                                IsDelete = 0,
                                IsSystem = 0,
                                Status_Id = 0,
                                Create_By = "OMS With new Dock",
                                Create_Date = DateTime.Now,
                                Document_status = 5
                            };

                            LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "newAppointment : " + JsonConvert.SerializeObject(newAppointment));

                            Tb_AppointmentItem newAppointmentItem = new Tb_AppointmentItem
                            {
                                AppointmentItem_Index = Guid.NewGuid(),
                                Appointment_Index = newAppointment.Appointment_Index,
                                AppointmentItem_Id = appointmentId + "-0",
                                Appointment_Id = newAppointment.Appointment_Id,
                                DocumentType_Index = newAppointment.DocumentType_Index,
                                DocumentType_Id = newAppointment.DocumentType_Id,
                                DocumentType_Name = newAppointment.DocumentType_Name,
                                Appointment_Date = get_update.Appointment_Date,
                                Appointment_Time = get_update.Appointment_Time,
                                DockQoutaInterval_Index = get_update.DockQoutaInterval_Index,
                                WareHouse_Index = Guid.Parse("B0AD4E8F-A7B1-4952-BAC7-1A9482BABA79"),
                                WareHouse_Id = "DC7",
                                WareHouse_Name = "Amazon วังน้อย",
                                Group_Index = Guid.NewGuid(),
                                Dock_Index = get_update.Dock_Index,
                                Dock_Id = get_update.Dock_Id,
                                Dock_Name = get_update.Dock_Name,
                                Ref_Document_No = get_update.Ref_Document_No,

                                IsActive = 1,
                                IsDelete = 0,
                                IsSystem = 0,
                                Status_Id = 0,
                                Create_By = "OMS With new Dock",
                                Create_Date = DateTime.Now
                            };
                            LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "newAppointmentItem : " + JsonConvert.SerializeObject(newAppointmentItem));

                            Tb_AppointmentItemDetail appointmentItemDetail = new Tb_AppointmentItemDetail()
                            {
                                AppointmentItemDetail_Index = Guid.NewGuid(),
                                AppointmentItem_Index = newAppointmentItem.AppointmentItem_Index,
                                Appointment_Index = newAppointment.Appointment_Index,
                                Appointment_Id = newAppointment.Appointment_Id,
                                VehicleType_Index = data.VehicleType_Index,
                                VehicleType_Id = data.VehicleType_Id,
                                VehicleType_Name = data.VehicleType_Name,
                                Vehicle_No = data.Vehicle_no,
                                Driver_Name = data.Driver_Name,
                                TransportManifest_Index = get_updateDetail.TransportManifest_Index,

                                IsActive = 1,
                                IsDelete = 0,
                                IsSystem = 0,
                                Status_Id = 0,
                                Create_By = "OMS With new Dock",
                                Create_Date = DateTime.Now
                            };
                            LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "appointmentItemDetail : " + JsonConvert.SerializeObject(appointmentItemDetail));

                            tb_TruckloadBooking truckloadBooking_insert = new tb_TruckloadBooking()
                            {
                                TruckloadBooking_index = Guid.NewGuid(),
                                Appointment_Index = newAppointment.Appointment_Index,
                                Appointment_Id = newAppointment.Appointment_Id,
                                AppointmentItem_Index = newAppointmentItem.AppointmentItem_Index,
                                AppointmentItem_Id = newAppointmentItem.AppointmentItem_Id,
                                DocumentType_Index = newAppointmentItem.DocumentType_Index,
                                DocumentType_Id = newAppointmentItem.DocumentType_Id,
                                DocumentType_Name = newAppointmentItem.DocumentType_Name,
                                Appointment_Date = newAppointmentItem.Appointment_Date,
                                Appointment_Time = newAppointmentItem.Appointment_Time,
                                Vehicle_No = appointmentItemDetail.Vehicle_No,
                                Driver_Name = appointmentItemDetail.Driver_Name,
                                VehicleType_Index = appointmentItemDetail.VehicleType_Index,
                                VehicleType_Id = appointmentItemDetail.VehicleType_Id,
                                VehicleType_Name = appointmentItemDetail.VehicleType_Name,
                                Dock_Index = newAppointmentItem.Dock_Index,
                                Dock_Id = newAppointmentItem.Dock_Id,
                                Dock_Name = newAppointmentItem.Dock_Name,
                                Ref_Document_No = newAppointmentItem.Ref_Document_No,
                                Ref_Document_Index = data.Truckload_Index,
                                TransportManifest_Index = appointmentItemDetail.TransportManifest_Index,
                                IsActive = 1,
                                IsDelete = 0,
                                IsSystem = 0,
                                Status_Id = 0,
                                Create_By = "OMS With new Dock",
                                Create_Date = DateTime.Now
                            };

                            LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "truckloadBooking_insert : " + JsonConvert.SerializeObject(truckloadBooking_insert));

                            db.Tb_Appointment.Add(newAppointment);
                            db.Tb_AppointmentItem.Add(newAppointmentItem);
                            db.Tb_AppointmentItemDetail.Add(appointmentItemDetail);
                            db.tb_TruckloadBooking.Add(truckloadBooking_insert);

                            get_update.IsActive = 0;
                            get_update.IsDelete = 1;
                            get_update.Cancel_By = "OMS With new Dock";
                            get_update.Cancel_Date = DateTime.Now;

                            Tb_Appointment appointment = db.Tb_Appointment.FirstOrDefault(c => c.Appointment_Index == get_update.Appointment_Index);
                            if (appointment != null)
                            {
                                appointment.IsActive = 0;
                                appointment.IsDelete = 1;
                                appointment.Document_status = -1;
                                appointment.Cancel_By = "OMS With new Dock";
                                appointment.Cancel_Date = DateTime.Now;

                                var yardBalance = db.Tb_YardBalance.Where(c => c.Appointment_Index == appointment.Appointment_Index).ToList();
                                db.Tb_YardBalance.RemoveRange(yardBalance);
                            }
                        }
                        else
                        {
                            string appointmentId = GetDocumentNumber(get_update.DocumentType_Index, DateTime.Now);
                            LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "update with old dock ");
                            LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "update with old dock ID : " + appointmentId);

                            Tb_Appointment newAppointment = new Tb_Appointment
                            {
                                Appointment_Index = Guid.NewGuid(),
                                Appointment_Id = appointmentId,
                                DocumentType_Index = get_update.DocumentType_Index,
                                DocumentType_Id = get_update.DocumentType_Id,
                                DocumentType_Name = get_update.DocumentType_Name,
                                IsActive = 1,
                                IsDelete = 0,
                                IsSystem = 0,
                                Status_Id = 0,
                                Create_By = "OMS With old Dock",
                                Create_Date = DateTime.Now,
                                Document_status = 5
                            };

                            LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "update with old dock ID : " + appointmentId);

                            Tb_AppointmentItem newAppointmentItem = new Tb_AppointmentItem
                            {
                                AppointmentItem_Index = Guid.NewGuid(),
                                Appointment_Index = newAppointment.Appointment_Index,
                                AppointmentItem_Id = appointmentId + "-0",
                                Appointment_Id = newAppointment.Appointment_Id,
                                DocumentType_Index = newAppointment.DocumentType_Index,
                                DocumentType_Id = newAppointment.DocumentType_Id,
                                DocumentType_Name = newAppointment.DocumentType_Name,
                                Appointment_Date = get_update.Appointment_Date,
                                Appointment_Time = get_update.Appointment_Time,
                                DockQoutaInterval_Index = get_update.DockQoutaInterval_Index,
                                WareHouse_Index = Guid.Parse("B0AD4E8F-A7B1-4952-BAC7-1A9482BABA79"),
                                WareHouse_Id = "DC7",
                                WareHouse_Name = "Amazon วังน้อย",
                                Group_Index = Guid.NewGuid(),
                                Dock_Index = get_update.Dock_Index,
                                Dock_Id = get_update.Dock_Id,
                                Dock_Name = get_update.Dock_Name,
                                Ref_Document_No = get_update.Ref_Document_No,

                                IsActive = 1,
                                IsDelete = 0,
                                IsSystem = 0,
                                Status_Id = 0,
                                Create_By = "OMS With old Dock",
                                Create_Date = DateTime.Now
                            };

                            Tb_AppointmentItemDetail appointmentItemDetail = new Tb_AppointmentItemDetail()
                            {
                                AppointmentItemDetail_Index = Guid.NewGuid(),
                                AppointmentItem_Index = newAppointmentItem.AppointmentItem_Index,
                                Appointment_Index = newAppointment.Appointment_Index,
                                Appointment_Id = newAppointment.Appointment_Id,
                                VehicleType_Index = data.VehicleType_Index,
                                VehicleType_Id = data.VehicleType_Id,
                                VehicleType_Name = data.VehicleType_Name,
                                Vehicle_No = data.Vehicle_no,
                                Driver_Name = data.Driver_Name,
                                TransportManifest_Index = get_updateDetail.TransportManifest_Index,

                                IsActive = 1,
                                IsDelete = 0,
                                IsSystem = 0,
                                Status_Id = 0,
                                Create_By = "OMS With old Dock",
                                Create_Date = DateTime.Now
                            };


                            db.Tb_Appointment.Add(newAppointment);
                            db.Tb_AppointmentItem.Add(newAppointmentItem);
                            db.Tb_AppointmentItemDetail.Add(appointmentItemDetail);

                            LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "update old Appointitem" + get_update.Appointment_Id);

                            get_update.IsActive = 0;
                            get_update.IsDelete = 1;
                            get_update.Cancel_By = "OMS With old Dock";
                            get_update.Cancel_Date = DateTime.Now;

                            Tb_Appointment appointment = db.Tb_Appointment.FirstOrDefault(c => c.Appointment_Index == get_update.Appointment_Index);
                            if (appointment != null)
                            {
                                LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "update old Appoint" + appointment.Appointment_Id);
                                appointment.IsActive = 0;
                                appointment.IsDelete = 1;
                                appointment.Document_status = -1;
                                appointment.Cancel_By = "OMS With old Dock";
                                appointment.Cancel_Date = DateTime.Now;

                                var yardBalance = db.Tb_YardBalance.Where(c => c.Appointment_Index == appointment.Appointment_Index).ToList();
                                db.Tb_YardBalance.RemoveRange(yardBalance);
                            }

                        }







                        LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "updateDock : " + updateDock);
                        if (updateDock)
                        {

                            var status = new
                            {
                                TransportManifest_No = get_update.Ref_Document_No,
                                Dock_Name = get_update.Dock_Name,
                                Dock_Time = get_update.Appointment_Time,
                                Dock_Status = "จอง Booking สำเร็จ"
                            };
                            var result_api = Utils.SendDataApi<TransportManifest>(new AppSettingConfig().GetUrl("Status_Booking"), JsonConvert.SerializeObject(status));
                        }
                        LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "myTransaction");
                        var myTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                        try
                        {
                            LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "start");

                            db.SaveChanges();
                            myTransaction.Commit();

                            LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "S");
                        }
                        catch (Exception saveEx)
                        {
                            LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "saveEx" + saveEx.Message);

                            myTransaction.Rollback();
                            throw saveEx;
                        }

                        result.ResultIsUse = true;
                    }
                    else
                    {
                        LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Reject : ไม่สามารถหาเลข Appointment นี้ได้");
                        result.ResultIsUse = false;
                        result.ResultMsg = "ไม่สามารถหาเลข Appointment นี้ได้";
                    }
                }
                else
                {
                    LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Reject : ไม่พบเลข Truckload นี้");
                    result.ResultIsUse = false;
                    result.ResultMsg = "ไม่พบเลข Truckload นี้";
                }

                return result;
            }
            catch (Exception EX)
            {
                result.ResultIsUse = false;
                result.ResultMsg = EX.Message;
                throw EX;
            }
        }
        //public Result UpdatebookingByTruckload(TransportManifestUpdate data) {
        //    var result = new Result();
        //    try
        //    {
        //        bool updateDock = false;
        //        Logtxt LoggingService = new Logtxt();
        //        LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Start Update Booking" + DateTime.Now.ToString("yyyy-MM-dd-HHmm"));
        //        var get_update = db.Tb_AppointmentItem.Where(c => c.Ref_Document_No == data.Truckload_no).FirstOrDefault();

        //        if (get_update != null)
        //        {
        //            Ms_DockQoutaInterval get_timecan_Use = new Ms_DockQoutaInterval();
        //            if (data.expect_Pickup_Date != null)
        //            {
        //                LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Appointment Date :" + get_update.Appointment_Date + " To "+ data.expect_Pickup_Date.GetValueOrDefault().TrimTime());
        //                if (get_update.Appointment_Date != data.expect_Pickup_Date.GetValueOrDefault().TrimTime())
        //                {
        //                    LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Reject : ไม่สามารถเปลี่ยนวันที่การ Booking ได้");
        //                    result.ResultMsg = "ไม่สามารถเปลี่ยนวันที่การ Booking ได้";
        //                    result.ResultIsUse = false;
        //                    return result;
        //                }
        //            }

        //            List<Ms_Dock> get_dock = dbMS.Ms_Dock.Where(c => c.DockType_Index == Guid.Parse("BDB6CC26-B144-4E44-BC3F-F8E78E0E97AE") && c.IsActive == 1).ToList();
        //            var gettime = db.Ms_DockQoutaInterval.Where(c => get_dock.Select(x => x.Dock_Index).Contains(c.Dock_Index)).ToList();
        //            var gettime_new = (from DQT in db.Ms_DockQoutaInterval.Where(c => get_dock.Select(x => x.Dock_Index).Contains(c.Dock_Index) && c.IsBreakTime == false)
        //                           join D in get_dock on DQT.Dock_Index equals D.Dock_Index
        //                           select new DockInteval
        //                           {
        //                               DockQoutaInterval_Index = DQT.DockQoutaInterval_Index,
        //                               Interval_Start = DQT.Interval_Start,
        //                               Interval_End = DQT.Interval_End,
        //                               Dock_Index = D.Dock_Index,
        //                               Dock_Id = D.Dock_Id,
        //                               Dock_Name = D.Dock_Name,
        //                               seq = D.seq
        //                           }).ToList();

        //            if (!string.IsNullOrEmpty(data.expect_Pickup_Time))
        //            {
        //                LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Time TMS : "+ data.expect_Pickup_Time + ":00");
        //                DateTime date_Booking = get_update.Appointment_Date.AddHours(double.Parse(data.expect_Pickup_Time));
        //                if (DateTime.Now > date_Booking)
        //                {
        //                    LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Reject : เกินเวลาจอง Dock");
        //                    result.ResultMsg = "เกินเวลาจอง Dock";
        //                    result.ResultIsUse = false;
        //                    return result;
        //                }
        //                string Time_S = data.expect_Pickup_Time + ":00";
        //                string Time_E = data.expect_Pickup_Time == "23" ? data.expect_Pickup_Time + ":59" : ((int.Parse(data.expect_Pickup_Time) + 1).ToString().Length > 1 ? (int.Parse(data.expect_Pickup_Time) + 1).ToString() : "0" + (int.Parse(data.expect_Pickup_Time) + 1).ToString()) + ":59";

        //                List<Guid> finduse_time = db.Tb_AppointmentItem.Where(c => c.Appointment_Date == get_update.Appointment_Date.TrimTime()).Select(c => c.DockQoutaInterval_Index).ToList();
        //                DockInteval gettime_frist = gettime_new.OrderBy(c => c.seq).FirstOrDefault(c => !finduse_time.Contains(c.DockQoutaInterval_Index) && c.Interval_Start == (Time_S) && c.Interval_End == (Time_E));
        //                if (gettime_frist != null)
        //                {
        //                    get_timecan_Use = db.Ms_DockQoutaInterval.Where(c => c.DockQoutaInterval_Index == gettime_frist.DockQoutaInterval_Index).FirstOrDefault();
        //                    if (get_timecan_Use == null)
        //                    {
        //                        LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Reject : ประตูในช่วงเวลาที่กำหนดไม่ว่าง");
        //                        result.ResultMsg = "ประตูในช่วงเวลาที่กำหนดไม่ว่าง";
        //                        result.ResultIsUse = false;
        //                        return result;
        //                    }
        //                }
        //                else
        //                {
        //                    LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Reject : เวลาที่ส่งมา ไม่ได้ถูก Config ไว้ในระบบ");
        //                    result.ResultMsg = "เวลาที่ส่งมา ไม่ได้ถูก Config ไว้ในระบบ";
        //                    result.ResultIsUse = false;
        //                    return result;
        //                }
        //            }
        //            else
        //            {
        //                LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Reject : This Route Time didn't Config");
        //                result.ResultIsUse = false;
        //                result.ResultMsg = "This Route Time didn't Config";
        //                return result;
        //            }



        //            var get_updateDetail = db.Tb_AppointmentItemDetail.FirstOrDefault(c => c.AppointmentItem_Index == get_update.AppointmentItem_Index);
        //            if (get_updateDetail != null)
        //            {
        //                LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Appointment ID : "+ get_update.Appointment_Id);
        //                if (get_update.Appointment_Time != (get_timecan_Use.Interval_Start + " - " + get_timecan_Use.Interval_End))
        //                {
        //                    LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Update Dock => Dock : " + get_update.Dock_Name + " To "+ get_timecan_Use.Dock_Name);
        //                    LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Update Dock => Time : " + get_update.Appointment_Time + " To "+ get_timecan_Use.Interval_Start + " - " + get_timecan_Use.Interval_End);


        //                    get_update.Appointment_Time = get_timecan_Use.Interval_Start + " - " + get_timecan_Use.Interval_End;
        //                    get_update.DockQoutaInterval_Index = get_timecan_Use.DockQoutaInterval_Index;
        //                    get_update.Dock_Index = get_timecan_Use.Dock_Index;
        //                    get_update.Dock_Id = get_timecan_Use.Dock_Id;
        //                    get_update.Dock_Name = get_timecan_Use.Dock_Name;
        //                    get_update.Update_By = "OMS TMS";
        //                    get_update.Update_Date = DateTime.Now;
        //                    updateDock = true;
        //                }

        //                LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Update Detail => Driver Name : " + get_updateDetail.Driver_Name + " To " + data.Driver_Name);
        //                LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Update Detail => Vehicle No : " + get_updateDetail.Vehicle_No + " To " + data.Vehicle_no);
        //                LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Update Detail => VehicleType Name : " + get_updateDetail.VehicleType_Name + " To " + data.VehicleType_Name);
        //                get_updateDetail.Driver_Name = data.Driver_Name;
        //                get_updateDetail.Vehicle_No = data.Vehicle_no;
        //                get_updateDetail.VehicleType_Index = data.VehicleType_Index;
        //                get_updateDetail.VehicleType_Id = data.VehicleType_Id;
        //                get_updateDetail.VehicleType_Name = data.VehicleType_Name;
        //                get_updateDetail.Update_By = "OMS TMS";
        //                get_updateDetail.Update_Date = DateTime.Now;

        //                if (updateDock)
        //                {

        //                    var status = new
        //                    {
        //                        TransportManifest_No = get_update.Ref_Document_No,
        //                        Dock_Name = get_update.Dock_Name,
        //                        Dock_Time = get_update.Appointment_Time,
        //                        Dock_Status = "จอง Booking สำเร็จ"
        //                    };
        //                    var result_api = Utils.SendDataApi<TransportManifest>(new AppSettingConfig().GetUrl("Status_Booking"), JsonConvert.SerializeObject(status));
        //                }

        //                var myTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
        //                try
        //                {
        //                    db.SaveChanges();
        //                    myTransaction.Commit();
        //                }
        //                catch (Exception saveEx)
        //                {
        //                    myTransaction.Rollback();
        //                    throw saveEx;
        //                }

        //                result.ResultIsUse = true;
        //            }
        //            else {
        //                LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Reject : ไม่สามารถหาเลข Appointment นี้ได้");
        //                result.ResultIsUse = false;
        //                result.ResultMsg = "ไม่สามารถหาเลข Appointment นี้ได้";
        //            }
        //        }
        //        else {
        //            LoggingService.DataLogLines("Update_Booking", "Update_Booking" + DateTime.Now.ToString("yyyy-MM-dd"), "Reject : ไม่พบเลข Truckload นี้");
        //            result.ResultIsUse = false;
        //            result.ResultMsg = "ไม่พบเลข Truckload นี้";
        //        }

        //        return result;
        //    }
        //    catch (Exception EX)
        //    {
        //        result.ResultIsUse = false;
        //        result.ResultMsg = EX.Message;  
        //        throw EX;
        //    }
        //}
        #endregion

        #region Checkupdate Went WAVE Or Checkin
        public Result Checkupdate_WentWAVE_OR_Checkin(string body)
        {
            var result = new Result();
            var data = JsonConvert.DeserializeObject<TransportManifestUpdate>(body.ToString());
            try
            {
                Logtxt LoggingService = new Logtxt();
                LoggingService.DataLogLines("check_Update", "check_Update_" + data.Truckload_no + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "Start Check Booking" + DateTime.Now.ToString("yyyy-MM-dd-HHmm"));
                LoggingService.DataLogLines("check_Update", "check_Update_" + data.Truckload_no + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "Json" + body);
                LoggingService.DataLogLines("check_Update", "check_Update_" + data.Truckload_no + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "data" + JsonConvert.SerializeObject(data));

                Tb_YardBalance yardBalance = db.Tb_YardBalance.FirstOrDefault(c => c.Ref_Document_No == data.Truckload_no);

                if (yardBalance != null)
                {
                    if (yardBalance.CheckIn_Index != null)
                    {
                        result.ResultIsUse = false;
                        result.reason_code = 1;
                        result.ResultMsg = "shipment นี้ถูก checkin ไปแล้ว ไม่สามารถทำการยกเลิกได้";
                        return result;
                    }
                    else
                    {
                        result.ResultIsUse = true;
                    }
                }
                else
                {
                    if (data.status == "c")
                    {
                        var checkstatus = dbGI.View_CheckReport_Status.Where(c => c.TruckLoad_No == data.Truckload_no).ToList();
                        if (checkstatus.Count() > 0)
                        {
                            result.ResultIsUse = false;
                            result.reason_code = 2;
                            result.ResultMsg = "ไม่สามารถยกเลิกได้ กรุณาติดต่อฝ่ายคลัง";
                            return result;
                        }
                        else
                        {
                            result.ResultIsUse = true;
                        }
                    }
                    else
                    {
                        var checkstatus = dbGI.View_CheckReport_Status.Where(c => c.TruckLoad_No == data.Truckload_no && c.Document_StatusDocktoStg == null).ToList();
                        if (checkstatus.Count() > 0)
                        {
                            result.ResultIsUse = false;
                            result.reason_code = 2;
                            result.ResultMsg = "ไม่สามารถยกเลิกได้ เนื่องจากยัง wave ไม่เสร็จ";
                            return result;
                        }
                        else
                        {
                            result.ResultIsUse = true;
                        }
                    }

                }


                return result;
            }
            catch (Exception EX)
            {
                result.ResultIsUse = false;
                result.ResultMsg = EX.Message;
                throw EX;
            }
        }
        #endregion

        #endregion

        #region + Delete +

        #region DeleteAppointment
        public bool DeleteAppointment(string jsonData)
        {
            try
            {
                AppointmentSearchModel model = Appointment.GetAppointmentSearchModel(jsonData, true);
                Tb_Appointment appointment = db.Tb_Appointment.Find(model.Appointment_Index);

                if (appointment is null)
                {
                    throw new Exception("Appointment not found");
                }

                List<Tb_AppointmentItem> items = db.Tb_AppointmentItem.Where(w => w.Appointment_Index == appointment.Appointment_Index).ToList();
                List<Tb_AppointmentItemDetail> details = db.Tb_AppointmentItemDetail.Where(w => w.Appointment_Index == appointment.Appointment_Index).ToList();

                if (model.IsRemove)
                {
                    db.Tb_Appointment.Remove(appointment);

                    if ((items?.Count ?? 0) > 0)
                    {
                        db.Tb_AppointmentItem.RemoveRange(items);
                    }

                    if ((details?.Count ?? 0) > 0)
                    {
                        db.Tb_AppointmentItemDetail.RemoveRange(details);
                    }
                }
                else
                {
                    appointment.IsActive = 0;
                    appointment.IsDelete = 1;
                    appointment.Status_Id = -1;
                    appointment.Cancel_By = model.Create_By;
                    appointment.Cancel_Date = DateTime.Now;

                    if ((items?.Count ?? 0) > 0)
                    {
                        items.ForEach(e =>
                        {
                            e.IsActive = 0;
                            e.IsDelete = 1;
                            e.Status_Id = -1;
                            e.Cancel_By = model.Create_By;
                            e.Cancel_Date = DateTime.Now;
                        }
                        );
                    }

                    if ((details?.Count ?? 0) > 0)
                    {
                        details.ForEach(e =>
                        {
                            e.IsActive = 0;
                            e.IsDelete = 1;
                            e.Status_Id = -1;
                            e.Cancel_By = model.Create_By;
                            e.Cancel_Date = DateTime.Now;
                        }
                        );
                    }
                }
                if (appointment.Ref_planGoodsReceive_Index != null)
                {
                    model.planGoodsReceive_Index = appointment.Ref_planGoodsReceive_Index.ToString();
                    model.DocumentType_Index = appointment.DocumentType_Index;
                    var result_api = Utils.SendDataApi<bool>(new AppSettingConfig().GetUrl("PlanGRCancel"), JsonConvert.SerializeObject(model));
                    if (result_api)
                    {
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
                    }
                    else { return false; }
                }
                else
                {
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
                }





                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DeleteAppointmentItem
        public bool DeleteAppointmentItem(string jsonData)
        {
            try
            {
                AppointmentItemSearchModel model = Appointment.GetAppointmentItemSearchModel(jsonData, true);
                Tb_AppointmentItem appointmentItem = db.Tb_AppointmentItem.Find(model.AppointmentItem_Index);

                if (appointmentItem is null)
                {
                    throw new Exception("AppointmentItem not found");
                }

                List<Tb_AppointmentItemDetail> details = db.Tb_AppointmentItemDetail.Where(w => w.AppointmentItem_Index == appointmentItem.AppointmentItem_Index).ToList();

                if (model.IsRemove)
                {
                    db.Tb_AppointmentItem.Remove(appointmentItem);
                    if ((details?.Count ?? 0) > 0)
                    {
                        db.Tb_AppointmentItemDetail.RemoveRange(details);
                    }
                }
                else
                {
                    appointmentItem.IsActive = 0;
                    appointmentItem.IsDelete = 1;
                    appointmentItem.Status_Id = -1;
                    appointmentItem.Cancel_By = model.Create_By;
                    appointmentItem.Cancel_Date = DateTime.Now;

                    if ((details?.Count ?? 0) > 0)
                    {
                        details.ForEach(e =>
                        {
                            e.IsActive = 0;
                            e.IsDelete = 1;
                            e.Status_Id = -1;
                            e.Cancel_By = model.Create_By;
                            e.Cancel_Date = DateTime.Now;
                        }
                        );
                    }
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

        #region DeleteAppointmentItemGroup
        public bool DeleteAppointmentItemGroup(string jsonData)
        {
            try
            {
                AppointmentItemSearchModel model = Appointment.GetAppointmentItemSearchModel(jsonData);
                if (!model.Group_Index.HasValue) { throw new Exception("GroupIndex not found"); }

                List<Tb_AppointmentItem> appointmentItems = db.Tb_AppointmentItem.Where(w => w.Group_Index == model.Group_Index).ToList();
                if ((appointmentItems?.Count ?? 0) == 0) { throw new Exception("AppointmentItem not found"); }

                List<Guid> listAppointmentItemIndex = appointmentItems.Select(s => s.AppointmentItem_Index).Distinct().ToList();
                List<Tb_AppointmentItemDetail> details = db.Tb_AppointmentItemDetail.Where(w => listAppointmentItemIndex.Contains(w.AppointmentItem_Index)).ToList();

                if (model.IsRemove)
                {
                    db.Tb_AppointmentItem.RemoveRange(appointmentItems);
                    if ((details?.Count ?? 0) > 0)
                    {
                        db.Tb_AppointmentItemDetail.RemoveRange(details);
                    }
                }
                else
                {

                    appointmentItems.ForEach(e =>
                    {
                        e.IsActive = 0;
                        e.IsDelete = 1;
                        e.Status_Id = -1;
                        e.Cancel_By = model.Create_By;
                        e.Cancel_Date = DateTime.Now;
                    });

                    if ((details?.Count ?? 0) > 0)
                    {
                        details.ForEach(e =>
                        {
                            e.IsActive = 0;
                            e.IsDelete = 1;
                            e.Status_Id = -1;
                            e.Cancel_By = model.Create_By;
                            e.Cancel_Date = DateTime.Now;
                        });
                    }
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

        #region DeleteAppointmentItemDetail
        public bool DeleteAppointmentItemDetail(string jsonData)
        {
            try
            {
                AppointmentItemDetailSearchModel model = Appointment.GetAppointmentItemDetailSearchModel(jsonData, true);
                Tb_AppointmentItemDetail appointmentItemDetail = db.Tb_AppointmentItemDetail.Find(model.AppointmentItemDetail_Index);

                if (appointmentItemDetail is null)
                {
                    throw new Exception("AppointmentItemDetail not found");
                }

                if (model.IsRemove)
                {
                    db.Tb_AppointmentItemDetail.Remove(appointmentItemDetail);
                }
                else
                {
                    appointmentItemDetail.IsActive = 0;
                    appointmentItemDetail.IsDelete = 1;
                    appointmentItemDetail.Status_Id = -1;
                    appointmentItemDetail.Cancel_By = model.Create_By;
                    appointmentItemDetail.Cancel_Date = DateTime.Now;
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

        #endregion

        #endregion

        #region + CheckIn +

        #region + List +
        public List<CheckInAppointmentModel> ListAppointmentForCheckIn(string jsonData)
        {
            try
            {
                CheckInSearchModel model = CheckIn.GetCheckInSearchModel(jsonData);
                if (model.Appointment_Id.IsNull()) { throw new Exception("Invalid Parameter : Appointment ID not found"); }

                List<Tb_AppointmentItem> appointmentItem = db.Tb_AppointmentItem.Where(w => w.Appointment_Id == model.Appointment_Id && w.Status_Id == 0).ToList();
                if ((appointmentItem?.Count ?? 0) == 0) { throw new Exception("Appointment not found"); }

                List<CheckInAppointmentModel> result = new List<CheckInAppointmentModel>();
                CheckInAppointmentModel checkInModel;
                CheckInAppointmentItemModel checkInItemModel;

                List<DateTime> dates = appointmentItem.GroupBy(g => g.Appointment_Date.TrimTime()).Select(s => s.Key.TrimTime()).ToList();
                foreach (DateTime date in dates)
                {
                    checkInModel = new CheckInAppointmentModel() { Appointment_Date = date };

                    appointmentItem.Where(w => w.Appointment_Date.TrimTime() == date).GroupBy(g =>
                        new { g.Appointment_Index, g.Appointment_Id, g.Appointment_Time }).Select(s =>
                            new { s.Key.Appointment_Index, s.Key.Appointment_Id, s.Key.Appointment_Time }).ToList().ForEach(e =>
                            {
                                checkInItemModel = new CheckInAppointmentItemModel()
                                {
                                    Appointment_Index = e.Appointment_Index,
                                    Appointment_Id = e.Appointment_Id,
                                    Appointment_Time = e.Appointment_Time,
                                    Appointment_Date = date,
                                    Status = "Pending Check In"
                                };

                                appointmentItem.Where(
                                    w => w.Appointment_Date.TrimTime() == date &&
                                         w.Appointment_Index == e.Appointment_Index &&
                                         w.Appointment_Time == e.Appointment_Time).ToList().ForEach(ea =>
                                         { checkInItemModel.Items.Add(JsonConvert.DeserializeObject<AppointmentItemModel>(JsonConvert.SerializeObject(ea))); });

                                checkInModel.Items.Add(checkInItemModel);
                            });

                    result.Add(checkInModel);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region + Get +
        public AppointmentItemModel GetAppointmentForCheckIn(string jsonData)
        {
            try
            {
                CheckInSearchModel model = CheckIn.GetCheckInSearchModel(jsonData);
                if (!model.AppointmentItem_Index.HasValue) { throw new Exception("Invalid Parameter : Appointment not found"); }

                Tb_AppointmentItem result = db.Tb_AppointmentItem.Find(model.AppointmentItem_Index);
                if (result is null) { throw new Exception("Appointment not found"); }
                if (result.Status_Id == -1) { throw new Exception("Appointment already Cancelled"); }
                if (result.Status_Id == 1) { throw new Exception("Appointment already Checked In"); }

                return JsonConvert.DeserializeObject<AppointmentItemModel>(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region + Save +
        public Result SaveCheckIn(string jsonData)
        {
            Result result = new Result();
            try
            {
                CheckInModel model = CheckIn.GetCheckInModel(jsonData);
                if (model.Appointment_Id == null)
                {
                    result.ResultIsUse = false;
                    result.ResultMsg = "AppointmentItem not found";
                    return result;
                }

                //List<Tb_AppointmentItem> appointmentItem = db.Tb_AppointmentItem.Where(c => c.Appointment_Id == model.Appointment_Id).ToList();
                //if (appointmentItem is null) { throw new Exception("AppointmentItem not found"); }
                //foreach (var item in appointmentItem)
                //{

                //    if (item.Status_Id != 0) { throw new Exception("Invalid CheckIn : AppointmentItem already Processes"); }

                //    Ms_WareHouseQouta wareHouseQouta = db.Ms_WareHouseQouta.FirstOrDefault(w => w.WareHouse_Index == item.WareHouse_Index);
                //    if (wareHouseQouta is null) { throw new Exception("WareHouseQouta not found"); }

                //    Ms_DockQoutaInterval dockQoutaInterval = db.Ms_DockQoutaInterval.Find(item.DockQoutaInterval_Index);
                //    if (dockQoutaInterval is null) { throw new Exception("DockQoutaInterval not found"); }



                //    DateTime checkInDate = (model.CheckIn_Date ?? DateTime.Now).TrimTime();
                //    TimeSpan checkInTime = model.CheckIn_Time ?? TimeSpan.Parse(DateTime.Now.ToShortTimeString());

                //    checkInDate = checkInDate.AddSeconds(checkInTime.TotalSeconds);

                //    TimeSpan intervalStart = TimeSpan.Parse(dockQoutaInterval.Interval_Start);
                //    TimeSpan intervalEnd = TimeSpan.Parse(dockQoutaInterval.Interval_End);

                //    TimeSpan timeStart = new TimeSpan(item.Appointment_Date.Ticks).Add(intervalStart);
                //    TimeSpan timeEnd = new TimeSpan(item.Appointment_Date.Ticks).Add(intervalEnd);
                //    TimeSpan timeCheck = new TimeSpan(checkInDate.Ticks).Add(checkInTime);

                //    var time = item.Appointment_Time.Split('-');
                //    var starttime = time[0].Split(':');
                //    var endtime = time[1].Split(':');
                //    DateTime dateS = item.Appointment_Date.AddHours(double.Parse(starttime[0])).AddMinutes(double.Parse(starttime[1]));
                //    DateTime dateE = item.Appointment_Date.AddHours(double.Parse(endtime[0])).AddMinutes(double.Parse(endtime[1]));

                //    string checkInStatus;
                //    //if (dateS > checkInDate.AddMinutes(-(wareHouseQouta.CheckIn_Limit_Before)))
                //    //{
                //    //    checkInStatus = "Before Time";
                //    //}
                //    if (dateE < checkInDate.AddMinutes(wareHouseQouta.CheckIn_Limit_After))
                //    {
                //        checkInStatus = "Late Time";
                //    }
                //    else
                //    {
                //        checkInStatus = "On Time";
                //    }

                //    item.Status_Id = 1;

                //    Guid checkInIndex = Guid.NewGuid();
                //    Guid yardBalanceIndex = Guid.NewGuid();
                //    Guid yardMovementIndex = Guid.NewGuid();
                //    string userBy = model.Create_By;
                //    DateTime userDate = DateTime.Now;
                //    int activityID = 1;

                //    Tb_CheckIn newCheckIn = new Tb_CheckIn
                //    {
                //        CheckIn_Index = checkInIndex,
                //        Appointment_Index = item.Appointment_Index,
                //        Appointment_Id = item.Appointment_Id,
                //        AppointmentItem_Index = item.AppointmentItem_Index,
                //        AppointmentItem_Id = item.AppointmentItem_Id,
                //        Appointment_Date = item.Appointment_Date,
                //        Appointment_Time = item.Appointment_Time,

                //        CheckIn_Date = checkInDate,
                //        CheckIn_Time = checkInTime,
                //        Remark = model.Remark,

                //        IsActive = 1,
                //        IsDelete = 0,
                //        IsSystem = 0,
                //        Status_Id = 0,
                //        Create_By = userBy,
                //        Create_Date = userDate
                //    };
                //    db.Tb_CheckIn.Add(newCheckIn);

                //    Tb_YardBalance newYardBalance = new Tb_YardBalance
                //    {
                //        YardBalance_Index = yardBalanceIndex,

                //        Appointment_Index = item.Appointment_Index,
                //        Appointment_Id = item.Appointment_Id,
                //        AppointmentItem_Index = item.AppointmentItem_Index,
                //        AppointmentItem_Id = item.AppointmentItem_Id,
                //        DocumentType_Index = item.DocumentType_Index,
                //        DocumentType_Id = item.DocumentType_Id,
                //        DocumentType_Name = item.DocumentType_Name,
                //        Appointment_Date = item.Appointment_Date,
                //        Appointment_Time = item.Appointment_Time,

                //        DockQoutaInterval_Index = item.DockQoutaInterval_Index,
                //        WareHouse_Index = item.WareHouse_Index,
                //        WareHouse_Id = item.WareHouse_Id,
                //        WareHouse_Name = item.WareHouse_Name,
                //        Dock_Index = item.Dock_Index,
                //        Dock_Id = item.Dock_Id,
                //        Dock_Name = item.Dock_Name,
                //        Owner_Index = item.Owner_Index,
                //        Owner_Id = item.Owner_Id,
                //        Owner_Name = item.Owner_Name,
                //        Ref_Document_No = item.Ref_Document_No,
                //        Ref_Document_Date = item.Ref_Document_Date,
                //        ContactPerson_Name = item.ContactPerson_Name,
                //        ContactPerson_EMail = item.ContactPerson_EMail,
                //        ContactPerson_Tel = item.ContactPerson_Tel,
                //        Remark = item.Remark,

                //        CheckIn_Index = newCheckIn.CheckIn_Index,
                //        CheckIn_Date = newCheckIn.CheckIn_Date,
                //        CheckIn_Time = newCheckIn.CheckIn_Time,
                //        CheckIn_Remark = newCheckIn.Remark,
                //        CheckIn_Status = checkInStatus,
                //        CheckIn_By = userBy,

                //        IsActive = 1,
                //        IsDelete = 0,
                //        IsSystem = 0,
                //        Status_Id = 0,
                //        Create_By = userBy,
                //        Create_Date = userDate
                //    };
                //    db.Tb_YardBalance.Add(newYardBalance);

                //    Tb_YardMovement newYardMovement = new Tb_YardMovement
                //    {
                //        YardMovement_Index = yardMovementIndex,
                //        YardBalance_Index = yardBalanceIndex,

                //        Appointment_Index = item.Appointment_Index,
                //        Appointment_Id = item.Appointment_Id,
                //        AppointmentItem_Index = item.AppointmentItem_Index,
                //        AppointmentItem_Id = item.AppointmentItem_Id,
                //        Appointment_Date = item.Appointment_Date,
                //        Appointment_Time = item.Appointment_Time,

                //        Activity_Id = activityID,

                //        CheckIn_Index = newCheckIn.CheckIn_Index,
                //        CheckIn_Date = newCheckIn.CheckIn_Date,
                //        CheckIn_Time = newCheckIn.CheckIn_Time,
                //        CheckIn_Remark = newCheckIn.Remark,
                //        CheckIn_Status = checkInStatus,
                //        CheckIn_By = userBy,

                //        IsActive = 1,
                //        IsDelete = 0,
                //        IsSystem = 0,
                //        Status_Id = 0,
                //        Create_By = userBy,
                //        Create_Date = userDate
                //    };
                //    db.Tb_YardMovement.Add(newYardMovement);
                //}

                List<Tb_YardBalance> appointmentItem = db.Tb_YardBalance.Where(w => w.Appointment_Id == model.Appointment_Id).ToList();
                if (appointmentItem is null)
                {
                    result.ResultIsUse = false;
                    result.ResultMsg = "AppointmentItem not found";
                    return result;
                }
                foreach (var item in appointmentItem)
                {

                    var dock = dbMS.Ms_Dock.FirstOrDefault(c => c.Dock_Index == item.Dock_Index);
                    if (dock.Dock_use == 1)
                    {
                        result.ResultIsUse = false;
                        result.ResultMsg = "Dock is use";
                        return result;
                    }
                    if (item.Status_Id != 0)
                    {
                        result.ResultIsUse = false;
                        result.ResultMsg = "Invalid CheckIn : AppointmentItem already Processes";
                        return result;

                    }

                    Ms_WareHouseQouta wareHouseQouta = db.Ms_WareHouseQouta.FirstOrDefault(w => w.WareHouse_Index == item.WareHouse_Index);
                    if (wareHouseQouta is null)
                    {
                        result.ResultIsUse = false;
                        result.ResultMsg = "WareHouseQouta not found";
                        return result;
                    }

                    Ms_DockQoutaInterval dockQoutaInterval = db.Ms_DockQoutaInterval.Find(item.DockQoutaInterval_Index);
                    if (dockQoutaInterval is null)
                    {
                        result.ResultIsUse = false;
                        result.ResultMsg = "DockQoutaInterval not found";
                        return result;
                    }



                    DateTime checkInDate = (model.CheckIn_Date ?? DateTime.Now).TrimTime();
                    TimeSpan checkInTime = model.CheckIn_Time ?? TimeSpan.Parse(DateTime.Now.ToShortTimeString());

                    checkInDate = checkInDate.AddSeconds(checkInTime.TotalSeconds);

                    TimeSpan intervalStart = TimeSpan.Parse(dockQoutaInterval.Interval_Start);
                    TimeSpan intervalEnd = TimeSpan.Parse(dockQoutaInterval.Interval_End);

                    TimeSpan timeStart = new TimeSpan(item.Appointment_Date.Value.Ticks).Add(intervalStart);
                    TimeSpan timeEnd = new TimeSpan(item.Appointment_Date.Value.Ticks).Add(intervalEnd);
                    TimeSpan timeCheck = new TimeSpan(checkInDate.Ticks).Add(checkInTime);

                    var time = item.Appointment_Time.Split('-');
                    var starttime = time[0].Split(':');
                    var endtime = time[1].Split(':');
                    DateTime dateS = item.Appointment_Date.Value.AddHours(double.Parse(starttime[0])).AddMinutes(double.Parse(starttime[1]));
                    DateTime dateE = item.Appointment_Date.Value.AddHours(double.Parse(endtime[0])).AddMinutes(double.Parse(endtime[1]));

                    string checkInStatus;
                    //if (dateS > checkInDate.AddMinutes(-(wareHouseQouta.CheckIn_Limit_Before)))
                    //{
                    //    checkInStatus = "Before Time";
                    //}
                    if (dateE < checkInDate.AddMinutes(wareHouseQouta.CheckIn_Limit_After))
                    {
                        checkInStatus = "Late Time";
                    }
                    else
                    {
                        checkInStatus = "On Time";
                    }

                    item.Status_Id = 1;

                    Guid checkInIndex = Guid.NewGuid();
                    Guid yardBalanceIndex = Guid.NewGuid();
                    Guid yardMovementIndex = Guid.NewGuid();
                    string userBy = model.Create_By;
                    DateTime userDate = DateTime.Now;
                    int activityID = 1;

                    Tb_CheckIn newCheckIn = new Tb_CheckIn
                    {
                        CheckIn_Index = checkInIndex,
                        Appointment_Index = item.Appointment_Index,
                        Appointment_Id = item.Appointment_Id,
                        AppointmentItem_Index = item.AppointmentItem_Index,
                        AppointmentItem_Id = item.AppointmentItem_Id,
                        Appointment_Date = item.Appointment_Date.Value,
                        Appointment_Time = item.Appointment_Time,

                        CheckIn_Date = checkInDate,
                        CheckIn_Time = checkInTime,
                        Remark = model.Remark,

                        IsActive = 1,
                        IsDelete = 0,
                        IsSystem = 0,
                        Status_Id = 0,
                        Create_By = userBy,
                        Create_Date = userDate
                    };
                    db.Tb_CheckIn.Add(newCheckIn);


                    item.CheckIn_Index = newCheckIn.CheckIn_Index;
                    item.CheckIn_Date = newCheckIn.CheckIn_Date;
                    item.CheckIn_Time = newCheckIn.CheckIn_Time;
                    item.CheckIn_Remark = newCheckIn.Remark;
                    item.CheckIn_By = userBy;
                    item.CheckIn_Status = checkInStatus;
                    item.Status_Id = 0;

                    Tb_YardMovement newYardMovement = new Tb_YardMovement
                    {
                        YardMovement_Index = yardMovementIndex,
                        YardBalance_Index = item.YardBalance_Index,

                        Appointment_Index = item.Appointment_Index,
                        Appointment_Id = item.Appointment_Id,
                        AppointmentItem_Index = item.AppointmentItem_Index,
                        AppointmentItem_Id = item.AppointmentItem_Id,
                        Appointment_Date = item.Appointment_Date.Value,
                        Appointment_Time = item.Appointment_Time,

                        Activity_Id = activityID,

                        CheckIn_Index = newCheckIn.CheckIn_Index,
                        CheckIn_Date = newCheckIn.CheckIn_Date,
                        CheckIn_Time = newCheckIn.CheckIn_Time,
                        CheckIn_Remark = newCheckIn.Remark,
                        CheckIn_Status = checkInStatus,
                        CheckIn_By = userBy,

                        IsActive = 1,
                        IsDelete = 0,
                        IsSystem = 0,
                        Status_Id = 0,
                        Create_By = userBy,
                        Create_Date = userDate
                    };
                    db.Tb_YardMovement.Add(newYardMovement);


                    var dock_update = dbMS.Ms_Dock.FirstOrDefault(c => c.Dock_Index == item.Dock_Index);
                    dock_update.Last_Checkin = DateTime.Now;
                    dock_update.Dock_use = 1;



                }
                Tb_Appointment appointmentstatus = db.Tb_Appointment.FirstOrDefault(w => w.Appointment_Id == model.Appointment_Id);
                var update = db.Tb_Appointment.Find(appointmentstatus.Appointment_Index);
                update.Document_status = 2;

                //if (appointmentstatus.DocumentType_Index == Guid.Parse("C392D865-8E69-4985-B72F-2421EBE8BCDB") || appointmentstatus.DocumentType_Index == Guid.Parse("13091631-5829-4341-BCB4-272B0ED854D7"))
                //{
                //    Result result_api = Utils.GetDataApi<Result>(new AppSettingConfig().GetUrl("GI_Before_Checkin"), appointmentItem[0].Ref_Document_No);

                //    if (!result_api.ResultIsUse)
                //    {
                //        result.ResultIsUse = false;
                //        result.ResultMsg = "การสร้าง ข้อมูล ไม่สำเร็จ";
                //        return result;
                //    }
                //}

                var myTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    myTransaction.Commit();
                }

                catch (Exception saveEx)
                {
                    result.ResultIsUse = false;
                    result.ResultMsg = saveEx.Message;
                    return result;
                }
                var myTransactionMS = dbMS.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    dbMS.SaveChanges();
                    myTransactionMS.Commit();
                }

                catch (Exception saveEx)
                {
                    result.ResultIsUse = false;
                    result.ResultMsg = saveEx.Message;
                    return result;
                }

                result.ResultIsUse = true;
                return result;
            }
            catch (Exception ex)
            {
                result.ResultIsUse = false;
                result.ResultMsg = ex.Message;
                return result;
            }
        }
        #endregion

        #region + Delete +
        public bool DeleteCheckIn(string jsonData)
        {
            try
            {
                CheckInSearchModel model = CheckIn.GetCheckInSearchModel(jsonData, true);

                Tb_CheckIn checkIn = db.Tb_CheckIn.Find(model.CheckIn_Index);
                if (checkIn is null)
                {
                    throw new Exception("CheckIn not found");
                }

                Tb_YardMovement movement = db.Tb_YardMovement.FirstOrDefault(w => w.CheckIn_Index == model.CheckIn_Index);
                if (movement is null)
                {
                    throw new Exception("Movement not found");
                }

                Tb_YardBalance balance = db.Tb_YardBalance.FirstOrDefault(w => w.CheckIn_Index == model.CheckIn_Index);
                if (balance is null)
                {
                    throw new Exception("Balance not found");
                }

                Tb_AppointmentItem appointmentItem = db.Tb_AppointmentItem.Find(model.AppointmentItem_Index);
                if (appointmentItem is null)
                {
                    throw new Exception("AppointmentItem not found");
                }

                if (model.IsRemove)
                {
                    db.Tb_CheckIn.Remove(checkIn);
                    db.Tb_YardMovement.Remove(movement);
                    db.Tb_YardBalance.Remove(balance);
                }
                else
                {
                    string userBy = model.Create_By;
                    DateTime userDate = DateTime.Now;

                    checkIn.IsDelete = 1;
                    checkIn.Status_Id = -1;
                    checkIn.Cancel_By = userBy;
                    checkIn.Cancel_Date = userDate;

                    movement.IsDelete = 1;
                    movement.Status_Id = -1;
                    movement.Cancel_By = userBy;
                    movement.Cancel_Date = userDate;

                    balance.IsDelete = 1;
                    balance.Status_Id = -1;
                    balance.Cancel_By = userBy;
                    balance.Cancel_Date = userDate;

                    appointmentItem.Status_Id = 0;
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

        #endregion

        #region + CheckOut +

        #region + List +
        public List<CheckOutAppointmentModel> ListAppointmentForCheckOut(string jsonData)
        {
            try
            {
                CheckOutSearchModel model = CheckOut.GetCheckOutSearchModel(jsonData);
                if (model.Appointment_Id.IsNull()) { throw new Exception("Invalid Parameter : Appointment ID not found"); }

                List<Tb_YardBalance> appointmentItem = db.Tb_YardBalance.Where(w => w.Appointment_Id == model.Appointment_Id && w.Status_Id == 0).ToList();
                if ((appointmentItem?.Count ?? 0) == 0) { throw new Exception("Appointment not found"); }

                List<CheckOutAppointmentModel> result = new List<CheckOutAppointmentModel>();
                CheckOutAppointmentModel checkOutModel;
                CheckOutAppointmentItemModel checkOutItemModel;

                List<DateTime> dates = appointmentItem.GroupBy(g => g.Appointment_Date.Value.TrimTime()).Select(s => s.Key.TrimTime()).ToList();
                foreach (DateTime date in dates)
                {
                    checkOutModel = new CheckOutAppointmentModel() { Appointment_Date = date };

                    appointmentItem.Where(w => w.Appointment_Date.Value.TrimTime() == date).GroupBy(g =>
                        new { g.Appointment_Index, g.Appointment_Id, g.Appointment_Time }).Select(s =>
                            new { s.Key.Appointment_Index, s.Key.Appointment_Id, s.Key.Appointment_Time }).ToList().ForEach(e =>
                            {
                                checkOutItemModel = new CheckOutAppointmentItemModel()
                                {
                                    Appointment_Index = e.Appointment_Index,
                                    Appointment_Id = e.Appointment_Id,
                                    Appointment_Time = e.Appointment_Time,
                                    Appointment_Date = date,
                                    Status = "Checked In"
                                };

                                appointmentItem.Where(
                                    w => w.Appointment_Date.Value.TrimTime() == date &&
                                         w.Appointment_Index == e.Appointment_Index &&
                                         w.Appointment_Time == e.Appointment_Time).ToList().ForEach(ea =>
                                         { checkOutItemModel.Items.Add(JsonConvert.DeserializeObject<AppointmentItemModel>(JsonConvert.SerializeObject(ea))); });

                                checkOutModel.Items.Add(checkOutItemModel);
                            });

                    result.Add(checkOutModel);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region + Get +
        public AppointmentItemModel GetAppointmentForCheckOut(string jsonData)
        {
            try
            {
                CheckOutSearchModel model = CheckOut.GetCheckOutSearchModel(jsonData);
                if (model.Appointment_Id == null) { throw new Exception("Invalid Parameter : Appointment Id not found"); }

                Tb_YardBalance result = db.Tb_YardBalance.FirstOrDefault(w => w.Appointment_Id == model.Appointment_Id);
                if (result is null) { throw new Exception("Appointment not found"); }
                if (result.Status_Id == -1) { throw new Exception("Appointment state is not Checked In"); }
                if (result.Status_Id == 1) { throw new Exception("Appointment already Checked Out"); }

                return JsonConvert.DeserializeObject<AppointmentItemModel>(JsonConvert.SerializeObject(result));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region + Save +
        public Result SaveCheckOut(string jsonData)
        {
            Logtxt LoggingService = new Logtxt();
            var result = new Result();
            var id = "";
            try
            {
                CheckOutModel model = CheckOut.GetCheckOutModel(jsonData);
                id = model.Appointment_Id;
                LoggingService.DataLogLines("Checkout", "Checkout" + model.Appointment_Id + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "Start Checkout" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                LoggingService.DataLogLines("Checkout", "Checkout" + model.Appointment_Id + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "Checkout ID : " + model.Appointment_Id + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                if (model.Appointment_Id == null) { throw new Exception("Invalid Parameter : Appointment Id not found"); }

                List<Tb_YardBalance> appointmentItem = db.Tb_YardBalance.Where(w => w.Appointment_Id == model.Appointment_Id).ToList();
                if (appointmentItem is null) { throw new Exception("AppointmentItem not found"); }
                else if (appointmentItem[0].DocumentType_Index == Guid.Parse("C392D865-8E69-4985-B72F-2421EBE8BCDB"))
                {
                    var checkload = dbGI.im_TruckLoad.FirstOrDefault(c => c.TruckLoad_No == appointmentItem[0].Ref_Document_No && c.Document_Status == 2);
                    if (checkload == null)
                    {
                        result.ResultIsUse = false;
                        result.ResultMsg = "กรุณาทำการ Load สินค้าก่อนทำการ Check out dock";
                        LoggingService.DataLogLines("Checkout", "Checkout" + model.Appointment_Id + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "Checkout Error : " + result.ResultMsg + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                        return result;
                    }
                }
                foreach (var item in appointmentItem)
                {

                    if (item.Status_Id != 0) { throw new Exception("Invalid CheckIn : AppointmentItem already Processes"); }

                    string checkOutStatus = "Checked Out";
                    DateTime checkOutDate = (model.CheckOut_Date ?? DateTime.Now).TrimTime();
                    TimeSpan checkOutTime = model.CheckOut_Time ?? TimeSpan.Parse(DateTime.Now.ToShortTimeString());

                    Guid checkOutIndex = Guid.NewGuid();
                    Guid yardMovementIndex = Guid.NewGuid();
                    string userBy = model.Create_By;
                    DateTime userDate = DateTime.Now;
                    int activityID = 2;

                    Tb_CheckOut newCheckOut = new Tb_CheckOut
                    {
                        CheckOut_Index = checkOutIndex,
                        Appointment_Index = item.Appointment_Index,
                        Appointment_Id = item.Appointment_Id,
                        AppointmentItem_Index = item.AppointmentItem_Index,
                        AppointmentItem_Id = item.AppointmentItem_Id,
                        Appointment_Date = item.Appointment_Date.Value,
                        Appointment_Time = item.Appointment_Time,

                        CheckOut_Date = checkOutDate,
                        CheckOut_Time = checkOutTime,
                        Remark = model.Remark,

                        IsActive = 1,
                        IsDelete = 0,
                        IsSystem = 0,
                        Status_Id = 0,
                        Create_By = userBy,
                        Create_Date = userDate
                    };
                    db.Tb_CheckOut.Add(newCheckOut);

                    item.CheckOut_Index = newCheckOut.CheckOut_Index;
                    item.CheckOut_Date = newCheckOut.CheckOut_Date;
                    item.CheckOut_Time = newCheckOut.CheckOut_Time;
                    item.CheckOut_Remark = newCheckOut.Remark;
                    item.CheckOut_By = userBy;
                    item.CheckOut_Status = checkOutStatus;
                    item.Status_Id = 1;

                    Tb_YardMovement newYardMovement = new Tb_YardMovement
                    {
                        YardMovement_Index = yardMovementIndex,
                        YardBalance_Index = item.YardBalance_Index,

                        Appointment_Index = item.Appointment_Index,
                        Appointment_Id = item.Appointment_Id,
                        AppointmentItem_Index = item.AppointmentItem_Index,
                        AppointmentItem_Id = item.AppointmentItem_Id,
                        Appointment_Date = item.Appointment_Date.Value,
                        Appointment_Time = item.Appointment_Time,

                        Activity_Id = activityID,

                        CheckOut_Index = newCheckOut.CheckOut_Index,
                        CheckOut_Date = newCheckOut.CheckOut_Date,
                        CheckOut_Time = newCheckOut.CheckOut_Time,
                        CheckOut_Remark = newCheckOut.Remark,
                        CheckOut_Status = checkOutStatus,
                        CheckOut_By = userBy,

                        IsActive = 1,
                        IsDelete = 0,
                        IsSystem = 0,
                        Status_Id = 0,
                        Create_By = userBy,
                        Create_Date = userDate
                    };
                    db.Tb_YardMovement.Add(newYardMovement);

                    var dock_update = dbMS.Ms_Dock.FirstOrDefault(c => c.Dock_Index == item.Dock_Index);
                    dock_update.Last_Checkout = DateTime.Now;
                    dock_update.Dock_use = 0;

                    if (item.DocumentType_Index == Guid.Parse("C392D865-8E69-4985-B72F-2421EBE8BCDB") || item.DocumentType_Index == Guid.Parse("13091631-5829-4341-BCB4-272B0ED854D7"))
                    {
                        var transportManifest = new TransportManifest();
                        var Appdetail = db.Tb_AppointmentItemDetail.Where(c => c.AppointmentItem_Index == item.AppointmentItem_Index).ToList();
                        foreach (var itemXX in Appdetail)
                        {
                            var itemX = new Item();
                            if (itemXX.TransportManifest_Index != null)
                            {
                                LoggingService.DataLogLines("Checkout", "Checkout" + model.Appointment_Id + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "Checkout Send TMS  ");
                                var Appitem = db.Tb_AppointmentItem.FirstOrDefault(c => c.AppointmentItem_Index == item.AppointmentItem_Index);
                                LoggingService.DataLogLines("Checkout", "Checkout" + model.Appointment_Id + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "Checkout Shipment No. : " + Appitem.Ref_Document_No + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                                dbGI.Database.SetCommandTimeout(360);

                                var TM_NO = new SqlParameter("@TruckLoad_No", Appitem.Ref_Document_No);
                                var tagout = dbGI.View_tagout_with_truckload.FromSql("sp_View_tagout_with_truckload @TruckLoad_No", TM_NO).ToList();
                                //var tagout = dbGI.View_tagout_with_truckload.Where(c => c.TruckLoad_No == Appitem.Ref_Document_No).ToList();
                                if (tagout.Count > 0)
                                {
                                    foreach (var item_tag in tagout)
                                    {
                                        var ItemsDO = new itemsDO();
                                        ItemsDO.TransportOrder_No = item_tag.PlanGoodsIssue_No;
                                        ItemsDO.Tag_No = item_tag.TagOut_No;
                                        ItemsDO.Product_Id = item_tag.Product_Id;
                                        ItemsDO.Qty = item_tag.QTY;
                                        ItemsDO.ProductConversion_Name = item_tag.ProductConversion_Name;
                                        ItemsDO.Width = item_tag.Width;
                                        ItemsDO.Length = item_tag.Length;
                                        ItemsDO.Height = item_tag.Height;
                                        ItemsDO.Volume = item_tag.Volume;
                                        ItemsDO.Weight_Unit = item_tag.Weight;
                                        itemX.itemsDO.Add(ItemsDO);
                                    }
                                }
                                else
                                {

                                    result.ResultIsUse = false;
                                    result.ResultMsg = "ไม่พบ Tag ที่จะทำการส่ง";
                                    LoggingService.DataLogLines("Checkout", "Checkout" + model.Appointment_Id + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "Checkout Error : " + result.ResultMsg + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                                    return result;
                                }

                                itemX.transportManifest_Index = itemXX.TransportManifest_Index.ToString();
                                itemX.transportManifest_No = Appitem.Ref_Document_No;
                                itemX.release_Index = Guid.NewGuid();
                                transportManifest.items.Add(itemX);

                            }
                        }
                        //if (transportManifest.items.Count > 0)
                        //{
                        //    //LoggingService.DataLogLines("Checkout", "Checkout" + DateTime.Now.ToString("yyyy-MM-dd"), "Checkout model : " + transportManifest);
                        //    LoggingService.DataLogLines("Checkout", "Checkout" + model.Appointment_Id + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "Checkout TMS : " + JsonConvert.SerializeObject(transportManifest) + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                        //    var result_api = Utils.SendDataApi_release_manifest<TransportManifest>(new AppSettingConfig().GetUrl("release_manifest"), JsonConvert.SerializeObject(transportManifest), "02B31868-9D3D-448E-B023-05C121A424F4");
                        //    //foreach (var code in result_api.items)
                        //    //{
                        //    LoggingService.DataLogLines("Checkout", "Checkout" + model.Appointment_Id + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "Checkout TMS : " + JsonConvert.SerializeObject(result_api) + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                        //    if (!result_api.response)
                        //        {
                        //            result.ResultIsUse = false;
                        //            result.ResultMsg = "ไม่สามารถ ปล่อยรถได้" + result_api.message;
                        //            LoggingService.DataLogLines("Checkout", "Checkout" + model.Appointment_Id + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "Checkout Error : " + JsonConvert.SerializeObject(result_api) + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                        //            return result;
                        //        }
                        //    //}
                        //}


                    }


                    var myTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                    try
                    {
                        db.SaveChanges();
                        myTransaction.Commit();

                        foreach (var itemmodel in appointmentItem)
                        {
                            if (itemmodel.DocumentType_Index == Guid.Parse("C392D865-8E69-4985-B72F-2421EBE8BCDB"))
                            {
                                var truckLoads = dbGI.im_TruckLoad.FirstOrDefault(c => c.TruckLoad_No == itemmodel.Ref_Document_No && c.Document_Status != -1);
                                if (truckLoads != null)
                                {
                                    List<DataAccess.Models.GI.Table.im_TruckLoadItem> truckLoadItems = dbGI.im_TruckLoadItem.Where(c => c.TruckLoad_Index == truckLoads.TruckLoad_Index).ToList();

                                    foreach (var itemList in truckLoadItems)
                                    {
                                        var des = "นำจ่าย";
                                        try
                                        {

                                            var resmodel = new
                                            {
                                                referenceNo = itemList.PlanGoodsIssue_No,
                                                status = 104,
                                                statusAfter = 104,
                                                statusBefore = 103,
                                                statusDesc = des,
                                                statusDateTime = DateTime.Now
                                            };
                                            SaveLogRequest(itemList.PlanGoodsIssue_No, JsonConvert.SerializeObject(resmodel), des, 1, des, Guid.NewGuid());
                                            var result_api = Utils.SendDataApi<DemoCallbackResponseViewModel>(new AppSettingConfig().GetUrl("TMS_status"), JsonConvert.SerializeObject(resmodel));
                                            SaveLogResponse(itemList.PlanGoodsIssue_No, JsonConvert.SerializeObject(result_api), resmodel.statusDesc, 2, resmodel.statusDesc, Guid.NewGuid());
                                        }
                                        catch (Exception ex)
                                        {
                                            SaveLogResponse(itemList.PlanGoodsIssue_No, JsonConvert.SerializeObject(ex.Message), des, -1, des, Guid.NewGuid());
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception saveEx)
                    {
                        LoggingService.DataLogLines("Checkout", "Checkout" + model.Appointment_Id + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "Checkout Error : " + saveEx + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                        myTransaction.Rollback();
                        throw saveEx;
                    }
                    var myTransactionMS = dbMS.Database.BeginTransaction(IsolationLevel.Serializable);
                    try
                    {
                        dbMS.SaveChanges();
                        myTransactionMS.Commit();
                    }
                    catch (Exception saveEx)
                    {
                        LoggingService.DataLogLines("Checkout", "Checkout" + model.Appointment_Id + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "Checkout Error : " + saveEx + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                        myTransactionMS.Rollback();
                        throw saveEx;
                    }
                    var check_user = db.Tb_YardBalance.FirstOrDefault(C => C.Appointment_Id == model.Appointment_Id);
                    if (check_user.User_id != null)
                    {
                        var call_Q = callQueue(check_user.User_id);
                        //var result_api = Utils.GetDataApi<string>(new AppSettingConfig().GetUrl("callQueue"), check_user.User_id);
                        //if (result_api == "")
                        //{
                        //    LoggingService.DataLogLines("Autoget_Q", "Autoget_Q" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "Check User " + check_user.User_id + " -------------------------S " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                        //}
                        //else
                        //{
                        //    LoggingService.DataLogLines("Autoget_Q", "Autoget_Q" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "Check User " + check_user.User_id + " -------------------------S " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                        //}
                    }
                }
                result.ResultIsUse = true;
                return result;
            }
            catch (Exception ex)
            {
                LoggingService.DataLogLines("Checkout", "Checkout" + id + "_" + DateTime.Now.ToString("yyyy-MM-dd"), "Checkout Error : " + ex + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                result.ResultIsUse = false;
                result.ResultMsg = ex.Message;
                return result;
            }
        }
        #endregion

        #region + Delete +
        public bool DeleteCheckOut(string jsonData)
        {
            try
            {
                CheckOutSearchModel model = CheckOut.GetCheckOutSearchModel(jsonData, true);

                Tb_CheckOut checkOut = db.Tb_CheckOut.Find(model.CheckOut_Index);
                if (checkOut is null)
                {
                    throw new Exception("CheckOut not found");
                }

                Tb_YardMovement movement = db.Tb_YardMovement.FirstOrDefault(w => w.CheckOut_Index == model.CheckOut_Index);
                if (movement is null)
                {
                    throw new Exception("Movement not found");
                }

                Tb_YardBalance balance = db.Tb_YardBalance.FirstOrDefault(w => w.CheckOut_Index == model.CheckOut_Index);
                if (balance is null)
                {
                    throw new Exception("Balance not found");
                }

                balance.CheckOut_Index = null;
                balance.CheckOut_Date = null;
                balance.CheckOut_Time = null;
                balance.CheckOut_Status = null;
                balance.CheckOut_By = null;
                balance.CheckOut_Remark = null;

                if (model.IsRemove)
                {
                    db.Tb_CheckOut.Remove(checkOut);
                    db.Tb_YardMovement.Remove(movement);
                }
                else
                {
                    string userBy = model.Create_By;
                    DateTime userDate = DateTime.Now;

                    checkOut.IsDelete = 1;
                    checkOut.Status_Id = -1;
                    checkOut.Cancel_By = userBy;
                    checkOut.Cancel_Date = userDate;

                    movement.IsDelete = 1;
                    movement.Status_Id = -1;
                    movement.Cancel_By = userBy;
                    movement.Cancel_Date = userDate;
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

        #endregion

        #region + Balance +

        #region + List +
        public List<AppointmentWithItemModel> ListAppointmentItem(string jsonData)
        {
            try
            {
                AppointmentSearchModel model = Appointment.GetAppointmentSearchModel(jsonData);

                if (model.DatePeriod != null)
                {
                    var date = model.DatePeriod.Split('-');
                    var dateS = date[0];
                    var dateE = date[1];
                }
                List<Tb_Appointment> appointment = db.Tb_Appointment.Where(c => c.Document_status != 0 && c.In_queue == 1).ToList();
                if (model.Appointment_Id != null && model.Appointment_Id != "")
                {
                    appointment = appointment.Where(a => a.Appointment_Id == model.Appointment_Id).ToList();
                }
                List<Guid> appointment_guid = appointment.Select(c => c.Appointment_Index).ToList();
                var statusModels = new List<int?>();

                List<AppointmentWithItemModel> result = new List<AppointmentWithItemModel>();
                AppointmentWithItemModel apponitmentlist;

                var statusModels_InAndOut = new List<int?>();

                var qurey = db.Tb_YardBalance.Where(
                        w => w.IsDelete == 0 &&
                             w.Status_Id != -1 &
                             w.WareHouse_Index.IsEquals(model.WareHouse_Index) &&
                             w.Dock_Index.IsEquals(model.Dock_Index) &&
                             appointment_guid.Contains(w.Appointment_Index)).ToList();
                if (model.Appointment_Id != null && model.Appointment_Id != "")
                {
                    qurey = qurey.Where(w => w.Appointment_Id == model.Appointment_Id).ToList();
                }
                if (model.Appointment_Date != null && model.Appointment_Date_To != null)
                {
                    qurey = qurey.Where(w => w.Appointment_Date >= model.Appointment_Date && w.Appointment_Date <= model.Appointment_Date_To).ToList();
                }
                else
                {
                    qurey = qurey.Where(w => w.Appointment_Date >= DateTime.Now.TrimTime() && w.Appointment_Date <= DateTime.Now.TrimTime().AddDays(1)).ToList();
                }

                List<String> yardBalance_id = qurey.GroupBy(g => g.Appointment_Id).Select(s => s.Key).ToList();

                foreach (var yardbalanceList in yardBalance_id)
                {

                    var yardBalance = db.Tb_YardBalance.FirstOrDefault(w => w.Appointment_Id == yardbalanceList);
                    apponitmentlist = new AppointmentWithItemModel();
                    apponitmentlist.Appointment_Index = yardBalance.Appointment_Index;
                    apponitmentlist.Appointment_Id = yardBalance.Appointment_Id;
                    apponitmentlist.Appointment_Date = yardBalance.Appointment_Date.Value;
                    apponitmentlist.Dock_Name = yardBalance.Dock_Name;
                    apponitmentlist.Ref_Document_No = yardBalance.Ref_Document_No;
                    apponitmentlist.Appointment_Time = yardBalance.Appointment_Time;
                    apponitmentlist.DocumentType_Name = yardBalance.DocumentType_Name;
                    apponitmentlist.DocumentType_Index = yardBalance.DocumentType_Index;
                    apponitmentlist.DocumentType_Id = yardBalance.DocumentType_Id;
                    apponitmentlist.Owner_Id = yardBalance.Owner_Id;
                    apponitmentlist.Owner_Index = yardBalance.Owner_Index;
                    apponitmentlist.Owner_Name = yardBalance.Owner_Name;
                    apponitmentlist.Status = yardBalance.CheckIn_Index.HasValue ? (yardBalance.CheckOut_Index.HasValue ? 2 : 1) : 0;
                    apponitmentlist.Status_Desc = yardBalance.CheckIn_Index.HasValue ? (yardBalance.CheckOut_Index.HasValue ? "Checked Out" : "Checked In") : "";
                    apponitmentlist.Create_By = yardBalance.Create_By;
                    apponitmentlist.Update_By = yardBalance.Update_By;
                    apponitmentlist.Cancel_By = yardBalance.Cancel_By;
                    apponitmentlist.CheckIn_Date = yardBalance.CheckIn_Date;
                    apponitmentlist.CheckIn_Time = yardBalance.CheckIn_Time;
                    apponitmentlist.CheckIn_By = yardBalance.CheckIn_By;
                    apponitmentlist.CheckIn_Index = yardBalance.CheckIn_Index;
                    apponitmentlist.CheckIn_Remark = yardBalance.CheckIn_Remark;
                    apponitmentlist.CheckIn_Status = yardBalance.CheckIn_Status;
                    apponitmentlist.CheckOut_By = yardBalance.CheckOut_By;
                    apponitmentlist.CheckOut_Date = yardBalance.CheckOut_Date;
                    apponitmentlist.CheckOut_Index = yardBalance.CheckOut_Index;
                    apponitmentlist.CheckOut_Remark = yardBalance.CheckOut_Remark;
                    apponitmentlist.CheckOut_Status = yardBalance.CheckOut_Status;
                    apponitmentlist.CheckOut_Time = yardBalance.CheckOut_Time;
                    var yardBalanceitem = db.Tb_YardBalance.Where(w => w.Appointment_Id == yardBalance.Appointment_Id && w.YardBalance_Index != yardBalance.YardBalance_Index).ToList();

                    foreach (var moreitem in yardBalanceitem)
                    {
                        AppointmentItemModel itemyard = new AppointmentItemModel();
                        itemyard.Appointment_Date = moreitem.Appointment_Date.Value;
                        itemyard.Dock_Name = moreitem.Dock_Name;
                        itemyard.Ref_Document_No = moreitem.Ref_Document_No;
                        itemyard.Appointment_Time = moreitem.Appointment_Time;
                        itemyard.DocumentType_Name = moreitem.DocumentType_Name;
                        itemyard.Owner_Name = yardBalance.Owner_Name;
                        itemyard.Status = yardBalance.CheckIn_Index.HasValue ? (yardBalance.CheckOut_Index.HasValue ? 2 : 1) : 0;
                        itemyard.Status_Desc = yardBalance.CheckIn_Index.HasValue ? (yardBalance.CheckOut_Index.HasValue ? "Checked Out" : "Checked In") : "";
                        itemyard.CheckIn_Time = moreitem.CheckIn_Time;
                        itemyard.CheckOut_Time = moreitem.CheckOut_Time;
                        itemyard.CheckIn_Status = moreitem.CheckIn_Status;
                        itemyard.Create_By = moreitem.Create_By;
                        itemyard.Update_By = moreitem.Update_By;
                        itemyard.Cancel_By = moreitem.Cancel_By;
                        apponitmentlist.Items.Add(itemyard);
                    }

                    result.Add(apponitmentlist);
                }

                List<AppointmentWithItemModel> resultOrderBy = new List<AppointmentWithItemModel>();
                int seq = 0;
                result.OrderBy(o => o.Appointment_Id).ToList().ForEach(e =>
                { e.Seq = ++seq; resultOrderBy.Add(e); });

                return resultOrderBy;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region + Cancel +
        public bool CancelBalance(string jsonData)
        {
            try
            {
                AppointmentModel model = Appointment.GetAppointmentModel(jsonData);
                Tb_Appointment appointment = db.Tb_Appointment.Find(model.Appointment_Index);
                if (appointment != null)
                {
                    appointment.Remark = model.Remark;
                    //appointment.Document_status = -1;
                    appointment.Status_Id = -2;
                    appointment.IsActive = 0;
                    appointment.IsDelete = 1;
                    appointment.Cancel_By = model.Cancel_By;
                    appointment.Cancel_Date = DateTime.Now;
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

        #region + Auto Cancel +
        public bool Autocheckcancel(string jsonData)
        {
            try
            {
                var checktimeforcancel = DateTime.Now.AddHours(-6);
                List<Guid> Appointment_Index = db.Tb_Appointment.Where(a => a.Document_status == 1 && a.Status_Id == 0 && a.IsActive == 1 && a.IsDelete == 0).GroupBy(g => g.Appointment_Index).Select(s => s.Key).ToList();
                foreach (Guid listindex in Appointment_Index)
                {
                    var Findfrist = db.Tb_AppointmentItem.Where(w => w.Appointment_Index == listindex).OrderByDescending(s => s.AppointmentItem_Id);
                    Tb_AppointmentItem item = Findfrist.FirstOrDefault(w => w.Appointment_Index == listindex);
                    var time = item.Appointment_Time.Split('-');
                    var endtime = time[1].Split(':');
                    DateTime date = item.Appointment_Date.AddHours(double.Parse(endtime[0])).AddMinutes(double.Parse(endtime[1]));
                    if (date < checktimeforcancel)
                    {
                        Tb_Appointment appointment = db.Tb_Appointment.Find(item.Appointment_Index);
                        if (appointment != null)
                        {
                            appointment.Status_Id = -2;
                            appointment.IsActive = 0;
                            appointment.IsDelete = 1;
                            appointment.Cancel_By = "System";
                            appointment.Cancel_Date = DateTime.Now;
                        }
                    }
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

        #endregion

        #region + Master +
        private string GetDocumentNumber(Guid documentTypeIndex, DateTime documentDate)
        {
            SearchDocumentTypeInClauseViewModel documentType_model = new SearchDocumentTypeInClauseViewModel() { List_DocumentType_Index = new List<Guid>() { documentTypeIndex } };
            ActionResultSearchDocumentTypeModel result_api = Utils.SendDataApi<ActionResultSearchDocumentTypeModel>(new AppSettingConfig().GetUrl("SearchDocumentTypeInClause"), JsonConvert.SerializeObject(documentType_model));

            DocumentType documentType_api = result_api?.ItemsDocumentType?.FirstOrDefault();
            if (documentType_api is null) { throw new Exception("DocumentType not found"); }

            string formatDocument = documentType_api.Format_Document?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(formatDocument)) { throw new Exception("FormatDocument not found"); }

            string formatRunning = documentType_api.Format_Running?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(formatRunning)) { throw new Exception("FormatRunning not found"); }

            bool resetByYear = documentType_api.IsResetByYear == 1;
            bool resetByMonth = documentType_api.IsResetByMonth == 1;

            string docYear = documentDate.Year.ToString();
            string docMonth = !resetByYear ? documentDate.Month.ToString() : string.Empty;
            string docDay = !resetByYear && !resetByMonth ? documentDate.Day.ToString() : string.Empty;
            try
            {
                string formatDate = documentType_api.Format_Date ?? string.Empty;
                if (!string.IsNullOrEmpty(formatDate.Trim()))
                {
                    formatDate = documentDate.ToString(formatDate, System.Globalization.CultureInfo.CreateSpecificCulture("en-GB"));
                }

                Ms_DocumentTypeNumber documentTypeNumber = db.Ms_DocumentTypeNumber.FirstOrDefault(
                    w => w.IsActive == 1 && w.IsDelete == 0 &&
                         w.DocumentType_Index == documentType_api.DocumentType_Index &&
                         w.DocumentTypeNumber_Year == docYear &&
                         w.DocumentTypeNumber_Month == docMonth &&
                         w.DocumentTypeNumber_Day == docDay
                );

                int runningNumber = (documentTypeNumber?.DocumentTypeNumber_Running ?? 0) + 1;
                int runningLength = runningNumber.ToString().Length;
                int formatLength = formatRunning.Length;
                if (formatLength > runningLength)
                {
                    formatRunning = new string('0', formatLength - runningLength) + runningNumber.ToString();
                }
                else
                {
                    formatRunning = runningNumber.ToString().Substring(runningLength - formatLength, formatLength);
                }

                if (documentTypeNumber is null)
                {
                    //Create new Running
                    Ms_DocumentTypeNumber newDocumentTypeNumber = new Ms_DocumentTypeNumber()
                    {
                        DocumentTypeNumber_Index = Guid.NewGuid(),
                        DocumentType_Index = documentTypeIndex,
                        DocumentTypeNumber_Year = docYear,
                        DocumentTypeNumber_Month = docMonth,
                        DocumentTypeNumber_Day = docDay,
                        DocumentTypeNumber_Running = runningNumber,
                        IsActive = 1,
                        IsDelete = 0,
                        IsSystem = 1,
                        Status_Id = 0,
                        Create_By = "System",
                        Create_Date = DateTime.Now
                    };
                    db.Ms_DocumentTypeNumber.Add(newDocumentTypeNumber);
                }
                else
                {
                    documentTypeNumber.DocumentTypeNumber_Running = runningNumber;
                    documentTypeNumber.Update_By = "System";
                    documentTypeNumber.Update_Date = DateTime.Now;
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

                string formatText = documentType_api.Format_Text?.Trim() ?? string.Empty;
                string newDocumentNumber = formatDocument.ToUpper().Replace(" ", string.Empty)
                                                         .Replace("[FORMAT_TEXT]", formatText)
                                                         .Replace("[FORMAT_DATE]", formatDate)
                                                         .Replace("[FORMAT_RUNNING]", formatRunning);

                documentType_api.DocumentRunningNo = newDocumentNumber;
                return newDocumentNumber;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region + ReportPrintOutApponitment +
        public string ReportPrintOutApponitment(string jsonData, string rootPath = "")
        {
            Logtxt LoggingService = new Logtxt();
            ReportPrintAppointmentViewModel data = JsonConvert.DeserializeObject<ReportPrintAppointmentViewModel>(jsonData);
            LoggingService.DataLogLines("ReportPrintOutApponitment", "ReportPrintOutApponitment" + DateTime.Now.ToString("yyyy-MM-dd"), "Start ReportPrintOutApponitment" + DateTime.Now.ToString("yyyy-MM-dd-HHmm"));
            var culture = new System.Globalization.CultureInfo("en-US");
            try
            {
                var query = db.View_RPT_Appointment.AsQueryable();
                LoggingService.DataLogLines("ReportPrintOutApponitment", "ReportPrintOutApponitment" + DateTime.Now.ToString("yyyy-MM-dd"), "data.Appointment_Index" + data.Appointment_Index + DateTime.Now.ToString("yyyy-MM-dd-HHmm"));
                if (data.Appointment_Index != null)
                {
                    query = query.Where(c => c.Appointment_Index == data.Appointment_Index);
                }

                //List<IGrouping<string, DataAccess.Models.Yard.View.View_RPT_Appointment>> refNo = query.Where(c => !(c.Ref_Document_No == null || c.Ref_Document_No == string.Empty)).GroupBy(g => g.Ref_Document_No).ToList();


                var time = DateTime.Now.ToString("HH:mm");
                var count = query.Count();
                var truckload = "";
                var result = new List<ReportPrintAppointmentViewModel>();
                foreach (var List in query)
                {
                    LoggingService.DataLogLines("ReportPrintOutApponitment", "ReportPrintOutApponitment" + DateTime.Now.ToString("yyyy-MM-dd"), "List" + JsonConvert.SerializeObject(List) + DateTime.Now.ToString("yyyy-MM-dd-HHmm"));
                    var vehNo = query.Where(c => !(c.Vehicle_No == null || c.Vehicle_No == string.Empty) && c.VehicleType_Name == List.VehicleType_Name && c.Dock_Name == List.Dock_Name && c.Appointment_Time == List.Appointment_Time && c.Owner_Id == List.Owner_Id && c.Driver_Name == List.Driver_Name).GroupBy(g => g.Vehicle_No).ToList();

                    LoggingService.DataLogLines("ReportPrintOutApponitment", "ReportPrintOutApponitment" + DateTime.Now.ToString("yyyy-MM-dd"), "List" + JsonConvert.SerializeObject(vehNo) + DateTime.Now.ToString("yyyy-MM-dd-HHmm"));
                    Bitmap qrCodeImage;

                    QRCoder.QRCodeGenerator qrGenerator = new QRCoder.QRCodeGenerator();


                    List<string> ref_no_item = new List<string>();

                    if (List.DocumentType_Index == Guid.Parse("c392d865-8e69-4985-b72f-2421ebe8bcdb") || List.DocumentType_Index == Guid.Parse("13091631-5829-4341-BCB4-272B0ED854D7"))
                    {
                        QRCoder.QRCodeData qrCodeData = qrGenerator.CreateQrCode(List.Appointment_Id, QRCoder.QRCodeGenerator.ECCLevel.Q);
                        QRCoder.QRCode qrCode = new QRCoder.QRCode(qrCodeData);
                        qrCodeImage = qrCode.GetGraphic(20);

                        var TruckLoad_no = dbGI.im_TruckLoad.FirstOrDefault(C => C.TruckLoad_No == List.Ref_Document_No);
                        ref_no_item = dbGI.im_TruckLoadItem.Where(c => c.TruckLoad_Index == TruckLoad_no.TruckLoad_Index).GroupBy(g => g.PlanGoodsIssue_No).Select(s => s.Key).ToList();
                        if (TruckLoad_no != null)
                        {
                            truckload = TruckLoad_no.TruckLoad_No;
                        }
                    }
                    else
                    {

                        QRCoder.QRCodeData qrCodeData = qrGenerator.CreateQrCode(List.Appointment_Id, QRCoder.QRCodeGenerator.ECCLevel.Q);
                        QRCoder.QRCode qrCode = new QRCoder.QRCode(qrCodeData);
                        qrCodeImage = qrCode.GetGraphic(20);

                        var plan_no = dbGR.IM_PlanGoodsReceive.FirstOrDefault(C => C.PlanGoodsReceive_No == List.Ref_Document_No);
                        ref_no_item = dbGR.IM_PlanGoodsReceiveItem.Where(c => c.PlanGoodsReceive_Index == plan_no.PlanGoodsReceive_Index).GroupBy(g => g.Ref_Document_No).Select(s => s.Key).ToList();
                    }
                    var resultlist = new ReportPrintAppointmentViewModel()
                    {
                        WareHouse_Name = List.WareHouse_Name,
                        Dock_Name = List.Dock_Name,
                        Doccumenttype_name = List.Doccumenttype_name,
                        Appointment_Id = List.Appointment_Id,
                        Appointment_Barcode = Convert.ToBase64String(BitmapToBytes(qrCodeImage)),
                        Owner_Id = new NetBarcode.Barcode(List.Ref_Document_No, NetBarcode.Type.Code128B).GetBase64Image(),
                        Owner_Name = truckload,
                        Ref_Document_No = string.Join(" , ", ref_no_item),
                        Appointment_Date = List.Appointment_Date.ToString("dd/MM/yyyy"),
                        Appointment_Time = List.Appointment_Time,
                        ContactPerson_Name = List.ContactPerson_Name,
                        ContactPerson_Tel = List.ContactPerson_Tel,
                        ContactPerson_EMail = List.ContactPerson_EMail,
                        VehicleType_Name = List.VehicleType_Name,
                        Vehicle_No = string.Join(" ,", vehNo.Select(s => s.Key)),
                        Driver_Name = List.Driver_Name,
                        Create_Date = List.Create_Date.ToString("dd/MM/yyyy"),
                        remark = List.remark,
                        ASN = List.Ref_Document_No,
                        Vehicle_count = count.ToString()

                    };


                    result.Add(resultlist);
                }



                result.ToList();

                rootPath = rootPath.Replace("\\YardAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportPrintOutAppointment");
                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);
                report.AddDataSource("DataSet2", result);


                string fileName = "";
                string fullPath = "";
                fileName = "tmpReport" + DateTime.Now.ToString("yyyyMMddHHmmss");

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


                var renderedBytes = report.Execute(RenderType.Pdf);

                Libs.Utils objReport = new Libs.Utils();
                fullPath = objReport.saveReport(renderedBytes.MainStream, fileName + ".pdf", rootPath);
                var saveLocation = objReport.PhysicalPath(fileName + ".pdf", rootPath);
                return saveLocation;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static Byte[] BitmapToBytes(Bitmap img)
        {
            using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
        #endregion

        #region + GateBalance +

        #region + ListSummary +
        public List<AppointmentWithItemModel> ListGateAppointmentItemSummary(string jsonData)
        {
            try
            {
                AppointmentSearchModel model = Appointment.GetAppointmentSearchModel(jsonData);

                var appointGatecheck = (from am in db.Tb_Appointment.Where(c => (c.Document_status == 5 || c.Document_status == 1 || c.Document_status == 2))
                                        join ami in db.Tb_AppointmentItem.Where(c => c.IsActive == 1 && c.IsDelete == 0) on am.Appointment_Index equals ami.Appointment_Index
                                        let amid = db.Tb_AppointmentItemDetail.FirstOrDefault(c => c.AppointmentItem_Index == ami.AppointmentItem_Index)
                                        select new
                                        {
                                            am.Appointment_Index,
                                            am.Appointment_Id,
                                            am.DocumentType_Index,
                                            am.DocumentType_Id,
                                            am.DocumentType_Name,
                                            am.Document_status,
                                            am.Status_Id,
                                            am.GateCheckIn_Index,
                                            am.Create_By,
                                            am.Update_By,
                                            am.Cancel_By,
                                            ami.Appointment_Date,
                                            ami.Dock_Index,
                                            ami.Dock_Id,
                                            ami.Dock_Name,
                                            ami.Ref_Document_No,
                                            ami.Appointment_Time,
                                            amid.Vehicle_No
                                        }).ToList();


                #region -- Fillter --
                if (!string.IsNullOrEmpty(model.Appointment_Id))
                {
                    appointGatecheck = appointGatecheck.Where(c => c.Appointment_Id == model.Appointment_Id).ToList();
                }
                if (model.Appointment_Date != null && model.Appointment_Date_To != null)
                {
                    appointGatecheck = appointGatecheck.Where(c => c.Appointment_Date >= model.Appointment_Date && c.Appointment_Date <= model.Appointment_Date_To).ToList();
                }
                else
                {
                    appointGatecheck = appointGatecheck.Where(c => c.Appointment_Date >= DateTime.Now.TrimTime() && c.Appointment_Date <= DateTime.Now.TrimTime().AddDays(1)).ToList();
                }
                if (!string.IsNullOrEmpty(model.Document_no))
                {
                    appointGatecheck = appointGatecheck.Where(c => c.Ref_Document_No == model.Document_no).ToList();
                }
                if (!string.IsNullOrEmpty(model.Vehicle_No))
                {
                    appointGatecheck = appointGatecheck.Where(c => c.Vehicle_No == model.Vehicle_No).ToList();
                }
                #endregion

                List<AppointmentWithItemModel> result = new List<AppointmentWithItemModel>();
                AppointmentWithItemModel apponitmentlist;

                foreach (var itemList in appointGatecheck)
                {
                    apponitmentlist = new AppointmentWithItemModel();
                    var gate = dbMS.ms_GateDock.FirstOrDefault(c => c.Dock_Index == itemList.Dock_Index);
                    if (gate != null)
                    {
                        apponitmentlist.Gate_Name = gate.Gate_Name;
                    }
                    string[] values = itemList.Appointment_Time.Split('-');


                    TimeSpan time = TimeSpan.Parse(values[0]);

                    apponitmentlist.Appointment_Index = itemList.Appointment_Index;
                    apponitmentlist.Appointment_Id = itemList.Appointment_Id;
                    apponitmentlist.DocumentType_Name = itemList.DocumentType_Name;
                    apponitmentlist.Document_status = itemList.Document_status;
                    apponitmentlist.Appointment_Date = itemList.Appointment_Date;
                    apponitmentlist.Dock_Name = itemList.Dock_Name;
                    apponitmentlist.license = itemList.Vehicle_No;
                    apponitmentlist.Ref_Document_No = itemList.Ref_Document_No;
                    apponitmentlist.Appointment_Time = itemList.Appointment_Time;
                    apponitmentlist.DocumentType_Name = itemList.DocumentType_Name;
                    apponitmentlist.DocumentType_Index = itemList.DocumentType_Index;
                    apponitmentlist.DocumentType_Id = itemList.DocumentType_Id;
                    apponitmentlist.Status = itemList.Status_Id == 0 ? 0 : itemList.Status_Id.Value;
                    apponitmentlist.Status_Desc = "";
                    apponitmentlist.Create_By = itemList.Create_By;
                    apponitmentlist.Update_By = itemList.Update_By;
                    apponitmentlist.Cancel_By = itemList.Cancel_By;

                    if (!string.IsNullOrEmpty(itemList.GateCheckIn_Index.ToString()))
                    {
                        var gatecheckin = db.Tb_YardBalance.FirstOrDefault(c => c.GateCheckIn_Index == itemList.GateCheckIn_Index);
                        if (gatecheckin != null)
                        {
                            apponitmentlist.GateCheckIn_Date = gatecheckin.GateCheckIn_Date;
                            apponitmentlist.GateCheckIn_Status = gatecheckin.GateCheckIn_Status;
                            apponitmentlist.GateCheckIn_By = gatecheckin.Create_By;

                            var datein = gatecheckin.CheckIn_Date;
                            if (gatecheckin.CheckIn_Time != null)
                            {
                                var time_in = gatecheckin.CheckIn_Time.ToString().Split(':');
                                datein = datein.GetValueOrDefault().AddHours(double.Parse(time_in[0])).AddMinutes(double.Parse(time_in[1]));
                            }

                            apponitmentlist.CheckIn_Date = datein;
                            apponitmentlist.CheckIn_By = gatecheckin.CheckIn_By;

                            apponitmentlist.GateCheckOut_Date = gatecheckin.GateCheckOut_Date;
                            apponitmentlist.GateCheckOut_Status = gatecheckin.GateCheckOut_Status;
                            apponitmentlist.GateCheckOut_By = gatecheckin.Update_By;

                            var dateout = gatecheckin.CheckOut_Date;
                            if (gatecheckin.CheckOut_Time != null)
                            {
                                var time_Out = gatecheckin.CheckOut_Time.ToString().Split(':');
                                dateout = dateout.GetValueOrDefault().AddHours(double.Parse(time_Out[0])).AddMinutes(double.Parse(time_Out[1]));
                            }

                            apponitmentlist.CheckOut_Date = dateout;
                            apponitmentlist.CheckOut_By = gatecheckin.CheckOut_By;
                        }

                    }

                    result.Add(apponitmentlist);
                }

                List<AppointmentWithItemModel> resultOrderBy = new List<AppointmentWithItemModel>();
                int seq = 0;
                result.OrderByDescending(o => o.Appointment_Date).ThenByDescending(c => c.Appointment_Time).ThenByDescending(c => c.Appointment_Id).ToList().ForEach(e =>
                  { e.Seq = ++seq; resultOrderBy.Add(e); });

                return resultOrderBy;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region + Listin +
        public List<AppointmentWithItemModel> ListGateAppointmentItem(string jsonData)
        {
            try
            {
                AppointmentSearchModel model = Appointment.GetAppointmentSearchModel(jsonData);

                if (model.DatePeriod != null)
                {
                    var date = model.DatePeriod.Split('-');
                    var dateS = date[0];
                    var dateE = date[1];
                }

                List<Tb_Appointment> appointment = db.Tb_Appointment.Where(c => (c.Document_status == 5 || c.Document_status == 1)).ToList();
                if (!string.IsNullOrEmpty(model.Appointment_Id))
                {
                    appointment = appointment.Where(c => c.Appointment_Id == model.Appointment_Id).ToList();
                }


                List<AppointmentWithItemModel> result = new List<AppointmentWithItemModel>();
                AppointmentWithItemModel apponitmentlist;

                var statusModels_InAndOut = new List<int?>();


                foreach (var itemList in appointment)
                {
                    apponitmentlist = new AppointmentWithItemModel();

                    var appointmentitem = db.Tb_AppointmentItem.FirstOrDefault(w => w.Appointment_Id == itemList.Appointment_Id && model.Appointment_Date != null ? (w.Appointment_Date >= model.Appointment_Date && w.Appointment_Date <= model.Appointment_Date_To) : w.Appointment_Id == itemList.Appointment_Id);
                    if (appointmentitem == null) { continue; }
                    var appointmentitemdetail = db.Tb_AppointmentItemDetail.FirstOrDefault(w => w.Appointment_Id == itemList.Appointment_Id && w.Vehicle_No == model.Vehicle_No);
                    if (appointmentitemdetail == null) { continue; }
                    var gate = dbMS.ms_GateDock.FirstOrDefault(c => c.Dock_Index == appointmentitem.Dock_Index);
                    if (gate != null)
                    {
                        apponitmentlist.Gate_Name = gate.Gate_Name;
                    }
                    string[] values = appointmentitem.Appointment_Time.Split('-');

                    TimeSpan time = TimeSpan.Parse(values[0]);

                    apponitmentlist.Appointment_Index = itemList.Appointment_Index;
                    apponitmentlist.Appointment_Id = itemList.Appointment_Id;
                    apponitmentlist.DocumentType_Name = itemList.DocumentType_Name;
                    apponitmentlist.Document_status = itemList.Document_status;
                    apponitmentlist.Appointment_Date = appointmentitem.Appointment_Date.AddSeconds(time.TotalSeconds);
                    apponitmentlist.Dock_Name = appointmentitem.Dock_Name;
                    apponitmentlist.license = appointmentitemdetail.Vehicle_No;
                    apponitmentlist.Ref_Document_No = appointmentitem.Ref_Document_No;
                    apponitmentlist.Appointment_Time = appointmentitem.Appointment_Time;
                    apponitmentlist.DocumentType_Name = appointmentitem.DocumentType_Name;
                    apponitmentlist.DocumentType_Index = appointmentitem.DocumentType_Index;
                    apponitmentlist.DocumentType_Id = appointmentitem.DocumentType_Id;
                    apponitmentlist.Owner_Id = appointmentitem.Owner_Id;
                    apponitmentlist.Owner_Index = appointmentitem.Owner_Index;
                    apponitmentlist.Owner_Name = appointmentitem.Owner_Name;
                    apponitmentlist.Status = itemList.Status_Id == 0 ? 0 : itemList.Status_Id.Value;
                    apponitmentlist.Status_Desc = "";
                    apponitmentlist.Create_By = appointmentitem.Create_By;
                    apponitmentlist.Update_By = appointmentitem.Update_By;
                    apponitmentlist.Cancel_By = itemList.Cancel_By;

                    if (!string.IsNullOrEmpty(itemList.GateCheckIn_Index.ToString()))
                    {
                        var gatecheckin = db.Tb_YardBalance.FirstOrDefault(c => c.GateCheckIn_Index == itemList.GateCheckIn_Index);
                        if (gatecheckin != null)
                        {
                            apponitmentlist.CheckIn_Date = gatecheckin.GateCheckIn_Date;
                            apponitmentlist.GateCheckIn_Status = gatecheckin.GateCheckIn_Status;
                            apponitmentlist.CheckIn_By = gatecheckin.Create_By;

                            apponitmentlist.CheckOut_Date = gatecheckin.GateCheckOut_Date;
                            apponitmentlist.GateCheckOut_Status = gatecheckin.GateCheckOut_Status;
                            apponitmentlist.CheckOut_By = gatecheckin.Create_By;
                        }

                    }


                    //if (!string.IsNullOrEmpty(itemList.GateCheckIn_Index.ToString()))
                    //{
                    //    var gatecheckin = db.tb_GateCheckIn.FirstOrDefault(c => c.GateCheckIn_Index == itemList.GateCheckIn_Index);
                    //    apponitmentlist.CheckIn_Date = gatecheckin.Create_Date;
                    //    apponitmentlist.CheckIn_By = gatecheckin.Create_By;
                    //}
                    //else {
                    //    apponitmentlist.CheckIn_By = "-";
                    //    apponitmentlist.CheckIn_Status = "-";
                    //}
                    //if (!string.IsNullOrEmpty(itemList.GateCheckOut_Index.ToString()))
                    //{
                    //    var gatecheckin = db.tb_GateCheckOut.FirstOrDefault(c => c.GateCheckOut_Index == itemList.GateCheckOut_Index);
                    //    apponitmentlist.CheckOut_By = gatecheckin.Create_By;
                    //    apponitmentlist.CheckOut_Date = gatecheckin.Create_Date;
                    //}
                    //else {
                    //    apponitmentlist.CheckOut_By = "-";
                    //    apponitmentlist.CheckOut_Status = "-";

                    //}
                    result.Add(apponitmentlist);
                }

                List<AppointmentWithItemModel> resultOrderBy = new List<AppointmentWithItemModel>();
                int seq = 0;
                result.OrderBy(o => o.Appointment_Id).ToList().ForEach(e =>
                { e.Seq = ++seq; resultOrderBy.Add(e); });

                return resultOrderBy;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region + Listout +
        public List<AppointmentWithItemModel> ListGateAppointmentItemout(string jsonData)
        {
            try
            {
                AppointmentSearchModel model = Appointment.GetAppointmentSearchModel(jsonData);

                if (model.DatePeriod != null)
                {
                    var date = model.DatePeriod.Split('-');
                    var dateS = date[0];
                    var dateE = date[1];
                }

                List<Tb_Appointment> appointment = db.Tb_Appointment.Where(c => c.Document_status == 2).ToList();
                if (!string.IsNullOrEmpty(model.Appointment_Id))
                {
                    appointment = appointment.Where(c => c.Appointment_Id == model.Appointment_Id).ToList();
                }

                List<AppointmentWithItemModel> result = new List<AppointmentWithItemModel>();
                AppointmentWithItemModel apponitmentlist;

                var statusModels_InAndOut = new List<int?>();


                foreach (var itemList in appointment)
                {
                    apponitmentlist = new AppointmentWithItemModel();

                    var appointmentitem = db.Tb_AppointmentItem.FirstOrDefault(w => w.Appointment_Id == itemList.Appointment_Id && model.Appointment_Date != null ? (w.Appointment_Date >= model.Appointment_Date && w.Appointment_Date <= model.Appointment_Date_To) : w.Appointment_Id == itemList.Appointment_Id);
                    if (appointmentitem == null) { continue; }
                    var appointmentitemdetail = db.Tb_AppointmentItemDetail.FirstOrDefault(w => w.Appointment_Id == itemList.Appointment_Id && w.Vehicle_No == model.Vehicle_No);
                    if (appointmentitemdetail == null) { continue; }
                    var gate = dbMS.ms_GateDock.FirstOrDefault(c => c.Dock_Index == appointmentitem.Dock_Index);
                    if (gate != null)
                    {
                        apponitmentlist.Gate_Name = gate.Gate_Name;
                    }
                    string[] values = appointmentitem.Appointment_Time.Split('-');

                    TimeSpan time = TimeSpan.Parse(values[0]);

                    apponitmentlist.Appointment_Index = itemList.Appointment_Index;
                    apponitmentlist.Appointment_Id = itemList.Appointment_Id;
                    apponitmentlist.DocumentType_Name = itemList.DocumentType_Name;
                    apponitmentlist.Document_status = itemList.Document_status;
                    apponitmentlist.Appointment_Date = appointmentitem.Appointment_Date.AddSeconds(time.TotalSeconds);
                    apponitmentlist.Dock_Name = appointmentitem.Dock_Name;
                    apponitmentlist.license = appointmentitemdetail.Vehicle_No;
                    apponitmentlist.Ref_Document_No = appointmentitem.Ref_Document_No;
                    apponitmentlist.Appointment_Time = appointmentitem.Appointment_Time;
                    apponitmentlist.DocumentType_Name = appointmentitem.DocumentType_Name;
                    apponitmentlist.DocumentType_Index = appointmentitem.DocumentType_Index;
                    apponitmentlist.DocumentType_Id = appointmentitem.DocumentType_Id;
                    apponitmentlist.Owner_Id = appointmentitem.Owner_Id;
                    apponitmentlist.Owner_Index = appointmentitem.Owner_Index;
                    apponitmentlist.Owner_Name = appointmentitem.Owner_Name;
                    apponitmentlist.Status = itemList.Status_Id == 0 ? 0 : itemList.Status_Id.Value;
                    apponitmentlist.Status_Desc = "";
                    apponitmentlist.Create_By = appointmentitem.Create_By;
                    apponitmentlist.Update_By = appointmentitem.Update_By;
                    apponitmentlist.Cancel_By = itemList.Cancel_By;

                    if (!string.IsNullOrEmpty(itemList.GateCheckIn_Index.ToString()))
                    {
                        var gatecheckin = db.Tb_YardBalance.FirstOrDefault(c => c.GateCheckIn_Index == itemList.GateCheckIn_Index);
                        apponitmentlist.CheckIn_Date = gatecheckin.GateCheckIn_Date;
                        apponitmentlist.GateCheckIn_Status = gatecheckin.GateCheckIn_Status;
                        apponitmentlist.CheckIn_By = gatecheckin.Create_By;

                        apponitmentlist.CheckOut_Date = gatecheckin.GateCheckOut_Date;
                        apponitmentlist.GateCheckOut_Status = gatecheckin.GateCheckOut_Status;
                        apponitmentlist.CheckOut_By = gatecheckin.Create_By;
                    }


                    //if (!string.IsNullOrEmpty(itemList.GateCheckIn_Index.ToString()))
                    //{
                    //    var gatecheckin = db.tb_GateCheckIn.FirstOrDefault(c => c.GateCheckIn_Index == itemList.GateCheckIn_Index);
                    //    apponitmentlist.CheckIn_Date = gatecheckin.Create_Date;
                    //    apponitmentlist.CheckIn_By = gatecheckin.Create_By;
                    //}
                    //else {
                    //    apponitmentlist.CheckIn_By = "-";
                    //    apponitmentlist.CheckIn_Status = "-";
                    //}
                    //if (!string.IsNullOrEmpty(itemList.GateCheckOut_Index.ToString()))
                    //{
                    //    var gatecheckin = db.tb_GateCheckOut.FirstOrDefault(c => c.GateCheckOut_Index == itemList.GateCheckOut_Index);
                    //    apponitmentlist.CheckOut_By = gatecheckin.Create_By;
                    //    apponitmentlist.CheckOut_Date = gatecheckin.Create_Date;
                    //}
                    //else {
                    //    apponitmentlist.CheckOut_By = "-";
                    //    apponitmentlist.CheckOut_Status = "-";

                    //}
                    result.Add(apponitmentlist);
                }

                List<AppointmentWithItemModel> resultOrderBy = new List<AppointmentWithItemModel>();
                int seq = 0;
                result.OrderBy(o => o.Appointment_Id).ToList().ForEach(e =>
                { e.Seq = ++seq; resultOrderBy.Add(e); });

                return resultOrderBy;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #region + GateCheckIn +

        #region + Save +
        public string SaveGateCheckIn(string jsonData)
        {
            try
            {
                var resultmess = "";
                Guid gatecheckin;
                CheckInModel model = CheckIn.GetCheckInModel(jsonData);
                if (model.Appointment_Id == null) { throw new Exception("Invalid Parameter : AppointmentItem Id not found"); }

                List<Tb_AppointmentItem> appointmentItem = db.Tb_AppointmentItem.Where(c => c.Appointment_Id == model.Appointment_Id).ToList();
                if (appointmentItem is null) { throw new Exception("AppointmentItem not found"); }

                var appoint = db.Tb_YardBalance.FirstOrDefault(c => c.Appointment_Id == model.Appointment_Id);
                if (appoint != null)
                {
                    return " Appointment นี้ได้ทำการ checkin Gate เรียบร้อยแล้ว ";
                }
                foreach (var item in appointmentItem)
                {

                    if (item.Status_Id != 0) { throw new Exception("Invalid CheckIn : AppointmentItem already Processes"); }

                    Ms_WareHouseQouta wareHouseQouta = db.Ms_WareHouseQouta.FirstOrDefault(w => w.WareHouse_Index == item.WareHouse_Index);
                    if (wareHouseQouta is null) { throw new Exception("WareHouseQouta not found"); }

                    Ms_DockQoutaInterval dockQoutaInterval = db.Ms_DockQoutaInterval.Find(item.DockQoutaInterval_Index);
                    if (dockQoutaInterval is null) { throw new Exception("DockQoutaInterval not found"); }

                    DateTime checkInDate = (model.CheckIn_Date ?? DateTime.Now).TrimTime();
                    TimeSpan checkInTime = model.CheckIn_Time ?? TimeSpan.Parse(DateTime.Now.ToShortTimeString());

                    checkInDate = checkInDate.AddSeconds(checkInTime.TotalSeconds);

                    TimeSpan intervalStart = TimeSpan.Parse(dockQoutaInterval.Interval_Start);
                    TimeSpan intervalEnd = TimeSpan.Parse(dockQoutaInterval.Interval_End);

                    TimeSpan timeStart = new TimeSpan(item.Appointment_Date.Ticks).Add(intervalStart);
                    TimeSpan timeEnd = new TimeSpan(item.Appointment_Date.Ticks).Add(intervalEnd);
                    TimeSpan timeCheck = new TimeSpan(checkInDate.Ticks).Add(checkInTime);

                    var time = item.Appointment_Time.Split('-');
                    var starttime = time[0].Split(':');
                    var endtime = time[1].Split(':');
                    DateTime dateS = item.Appointment_Date.AddHours(double.Parse(starttime[0])).AddMinutes(double.Parse(starttime[1]));
                    DateTime dateE = item.Appointment_Date.AddHours(double.Parse(endtime[0])).AddMinutes(double.Parse(endtime[1]));

                    string start = ConfigurationManager.AppSettings["Config:TIMESTART"];
                    string end = ConfigurationManager.AppSettings["Config:TIMEEND"];
                    string checkInStatus = "";
                    if (dockQoutaInterval.checkIn_Limit_Before != null && dockQoutaInterval.checkIn_Limit_After != null)
                    {
                        if (dockQoutaInterval.checkIn_Limit_Before != null)
                        {
                            if (dateS.AddMinutes(-int.Parse(string.IsNullOrEmpty(dockQoutaInterval.checkIn_Limit_Before) != null ? dockQoutaInterval.checkIn_Limit_Before : "0")) > checkInDate)
                            {
                                resultmess = "ไม่สามารถเข้าก่อนเวลา ที่นัดไว้ได้ " + dockQoutaInterval.checkIn_Limit_Before + " นาที";
                                return resultmess;
                            }
                        }
                        if (dockQoutaInterval.checkIn_Limit_After != null)
                        {
                            if (dateS.AddMinutes(int.Parse(string.IsNullOrEmpty(dockQoutaInterval.checkIn_Limit_After) != null ? dockQoutaInterval.checkIn_Limit_After : "0")) < checkInDate)
                            {
                                resultmess = "คุณมาสายเกินเวลาที่นัด " + dockQoutaInterval.checkIn_Limit_After + " นาที ไม่สามารถเข้าได้";
                                return resultmess;
                            }
                        }
                        checkInStatus = "On Time";
                    }


                    item.Status_Id = 0;

                    Guid GatecheckInIndex = Guid.NewGuid();
                    Guid yardBalanceIndex = Guid.NewGuid();
                    Guid yardMovementIndex = Guid.NewGuid();
                    string userBy = model.Create_By;
                    DateTime userDate = DateTime.Now;
                    gatecheckin = GatecheckInIndex;
                    int activityID = 1;

                    tb_GateCheckIn newGateCheckIn = new tb_GateCheckIn
                    {
                        GateCheckIn_Index = GatecheckInIndex,
                        Appointment_Index = item.Appointment_Index,
                        Appointment_Id = item.Appointment_Id,
                        AppointmentItem_Index = item.AppointmentItem_Index,
                        AppointmentItem_Id = item.AppointmentItem_Id,
                        Appointment_Date = item.Appointment_Date,
                        Appointment_Time = item.Appointment_Time,

                        GateCheckIn_Date = checkInDate,
                        Remark = model.Remark,

                        IsActive = 1,
                        IsDelete = 0,
                        IsSystem = 0,
                        Status_Id = 0,
                        Create_By = userBy,
                        Create_Date = userDate
                    };
                    db.tb_GateCheckIn.Add(newGateCheckIn);

                    Tb_YardBalance newYardBalance = new Tb_YardBalance
                    {
                        YardBalance_Index = yardBalanceIndex,

                        Appointment_Index = item.Appointment_Index,
                        Appointment_Id = item.Appointment_Id,
                        AppointmentItem_Index = item.AppointmentItem_Index,
                        AppointmentItem_Id = item.AppointmentItem_Id,
                        DocumentType_Index = item.DocumentType_Index,
                        DocumentType_Id = item.DocumentType_Id,
                        DocumentType_Name = item.DocumentType_Name,
                        Appointment_Date = item.Appointment_Date,
                        Appointment_Time = item.Appointment_Time,

                        DockQoutaInterval_Index = item.DockQoutaInterval_Index,
                        WareHouse_Index = item.WareHouse_Index,
                        WareHouse_Id = item.WareHouse_Id,
                        WareHouse_Name = item.WareHouse_Name,
                        Dock_Index = item.Dock_Index,
                        Dock_Id = item.Dock_Id,
                        Dock_Name = item.Dock_Name,
                        Owner_Index = item.Owner_Index,
                        Owner_Id = item.Owner_Id,
                        Owner_Name = item.Owner_Name,
                        Ref_Document_No = item.Ref_Document_No,
                        Ref_Document_Date = item.Ref_Document_Date,
                        ContactPerson_Name = item.ContactPerson_Name,
                        ContactPerson_EMail = item.ContactPerson_EMail,
                        ContactPerson_Tel = item.ContactPerson_Tel,
                        Remark = item.Remark,

                        GateCheckIn_Index = newGateCheckIn.GateCheckIn_Index,
                        GateCheckIn_Date = newGateCheckIn.GateCheckIn_Date,
                        GateCheckIn_Status = checkInStatus,
                        GateCheckIN_lat = model.GateCheckIN_lat,
                        GateCheckIN_long = model.GateCheckIN_long,
                        User_id = model.User_id,

                        IsActive = 1,
                        IsDelete = 0,
                        IsSystem = 0,
                        Status_Id = 0,
                        Create_By = userBy,
                        Create_Date = userDate
                    };
                    db.Tb_YardBalance.Add(newYardBalance);

                    Tb_YardMovement newYardMovement = new Tb_YardMovement
                    {
                        YardMovement_Index = yardMovementIndex,
                        YardBalance_Index = yardBalanceIndex,

                        Appointment_Index = item.Appointment_Index,
                        Appointment_Id = item.Appointment_Id,
                        AppointmentItem_Index = item.AppointmentItem_Index,
                        AppointmentItem_Id = item.AppointmentItem_Id,
                        Appointment_Date = item.Appointment_Date,
                        Appointment_Time = item.Appointment_Time,

                        Activity_Id = activityID,

                        GateCheckIn_Index = newGateCheckIn.GateCheckIn_Index,
                        GateCheckOut_Date = newGateCheckIn.GateCheckIn_Date,
                        GateCheckIn_Status = checkInStatus,
                        CheckIn_By = userBy,

                        IsActive = 1,
                        IsDelete = 0,
                        IsSystem = 0,
                        Status_Id = 0,
                        Create_By = userBy,
                        Create_Date = userDate
                    };
                    db.Tb_YardMovement.Add(newYardMovement);
                }
                var Q_appointment = GetDocumentNumber(Guid.Parse("6A6D5A1C-3998-4C2B-B44A-330A78BA6335"), DateTime.Now);
                Tb_Appointment appointmentstatus = db.Tb_Appointment.FirstOrDefault(w => w.Appointment_Id == model.Appointment_Id);
                var update = db.Tb_Appointment.Find(appointmentstatus.Appointment_Index);
                update.Document_status = 1;
                update.GateCheckIn_Index = gatecheckin;
                update.Runing_Q = Q_appointment;

                var myTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    db.SaveChanges();
                    myTransaction.Commit();
                    resultmess = "คุณได้คิวที่ : " + Q_appointment;
                }

                catch (Exception saveEx)
                {
                    myTransaction.Rollback();
                    throw saveEx;
                }

                return resultmess;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #region + GateCheckOut +

        #region + Save +
        public string saveGateOut(string jsonData)
        {
            try
            {
                CheckOutModel model = CheckOut.GetCheckOutModel(jsonData);
                if (model.Appointment_Id == null) { throw new Exception("Invalid Parameter : Appointment Id not found"); }

                var Gateout = db.Tb_Appointment.FirstOrDefault(C => C.Appointment_Id == model.Appointment_Id);
                if (Gateout.GateCheckOut_Index != null)
                {
                    return "ไม่สามารถ Check out ได้เนื่องจากรถได้ทำการ check out ไปแล้ว";
                }

                List<Tb_YardBalance> appointmentItem = db.Tb_YardBalance.Where(w => w.Appointment_Id == model.Appointment_Id).ToList();
                if (appointmentItem is null) { throw new Exception("AppointmentItem not found"); }
                foreach (var item in appointmentItem)
                {

                    if (item.Status_Id != 1) { throw new Exception("Invalid CheckIn : AppointmentItem already Processes"); }

                    string checkOutStatus = "Checked Out";
                    DateTime checkOutDate = (model.CheckOut_Date ?? DateTime.Now).TrimTime();
                    TimeSpan checkOutTime = model.CheckOut_Time ?? TimeSpan.Parse(DateTime.Now.ToShortTimeString());
                    checkOutDate = checkOutDate.AddSeconds(checkOutTime.TotalSeconds);
                    Guid checkOutIndex = Guid.NewGuid();
                    Guid yardMovementIndex = Guid.NewGuid();
                    string userBy = model.Create_By;
                    DateTime userDate = DateTime.Now;
                    int activityID = 2;

                    tb_GateCheckOut newCheckOut = new tb_GateCheckOut
                    {
                        GateCheckOut_Index = checkOutIndex,
                        Appointment_Index = item.Appointment_Index,
                        Appointment_Id = item.Appointment_Id,
                        AppointmentItem_Index = item.AppointmentItem_Index,
                        AppointmentItem_Id = item.AppointmentItem_Id,
                        Appointment_Date = item.Appointment_Date.Value,
                        Appointment_Time = item.Appointment_Time,

                        GateCheckOut_Date = checkOutDate,
                        Remark = model.Remark,

                        IsActive = 1,
                        IsDelete = 0,
                        IsSystem = 0,
                        Status_Id = 0,
                        Create_By = userBy,
                        Create_Date = userDate
                    };
                    db.tb_GateCheckOut.Add(newCheckOut);

                    item.GateCheckOut_Index = newCheckOut.GateCheckOut_Index;
                    item.GateCheckOut_Date = newCheckOut.GateCheckOut_Date;
                    item.GateCheckOut_Status = checkOutStatus;
                    item.GateCheckOut_lat = model.GateCheckOut_lat;
                    item.GateCheckOut_long = model.GateCheckOut_long;
                    item.Update_By = userBy;
                    item.Status_Id = 2;

                    Tb_YardMovement newYardMovement = new Tb_YardMovement
                    {
                        YardMovement_Index = yardMovementIndex,
                        YardBalance_Index = item.YardBalance_Index,

                        Appointment_Index = item.Appointment_Index,
                        Appointment_Id = item.Appointment_Id,
                        AppointmentItem_Index = item.AppointmentItem_Index,
                        AppointmentItem_Id = item.AppointmentItem_Id,
                        Appointment_Date = item.Appointment_Date.Value,
                        Appointment_Time = item.Appointment_Time,

                        Activity_Id = activityID,

                        GateCheckOut_Index = newCheckOut.GateCheckOut_Index,
                        GateCheckOut_Date = newCheckOut.GateCheckOut_Date,
                        GateCheckOut_Status = checkOutStatus,
                        CheckOut_By = userBy,

                        IsActive = 1,
                        IsDelete = 0,
                        IsSystem = 0,
                        Status_Id = 0,
                        Create_By = userBy,
                        Create_Date = userDate
                    };
                    db.Tb_YardMovement.Add(newYardMovement);

                    Tb_Appointment appointmentstatus = db.Tb_Appointment.FirstOrDefault(w => w.Appointment_Id == model.Appointment_Id);
                    var update = db.Tb_Appointment.Find(appointmentstatus.Appointment_Index);
                    update.Document_status = 1;
                    update.GateCheckOut_Index = newCheckOut.GateCheckOut_Index;

                    var myTransaction = db.Database.BeginTransaction(IsolationLevel.Serializable);
                    try
                    {
                        db.SaveChanges();
                        myTransaction.Commit();

                        //foreach (var itemmodel in appointmentItem)
                        //{
                        //    if (itemmodel.DocumentType_Index == Guid.Parse("C392D865-8E69-4985-B72F-2421EBE8BCDB"))
                        //    {
                        //        var truckLoads = dbGI.im_TruckLoad.FirstOrDefault(c => c.TruckLoad_No == itemmodel.Ref_Document_No && c.Document_Status != -1);
                        //        if (truckLoads != null)
                        //        {
                        //            List<DataAccess.Models.GI.Table.im_TruckLoadItem> truckLoadItems = dbGI.im_TruckLoadItem.Where(c => c.TruckLoad_Index == truckLoads.TruckLoad_Index).ToList();

                        //            foreach (var itemList in truckLoadItems)
                        //            {
                        //                var des = "นำจ่าย";
                        //                try
                        //                {

                        //                    var resmodel = new
                        //                    {
                        //                        referenceNo = itemList.PlanGoodsIssue_No,
                        //                        status = 104,
                        //                        statusAfter = 104,
                        //                        statusBefore = 103,
                        //                        statusDesc = des,
                        //                        statusDateTime = DateTime.Now
                        //                    };
                        //                    SaveLogRequest(itemList.PlanGoodsIssue_No, JsonConvert.SerializeObject(resmodel), des, 1, des, Guid.NewGuid());
                        //                    var result_api = Utils.SendDataApi<DemoCallbackResponseViewModel>(new AppSettingConfig().GetUrl("TMS_status"), JsonConvert.SerializeObject(resmodel));
                        //                    SaveLogResponse(itemList.PlanGoodsIssue_No, JsonConvert.SerializeObject(result_api), resmodel.statusDesc, 2, resmodel.statusDesc, Guid.NewGuid());
                        //                }
                        //                catch (Exception ex)
                        //                {
                        //                    SaveLogResponse(itemList.PlanGoodsIssue_No, JsonConvert.SerializeObject(ex.Message), des, -1, des, Guid.NewGuid());
                        //                }
                        //            }
                        //        }
                        //    }
                        //}

                    }
                    catch (Exception saveEx)
                    {
                        myTransaction.Rollback();
                        throw saveEx;
                    }
                }
                return "สำเร็จ";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #region Q

        #region list_queue
        public List<AppointmentWithItemModel> list_queue(string jsonData)
        {
            try
            {
                AppointmentSearchModel model = Appointment.GetAppointmentSearchModel(jsonData);

                var appointmentItem = db.Tb_AppointmentItem.Where(
                    w => w.IsActive == 1 &&
                         w.IsDelete == 0 &&
                         w.Dock_Index.IsEquals(model.Dock_Index) &&
                         w.Appointment_Time.Like(model.time)
                ).ToList();
                if (model.Appointment_Date != null && model.Appointment_Date_To != null)
                {
                    appointmentItem = appointmentItem.Where(w => w.Appointment_Date.DateBetweenCondition(model.Appointment_Date, model.Appointment_Date_To, true)).ToList();
                }
                else
                {
                    appointmentItem = appointmentItem.Where(w => w.Appointment_Date.DateBetweenCondition(DateTime.Now.TrimTime(), DateTime.Now.AddDays(1).TrimTime(), true)).ToList();
                }
                if (!string.IsNullOrEmpty(model.Appointment_Id))
                {
                    appointmentItem = appointmentItem.Where(c => c.Appointment_Id == model.Appointment_Id).ToList();
                }

                appointmentItem = appointmentItem.OrderBy(c => c.Appointment_Id).ToList();
                var result = new List<AppointmentWithItemModel>();
                var id = "";
                foreach (var app in appointmentItem)
                {
                    if (id == app.Appointment_Id)
                    {
                        continue;
                    }
                    else
                    {
                        id = app.Appointment_Id;
                    }
                    //var dock_status = dbMS.Ms_Dock.FirstOrDefault(c => c.Dock_Index == app.Dock_Index);
                    //if (dock_status.Dock_use ==1)
                    //{
                    //    continue;
                    //}
                    var gatecheck = db.Tb_Appointment.FirstOrDefault(c => c.Appointment_Index == app.Appointment_Index && c.GateCheckIn_Index != null && c.Car_Inspection == 1);
                    if (gatecheck == null) { continue; }
                    var detail = db.Tb_AppointmentItemDetail.FirstOrDefault(c => c.Appointment_Index == app.Appointment_Index);
                    var item = new AppointmentWithItemModel
                    {
                        Appointment_Index = app.Appointment_Index,
                        DockQoutaInterval_Index = app.DockQoutaInterval_Index,
                        Dock_Index = app.Dock_Index,
                        Dock_Name = app.Dock_Name,
                        Appointment_Time = app.Appointment_Time,
                        Appointment_Id = app.Appointment_Id,
                        Appointment_Date = app.Appointment_Date,
                        DocumentType_Index = gatecheck.DocumentType_Index,
                        runing_Q = gatecheck.Runing_Q,
                        Remark = gatecheck.In_queue == null ? "Waiting" : "Calling",
                        Vehicle_No = detail.Vehicle_No

                    };

                    result.Add(item);
                }


                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region + Confirm_Approve_Q +
        public string Approve_Q(string jsonData)
        {
            Logtxt LoggingService = new Logtxt();
            try
            {
                LoggingService.DataLogLines("Approve_Q", "Approve_Q" + DateTime.Now.ToString("yyyy-MM-dd"), "Start Approve_Q" + DateTime.Now.ToString("yyyy-MM-dd-HHmm"));
                ConfirmQ_Model model = JsonConvert.DeserializeObject<ConfirmQ_Model>(jsonData); ;
                string userBy = model.Create_By;
                DateTime userDate = DateTime.Now;
                var dockfill = dbMS.Ms_Dock.FirstOrDefault(c => c.Dock_Index == model.Dock_Index);
                if (dockfill == null)
                {
                    return "E";
                }
                if (dockfill.Dock_use == 1)
                {
                    return "Dock Already Use";
                }
                Tb_Appointment appointment = db.Tb_Appointment.Find(model.Appointment_Index);
                if (appointment.In_queue == 1)
                {
                    return "This Appointment Already in Q";
                }
                List<Tb_AppointmentItem> appitemF = db.Tb_AppointmentItem.Where(c => c.Appointment_Index == appointment.Appointment_Index).ToList();
                List<Tb_AppointmentItemDetail> apid = db.Tb_AppointmentItemDetail.Where(c => c.Appointment_Index == appointment.Appointment_Index).ToList();
                List<Tb_YardBalance> ybl = db.Tb_YardBalance.Where(c => c.Appointment_Index == appointment.Appointment_Index).ToList();
                LoggingService.DataLogLines("Approve_Q", "Approve_Q" + DateTime.Now.ToString("yyyy-MM-dd"), "check Dock" + appitemF[0].Dock_Index + " TO " + model.Dock_Index);
                if (appitemF[0].Dock_Index != model.Dock_Index)
                {
                    var checkstatus = dbGI.View_CheckReport_Status.Where(c => c.TruckLoad_No == appitemF[0].Ref_Document_No && c.Document_StatusDocktoStg == null).ToList();
                    LoggingService.DataLogLines("Approve_Q", "Approve_Q" + DateTime.Now.ToString("yyyy-MM-dd"), "checkstatus : " + checkstatus.Count());
                    if (checkstatus.Count() > 0)
                    {
                        return "Shipment นี้อยู่ระหว่างการ Wave กรุณารอทำรายการหลัง Wave เสร็จ";
                    }
                }

                Ms_DockQoutaInterval dockQoutaInterval = db.Ms_DockQoutaInterval.FirstOrDefault(c => c.DockQoutaInterval_Index == model.dockQoutaInterval_Index);
                if (dockQoutaInterval == null)
                {
                    return "Can't Confirm Dock";
                }
                foreach (var item in apid)
                {
                    var Queue_index = new Guid();
                    tb_QueueAppointment Queue_new = new tb_QueueAppointment
                    {
                        Queue_Index = Queue_index,
                        Appointment_index = appointment.Appointment_Index,
                        Appointment_Id = appointment.Appointment_Id,
                        Vehicle_no = item.Vehicle_No,
                        Queue_no = appointment.Runing_Q,
                        Queue_date = DateTime.Now,
                        Dock_Index = dockfill.Dock_Index,
                        Dock_Id = dockfill.Dock_Id,
                        Dock_Name = dockfill.Dock_Name,
                        Create_date = DateTime.Now,
                        Create_by = userBy
                    };
                    db.tb_QueueAppointment.Add(Queue_new);

                }

                foreach (var item in appitemF)
                {
                    item.Dock_Index = dockQoutaInterval.Dock_Index;
                    item.Dock_Id = dockQoutaInterval.Dock_Id;
                    item.Dock_Name = dockQoutaInterval.Dock_Name;
                    item.DockQoutaInterval_Index = dockQoutaInterval.DockQoutaInterval_Index;

                }

                foreach (var item in ybl)
                {
                    item.Dock_Index = dockQoutaInterval.Dock_Index;
                    item.Dock_Id = dockQoutaInterval.Dock_Id;
                    item.Dock_Name = dockQoutaInterval.Dock_Name;
                    item.DockQoutaInterval_Index = dockQoutaInterval.DockQoutaInterval_Index;
                }

                appointment.Document_status = 5;
                appointment.In_queue = 1;
                appointment.Update_By = userBy;
                appointment.Update_Date = userDate;

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
                var get_user = db.Tb_YardBalance.FirstOrDefault(c => c.Appointment_Index == model.Appointment_Index);
                if (get_user != null)
                {
                    if (get_user.User_id != null)
                    {
                        var call_Q = callQueue(get_user.User_id);
                        //var result_api = Utils.GetDataApi<string>(new AppSettingConfig().GetUrl("callQueue"), get_user.User_id);
                        //if (result_api == "")
                        //{
                        //    LoggingService.DataLogLines("Autoget_Q", "Autoget_Q" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "Check User " + get_user.User_id + " -------------------------S " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                        //}
                        //else
                        //{
                        //    LoggingService.DataLogLines("Autoget_Q", "Autoget_Q" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "Check User " + get_user.User_id + " -------------------------S " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                        //}
                    }
                }
                return "S";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Report_queue
        public string printOutQ(string jsonData, string rootPath = "")
        {
            try
            {
                AppointmentSearchModel model = Appointment.GetAppointmentSearchModel(jsonData);

                var appointmentItem = db.Tb_AppointmentItem.Where(
                    w => w.IsActive == 1 &&
                         w.IsDelete == 0 &&
                         w.Appointment_Index == model.Appointment_Index
                ).ToList();

                var result = new List<ReportQYardViewModel>();

                foreach (var app in appointmentItem)
                {
                    var gatecheck = db.Tb_Appointment.FirstOrDefault(c => c.Appointment_Index == app.Appointment_Index);
                    if (gatecheck == null) { continue; }
                    var detail = db.Tb_AppointmentItemDetail.FirstOrDefault(c => c.Appointment_Index == app.Appointment_Index);
                    var item = new ReportQYardViewModel
                    {
                        Appointment_Index = app.Appointment_Index,
                        Dock_Name = app.Dock_Name,
                        Appointment_Time = app.Appointment_Time,
                        Appointment_Id = app.Appointment_Id,
                        Appointment_Date = app.Appointment_Date,
                        runing_Q = gatecheck.Runing_Q,
                        Remark = gatecheck.In_queue == null ? "Waiting" : "Calling",
                        Vehicle_No = detail.Vehicle_No

                    };

                    result.Add(item);
                }

                result.ToList();


                //rootPath = rootPath.Replace("\\YardAPI", "");
                //var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportPrintQ");
                //LocalReport report = new LocalReport(reportPath);
                //report.AddDataSource("DataSet1", result);


                //string fileName = "";
                //string fullPath = "";
                //fileName = "tmpReport" + DateTime.Now.ToString("yyyyMMddHHmmss");

                //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);


                //var renderedBytes = report.Execute(RenderType.Pdf);

                //Libs.Utils objReport = new Libs.Utils();
                //fullPath = objReport.SaveReport(renderedBytes.MainStream, fileName + ".pdf", rootPath);
                //var saveLocation = objReport.PhysicalPath(fileName + ".pdf", rootPath);
                //return saveLocation;


                rootPath = rootPath.Replace("\\YardAPI", "");
                //var reportPath = rootPath + "\\GRBusiness\\Reports\\ReportTagPutaway\\ReportTagPutaway.rdlc";
                //var reportPath = rootPath + "\\Reports\\ReportTagPutaway\\ReportTagPutaway.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportPrintQ");
                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "tmpReport" + DateTime.Now.ToString("yyyyMMddHHmmss");

                var renderedBytes = report.Execute(RenderType.Pdf);

                Libs.Utils objReport = new Libs.Utils();
                fullPath = objReport.saveReport(renderedBytes.MainStream, fileName + ".pdf", rootPath);
                var saveLocation = objReport.PhysicalPath(fileName + ".pdf", rootPath);
                return saveLocation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Autoget_Q
        public bool Autoget_Q()
        {
            Logtxt LoggingService = new Logtxt();
            try
            {

                LoggingService.DataLogLines("Autoget_Q", "Autoget_Q" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "Autoget_Q : " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                List<Guid> listappointment_In_Q = db.Tb_Appointment.Where(c => c.In_queue == null && c.GateCheckIn_Index != null && c.Car_Inspection == 1).Select(c => c.Appointment_Index).ToList();
                LoggingService.DataLogLines("Autoget_Q", "Autoget_Q" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "listappointment_In_Q : " + listappointment_In_Q.Count + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                var Appointmentitem = db.Tb_AppointmentItem.Where(c => listappointment_In_Q.Contains(c.Appointment_Index)).ToList();
                foreach (var item in Appointmentitem)
                {
                    LoggingService.DataLogLines("Autoget_Q", "Autoget_Q" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "Appointment : " + item.Appointment_Id + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                    LoggingService.DataLogLines("Autoget_Q", "Autoget_Q" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "Dock : " + item.Dock_Name + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                    LoggingService.DataLogLines("Autoget_Q", "Autoget_Q" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "Time : " + item.Appointment_Time + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                    var appointment = db.Tb_Appointment.FirstOrDefault(c => c.Appointment_Index == item.Appointment_Index);
                    var dock_use = dbMS.Ms_Dock.FirstOrDefault(c => c.Dock_Index == item.Dock_Index);
                    if (dock_use.Dock_use == 0)
                    {

                        string[] make_time = item.Appointment_Time.Split('-');
                        var time = make_time[0].Trim().Split(':');
                        var time_h = item.Appointment_Date.AddHours(double.Parse(time[0])).AddMinutes(double.Parse(time[1])).AddMinutes(-30);
                        var time_e = item.Appointment_Date.AddHours(double.Parse(time[0])).AddMinutes(double.Parse(time[1])).AddMinutes(15);
                        LoggingService.DataLogLines("Autoget_Q", "Autoget_Q" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "Time : " + time_h + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                        LoggingService.DataLogLines("Autoget_Q", "Autoget_Q" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "Time : " + time_e + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                        if (time_h > DateTime.Now)
                        {
                            LoggingService.DataLogLines("Autoget_Q", "Autoget_Q" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "Time Validate " + time_h + " > " + DateTime.Now + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                            LoggingService.DataLogLines("Autoget_Q", "Autoget_Q" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "------------------------------------------------------------------------------" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                            continue;
                        }
                        else if (time_e < DateTime.Now)
                        {
                            LoggingService.DataLogLines("Autoget_Q", "Autoget_Q" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "Time Validate " + time_e + " < " + DateTime.Now + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                            LoggingService.DataLogLines("Autoget_Q", "Autoget_Q" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "------------------------------------------------------------------------------" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                            continue;
                        }
                        else
                        {
                            List<Tb_AppointmentItemDetail> apid = db.Tb_AppointmentItemDetail.Where(c => c.Appointment_Index == item.Appointment_Index).ToList();

                            foreach (var itemdetail in apid)
                            {
                                var Queue_index = new Guid();
                                tb_QueueAppointment Queue_new = new tb_QueueAppointment
                                {
                                    Queue_Index = Queue_index,
                                    Appointment_index = itemdetail.Appointment_Index,
                                    Appointment_Id = itemdetail.Appointment_Id,
                                    Vehicle_no = itemdetail.Vehicle_No,
                                    Queue_no = appointment.Runing_Q,
                                    Queue_date = DateTime.Now,
                                    Dock_Index = item.Dock_Index,
                                    Dock_Id = item.Dock_Id,
                                    Dock_Name = item.Dock_Name,
                                    Create_date = DateTime.Now,
                                    Create_by = "Auto System"
                                };
                                db.tb_QueueAppointment.Add(Queue_new);

                            }
                        }
                        appointment.Document_status = 5;
                        appointment.In_queue = 1;
                        appointment.Update_By = "Auto System";
                        appointment.Update_Date = DateTime.Now;
                        LoggingService.DataLogLines("Autoget_Q", "Autoget_Q" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "Check User" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                        var get_user = db.Tb_YardBalance.FirstOrDefault(c => c.Appointment_Index == item.Appointment_Index);
                        if (get_user != null)
                        {
                            LoggingService.DataLogLines("Autoget_Q", "Autoget_Q" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "Check User not found" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                            if (get_user.User_id != null)
                            {
                                LoggingService.DataLogLines("Autoget_Q", "Autoget_Q" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "Check User " + get_user.User_id + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                                var call_Q = callQueue(get_user.User_id);
                                //var result_api = Utils.GetDataApi<string>(new AppSettingConfig().GetUrl("callQueue"), get_user.User_id);
                                //if (result_api == "")
                                //{
                                //    LoggingService.DataLogLines("Autoget_Q", "Autoget_Q" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "Check User " + get_user.User_id + " -------------------------S " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                                //}
                                //else {
                                //    LoggingService.DataLogLines("Autoget_Q", "Autoget_Q" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "Check User " + get_user.User_id + " -------------------------S " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                                //}
                            }
                            else
                            {
                                LoggingService.DataLogLines("Autoget_Q", "Autoget_Q" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "Check User not found" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                            }
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
                    }
                    else
                    {
                        LoggingService.DataLogLines("Autoget_Q", "Autoget_Q" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "Time : " + item.Dock_Name + "Is Use " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                        LoggingService.DataLogLines("Autoget_Q", "Autoget_Q" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "------------------------------------------------------------------------------" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                        continue;
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                LoggingService.DataLogLines("Autoget_Q", "Autoget_Q" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "EX : " + ex + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                throw ex;
            }
        }
        #endregion

        #region calling_Q_withmoblie
        public string callQueue(string userId)
        {
            Logtxt LoggingService = new Logtxt();
            HttpWebResponse response = null;
            try
            {
                LoggingService.DataLogLines("Call_moblie", "moblie" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "User : " + userId + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create($"{urlFireBase}driver/{userId}/queue/.json?auth={authFireBaseRealtimebase}");

                var json = new
                {
                    call_date = DateTime.Now
                };

                httpWebRequest.Method = "PATCH";
                byte[] byteArray = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(json));
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.ContentLength = byteArray.Length;

                Stream dataStream = httpWebRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                try
                {
                    response = (HttpWebResponse)httpWebRequest.GetResponse();
                }
                catch (WebException ex)
                {
                    response = (HttpWebResponse)ex.Response;
                }
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    LoggingService.DataLogLines("Call_moblie", "moblie" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "User : Sucess " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                    return "";
                }
                else
                {
                    LoggingService.DataLogLines("Call_moblie", "moblie" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), $"error http status {response.StatusCode} {response.StatusDescription}" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                    return $"error http status {response.StatusCode} {response.StatusDescription}";
                }

            }
            catch (Exception ex)
            {
                LoggingService.DataLogLines("Call_moblie", "moblie" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "Error User : " + userId + " -----------------" + ex.Message + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                throw new Exception($"(can't connect to realtime base) ExceptionMessage : {ex.Message}");
            }
        }
        #endregion

        #endregion

        #region all status

        #region Before_GateCheckIn
        public List<CheckQueueResponseViewModel> Before_GateCheckIn(string jsonData)
        {
            try
            {
                Logtxt LoggingService = new Logtxt();
                LoggingService.DataLogLines("Before_GateCheckIn", "Before_GateCheckIn" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "Before_GateCheckIn : " + jsonData + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                AppointmentSearchModel model = Appointment.GetAppointmentSearchModel(jsonData);
                List<CheckQueueResponseViewModel> result = new List<CheckQueueResponseViewModel>();

                var before_gatecheckin = (from am in db.Tb_Appointment.AsQueryable()
                                          join ami in db.Tb_AppointmentItem.Where(c => c.IsActive == 1 && c.IsDelete == 0) on am.Appointment_Index equals ami.Appointment_Index
                                          join amid in db.Tb_AppointmentItemDetail.AsQueryable() on ami.AppointmentItem_Index equals amid.AppointmentItem_Index
                                          join t_yb in db.Tb_YardBalance.AsQueryable() on ami.AppointmentItem_Index equals t_yb.AppointmentItem_Index into yb
                                          from yb_d in yb.DefaultIfEmpty()
                                          select new
                                          {
                                              am.Appointment_Index,
                                              am.Appointment_Id,
                                              am.Document_status,
                                              am.GateCheckIn_Index,
                                              am.DocumentType_Id,
                                              am.DocumentType_Name,
                                              am.Runing_Q,
                                              am.Create_Date,
                                              ami.Appointment_Date,
                                              ami.Dock_Id,
                                              ami.Dock_Name,
                                              ami.Ref_Document_No,
                                              ami.Appointment_Time,
                                              amid.VehicleType_Id,
                                              amid.VehicleType_Name,
                                              amid.Vehicle_No,
                                              yb_d.GateCheckIn_Date,
                                              yb_d.GateCheckOut_Date
                                          }).Where(c => c.Document_status == 5 && c.GateCheckIn_Index == null);
                var data = before_gatecheckin.Where(c => c.Vehicle_No == model.Vehicle_No && (c.Appointment_Date >= model.Appointment_Date && c.Appointment_Date <= model.Appointment_Date_To)).OrderByDescending(c => c.Create_Date).ThenByDescending(c => c.Appointment_Id).Take(1).ToList();

                foreach (var item in data)
                {
                    result.Add(new CheckQueueResponseViewModel
                    {
                        Vehicle_Id = null,
                        Vehicle_LicenseNo = item.Vehicle_No,
                        Appointment_Id = item.Appointment_Id,
                        Appointment_Date = item.Appointment_Date.ToString("yyyy-MM-dd"),
                        Appointment_Time = item.Appointment_Time,
                        Dock_ID = item.Dock_Id,
                        Dock_Name = item.Dock_Name,
                        VehicleType_Id = item.VehicleType_Id,
                        VehicleType_Name = item.VehicleType_Name,
                        Ref_Document_No = item.Ref_Document_No,
                        DocumentType_Id = item.DocumentType_Id,
                        DocumentType_Name = item.DocumentType_Name,
                        GateCheckIn_Date = item.GateCheckIn_Date == null ? null : item.GateCheckIn_Date.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                        GateCheckOut_Date = item.GateCheckOut_Date == null ? null : item.GateCheckOut_Date.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                        Queue_No = item.Runing_Q,
                        Before_Queue = "0003",
                        Status = "CHECKIN",
                        GateCheckIN_lat = Convert.ToDecimal(latti),
                        GateCheckIN_long = Convert.ToDecimal(longti),
                        GateCheckOUT_lat = Convert.ToDecimal(latti),
                        GateCheckOUT_long = Convert.ToDecimal(longti),
                        GateCheckIN_Radius = 1000,
                        GateCheckOUT_Radius = 1000,
                        GateCheckIn_NO = null,
                        GateCheckOut_NO = null,
                    });
                }

                LoggingService.DataLogLines("Before_GateCheckIn", "Before_GateCheckIn" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "Before_GateCheckIn : " + JsonConvert.SerializeObject(result) + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region After_GateCheckIn
        public List<CheckQueueResponseViewModel> After_GateCheckIn(string jsonData)
        {
            Logtxt LoggingService = new Logtxt();
            try
            {
                LoggingService.DataLogLines("After_GateCheckIn", "After_GateCheckIn" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "After_GateCheckIn : " + jsonData + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                AppointmentSearchModel model = Appointment.GetAppointmentSearchModel(jsonData);
                List<CheckQueueResponseViewModel> result = new List<CheckQueueResponseViewModel>();

                var before_gatecheckin = (from am in db.Tb_Appointment.AsQueryable()
                                          join ami in db.Tb_AppointmentItem.Where(c => c.IsActive == 1 && c.IsDelete == 0) on am.Appointment_Index equals ami.Appointment_Index
                                          join amid in db.Tb_AppointmentItemDetail.AsQueryable() on ami.Appointment_Index equals amid.Appointment_Index
                                          join t_yb in db.Tb_YardBalance.AsQueryable() on ami.Appointment_Index equals t_yb.Appointment_Index into yb
                                          from yb_d in yb.DefaultIfEmpty()
                                          select new
                                          {
                                              am.Appointment_Index,
                                              am.Appointment_Id,
                                              am.Document_status,
                                              am.GateCheckIn_Index,
                                              am.DocumentType_Id,
                                              am.DocumentType_Name,
                                              am.Runing_Q,
                                              am.In_queue,
                                              am.Create_Date,
                                              ami.Appointment_Date,
                                              ami.Dock_Id,
                                              ami.Dock_Name,
                                              ami.Ref_Document_No,
                                              ami.Appointment_Time,
                                              amid.VehicleType_Id,
                                              amid.VehicleType_Name,
                                              amid.Vehicle_No,
                                              yb_d.GateCheckIn_Date,
                                              yb_d.GateCheckOut_Date
                                          }).Where(c => c.Document_status == 1 && c.GateCheckIn_Index != null && c.In_queue == null);
                var data = before_gatecheckin.Where(c => c.Vehicle_No == model.Vehicle_No && (c.Appointment_Date >= model.Appointment_Date && c.Appointment_Date <= model.Appointment_Date_To)).OrderByDescending(c => c.Create_Date).ThenByDescending(c => c.Appointment_Id).Take(1).ToList();

                foreach (var item in data)
                {
                    result.Add(new CheckQueueResponseViewModel
                    {
                        Vehicle_Id = null,
                        Vehicle_LicenseNo = item.Vehicle_No,
                        Appointment_Id = item.Appointment_Id,
                        Appointment_Date = item.Appointment_Date.ToString("yyyy-MM-dd"),
                        Appointment_Time = item.Appointment_Time,
                        Dock_ID = item.Dock_Id,
                        Dock_Name = item.Dock_Name,
                        VehicleType_Id = item.VehicleType_Id,
                        VehicleType_Name = item.VehicleType_Name,
                        Ref_Document_No = item.Ref_Document_No,
                        DocumentType_Id = item.DocumentType_Id,
                        DocumentType_Name = item.DocumentType_Name,
                        GateCheckIn_Date = item.GateCheckIn_Date == null ? null : item.GateCheckIn_Date.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                        GateCheckOut_Date = item.GateCheckOut_Date == null ? null : item.GateCheckOut_Date.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                        Queue_No = item.Runing_Q,
                        Before_Queue = "0001",
                        Status = "WAIT",
                        GateCheckIN_lat = Convert.ToDecimal(latti),
                        GateCheckIN_long = Convert.ToDecimal(longti),
                        GateCheckOUT_lat = Convert.ToDecimal(latti),
                        GateCheckOUT_long = Convert.ToDecimal(longti),
                        GateCheckIN_Radius = 1000,
                        GateCheckOUT_Radius = 1000,
                        GateCheckIn_NO = null,
                        GateCheckOut_NO = null,
                    });
                }
                LoggingService.DataLogLines("After_GateCheckIn", "After_GateCheckIn" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "After_GateCheckIn : " + JsonConvert.SerializeObject(result) + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region After_GetQueue
        public List<CheckQueueResponseViewModel> After_GetQueue(string jsonData)
        {
            Logtxt LoggingService = new Logtxt();
            try
            {
                LoggingService.DataLogLines("After_GetQueue", "After_GetQueue" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "After_GetQueue : " + jsonData + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                AppointmentSearchModel model = Appointment.GetAppointmentSearchModel(jsonData);
                List<CheckQueueResponseViewModel> result = new List<CheckQueueResponseViewModel>();

                var before_gatecheckin = (from am in db.Tb_Appointment.AsQueryable()
                                          join ami in db.Tb_AppointmentItem.Where(c => c.IsActive == 1 && c.IsDelete == 0) on am.Appointment_Index equals ami.Appointment_Index
                                          join amid in db.Tb_AppointmentItemDetail.AsQueryable() on ami.Appointment_Index equals amid.Appointment_Index
                                          join t_yb in db.Tb_YardBalance.AsQueryable() on ami.Appointment_Index equals t_yb.Appointment_Index into yb
                                          from yb_d in yb.DefaultIfEmpty()
                                          select new
                                          {
                                              am.Appointment_Index,
                                              am.Appointment_Id,
                                              am.Document_status,
                                              am.GateCheckIn_Index,
                                              am.DocumentType_Id,
                                              am.DocumentType_Name,
                                              am.Runing_Q,
                                              am.In_queue,
                                              am.Create_Date,
                                              ami.Appointment_Date,
                                              ami.Dock_Id,
                                              ami.Dock_Name,
                                              ami.Ref_Document_No,
                                              ami.Appointment_Time,
                                              amid.VehicleType_Id,
                                              amid.VehicleType_Name,
                                              amid.Vehicle_No,
                                              yb_d.GateCheckIn_Date,
                                              yb_d.GateCheckOut_Date,
                                              yb_d.CheckIn_Index
                                          }).Where(c => c.Document_status == 5 && c.GateCheckIn_Index != null && c.In_queue != null && c.CheckIn_Index == null).ToList();
                var data = before_gatecheckin.Where(c => c.Vehicle_No == model.Vehicle_No && (c.Appointment_Date >= model.Appointment_Date && c.Appointment_Date <= model.Appointment_Date_To)).OrderByDescending(c => c.Create_Date).ThenByDescending(c => c.Appointment_Id).Take(1).ToList();

                foreach (var item in data)
                {
                    result.Add(new CheckQueueResponseViewModel
                    {
                        Vehicle_Id = null,
                        Vehicle_LicenseNo = item.Vehicle_No,
                        Appointment_Id = item.Appointment_Id,
                        Appointment_Date = item.Appointment_Date.ToString("yyyy-MM-dd"),
                        Appointment_Time = item.Appointment_Time,
                        Dock_ID = item.Dock_Id,
                        Dock_Name = item.Dock_Name,
                        VehicleType_Id = item.VehicleType_Id,
                        VehicleType_Name = item.VehicleType_Name,
                        Ref_Document_No = item.Ref_Document_No,
                        DocumentType_Id = item.DocumentType_Id,
                        DocumentType_Name = item.DocumentType_Name,
                        GateCheckIn_Date = item.GateCheckIn_Date == null ? null : item.GateCheckIn_Date.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                        GateCheckOut_Date = item.GateCheckOut_Date == null ? null : item.GateCheckOut_Date.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                        Queue_No = item.Runing_Q,
                        Before_Queue = "0000",
                        Status = "QUEUE",
                        GateCheckIN_lat = Convert.ToDecimal(latti),
                        GateCheckIN_long = Convert.ToDecimal(longti),
                        GateCheckOUT_lat = Convert.ToDecimal(latti),
                        GateCheckOUT_long = Convert.ToDecimal(longti),
                        GateCheckIN_Radius = 1000,
                        GateCheckOUT_Radius = 1000,
                        GateCheckIn_NO = null,
                        GateCheckOut_NO = null,
                    });
                }
                LoggingService.DataLogLines("After_GetQueue", "After_GetQueue" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "After_GetQueue : " + JsonConvert.SerializeObject(result) + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region After_CheckOut
        public List<CheckQueueResponseViewModel> After_CheckOut(string jsonData)
        {
            Logtxt LoggingService = new Logtxt();
            try
            {
                LoggingService.DataLogLines("After_CheckOut", "After_CheckOut" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "After_CheckOut : " + jsonData + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                AppointmentSearchModel model = Appointment.GetAppointmentSearchModel(jsonData);
                List<CheckQueueResponseViewModel> result = new List<CheckQueueResponseViewModel>();

                var before_gatecheckin = (from am in db.Tb_Appointment.AsQueryable()
                                          join ami in db.Tb_AppointmentItem.Where(c => c.IsActive == 1 && c.IsDelete == 0) on am.Appointment_Index equals ami.Appointment_Index
                                          join amid in db.Tb_AppointmentItemDetail.AsQueryable() on ami.AppointmentItem_Index equals amid.AppointmentItem_Index
                                          join t_yb in db.Tb_YardBalance.AsQueryable() on ami.AppointmentItem_Index equals t_yb.AppointmentItem_Index into yb
                                          from yb_d in yb.DefaultIfEmpty()
                                          select new
                                          {
                                              am.Appointment_Index,
                                              am.Appointment_Id,
                                              am.Document_status,
                                              am.GateCheckIn_Index,
                                              am.GateCheckOut_Index,
                                              am.DocumentType_Id,
                                              am.DocumentType_Name,
                                              am.Runing_Q,
                                              am.In_queue,
                                              am.Create_Date,
                                              ami.Appointment_Date,
                                              ami.Dock_Id,
                                              ami.Dock_Name,
                                              ami.Ref_Document_No,
                                              ami.Appointment_Time,
                                              amid.VehicleType_Id,
                                              amid.VehicleType_Name,
                                              amid.Vehicle_No,
                                              yb_d.GateCheckIn_Date,
                                              yb_d.GateCheckOut_Date,
                                              yb_d.CheckIn_Index,
                                              yb_d.CheckOut_Index
                                          }).Where(c => c.Document_status == 2 && c.GateCheckIn_Index != null && c.In_queue != null && c.CheckOut_Index != null && c.GateCheckOut_Index == null).ToList();
                var data = before_gatecheckin.Where(c => c.Vehicle_No == model.Vehicle_No && (c.Appointment_Date >= model.Appointment_Date && c.Appointment_Date <= model.Appointment_Date_To)).OrderByDescending(c => c.Create_Date).ThenByDescending(c => c.Appointment_Id).Take(1).ToList();

                foreach (var item in data)
                {
                    result.Add(new CheckQueueResponseViewModel
                    {
                        Vehicle_Id = null,
                        Vehicle_LicenseNo = item.Vehicle_No,
                        Appointment_Id = item.Appointment_Id,
                        Appointment_Date = item.Appointment_Date.ToString("yyyy-MM-dd"),
                        Appointment_Time = item.Appointment_Time,
                        Dock_ID = item.Dock_Id,
                        Dock_Name = item.Dock_Name,
                        VehicleType_Id = item.VehicleType_Id,
                        VehicleType_Name = item.VehicleType_Name,
                        Ref_Document_No = item.Ref_Document_No,
                        DocumentType_Id = item.DocumentType_Id,
                        DocumentType_Name = item.DocumentType_Name,
                        GateCheckIn_Date = item.GateCheckIn_Date == null ? null : item.GateCheckIn_Date.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                        GateCheckOut_Date = item.GateCheckOut_Date == null ? null : item.GateCheckOut_Date.Value.ToString("yyyy-MM-dd hh:mm:ss"),
                        Queue_No = item.Runing_Q,
                        Before_Queue = "0000",
                        Status = "CHECKOUT",
                        GateCheckIN_lat = Convert.ToDecimal(latti),
                        GateCheckIN_long = Convert.ToDecimal(longti),
                        GateCheckOUT_lat = Convert.ToDecimal(latti),
                        GateCheckOUT_long = Convert.ToDecimal(longti),
                        GateCheckIN_Radius = 1000,
                        GateCheckOUT_Radius = 1000,
                        GateCheckIn_NO = null,
                        GateCheckOut_NO = null,
                    });
                }
                LoggingService.DataLogLines("After_CheckOut", "After_CheckOut" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "After_CheckOut : " + JsonConvert.SerializeObject(result) + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #endregion

        #region test_Api

        #region Checkout
        public string SaveCheckOut_test(string jsonData)
        {
            try
            {
                var XX = "";
                TransportManifest transportManifest_result = new TransportManifest();
                CheckOutModel model = CheckOut.GetCheckOutModel(jsonData);
                if (model.Appointment_Id == null) { throw new Exception("Invalid Parameter : Appointment Id not found"); }

                var appointmentItem = db.Tb_AppointmentItem.Where(w => w.Appointment_Id == model.Appointment_Id).ToList();
                if (appointmentItem is null) { throw new Exception("AppointmentItem not found"); }
                foreach (var item in appointmentItem)
                {
                    if (item.DocumentType_Index == Guid.Parse("C392D865-8E69-4985-B72F-2421EBE8BCDB"))
                    {
                        var transportManifest = new TransportManifest();
                        var Appdetail = db.Tb_AppointmentItemDetail.Where(c => c.AppointmentItem_Index == item.AppointmentItem_Index).ToList();
                        foreach (var itemXX in Appdetail)
                        {
                            var itemX = new Item();
                            if (itemXX.TransportManifest_Index != null)
                            {

                                var Appitem = db.Tb_AppointmentItem.FirstOrDefault(c => c.AppointmentItem_Index == item.AppointmentItem_Index);
                                var TM_NO = new SqlParameter("@TruckLoad_No", Appitem.Ref_Document_No);
                                var tagout = dbGI.View_tagout_with_truckload.FromSql("sp_View_tagout_with_truckload @TruckLoad_No", TM_NO).ToList();
                                if (tagout.Count > 0)
                                {
                                    foreach (var item_tag in tagout)
                                    {
                                        var ItemsDO = new itemsDO();
                                        ItemsDO.TransportOrder_No = item_tag.PlanGoodsIssue_No;
                                        ItemsDO.Tag_No = item_tag.TagOut_No;
                                        ItemsDO.Product_Id = item_tag.Product_Id;
                                        ItemsDO.Qty = item_tag.QTY;
                                        ItemsDO.ProductConversion_Name = item_tag.ProductConversion_Name;
                                        ItemsDO.Width = item_tag.Width;
                                        ItemsDO.Length = item_tag.Length;
                                        ItemsDO.Height = item_tag.Height;
                                        ItemsDO.Volume = item_tag.Volume;
                                        ItemsDO.Weight_Unit = item_tag.Weight;
                                        itemX.itemsDO.Add(ItemsDO);
                                    }
                                }
                                else
                                {
                                    //return transportManifest_result;
                                }

                                itemX.transportManifest_Index = itemXX.TransportManifest_Index.ToString();
                                itemX.transportManifest_No = Appitem.Ref_Document_No;
                                transportManifest.items.Add(itemX);
                            }
                        }
                        XX = JsonConvert.SerializeObject(transportManifest);
                        //var result_api = new TransportManifest();
                        //transportManifest_result = Utils.SendDataApi_release_manifest<TransportManifest>(new AppSettingConfig().GetUrl("release_manifest"), JsonConvert.SerializeObject(transportManifest), "02B31868-9D3D-448E-B023-05C121A424F4");
                        //foreach (var code in result_api.items)
                        //{
                        //    if (code.code != "000")
                        //    {
                        //        return false;
                        //    }
                        //}

                    }
                }
                return XX;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Time
        public bool Main()
        {
            var appoint = (from am in db.Tb_Appointment.AsQueryable()
                           join ami in db.Tb_AppointmentItem.Where(c => c.IsActive == 1 && c.IsDelete == 0) on am.Appointment_Index equals ami.Appointment_Index
                           select new
                           {
                               am.Appointment_Index
                               ,
                               am.Appointment_Id
                               ,
                               am.DocumentType_Index
                               ,
                               am.DocumentType_Id
                               ,
                               am.DocumentType_Name
                               ,
                               am.Create_By
                               ,
                               am.Update_By
                               ,
                               am.Cancel_By
                               ,
                               am.Update_Date
                               ,
                               am.Create_Date
                               ,
                               am.Document_status
                               ,
                               ami.Appointment_Date
                               ,
                               ami.Appointment_Time
                               ,
                               ami.Dock_Name
                               ,
                               ami.Ref_Document_No
                               ,
                               ami.WareHouse_Index
                           }).GroupBy(c => new
                           {
                               c.Appointment_Index
                               ,
                               c.Appointment_Id
                               ,
                               c.DocumentType_Index
                               ,
                               c.DocumentType_Id
                               ,
                               c.DocumentType_Name
                               ,
                               c.Create_By
                               ,
                               c.Update_By
                               ,
                               c.Cancel_By
                               ,
                               c.Create_Date
                               ,
                               c.Update_Date
                               ,
                               c.Document_status
                               ,
                               c.Appointment_Date
                               ,
                               c.Appointment_Time
                               ,
                               c.Ref_Document_No
                               ,
                               c.WareHouse_Index
                           }).Select(c => new
                           {
                               c.Key.Appointment_Index
                               ,
                               c.Key.Appointment_Id
                               ,
                               c.Key.DocumentType_Index
                               ,
                               c.Key.DocumentType_Id
                               ,
                               c.Key.DocumentType_Name
                               ,
                               c.Key.Create_By
                               ,
                               c.Key.Update_By
                               ,
                               c.Key.Cancel_By
                               ,
                               c.Key.Create_Date
                               ,
                               c.Key.Update_Date
                               ,
                               c.Key.Document_status
                               ,
                               c.Key.Appointment_Date
                               ,
                               Appointment_Time = DateTime.Parse(c.Key.Appointment_Time.Split('-')[0].Trim())
                               ,
                               Dock = string.Join(" , ", c.Select(w => w.Dock_Name))
                               ,
                               c.Key.Ref_Document_No
                               ,
                               c.Key.WareHouse_Index
                           }).OrderByDescending(c => c.Appointment_Date).Take(20).ToList();


            //var times = new[] { "10:30 - 11:30", "22:40", "12:00", "10:00", "13:00", "08:00", }.Select(x => DateTime.Parse(x));
            var splitXX = double.Parse(((DateTime.Now.ToString().Split(' '))[1]).Split(':')[0]);
            Console.WriteLine(splitXX);
            var output = appoint.OrderByDescending(x => (x.Appointment_Time.TimeOfDay.TotalHours <= 10 ? 24.0 : 0.0) + x.Appointment_Time.TimeOfDay.TotalHours).ToList();
            //foreach (var XX in output)
            //{

            //    Console.WriteLine(XX);
            //}

            var sdfa = JsonConvert.SerializeObject(output);
            return true;
        }
        #endregion

        #region checkout Error
        public Result checkout_Error(string id)
        {
            Logtxt LoggingService = new Logtxt();
            var result = new Result();
            try
            {
                CheckOut_ErrorModel errorModel = JsonConvert.DeserializeObject<CheckOut_ErrorModel>(id);
                //if (id == null) { throw new Exception("Invalid Parameter : Appointment Id not found"); }
                var appointment_list = errorModel.id.Split(',');

                foreach (var appoint_ID in appointment_list)
                {
                    List<Tb_YardBalance> appointmentItem = db.Tb_YardBalance.Where(w => w.Appointment_Id == appoint_ID).ToList();
                    if (appointmentItem is null) { throw new Exception("AppointmentItem not found"); }
                    else if (appointmentItem[0].DocumentType_Index == Guid.Parse("C392D865-8E69-4985-B72F-2421EBE8BCDB"))
                    {
                        var checkload = dbGI.im_TruckLoad.FirstOrDefault(c => c.TruckLoad_No == appointmentItem[0].Ref_Document_No && c.Document_Status == 2);
                        if (checkload == null)
                        {
                            result.ResultIsUse = false;
                            result.ResultMsg = "กรุณาทำการ Load สินค้าก่อนทำการ Check out dock";
                            return result;
                        }
                    }
                    foreach (var item in appointmentItem)
                    {

                        if (item.Status_Id != 0) { throw new Exception("Invalid CheckIn : AppointmentItem already Processes"); }

                        string checkOutStatus = "Checked Out";
                        DateTime checkOutDate = (DateTime.Now).TrimTime();
                        TimeSpan checkOutTime = TimeSpan.Parse(DateTime.Now.ToString("HH:mm"));

                        Guid checkOutIndex = Guid.NewGuid();
                        Guid yardMovementIndex = Guid.NewGuid();
                        string userBy = "Checkout";
                        DateTime userDate = DateTime.Now;
                        int activityID = 2;

                        Tb_CheckOut newCheckOut = new Tb_CheckOut
                        {
                            CheckOut_Index = checkOutIndex,
                            Appointment_Index = item.Appointment_Index,
                            Appointment_Id = item.Appointment_Id,
                            AppointmentItem_Index = item.AppointmentItem_Index,
                            AppointmentItem_Id = item.AppointmentItem_Id,
                            Appointment_Date = item.Appointment_Date.Value,
                            Appointment_Time = item.Appointment_Time,

                            CheckOut_Date = checkOutDate,
                            CheckOut_Time = checkOutTime,
                            Remark = null,

                            IsActive = 1,
                            IsDelete = 0,
                            IsSystem = 0,
                            Status_Id = 0,
                            Create_By = userBy,
                            Create_Date = userDate
                        };
                        db.Tb_CheckOut.Add(newCheckOut);

                        item.CheckOut_Index = newCheckOut.CheckOut_Index;
                        item.CheckOut_Date = newCheckOut.CheckOut_Date;
                        item.CheckOut_Time = newCheckOut.CheckOut_Time;
                        item.CheckOut_Remark = newCheckOut.Remark;
                        item.CheckOut_By = userBy;
                        item.CheckOut_Status = checkOutStatus;
                        item.Status_Id = 1;

                        Tb_YardMovement newYardMovement = new Tb_YardMovement
                        {
                            YardMovement_Index = yardMovementIndex,
                            YardBalance_Index = item.YardBalance_Index,

                            Appointment_Index = item.Appointment_Index,
                            Appointment_Id = item.Appointment_Id,
                            AppointmentItem_Index = item.AppointmentItem_Index,
                            AppointmentItem_Id = item.AppointmentItem_Id,
                            Appointment_Date = item.Appointment_Date.Value,
                            Appointment_Time = item.Appointment_Time,

                            Activity_Id = activityID,

                            CheckOut_Index = newCheckOut.CheckOut_Index,
                            CheckOut_Date = newCheckOut.CheckOut_Date,
                            CheckOut_Time = newCheckOut.CheckOut_Time,
                            CheckOut_Remark = newCheckOut.Remark,
                            CheckOut_Status = checkOutStatus,
                            CheckOut_By = userBy,

                            IsActive = 1,
                            IsDelete = 0,
                            IsSystem = 0,
                            Status_Id = 0,
                            Create_By = userBy,
                            Create_Date = userDate
                        };
                        db.Tb_YardMovement.Add(newYardMovement);

                        var dock_update = dbMS.Ms_Dock.FirstOrDefault(c => c.Dock_Index == item.Dock_Index);
                        dock_update.Last_Checkout = DateTime.Now;
                        dock_update.Dock_use = 0;

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
                        var myTransactionMS = dbMS.Database.BeginTransaction(IsolationLevel.Serializable);
                        try
                        {
                            dbMS.SaveChanges();
                            myTransactionMS.Commit();
                        }
                        catch (Exception saveEx)
                        {
                            myTransactionMS.Rollback();
                            throw saveEx;
                        }
                    }
                }
                result.ResultIsUse = true;
                return result;
            }
            catch (Exception ex)
            {
                result.ResultIsUse = false;
                result.ResultMsg = ex.Message;
                return result;
            }
        }
        #endregion

        #region checkStatus_gatecheckin_byTMS
        public Result checkStatus_gatecheckin_byTMS(string json)
        {
            Logtxt LoggingService = new Logtxt();
            var result = new Result();
            try
            {
                LoggingService.DataLogLines("Before_GateCheckIn", "Before_GateCheckIn" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"), "json : " + json + " " + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss"));
                status_byTMS status_ByTMS = JsonConvert.DeserializeObject<status_byTMS>(json);

                List<Tb_AppointmentItem> appointmentItem = db.Tb_AppointmentItem.Where(c => c.Ref_Document_No == status_ByTMS.tm_no && c.IsActive == 1).ToList();

                if (appointmentItem.Count <= 0)
                {
                    result.ResultIsUse = false;
                    result.ResultMsg = status_ByTMS.tm_no + " ยังไม่มีการส่งมา Booking กรุณาตรวจสอบเลข Shipment!!";
                    return result;
                }

                List<Tb_YardBalance> yardBalances = db.Tb_YardBalance.Where(c => c.Ref_Document_No == status_ByTMS.tm_no && c.IsActive == 1).ToList();

                if (yardBalances.Count > 0)
                {
                    result.ResultIsUse = false;
                    result.ResultMsg = status_ByTMS.tm_no + " มีการ check in gate แล้ว กรุณไปถอยเอกสารที่ระบบ TMS ก่อน";
                    return result;
                }


                result.ResultIsUse = true;
                return result;
            }
            catch (Exception ex)
            {
                result.ResultIsUse = false;
                result.ResultMsg = ex.Message;
                return result;
            }
        }
        #endregion

        #region LineNotify
        public Boolean LineNotify(string msg)
        {
            string token = "ozCYzfKZZyABflNSDsg0k4WTvWnhmWkhmVRCiKBOM2y";
            String State = "Start ";
            var olog = new Logtxt();




            try
            {
                olog.Logging("LineNoti", State);
                var request = (HttpWebRequest)WebRequest.Create("https://notify-api.line.me/api/notify");
                var postData = string.Format("message={0}", msg);
                var data = Encoding.UTF8.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                request.Headers.Add("Authorization", "Bearer " + token);

                using (var stream = request.GetRequestStream()) stream.Write(data, 0, data.Length);
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                olog.Logging("LineNoti", msg);
                return true;

            }
            catch (Exception ex)
            {
                olog.Logging("LineNoti", ex.ToString());
                return false;

            }
            //return true;
        }

        #endregion

        #endregion


        #region SaveLogRequest
        public string SaveLogRequest(string orderno, string json, string interfacename, int status, string txt, Guid logindex)
        {
            try
            {
                log_api_request l = new log_api_request();
                l.log_id = logindex;
                l.log_date = DateTime.Now;
                l.log_requestbody = json;
                l.log_absoluteuri = "";
                l.status = status;
                l.Interface_Name = interfacename;
                l.Status_Text = txt;
                l.File_Name = orderno;
                dbGI.log_api_request.Add(l);
                dbGI.SaveChanges();
                return "";
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        #endregion

        #region SaveLogResponse
        public string SaveLogResponse(string orderno, string json, string interfacename, int status, string txt, Guid logindex)
        {
            try
            {
                log_api_reponse l = new log_api_reponse();
                l.log_id = logindex;
                l.log_date = DateTime.Now;
                l.log_reponsebody = json;
                l.log_absoluteuri = "";
                l.status = status;
                l.Interface_Name = interfacename;
                l.Status_Text = txt;
                l.File_Name = orderno;
                dbGI.log_api_reponse.Add(l);

                //var d = dbGI.log_api_request.Where(c => c.log_id == logindex).FirstOrDefault();
                //d.status = status;

                dbGI.SaveChanges();
                return "";
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        #endregion

        #region Qcall
        public QcallModel Qcall()
        {
            try
            {
                QcallModel result = new QcallModel();
                List<InboundModel> resultInbound = new List<InboundModel>();
                List<OutboundModel> resultOutbound = new List<OutboundModel>();
                var lstIB = dbQcall.View_Monitor_Queue.Where(w => w.DockType == "IB").ToList();
                var lstOB = dbQcall.View_Monitor_Queue.Where(w => w.DockType == "OB").ToList();

                foreach (var item in lstIB)
                {
                    InboundModel model = new InboundModel();

                    model.TransactionID = item.TransactionID.ToString();
                    model.QNo = item.QNo;
                    model.DockNo = item.DockNo;
                    model.DockType = item.DockType;
                    model.LicenseNo = item.LicenseNo;
                    model.Appointment_Id = item.Appointment_Id;
                    model.QueueDate = item.QueueDate;
                    model.QueueTime = item.QueueTime;
                    model.CheckInDate = item.CheckInDate;
                    model.CheckInTime = item.CheckInTime;
                    model.StatusText = item.StatusText.ToString();
                    model.Status = item.Status.ToString();
                    model.Remark = item.Remark.ToString();
                    model.UpdateDT = item.UpdateDT.ToString();
                    model.UpdateBy = item.UpdateBy;
                    resultInbound.Add(model);
                }

                foreach (var item in lstOB)
                {
                    OutboundModel model = new OutboundModel();

                    model.TransactionID = Convert.ToInt32(item.TransactionID).ToString();
                    model.QNo = item.QNo;
                    model.DockNo = item.DockNo;
                    model.DockType = item.DockType;
                    model.LicenseNo = item.LicenseNo;
                    model.Appointment_Id = item.Appointment_Id;
                    model.QueueDate = item.QueueDate;
                    model.QueueTime = item.QueueTime;
                    model.CheckInDate = item.CheckInDate;
                    model.CheckInTime = item.CheckInTime;
                    model.StatusText = item.StatusText.ToString();
                    model.Status = item.Status.ToString();
                    model.Remark = item.Remark.ToString();
                    model.UpdateDT = item.UpdateDT.ToString();
                    model.UpdateBy = item.UpdateBy;
                    resultOutbound.Add(model);
                }

                result.lstInbound = resultInbound;
                result.lstOutbound = resultOutbound;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool Update_Qcall_Status(QueueModel model)
        {
            try
            {
                bool result = false;

                var resUpdate = dbQcall.tbl_qcall.Where(w => w.Appointment_Id == model.appointment_Id).FirstOrDefault();
                if (resUpdate != null)
                {
                    resUpdate.Status = Convert.ToInt16(model.status);
                }

                var Transaction = dbQcall.Database.BeginTransaction(IsolationLevel.Serializable);
                try
                {
                    dbQcall.SaveChanges();
                    Transaction.Commit();
                    result = true;
                }
                catch (Exception saveEx)
                {
                    Transaction.Rollback();
                    throw saveEx;
                }

                return result;

                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
