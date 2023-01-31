using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

using Business.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/facilitytype")]
    [ApiController]
    public class FacilityTypeController : ControllerBase
    {
        [HttpPost("list")]
        public IActionResult ListFacilityType([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                var result = service.ListFacilityType(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("get")]
        public IActionResult GetFacilityType([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                var result = service.GetFacilityType(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("save")]
        public IActionResult SaveFacilityType([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                Guid result = service.SaveFacilityType(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("delete")]
        public IActionResult DeleteFacilityType([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                service.DeleteFacilityType(body?.ToString() ?? string.Empty);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
