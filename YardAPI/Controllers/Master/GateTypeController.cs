using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

using Business.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/gatetype")]
    [ApiController]
    public class GateTypeController : ControllerBase
    {
        [HttpPost("list")]
        public IActionResult ListGateType([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                var result = service.ListGateType(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("get")]
        public IActionResult GetGateType([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                var result = service.GetGateType(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("save")]
        public IActionResult SaveGateType([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                Guid result = service.SaveGateType(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("delete")]
        public IActionResult DeleteGateType([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                service.DeleteGateType(body?.ToString() ?? string.Empty);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
