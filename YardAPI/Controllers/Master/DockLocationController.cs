using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

using Business.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/docklocation")]
    [ApiController]
    public class DockLocationController : ControllerBase
    {
        [HttpPost("list")]
        public IActionResult ListDockLocation([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                var result = service.ListDockLocation(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("get")]
        public IActionResult GetDockLocation([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                var result = service.GetDockLocation(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("save")]
        public IActionResult SaveDockLocation([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                Guid result = service.SaveDockLocation(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("delete")]
        public IActionResult DeleteDockLocation([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                service.DeleteDockLocation(body?.ToString() ?? string.Empty);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
