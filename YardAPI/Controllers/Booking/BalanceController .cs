using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

using Business.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/balance")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        [HttpPost("list_appointment")]
        public IActionResult ListAppointmentItem([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.ListAppointmentItem(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("list_GateappointmentSummary")]
        public IActionResult list_GateappointmentSummary([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.ListGateAppointmentItemSummary(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("list_Gateappointment")]
        public IActionResult ListGateAppointmentItem([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.ListGateAppointmentItem(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("list_Gateappointmentout")]
        public IActionResult list_Gateappointmentout([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.ListGateAppointmentItemout(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
