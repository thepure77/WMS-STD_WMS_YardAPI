using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

using Business.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/checkIn")]
    [ApiController]
    public class CheckInController : ControllerBase
    {
        [HttpPost("list_appointment")]
        public IActionResult ListAppointmentForCheckIn([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.ListAppointmentForCheckIn(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("get_appointment")]
        public IActionResult GetAppointmentForCheckIn([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.GetAppointmentForCheckIn(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("save")]
        public IActionResult SaveCheckIn([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.SaveCheckIn(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("saveGate")]
        public IActionResult SaveGateCheckIn([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                string result = service.SaveGateCheckIn(body?.ToString() ?? string.Empty);
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
