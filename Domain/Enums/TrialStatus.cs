using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace Domain.Enums;

[JsonConverter(typeof(StringEnumConverter))]
public enum TrialStatus
{
    [EnumMember(Value = "Not Started")]
    NotStarted,
    [EnumMember(Value = "Ongoing")]
    Ongoing,
    [EnumMember(Value = "Completed")]
    Completed
}
