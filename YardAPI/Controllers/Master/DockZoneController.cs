using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

using Business.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/dockzone")]
    [ApiController]
    public class DockZoneController : ControllerBase
    {
        [HttpPost("list")]
        public IActionResult ListDockZone([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                var result = service.ListDockZone(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("get")]
        public IActionResult GetDockZone([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                var result = service.GetDockZone(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("save")]
        public IActionResult SaveDockZone([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                Guid result = service.SaveDockZone(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("delete")]
        public IActionResult DeleteDockZone([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                service.DeleteDockZone(body?.ToString() ?? string.Empty);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
