using Tral.DTOs;

namespace Trial.Models;


  public record Room
  {   
      public long RoomId {get; set;}
      public string RoomNumber {get; set;}

      public string RoomType {get; set;}

      public string RoomTypeId {get; set;}

      public int RoomCost {get; set;}

      public long MobileNumber {get; set;}

      public string Image {get; set;}

      public string RoomAvailability {get; set;}

      public string StaffId {get; set;}


      
  } 
