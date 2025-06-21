using System;

namespace UFF.Service.Helpers
{
    public static class DateTimeHelper
    {
        public static (DateTime StartUtc, DateTime EndUtc) GetUtcRangeForTodayInBrazil()
        {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(
                OperatingSystem.IsWindows() ? "E. South America Standard Time" : "America/Sao_Paulo"
            );

            var nowInBrazil = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
            var todayInBrazil = nowInBrazil.Date;

            var startUtc = TimeZoneInfo.ConvertTimeToUtc(todayInBrazil, timeZone);
            var endUtc = TimeZoneInfo.ConvertTimeToUtc(todayInBrazil.AddDays(1), timeZone);

            return (startUtc, endUtc);
        }
    }
}