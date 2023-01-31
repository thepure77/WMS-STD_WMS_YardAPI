using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

using Business.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/gatelane")]
    [ApiController]
    public class GateLaneController : ControllerBase
    {
        [HttpPost("list")]
        public IActionResult ListGateLane([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                var result = service.ListGateLane(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("get")]
        public IActionResult GetGateLane([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                var result = service.GetGateLane(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("save")]
        public IActionResult SaveGateLane([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                Guid result = service.SaveGateLane(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("delete")]
        public IActionResult DeleteGateLane([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                service.DeleteGateLane(body?.ToString() ?? string.Empty);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
