using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Trial.Models;


using Trial.Repositories;

namespace Trial.Controllers;

[ApiController]
[Route("api/Schedule")]
public class ScheduleControllerController : ControllerBase
{


    private readonly ILogger<ScheduleControllerController> _logger;
    private readonly IGuestRepository _guest;
    private readonly IScheduleRepository _Schedule;
    
    public ScheduleControllerController(ILogger<ScheduleControllerController> logger, IScheduleRepository Schedule, IGuestRepository guest)
    {
        _logger = logger;
        _Schedule = Schedule;
        _guest = guest;
    }

    // [HttpGet]

    // public async Task<ActionResult<List<Schedule>>> GetAllSchedules()
    // {
    //       var SchedulesList = await _Schedule.GetList();

    //       var dtoList = SchedulesList.Select(x => x);
    //       return Ok(SchedulesList);
    // }

    [HttpGet("{Room_id}")]

    public async Task<ActionResult<Schedule>> GetScheduleById([FromRoute] int Room_id)
    {
         var Schedule = await _Schedule.GetById(Room_id);

         if (Schedule is null)
            return NotFound("No Schedule found with given Schedule id");

            var task = Schedule;

            task.Guests = await _guest.GetList(Room_id);
       
        return Ok(task);

        
    }


    [HttpPost]

    public async Task<ActionResult<Schedule>> CreateSchedule([FromBody] Schedule Data)
    {
       var toCreateSchedule = new Schedule
       {
          RoomId = Data.RoomId,
          GuestId = Data.GuestId,
          RoomTypeId = Data.RoomTypeId,
          BookingDate = Data.BookingDate,
          StayingDays = Data.StayingDays,
          MobileNumber = Data.MobileNumber,
          VecantTime = Data.VecantTime,

       };

       var createdSchedule = await _Schedule.Create(toCreateSchedule.RoomId);

       return StatusCode(StatusCodes.Status201Created, createdSchedule);
    }

    [HttpPut("{Room_id}")]

    public async Task<ActionResult> UpdateSchedule([FromRoute] int Room_id,
    [FromBody] Schedule Data)
    {
        var existing = await _Schedule.GetById(Room_id);
        if (existing is null)
        return NotFound("No Schedule found with given Schedule id");

        var toUpdateSchedule = existing with
        {            
            GuestId = Data.GuestId,
            StayingDays = Data.StayingDays,
            VecantTime = Data.VecantTime,
        };

         var didUpdate = await _Schedule.Update(toUpdateSchedule, Room_id);

         if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "could not update");

            return NoContent();
    }


    [HttpDelete("{Room_id}")]

    public async Task<ActionResult> DeleteSchedule([FromRoute] int Room_id)
    {
        var existing = await _Schedule.GetById(Room_id);
        if (existing is null)
            return NotFound("No Room found with given Room Id"); 

            var didDelete = await _Schedule.Delete(Room_id);

            return NoContent();
    }
}