using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Tral.DTOs;
using Trial.Models;
using Trial.Repositories;

namespace Trial.Controllers;

[ApiController]
[Route("api/Guest")]
public class GuestControllerController : ControllerBase
{
    

    private readonly ILogger<GuestControllerController> _logger;
    private readonly IGuestRepository _guest;
    public GuestControllerController(ILogger<GuestControllerController> logger, IGuestRepository guest)
    {
        _logger = logger;
        _guest = guest;
    }

    [HttpGet]

    public async Task<ActionResult<List<GuestDTO>>> GetAllGuests()
    {
          var guestsList = await _guest.GetList();

          var dtoList = guestsList.Select(x => x.asDto);
          return Ok(guestsList);
    }
     
    [HttpGet("{guest_id}")]

    public async Task<ActionResult<GuestDTO>> GetGuestById([FromRoute] int guest_id)
    {
         var guest = await _guest.GetById(guest_id);

         if (guest is null)
            return NotFound("No guest found with given guest id");

          return Ok(guest.asDto);
    }
    

    [HttpPost]

    public async Task<ActionResult<Guest>> CreateGuest([FromBody] GuestCreateDTO Data)
    {
       var toCreateGuest = new Guest
       {
          GuestName = Data.FullName.Trim(),
          GuestIdProof = Data.GuestIdProof.Trim(),
          Address = Data.Address.Trim(),
          DateOfBirth = Data.DateOfBirth.UtcDateTime,
          MobileNumber = Data.MobileNumber,
          Gender = Data.Gender.Trim(),
          RoomNumber = Data.RoomNumber.Trim(),

       };

       var createGuest = await _guest.Create(toCreateGuest);

       return StatusCode(StatusCodes.Status201Created, createGuest.asDto);
    }

    [HttpPut("{guest_id}")]

    public async Task<ActionResult> UpdateGuest([FromRoute] int guest_id,
    [FromBody] GuestUpdateDTO Data)
    {
        var existing = await _guest.GetById(guest_id);
        if (existing is null)
        return NotFound("No guest found with given guest id");

        var toUpdateGuest = existing with
        {
            GuestIdProof = Data.GuestIdProof?.Trim() ?? existing.GuestIdProof,
            Address = Data.Address?.Trim() ?? existing.Address,
            RoomNumber = Data.RoomNumber?.Trim() ?? existing.RoomNumber,
        };

         var didUpdate = await _guest.Update(toUpdateGuest);

         if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError, "could not update");

            return NoContent();
    }


    [HttpDelete("{guest_id}")]

    public async Task<ActionResult> DeleteGuest([FromRoute] int guest_id)
    {
        var existing = await _guest.GetById(guest_id);
        if (existing is null)
            return NotFound("No guest found with given guest id"); 

            var didDelete = await _guest.Delete(guest_id);

            return NoContent();
    }
}
