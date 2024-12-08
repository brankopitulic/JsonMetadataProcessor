using Domain.Enums;
using Newtonsoft.Json.Linq;

public static class MappingHelpers
{
    public static string ParseString(JObject source, string key)
    {
        return source[key]?.ToString() ?? string.Empty;
    }

    public static DateTime ParseDateTime(JObject source, string key)
    {
        var value = source[key]?.ToString();
        return string.IsNullOrEmpty(value) ? default : DateTime.Parse(value);
    }

    public static DateTime? ParseNullableDateTime(JObject source, string key)
    {
        var value = source[key]?.ToString();
        return string.IsNullOrEmpty(value) ? (DateTime?)null : DateTime.Parse(value);
    }

    public static int ParseInt(JObject source, string key)
    {
        var value = source[key]?.ToString();
        return string.IsNullOrEmpty(value) ? 0 : int.Parse(value);
    }

    public static TEnum ParseEnum<TEnum>(JObject source, string key) where TEnum : struct
    {
        var value = source[key]?.ToString();
        return Enum.TryParse(value, true, out TEnum result) ? result : default;
    }

    public static TrialStatus ConvertToTrialStatus(string status)
    {
        return Enum.TryParse<TrialStatus>(status, true, out var parsedStatus) ? parsedStatus : TrialStatus.NotStarted;
    }
}
