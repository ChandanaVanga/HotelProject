using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Tral.DTOs;


public record ScheduleDTO
  {   

    [JsonPropertyName("room_id")]
      public long RoomtId {get; set;}

      [JsonPropertyName("guest_id")]
      public long GuestId {get; set;}

      [JsonPropertyName("room_type_id")]

      public string RoomTypeId {get; set;}

      [JsonPropertyName("booking_date")]

      public DateOnly BookingDate {get; set;}

      [JsonPropertyName("incoming_time")]

      public DateTime IncomingTime {get; set;}

      [JsonPropertyName("staying_days")]

      public int StayingDays {get; set;}


      [JsonPropertyName("vecant_time")]

      public DateTime VecantTime {get; set;}
  }   



  public record ScheduleCreateDTO
  {   

    [JsonPropertyName("room_id")]
      public long RoomtId {get; set;}

      [JsonPropertyName("guest_id")]
      public long GuestId {get; set;}


      [JsonPropertyName("booking_date")]

      public DateOnly BookingDate {get; set;}



      [JsonPropertyName("vecant_time")]

      public DateTime VecantTime {get; set;}

  }   



  public record ScheduletUpdateDTO
  {   

       [JsonPropertyName("room_id")]
      public long RoomtId {get; set;}

      [JsonPropertyName("guest_id")]
      public long GuestId {get; set;}

      [JsonPropertyName("room_type_id")]

      public string RoomTypeId {get; set;}
  }   