using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

using Business.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/warehouseQouta")]
    [ApiController]
    public class WarehouseQoutaController : ControllerBase
    {
        [HttpPost("list")]
        public IActionResult ListWarehouseQouta([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.ListWareHouseQouta(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("list_warehouse")]
        public IActionResult ListWareHouseForWareHouseQouta([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.ListWareHouseForWareHouseQouta();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("get")]
        public IActionResult GetWarehouseQouta([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.GetWareHouseQouta(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("save")]
        public IActionResult SaveWarehouseQouta([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                Guid result = service.SaveWareHouseQouta(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("delete")]
        public IActionResult DeleteWarehouseQouta([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.DeleteWareHouseQouta(body?.ToString() ?? string.Empty);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
