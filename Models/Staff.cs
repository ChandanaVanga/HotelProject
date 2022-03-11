using Tral.DTOs;

namespace Trial.Models;


  public record Staff
  {   
      public long StaffId {get; set;}
      public string StaffName {get; set;}

      public string StaffType {get; set;}

      public DateTimeOffset ClockIn {get; set;}

      public DateTimeOffset ClockOut {get; set;}

      public long Shift {get; set;}

      
  } 
