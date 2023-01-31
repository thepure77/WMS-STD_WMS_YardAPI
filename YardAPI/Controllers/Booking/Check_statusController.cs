using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

using Business.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/Check_status")]
    [ApiController]
    public class Check_statusController : ControllerBase
    {
        #region Before_GateCheckIn
        [HttpPost("Before_GateCheckIn")]
        public IActionResult Before_GateCheckIn([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.Before_GateCheckIn(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region After_GateCheckIn
        [HttpPost("After_GateCheckIn")]
        public IActionResult After_GateCheckIn([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.After_GateCheckIn(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region After_GetQueue
        [HttpPost("After_GetQueue")]
        public IActionResult After_GetQueue([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.After_GetQueue(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region After_CheckOut
        [HttpPost("After_CheckOut")]
        public IActionResult After_CheckOut([FromBody]JObject body)
        {
            try
            {
                YardService service = new YardService();
                var result = service.After_CheckOut(body?.ToString() ?? string.Empty);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

    }
}
