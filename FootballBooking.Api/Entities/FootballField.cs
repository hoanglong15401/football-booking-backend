//using System;
//using System.Collections.Generic;

//namespace FootballBooking.Api.Entities
//{
//    public class FootballField
//    {
//        public Guid Id { get; set; }

//        // Thông tin cơ bản
//        public string Name { get; set; } = null!;
//        public string Location { get; set; } = null!;

//        // Loại sân: 5 người / 7 người
//        public int FieldType { get; set; }
//        // 5 = sân 5, 7 = sân 7

//        // Giá thuê theo giờ
//        public decimal PricePerHour { get; set; }

//        // Trạng thái hoạt động
//        public bool IsActive { get; set; } = true;

//        // Mô tả
//        public string? Description { get; set; }

//        // Giờ hoạt động
//        public TimeSpan OpenTime { get; set; }
//        public TimeSpan CloseTime { get; set; }

//        // Navigation
//        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
//    }
//}

//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace FootballBooking.Api.Entities
//{
//    [Table("footballfields")]
//    public class FootballField
//    {
//        public Guid Id { get; set; }

//        // Thông tin cơ bản
//        public string Name { get; set; } = null!;
//        public string Location { get; set; } = null!;

//        // Loại sân: 5 người / 7 người
//        public int FieldType { get; set; }

//        // Giá thuê theo giờ
//        public decimal PricePerHour { get; set; }

//        // Trạng thái hoạt động
//        public bool IsActive { get; set; } = true;

//        // Mô tả
//        public string? Description { get; set; }

//        // Giờ hoạt động
//        public TimeSpan OpenTime { get; set; }
//        public TimeSpan CloseTime { get; set; }

//        // Navigation
//        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
//    }
//}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FootballBooking.Api.Entities
{
    [Table("footballfields")]
    public class FootballField
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;

        public int FieldType { get; set; }
        public decimal PricePerHour { get; set; }

        public bool IsActive { get; set; } = true;
        public string? Description { get; set; }

        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}