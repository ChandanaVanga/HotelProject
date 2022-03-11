using Tral.DTOs;

namespace Trial.Models;


public record Schedule
{
    public int RoomId { get; set; }
    public int GuestId { get; set; }

    public string RoomTypeId { get; set; }

    public DateTime BookingDate { get; set; }

    public int StayingDays { get; set; }

    public int MobileNumber { get; set; }

    public DateTime VecantTime { get; set; }

    public List<GuestDTO> Guests { get; set; }

}
