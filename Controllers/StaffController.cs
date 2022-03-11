using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Tral.DTOs;
using Trial.Models;
using Trial.Repositories;

namespace Trial.Controllers;

[ApiController]
[Route("api/Staff")]
public class StaffController : ControllerBase
{
    

    private readonly ILogger<StaffController> _logger;
    private readonly IStaffRepository _Staff;
    public StaffController(ILogger<StaffController> logger, IStaffRepository Staff)
    {
        _logger = logger;
        _Staff = Staff;
    }

    [HttpGet]

    public async Task<ActionResult<List<Staff>>> GetAllStaffs()
    {
          var StaffsList = await _Staff.GetList();

        //   var dtoList = StaffsList.Select(x => x);
          return Ok(StaffsList);
    }
     
    [HttpGet("{Staff_id}")]

    public async Task<ActionResult<Staff>> GetStaffById([FromRoute] int Staff_id)
    {
         var Staff = await _Staff.GetById(Staff_id);

         if (Staff is null)
            return NotFound("No Staff found with given Staff id");

          return Ok(Staff);
    }
    

    [HttpPost]

    public async Task<ActionResult<Staff>> CreateStaff([FromBody] Staff Data)
    {
       var toCreateStaff = new Staff
       {
          StaffId = Data.StaffId,
          StaffName = Data.StaffName,
          StaffType = Data.StaffType,
          ClockIn = Data.ClockIn,
          ClockOut = Data.ClockOut,
          Shift = Data.Shift,
          

       };

       var createStaff = await _Staff.Create(toCreateStaff);

       return StatusCode(StatusCodes.Status201Created, createStaff);
    }

    [HttpPut("{Staff_id}")]

    public async Task<ActionResult> UpdateStaff([FromRoute] int Staff_id,
    [FromBody] Staff Data)
    {
        var existing = await _Staff.GetById(Staff_id);
        if (existing is null)
        return NotFound("No Staff found with given Staff id");

        var toUpdateStaff = existing with
        {
            StaffId = Data.StaffId,
            StaffName = Data.StaffName?.Trim() ?? existing.StaffName,
            StaffType = Data.StaffType?.Trim() ?? existing.StaffType,
            Shift = Data.Shift,
        };

         var didUpdate = await _Staff.Update(toUpdateStaff);

         if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "could not update");

            return NoContent();
    }


    [HttpDelete("{Staff_id}")]

    public async Task<ActionResult> DeleteStaff([FromRoute] int Staff_id)
    {
        // var existing = await _Staff.GetById(Staff_id);
        // if (existing is null)
        //     return NotFound("No Staff found with given Staff id"); 

            var didDelete = await _Staff.Delete(Staff_id);

            return NoContent();
    }
}
