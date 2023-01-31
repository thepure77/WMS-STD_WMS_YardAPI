using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

using Business.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/checkOut")]
    [ApiController]
    public class CheckOutController : ControllerBase
    {
        [HttpPost("list_appointment")]
        public IActionResult ListAppointmentForCheckOut([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.ListAppointmentForCheckOut(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("get_appointment")]
        public IActionResult GetAppointmentForCheckOut([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.GetAppointmentForCheckOut(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("save")]
        public IActionResult SaveCheckOut([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.SaveCheckOut(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("SaveCheckOut_test")]
        public IActionResult SaveCheckOut_test([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.SaveCheckOut_test(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        [HttpPost("saveGateOut")]
        public IActionResult saveGateOut([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.saveGateOut(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("delete")]
        public IActionResult DeleteCheckIn([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.DeleteCheckIn(body?.ToString() ?? string.Empty);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
