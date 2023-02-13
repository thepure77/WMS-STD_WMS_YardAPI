using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

using Business.Services;
using Business.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/qcall")]
    [ApiController]
    public class QcallController : ControllerBase
    {
        [HttpPost("get_qcall")]
        public IActionResult Get_qcall()
        {
            try
            {
                YardService service = new YardService();
                var result = service.Qcall();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("upd_qcall_status")]
        public IActionResult upd_qcall_status([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var Models = new QueueModel();
                Models = JsonConvert.DeserializeObject<QueueModel>(body.ToString());
                var result = service.Update_Qcall_Status(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
