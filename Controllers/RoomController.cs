using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Tral.DTOs;
using Tral.Repositories;
using Trial.Models;
using Trial.Repositories;


namespace Trial.Controllers;

[ApiController]
[Route("api/Room")]
public class RoomControllerController : ControllerBase
{
    

    private readonly ILogger<RoomControllerController> _logger;
    private readonly IRoomRepository _room;
    public RoomControllerController(ILogger<RoomControllerController> logger, IRoomRepository room)
    {
        _logger = logger;
        _room = room;
    }


     
    [HttpGet("{room_id}")]

    public async Task<ActionResult<RoomDTO>> GetroomById([FromRoute] int room_id)
    {
         var room = await _room.GetById(room_id);

         if (room is null)
            return NotFound("No room found with given room id");
            
            RoomDTO roomDTO = new RoomDTO();
            roomDTO.RoomId = room.RoomId;

          return Ok(roomDTO);
    }
    



    [HttpDelete("{room_id}")]

    public async Task<ActionResult> Deleteroom([FromRoute] int room_id)
    {
        var existing = await _room.GetById(room_id);
        if (existing is null)
            return NotFound("No room found with given room id"); 

            await _room.Delete(room_id);

            return NoContent();
    }
}
