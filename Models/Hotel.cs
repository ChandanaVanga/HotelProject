using Tral.DTOs;

namespace Trial.Models;


  public record Guest
  {   
      public long GuestId {get; set;}
      public string GuestName {get; set;}

      public string GuestIdProof {get; set;}

      public string Address {get; set;}

      public DateTimeOffset DateOfBirth {get; set;}

      public long MobileNumber {get; set;}

      public string Gender {get; set;}

      public string RoomNumber {get; set;}


      public GuestDTO asDto
      {
          get{
              return new GuestDTO {
                  GuestId = GuestId,
                   FullName = GuestName,
                  GuestIdProof = GuestIdProof,
                  Address = Address,
                  MobileNumber = MobileNumber,
                  Gender = Gender,
                  RoomNumber = RoomNumber

              };
          }
      }
  } 
