using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

using Business.Services;
using Microsoft.AspNetCore.Hosting;
using Business.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/appointment")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public AppointmentController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("list_warehouse")]
        public IActionResult ListWareHouseForAppointment([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.ListWareHouseForAppointment();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("list_dockqouta")]
        public IActionResult ListDockQouataForAppointment([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.ListDockQouataForAppointment(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        

        [HttpPost("list_dockqouta_reAssign")]
        public IActionResult ListDockQouataForAppointmentreAssign([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.ListDockQouataForAppointmentreAssign(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("ListDockForAppointmentQ")]
        public IActionResult ListDockForAppointmentQ([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.ListDockForAppointmentQ(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("ListDockForAppointmentreAssign")]
        public IActionResult ListDockForAppointmentreAssign([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.ListDockForAppointmentreAssign(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("ListBlockTime")]
        public IActionResult ListBlockTime([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.ListBlockTime(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
      [HttpPost("list_documentType")]
        public IActionResult ListDocumentTypeForAppointment([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.ListDocumentTypeForAppointment();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("list_dock")]
        public IActionResult ListDockForDockQouta([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.ListDockForAppointment();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("list_time")]
        public IActionResult ListTimeForQueue([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.ListTimeForQueue();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("list")]
        public IActionResult ListAppointment([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.ListAppointment(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("list_queue")]
        public IActionResult list_queue([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.list_queue(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("get")]
        public IActionResult GetAppointment([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.GetAppointmentGroup(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("get_item")]
        public IActionResult GetAppointmentItem([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.GetAppointmentItemGroup(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("save")]
        public IActionResult SaveAppointment([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                Guid result = service.SaveAppointment(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("save_item")]
        public IActionResult SaveAppointmentItem([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.SaveAppointmentItemGroup(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("save_detail")]
        public IActionResult SaveAppointmentItemDetail([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.SaveAppointmentItemDetail(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("delete")]
        public IActionResult DeleteAppointment([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.DeleteAppointment(body?.ToString() ?? string.Empty);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("delete_item")]
        public IActionResult DeleteAppointmentItem([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.DeleteAppointmentItemGroup(body?.ToString() ?? string.Empty);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("delete_detail")]
        public IActionResult DeleteAppointmentItemDetail([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.DeleteAppointmentItemDetail(body?.ToString() ?? string.Empty);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("Approve")]
        public IActionResult ApproveAppointment([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                bool result = service.Confirm_Approve(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("Approve_Q")]
        public IActionResult Approve_Q([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.Approve_Q(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        

       [HttpPost("Auto_Approve_Q")]
        public IActionResult Auto_Approve_Q()
        {
            try
            {
                YardService service = new YardService();
                var result = service.Autoget_Q();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("SaveAppointment_Auto_out")]
        public IActionResult SaveAppointment_Auto_out()
        {
            try
            {
                YardService service = new YardService();
                var result = service.SaveAppointment_Auto_out();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("again/{id}")]
        public IActionResult again(string id)
        {
            try
            {
                YardService service = new YardService();
                var result = service.callQueue(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("SaveAppointment_ReAssign")]
        public IActionResult SaveAppointment_ReAssign([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.SaveAppointment_ReAssign(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("UpdatebookingByTruckload")]
        public IActionResult UpdatebookingByTruckload([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var data = JsonConvert.DeserializeObject<TransportManifestUpdate>(body.ToString());
                var result = service.UpdatebookingByTruckload(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("Checkupdate_WentWAVE_OR_Checkin")]
        public IActionResult Checkupdate_WentWAVE_OR_Checkin([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                
                var result = service.Checkupdate_WentWAVE_OR_Checkin(body.ToString());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        #region ReportPrintOutAppointment
        [HttpPost("ReportPrintOutAppointment")]
        public IActionResult ReportPrintOutAppointment([FromBody]JObject body)
        {
            try
            {
                var service = new YardService();
                string localFilePath = service.ReportPrintOutApponitment(body?.ToString() ?? string.Empty, _hostingEnvironment.ContentRootPath);
                if (!System.IO.File.Exists(localFilePath))
                {
                    return NotFound();
                }

                var result = File(System.IO.File.ReadAllBytes(localFilePath), "application/octet-stream");
                System.IO.File.Delete(localFilePath);
                return result;
                //return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        #endregion


        #region testtime
        [HttpPost("testtime")]
        public IActionResult testtime()
        {
            try
            {
                var service = new YardService();
                var xx = service.Main();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        #endregion

        #region testtime
        [HttpPost("checkout_Error")]
        public IActionResult checkout_Error([FromBody]JObject id)
        {
            try
            {
                var service = new YardService();
                var xx = service.checkout_Error(id.ToString());

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        #endregion

        #region printOutQ
        [HttpPost("printOutQ")]
        public IActionResult printOutQ([FromBody]JObject body)
        {
            string localFilePath = "";

            try
            {
                //var service = new YardService();
                //string localFilePath = service.printOutQ(body?.ToString() ?? string.Empty, _hostingEnvironment.ContentRootPath);
                //if (!System.IO.File.Exists(localFilePath))
                //{
                //    return NotFound();
                //}

                //var result = File(System.IO.File.ReadAllBytes(localFilePath), "application/octet-stream");
                //System.IO.File.Delete(localFilePath);
                //return result;
                ////return Ok(result);
                ///

                    var service = new YardService();
                    //var Models = new LPNItemViewModel();
                    //Models = JsonConvert.DeserializeObject<LPNItemViewModel>(body.ToString());
                    localFilePath = service.printOutQ(body?.ToString() ?? string.Empty, _hostingEnvironment.ContentRootPath);
                    if (!System.IO.File.Exists(localFilePath))
                    {
                        return NotFound();
                    }
                    return File(System.IO.File.ReadAllBytes(localFilePath), "application/octet-stream");
                    //return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        #endregion

        #region sendMsg
        [HttpPost("sendMsg")]
        public IActionResult sendMsg([FromBody]JObject body)
        {
            try
            {
                var service = new YardService();
                //   var Models = new sendMsgModel();
                var Models = JsonConvert.DeserializeObject<sendMsgModel>(body.ToString());


                var result = service.LineNotify(Models.msg);

                return Ok(result);


            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region testtime
        [HttpPost("checkStatus_gatecheckin_byTMS")]
        public IActionResult checkStatus_gatecheckin_byTMS([FromBody]JObject json)
        {
            try
            {
                var service = new YardService();
                var xx = service.checkStatus_gatecheckin_byTMS(json.ToString());

                return Ok(xx);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }
        #endregion
    }
}
