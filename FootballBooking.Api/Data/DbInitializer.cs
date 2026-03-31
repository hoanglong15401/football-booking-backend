using FootballBooking.Api.Entities;
using System;
using System.Collections.Generic;

namespace FootballBooking.Api.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            if (context.FootballFields.Any())
                return;

            var fields = new List<FootballField>
            {
                new FootballField
                {
                    Id = Guid.NewGuid(),
                    Name = "Sân Phú Thọ",
                    Location = "Quận 11, TP.HCM",
                    FieldType = 5,
                    PricePerHour = 300000,
                    IsActive = true,
                    Description = "Sân 5 người - mặt cỏ nhân tạo",
                    OpenTime = new TimeSpan(6, 0, 0),
                    CloseTime = new TimeSpan(22, 0, 0)
                },
                new FootballField
                {
                    Id = Guid.NewGuid(),
                    Name = "Sân Tao Đàn",
                    Location = "Quận 1, TP.HCM",
                    FieldType = 7,
                    PricePerHour = 400000,
                    IsActive = true,
                    Description = "Sân 7 người - trung tâm thành phố",
                    OpenTime = new TimeSpan(5, 30, 0),
                    CloseTime = new TimeSpan(23, 0, 0)
                }
            };

            context.FootballFields.AddRange(fields);
            context.SaveChanges();
        }
    }
}
