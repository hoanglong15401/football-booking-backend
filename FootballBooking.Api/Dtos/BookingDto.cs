using System;

namespace FootballBooking.Api.DTOs
{
    public class BookingDto
    {
        public Guid Id { get; set; }

        public string? CustomerName { get; set; }

        public string? FieldName { get; set; }

        public string? FieldAddress { get; set; }   // map Location

        public int FieldType { get; set; }          // map FieldType

        public DateOnly BookingDate { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public decimal TotalPrice { get; set; }

        public string? Drink { get; set; }
    }
}