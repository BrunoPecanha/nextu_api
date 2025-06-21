using System;

public static class DateTimeExtensions
{
    /// <summary>
    /// Converte DateTime UTC para horário de Brasília
    /// </summary>
    public static DateTime ToBrasiliaTime(this DateTime utcDateTime)
    {
        var timeZone = TimeZoneInfo.FindSystemTimeZoneById(
            IsWindows() ? "E. South America Standard Time" : "America/Sao_Paulo"
        );

        return TimeZoneInfo.ConvertTimeFromUtc(
            utcDateTime.Kind == DateTimeKind.Utc ? utcDateTime : utcDateTime.ToUniversalTime(),
            timeZone
        );
    }

    /// <summary>
    /// Converte DateTime no horário de Brasília para UTC
    /// </summary>
    public static DateTime FromBrasiliaToUtc(this DateTime brasiliaDateTime)
    {
        var timeZone = TimeZoneInfo.FindSystemTimeZoneById(
            IsWindows() ? "E. South America Standard Time" : "America/Sao_Paulo"
        );

        return TimeZoneInfo.ConvertTimeToUtc(
            DateTime.SpecifyKind(brasiliaDateTime, DateTimeKind.Unspecified),
            timeZone
        );
    }

    private static bool IsWindows()
    {
        return System.Runtime.InteropServices.RuntimeInformation
            .IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows);
    }
}
