#if NET5_0_OR_GREATER
using JsonConverter = System.Text.Json.Serialization.JsonConverterAttribute;
using JsonStringEnumConverter = System.Text.Json.Serialization.JsonStringEnumConverter;
#else
using JsonConverter = Newtonsoft.Json.JsonConverterAttribute;
using JsonStringEnumConverter = Newtonsoft.Json.Converters.StringEnumConverter;
#endif

namespace DougMurphy.TargetFrameworks.EndOfLife.Enums {
	/// <summary>The timeframe units to use when forecasting to determine EOL TFMs.</summary>
	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum TimeframeUnit {
		/// <summary>Forecast out in increments of days</summary>
		Day,
		/// <summary>Forecast out in increments of weeks</summary>
		Week,
		/// <summary>Forecast out in increments of months</summary>
		Month,
		/// <summary>Forecast out in increments of years</summary>
		Year,
	}
}
