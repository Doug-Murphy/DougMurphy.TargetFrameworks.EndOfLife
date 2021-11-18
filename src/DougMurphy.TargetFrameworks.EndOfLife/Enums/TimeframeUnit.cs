#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
#endif

namespace DougMurphy.TargetFrameworks.EndOfLife.Enums {
#if NET5_0_OR_GREATER
	[JsonConverter(typeof(JsonStringEnumConverter))]
#else
	[JsonConverter(typeof(StringEnumConverter))]
#endif
	public enum TimeframeUnit {
		Day,
		Week,
		Month,
		Year,
	}
}
