using System;

namespace FootballBooking.Api.DTOs
{
    public class CreateBookingDto
    {
        public Guid FootballFieldId { get; set; }
        public DateOnly BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public string? Drink { get; set; }
    }
}