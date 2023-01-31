using System;
using System.Collections.Generic;
using System.Linq;

using DataAccess;
using Business.Models;
using Microsoft.Extensions.Configuration;
using static Business.Models.SearchDetailModel;
using Comone.Utils;
using Business.Commons;
using System.Globalization;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class CarInspectionService
    {
        private readonly YardDbContext db;
        private readonly MasterDbContext dbMS;
        private readonly GRDbContext dbGR;
        private readonly GIDbContext dbGI;
       
        public CarInspectionService()
        {
            db = new YardDbContext();
            dbMS = new MasterDbContext();
            dbGR = new GRDbContext();
            dbGI = new GIDbContext();
            
    }

        public CarInspectionService(YardDbContext db, MasterDbContext dbMS, GRDbContext dbGR, GIDbContext dbGI )
        {
            this.db = db;
            this.dbMS = dbMS;
            this.dbGR = dbGR;
            this.dbGI = dbGI;
        }

        public CarInspectionService(IConfiguration configuration)
        {
            
        }

        #region filter
        public actionResultViewModel filter(SearchDetailModel model)
        {
            try
            {
                var query = db.Tb_AppointmentItem.AsQueryable();

                var appoint = (from am in db.Tb_Appointment.AsQueryable()
                               join ami in db.Tb_AppointmentItem.AsQueryable() on am.Appointment_Index equals ami.Appointment_Index
                               join amid in db.Tb_AppointmentItemDetail.AsQueryable() on ami.AppointmentItem_Index equals amid.AppointmentItem_Index
                               select new {
                                   am.Appointment_Index,
                                   am.Appointment_Id,
                                   am.DocumentType_Name,
                                   am.DocumentType_Index,
                                   am.User_carInspection,
                                   am.Car_Inspection,
                                   am.GateCheckIn_Index,
                                   am.Document_status,
                                   date_booking =  ami.Appointment_Date,
                                   ami.Appointment_Time,
                                   ami.Dock_Name,
                                   ami.Dock_Index,
                                   ami.Ref_Document_No,
                                   amid.Vehicle_No,
                                   amid.Driver_Name,
                                   amid.VehicleType_Name,
                               }
                               ).Where(c=> c.GateCheckIn_Index != null ).ToList();
                
                #region Basic
                if (!string.IsNullOrEmpty(model.appointment_Date) && !string.IsNullOrEmpty(model.appointment_Date_To))
                {
                    DateTime dt_S = DateTime.ParseExact(model.appointment_Date, "yyyyMMdd",CultureInfo.InvariantCulture);
                    DateTime dt_E = DateTime.ParseExact(model.appointment_Date_To, "yyyyMMdd",CultureInfo.InvariantCulture).AddDays(1).AddMinutes(-1);
                    appoint = appoint.Where(c => c.date_booking >= dt_S && c.date_booking <= dt_E).ToList();
                }
                if (!string.IsNullOrEmpty(model.Appointment_Id))
                {
                    appoint = appoint.Where(c => c.Appointment_Id == model.Appointment_Id).ToList();
                }
                if (!string.IsNullOrEmpty(model.Vehicle_No))
                {
                    appoint = appoint.Where(c => c.Vehicle_No == model.Vehicle_No).ToList();
                }
                if (!string.IsNullOrEmpty(model.Dock_Index))
                {
                    appoint = appoint.Where(c => c.Dock_Index == Guid.Parse(model.Dock_Index)).ToList();
                }

                #region Status
                var statusModels = new List<int?>();
                if (model.status.Count > 0)
                {
                    foreach (var status in model.status)
                    {
                        if (status.value == "Success")
                        {
                            statusModels.Add(1);
                        }
                        else if (status.value == "Cancle")
                        {
                            statusModels.Add(-1);
                        }
                        else if (status.value == "Pending")
                        {
                            statusModels.Add(0);
                        }
                    }
                    appoint = appoint.Where(c => statusModels.Contains(c.Car_Inspection == null ? 0 : c.Car_Inspection)).ToList();
                }
                else {
                    appoint = appoint.Where(c => c.Car_Inspection == null).ToList();
                }
                
                #endregion

                #endregion


                var TotalRow = appoint.ToList().Count();


                if (model.CurrentPage != 0 && model.PerPage != 0)
                {
                    query = query.Skip(((model.CurrentPage - 1) * model.PerPage));
                }

                if (model.PerPage != 0)
                {
                    query = query.Take(model.PerPage);

                }
                
                var result = new List<SearchDetailModel>();

                foreach (var item in appoint)
                {
                    var Appoint = db.Tb_Appointment.FirstOrDefault(c => c.Appointment_Index == item.Appointment_Index);
                    var resultItem = new SearchDetailModel();
                    resultItem.Appointment_Index = item.Appointment_Index;
                    resultItem.Appointment_Id = item.Appointment_Id;
                    resultItem.DocumentType_Name = item.DocumentType_Name;
                    resultItem.DocumentType_Index = item.DocumentType_Index;
                    resultItem.User_carInspection = item.User_carInspection == null ? "-" : item.User_carInspection;
                    resultItem.date_booking = item.date_booking;
                    resultItem.Appointment_Time = item.Appointment_Time;
                    resultItem.Dock_Name = item.Dock_Name;
                    resultItem.Vehicle_No = item.Vehicle_No;
                    resultItem.Driver_Name = item.Driver_Name;
                    resultItem.VehicleType_Name = item.VehicleType_Name;
                    resultItem.VehicleType_Name = item.Ref_Document_No;
                    resultItem.Car_Inspection = item.Car_Inspection == null ? "-" : (item.Car_Inspection == 1 ? "ผ่าน" : "ไม่ผ่่าน");
                    if (Appoint.Document_status == -1)
                    {
                        resultItem.Is_Inspection = 1;
                    }
                    else {
                        resultItem.Is_Inspection = item.Car_Inspection == 1 ? 1 : 0;
                    }
                    
                    result.Add(resultItem);
                }
                var count = TotalRow;

                var actionResult = new actionResultViewModel();
                actionResult.itemsCarInspection = result.OrderByDescending(o => o.date_booking).ThenByDescending(o => o.Appointment_Id).ToList();
                actionResult.pagination = new Pagination() { TotalRow = count, CurrentPage = model.CurrentPage, PerPage = model.PerPage, };

                return actionResult;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ConfirmStatus
        public Result confirmStatus(SearchDetailModel model)
        {
            var result = new Result();
            Logtxt LoggingService = new Logtxt();
            try
            {
                LoggingService.DataLogLines("Car_Inspection", "Car_Inspection_" + DateTime.Now.ToString("yyyy-MM-dd"), "Start Appove" + DateTime.Now.ToString("yyyy-MM-dd-HHmm"));
                var appoint = db.Tb_Appointment.FirstOrDefault(c => c.Appointment_Index == model.Appointment_Index);
                if (appoint == null)
                {
                    result.ResultIsUse = false;
                    result.ResultMsg = "ไม่พบ Appointment ที่ทำการยืนยัน";
                    return result;
                }
                else
                {
                    LoggingService.DataLogLines("Car_Inspection", "Car_Inspection_" + DateTime.Now.ToString("yyyy-MM-dd"), "Appoint  Appove : " + appoint.Appointment_Id);
                    LoggingService.DataLogLines("Car_Inspection", "Car_Inspection_" + DateTime.Now.ToString("yyyy-MM-dd"), "Appoint  Appove : " + DateTime.Now.ToString("yyyy-MM-dd-HHmm"));
                    appoint.User_carInspection = model.update_by;
                    appoint.Car_Inspection = 1;
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
                    result.ResultIsUse = false;
                    result.ResultMsg = "ไม่สามารถทำการยืนยันการตรวจสอบได้";
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

        #region RejectStatus
        public Result rejectStatus(SearchDetailModel model)
        {
            var result = new Result();
            Logtxt LoggingService = new Logtxt();
            try
            {
                LoggingService.DataLogLines("Car_Inspection", "Car_Inspection_reject" + DateTime.Now.ToString("yyyy-MM-dd"), "Start Reject" + DateTime.Now.ToString("yyyy-MM-dd-HHmm"));
                var appoint = db.Tb_Appointment.FirstOrDefault(c => c.Appointment_Index == model.Appointment_Index);
                if (appoint == null)
                {
                    result.ResultIsUse = false;
                    result.ResultMsg = "ไม่พบ Appointment ที่ทำการ Reject";
                    return result;
                }
                else
                {
                    LoggingService.DataLogLines("Car_Inspection", "Car_Inspection_reject" + DateTime.Now.ToString("yyyy-MM-dd"), "Appoint  reject : " + appoint.Appointment_Id);
                    LoggingService.DataLogLines("Car_Inspection", "Car_Inspection_reject" + DateTime.Now.ToString("yyyy-MM-dd"), "Appoint  reject : " + DateTime.Now.ToString("yyyy-MM-dd-HHmm"));
                    appoint.User_carInspection = model.update_by;
                    appoint.Car_Inspection = -1;
                    appoint.Document_status = -1;
                    appoint.IsActive = 0;
                    appoint.IsDelete = 1;
                    appoint.Cancel_By = model.update_by == null ? "Null user" : model.update_by;
                    appoint.Cancel_Date = DateTime.Now;

                    List<DataAccess.Models.Yard.Table.Tb_AppointmentItem> D_API_reject = db.Tb_AppointmentItem.Where(c => c.Appointment_Index == appoint.Appointment_Index).ToList();
                    List<DataAccess.Models.Yard.Table.Tb_YardBalance> D_YBL_reject = db.Tb_YardBalance.Where(c => c.Appointment_Index == appoint.Appointment_Index).ToList();

                    foreach (var item in D_API_reject)
                    {
                        item.IsActive = 0;
                        item.IsDelete = 1;
                        item.Cancel_By = model.update_by;
                        item.Cancel_Date = DateTime.Now;
                    }
                    //db.Tb_AppointmentItem.RemoveRange(D_API_reject);
                    db.Tb_YardBalance.RemoveRange(D_YBL_reject);
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
                    result.ResultIsUse = false;
                    result.ResultMsg = "ไม่สามารถทำการยืนยันการตรวจสอบได้";
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

        #region autoAppoinmentNo
        public List<ItemListViewModel> autoAppoinmentNo(ItemListViewModel data)
        {
            try
            {
                var query = db.Tb_Appointment.Where(c=> c.GateCheckIn_Index != null && c.In_queue == null);

                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Appointment_Id.Contains(data.key));

                }

                var items = new List<ItemListViewModel>();

                var result = query.Select(c => new { c.Appointment_Index, c.Appointment_Id }).Distinct().Take(10).ToList();


                foreach (var item in result)
                {
                    var resultItem = new ItemListViewModel
                    {
                        index = item.Appointment_Index,
                        name = item.Appointment_Id
                    };
                    items.Add(resultItem);

                }



                return items;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoAppoinmentVehicle_no
        public List<ItemListViewModel> autoAppoinmentVehicle_no(ItemListViewModel data)
        {
            try
            {
                //var query = db.Tb_AppointmentItemDetail.Where(c => c.GateCheckIn_Index != null);
                var appointment_vehicle_no = (from am in db.Tb_Appointment.Where(c => c.GateCheckIn_Index != null)
                             join amid in db.Tb_AppointmentItemDetail on am.Appointment_Index equals amid.Appointment_Index
                             select new {
                                 am.Appointment_Index,
                                 amid.Vehicle_No
                             }).ToList();

                if (!string.IsNullOrEmpty(data.key))
                {
                    appointment_vehicle_no = appointment_vehicle_no.Where(c => c.Vehicle_No.Contains(data.key)).ToList();

                }

                var items = new List<ItemListViewModel>();

                var result = appointment_vehicle_no.Select(c => new { c.Appointment_Index, c.Vehicle_No }).Distinct().Take(10).ToList();


                foreach (var item in result)
                {
                    var resultItem = new ItemListViewModel
                    {
                        index = item.Appointment_Index,
                        name = item.Vehicle_No
                    };
                    items.Add(resultItem);

                }



                return items;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region dropdown
        public List<DockDoorViewModelV2> dockDoorDropdown(DockDoorViewModelV2 data)
        {
            try
            {
                var result = new List<DockDoorViewModelV2>();

                var query = dbMS.Ms_Dock.AsQueryable();

                query.Where(c => c.IsActive == 1 && c.IsDelete == 0);

                var queryResult = query.OrderBy(o => o.Dock_Id).ToList();

                foreach (var item in queryResult)
                {
                    var resultItem = new DockDoorViewModelV2();

                    resultItem.dockDoor_Index = item.Dock_Index;
                    resultItem.dockDoor_Id = item.Dock_Id;
                    resultItem.dockDoor_Name = item.Dock_Name;

                    result.Add(resultItem);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }

}
