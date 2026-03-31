//using System;

//namespace FootballBooking.Api.Entities
//{
//    public class Booking
//    {
//        public Guid Id { get; set; }

//        public Guid UserId { get; set; }
//        public Guid FootballFieldId { get; set; }

//        public DateOnly BookingDate { get; set; }
//        public TimeSpan StartTime { get; set; }
//        public TimeSpan EndTime { get; set; }

//        public decimal TotalPrice { get; set; }

//        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

//        public string? Drink { get; set; }

//        public User User { get; set; } = null!;
//        public FootballField FootballField { get; set; } = null!;
//    }
//}
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballBooking.Api.Entities
{
    [Table("bookings")]
    public class Booking
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public Guid FootballFieldId { get; set; }

        public DateOnly BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? Drink { get; set; }

        public User User { get; set; } = null!;
        public FootballField FootballField { get; set; } = null!;
    }
}