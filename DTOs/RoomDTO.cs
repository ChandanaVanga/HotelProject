using System.Text.Json.Serialization;

namespace Tral.DTOs;

public record RoomDTO
  {   

    [JsonPropertyName("room_id")]
      public long RoomId {get; set;}


  }
