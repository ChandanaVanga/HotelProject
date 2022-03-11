using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Tral.DTOs;


public record GuestDTO
  {   

    [JsonPropertyName("guest_id")]
      public long GuestId {get; set;}

      [JsonPropertyName("full_name")]
      public string FullName {get; set;}

      [JsonPropertyName("guest_id_proof")]

      public string GuestIdProof {get; set;}

      [JsonPropertyName("address")]

      public string Address {get; set;}

      [JsonPropertyName("mobile_number")]

      public long MobileNumber {get; set;}

      [JsonPropertyName("gender")]

      public string Gender {get; set;}


      [JsonPropertyName("room_number")]

      public string RoomNumber {get; set;}
  }   



  public record GuestCreateDTO
  {   

    

      [JsonPropertyName("full_name")]
      [Required]
      [MaxLength(50)]
      public string FullName {get; set;}

      [JsonPropertyName("guest_id_proof")]
      [Required]
      [MaxLength(20)]

      public string GuestIdProof {get; set;}

      [JsonPropertyName("address")]
      [Required]
      [MaxLength(255)]

      public string Address {get; set;}

      [JsonPropertyName("mobile_number")]
      [Required]

      public long MobileNumber {get; set;}

      [JsonPropertyName("gender")]
      [Required]

      public string Gender {get; set;}


      [JsonPropertyName("room_number")]
      [Required]

      public string RoomNumber {get; set;}

      [JsonPropertyName("date_of_birth")]
      [Required]

      public DateTimeOffset DateOfBirth {get; set;}
  }   



  public record GuestUpdateDTO
  {   

      [JsonPropertyName("guest_id_proof")]

      public string GuestIdProof {get; set;}

      [JsonPropertyName("address")]

      public string Address {get; set;}

      

      [JsonPropertyName("room_number")]

      public string RoomNumber {get; set;}
  }   