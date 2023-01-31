using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

using Business.Services;
using Business.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/CarInspection")]
    [ApiController]
    public class CarInspectionController : ControllerBase
    {
        #region filter
        [HttpPost("filter")]
        public IActionResult filter([FromBody]JObject body)
        {
            try
            {
                var service = new CarInspectionService();
                var Models = new SearchDetailModel();
                Models = JsonConvert.DeserializeObject<SearchDetailModel>(body.ToString());
                var result = service.filter(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region confirmStatus
        [HttpPost("confirmStatus")]
        public IActionResult confirmStatus([FromBody]JObject body)
        {
            try
            {
                var service = new CarInspectionService();
                var Models = new SearchDetailModel();
                Models = JsonConvert.DeserializeObject<SearchDetailModel>(body.ToString());
                var result = service.confirmStatus(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region rejectStatus
        [HttpPost("rejectStatus")]
        public IActionResult rejectStatus([FromBody]JObject body)
        {
            try
            {
                var service = new CarInspectionService();
                var Models = new SearchDetailModel();
                Models = JsonConvert.DeserializeObject<SearchDetailModel>(body.ToString());
                var result = service.rejectStatus(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion
        
        #region autoAppoinmentNo
        [HttpPost("autoAppoinmentNo")]
        public IActionResult autoPlanGoodIssueNo([FromBody]JObject body)
        {
            try
            {
                var service = new CarInspectionService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoAppoinmentNo(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region autoAppoinmentVehicle_no
        [HttpPost("autoAppoinmentVehicle_no")]
        public IActionResult autoAppoinmentVehicle_no([FromBody]JObject body)
        {
            try
            {
                var service = new CarInspectionService();
                var Models = new ItemListViewModel();
                Models = JsonConvert.DeserializeObject<ItemListViewModel>(body.ToString());
                var result = service.autoAppoinmentVehicle_no(Models);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        #endregion

        #region dropdowndock
        [HttpPost("dropdowndock")]
        public IActionResult dockDoorDropdown([FromBody]JObject body)
        {
            try
            {
                var service = new CarInspectionService();
                var Models = new DockDoorViewModelV2();
                Models = JsonConvert.DeserializeObject<DockDoorViewModelV2>(body.ToString());
                var result = service.dockDoorDropdown(Models);
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
