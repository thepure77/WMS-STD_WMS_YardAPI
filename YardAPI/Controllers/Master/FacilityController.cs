using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

using Business.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/facility")]
    [ApiController]
    public class FacilityController : ControllerBase
    {
        [HttpPost("list")]
        public IActionResult ListFacility([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                var result = service.ListFacility(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("get")]
        public IActionResult GetFacility([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                var result = service.GetFacility(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("save")]
        public IActionResult SaveFacility([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                Guid result = service.SaveFacility(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("delete")]
        public IActionResult DeleteFacility([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                service.DeleteFacility(body?.ToString() ?? string.Empty);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
