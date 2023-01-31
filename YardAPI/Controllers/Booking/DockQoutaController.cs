using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

using Business.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/dockQouta")]
    [ApiController]
    public class DockQoutaController : ControllerBase
    {
        [HttpPost("list")]
        public IActionResult ListDockQouta([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.ListDockQouta(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("list_warehouse")]
        public IActionResult ListWareHouseForDockQouta([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.ListWareHouseForDockQouta();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("list_dock")]
        public IActionResult ListDockForDockQouta([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.ListDockForDockQouta();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("list_interval")]
        public IActionResult ListIntervalTime([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.ListDockQoutaIntervalTime(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("get")]
        public IActionResult GetDockQouta([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.GetDockQouta(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("save")]
        public IActionResult SaveDockQouta([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                Guid result = service.SaveDockQouta(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("delete")]
        public IActionResult DeleteDockQouta([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.DeleteDockQouta(body?.ToString() ?? string.Empty);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
