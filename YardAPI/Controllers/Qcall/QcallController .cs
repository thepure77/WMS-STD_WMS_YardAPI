using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

using Business.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/qcall")]
    [ApiController]
    public class QcallController : ControllerBase
    {
        [HttpPost("get_qcall")]
        public IActionResult ListAppointmentItem()
        {
            try
            {
                YardService service = new YardService();
                var result = service.Qcall();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
