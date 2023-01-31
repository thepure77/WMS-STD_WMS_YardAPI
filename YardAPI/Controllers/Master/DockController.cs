using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

using Business.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/dock")]
    [ApiController]
    public class DockController : ControllerBase
    {
        [HttpPost("list")]
        public IActionResult ListDock([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                var result = service.ListDock(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("get")]
        public IActionResult GetDock([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                var result = service.GetDock(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("save")]
        public IActionResult SaveDock([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                Guid result = service.SaveDock(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("delete")]
        public IActionResult DeleteDock([FromBody]JObject body)
        {
            try
            {
                MasterService service = new MasterService();
                service.DeleteDock(body?.ToString() ?? string.Empty);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
