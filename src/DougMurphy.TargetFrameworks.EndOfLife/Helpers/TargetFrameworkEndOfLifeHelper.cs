using DougMurphy.TargetFrameworks.EndOfLife.Enums;
using DougMurphy.TargetFrameworks.EndOfLife.Exceptions;
using DougMurphy.TargetFrameworks.EndOfLife.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DougMurphy.TargetFrameworks.EndOfLife.Helpers {
	/// <summary>Contains methods to help in determining whether or not a TFM is EOL.</summary>
	public static class TargetFrameworkEndOfLifeHelper {
		//TFM list found on https://docs.microsoft.com/en-us/dotnet/standard/frameworks
		private static readonly Dictionary<string, DateTime?> TargetFrameworksWithEndOfLifeDate = new() {
			//.NET Standard does not have an EOL
			{"netstandard1.0", null},
			{"netstandard1.1", null},
			{"netstandard1.2", null},
			{"netstandard1.3", null},
			{"netstandard1.4", null},
			{"netstandard1.5", null},
			{"netstandard1.6", null},
			{"netstandard2.0", null},
			{"netstandard2.1", null},

			//EOL for .NET Framework versions found on https://docs.microsoft.com/en-us/lifecycle/products/microsoft-net-framework
			{"net11", new DateTime(2011, 07, 12)},
			{"v1.1", new DateTime(2011, 07, 12)},
			{"net20", new DateTime(2011, 07, 12)},
			{"v2.0", new DateTime(2011, 07, 12)},
			{"net30", new DateTime(2011, 07, 12)}, //net30 is not shown on Microsoft's exhaustive list of TFMs, so this TFM is assumed
			{"v3.0", new DateTime(2011, 07, 12)},
			{"net35", new DateTime(2029, 01, 09)},
			{"v3.5", new DateTime(2029, 01, 09)},
			{"net40", new DateTime(2016, 01, 12)},
			{"v4.0", new DateTime(2016, 01, 12)},
			{"net403", new DateTime(2016, 01, 12)}, //assumption to match 4.0 and 4.5
			{"v4.0.3", new DateTime(2016, 01, 12)}, //assumption to match 4.0 and 4.5
			{"net45", new DateTime(2016, 01, 12)},
			{"v4.5", new DateTime(2016, 01, 12)},
			{"net451", new DateTime(2016, 01, 12)},
			{"v4.5.1", new DateTime(2016, 01, 12)},
			{"net452", new DateTime(2022, 04, 26)},
			{"v4.5.2", new DateTime(2022, 04, 26)},
			{"net46", new DateTime(2022, 04, 26)},
			{"v4.6", new DateTime(2022, 04, 26)},
			{"net461", new DateTime(2022, 04, 26)},
			{"v4.6.1", new DateTime(2022, 04, 26)},
			{"net462", null},
			{"v4.6.2", null},
			{"net47", null},
			{"v4.7", null},
			{"net471", null},
			{"v4.7.1", null},
			{"net472", null},
			{"v4.7.2", null},
			{"net48", null},
			{"v4.8", null},

			//EOL for .NET Core and .NET 5+ found on https://docs.microsoft.com/en-us/lifecycle/products/microsoft-net-and-net-core
			{"netcoreapp1.0", new DateTime(2019, 06, 27)},
			{"netcoreapp1.1", new DateTime(2019, 06, 27)},
			{"netcoreapp2.0", new DateTime(2018, 10, 01)},
			{"netcoreapp2.1", new DateTime(2021, 08, 21)},
			{"netcoreapp2.2", new DateTime(2019, 12, 23)},
			{"netcoreapp3.0", new DateTime(2020, 03, 03)},
			{"netcoreapp3.1", new DateTime(2022, 12, 03)},

			//.NET 5 TFMs taken from https://github.com/dotnet/designs/blob/main/accepted/2020/net5/net5.md
			{"net5.0", new DateTime(2022, 05, 08)},
			{"net5.0-android", new DateTime(2022, 05, 08)},
			{"net5.0-ios", new DateTime(2022, 05, 08)},
			{"net5.0-macos", new DateTime(2022, 05, 08)},
			{"net5.0-tvos", new DateTime(2022, 05, 08)},
			{"net5.0-watchos", new DateTime(2022, 05, 08)},
			{"net5.0-windows", new DateTime(2022, 05, 08)},

			//.NET 6 TFMs taken from https://github.com/dotnet/designs/blob/main/accepted/2021/net6.0-tfms/net6.0-tfms.md
			{"net6.0", new DateTime(2024, 11, 08)},
			{"net6.0-android", new DateTime(2024, 11, 08)},
			{"net6.0-ios", new DateTime(2024, 11, 08)},
			{"net6.0-macos", new DateTime(2024, 11, 08)},
			{"net6.0-maccatalyst", new DateTime(2024, 11, 08)},
			{"net6.0-tizen", new DateTime(2024, 11, 08)},
			{"net6.0-tvos", new DateTime(2024, 11, 08)},
			{"net6.0-windows", new DateTime(2024, 11, 08)},
		};

		/// <summary>Get all TFMs with their respective EOL dates that will be EOL by a forecasted date from current date.</summary>
		/// <param name="timeframeUnit">The date unit to forecast.</param>
		/// <param name="timeframeAmount">The amount of the date unit to forecast.</param>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when the <see cref="TimeframeUnit" /> is an invalid option.</exception>
		public static TargetFrameworkCheckResponse GetAllEndOfLifeTargetFrameworkMonikers(TimeframeUnit timeframeUnit, byte timeframeAmount) {
			DateTime forecastedDateToCompare = GetForecastedDateTime(timeframeUnit, timeframeAmount);

			return GetAllEndOfLifeTargetFrameworkMonikers(forecastedDateToCompare);
		}

		/// <summary>Get all TFMs with their respective EOL dates that are currently EOL.</summary>
		public static TargetFrameworkCheckResponse GetAllEndOfLifeTargetFrameworkMonikers() {
			return GetAllEndOfLifeTargetFrameworkMonikers(DateTime.UtcNow);
		}

		/// <summary>Get all TFMs with their respective EOL dates that have an EOL date before the specified date.</summary>
		/// <param name="eolBoundaryDate">The date to check the TFM's EOL date against.</param>
		public static TargetFrameworkCheckResponse GetAllEndOfLifeTargetFrameworkMonikers(DateTime eolBoundaryDate) {
			return new TargetFrameworkCheckResponse(TargetFrameworksWithEndOfLifeDate.Where(tfm => tfm.Value <= eolBoundaryDate)
			                                                                         .OrderBy(tfm => tfm.Key)
			                                                                         .ToDictionary(tfm => tfm.Key, tfm => tfm.Value!.Value));
		}

		/// <summary>Given a singular or plural TFM, return the TFM(s) with their respective EOL date(s) that are currently EOL, if any.</summary>
		/// <param name="rawTfm">The TFM specifier that you want to check.</param>
		/// <exception cref="ArgumentNullException">Thrown when the TFM parameter is null or whitespace.</exception>
		/// <exception cref="ArgumentException">Thrown when the TFM parameter is invalid by containing only a semicolon.</exception>
		/// <exception cref="TargetFrameworkUnknownException">Thrown when the TFM is not currently registered by the application.</exception>
		public static TargetFrameworkCheckResponse CheckTargetFrameworkForEndOfLife(string rawTfm) {
			return CheckTargetFrameworkForEndOfLife(rawTfm, DateTime.UtcNow);
		}

		/// <summary>Given a singular or plural TFM, return the TFM(s) with their respective EOL date(s) that will be EOL by a forecasted date from the current date, if any.</summary>
		/// <param name="rawTfm">The TFM specifier that you want to check.</param>
		/// <param name="timeframeUnit">The date unit to forecast.</param>
		/// <param name="timeframeAmount">The amount of the date unit to forecast.</param>
		/// <exception cref="ArgumentNullException">Thrown when the TFM parameter is null or whitespace.</exception>
		/// <exception cref="ArgumentException">Thrown when the TFM parameter is invalid by containing only a semicolon.</exception>
		/// <exception cref="ArgumentOutOfRangeException">Thrown when the <see cref="TimeframeUnit" /> is an invalid option.</exception>
		/// <exception cref="TargetFrameworkUnknownException">Thrown when the TFM is not currently registered by the application.</exception>
		public static TargetFrameworkCheckResponse CheckTargetFrameworkForEndOfLife(string rawTfm, TimeframeUnit timeframeUnit, byte timeframeAmount) {
			DateTime forecastedDate = GetForecastedDateTime(timeframeUnit, timeframeAmount);

			return CheckTargetFrameworkForEndOfLife(rawTfm, forecastedDate);
		}

		/// <summary>Given a singular or plural TFM, return the TFM(s) with their respective EOL date(s) that will be EOL by the specified date, if any.</summary>
		/// <param name="rawTfm">The TFM specifier that you want to check.</param>
		/// <param name="eolBoundaryDate">The date to check the TFM's EOL date against.</param>
		/// <exception cref="ArgumentNullException">Thrown when the TFM parameter is null or whitespace.</exception>
		/// <exception cref="ArgumentException">Thrown when the TFM parameter is invalid by containing only a semicolon.</exception>
		/// <exception cref="TargetFrameworkUnknownException">Thrown when the TFM is not currently registered by the application.</exception>
		public static TargetFrameworkCheckResponse CheckTargetFrameworkForEndOfLife(string rawTfm, DateTime eolBoundaryDate) {
			IEnumerable<string> tfms = ParseRawTfm(rawTfm);

			Dictionary<string, DateTime> endOfLifeTargetFrameworksWithEolDate = new();
			foreach (string tfm in tfms) {
				if (IsSingularTfmEol(tfm, eolBoundaryDate, out DateTime? eolDate)) {
					endOfLifeTargetFrameworksWithEolDate.Add(tfm, eolDate!.Value);
				}
			}

			return new TargetFrameworkCheckResponse(endOfLifeTargetFrameworksWithEolDate);
		}

		/// <summary>Determine if a singular Target Framework Moniker is currently end of life.</summary>
		/// <param name="tfm">The singular Target Framework Moniker to check for (eg. net45, netcoreapp2.1)</param>
		/// <param name="eolBoundaryDate">The date to check the TFM's EOL date against.</param>
		/// <param name="eolDate">The date</param>
		/// <exception cref="TargetFrameworkUnknownException">Thrown when the TFM is not currently registered by the application.</exception>
		private static bool IsSingularTfmEol(string tfm, DateTime eolBoundaryDate, out DateTime? eolDate) {
			if (!TargetFrameworksWithEndOfLifeDate.ContainsKey(tfm)) {
				throw new TargetFrameworkUnknownException($"I do not have TFM '{tfm}' in my registry. If this is a valid TFM, please log an issue on GitHub at https://github.com/Doug-Murphy/DougMurphy.TargetFrameworks.EndOfLife/issues/new");
			}

			if (TargetFrameworksWithEndOfLifeDate[tfm].HasValue && TargetFrameworksWithEndOfLifeDate[tfm].Value <= eolBoundaryDate) {
				eolDate = TargetFrameworksWithEndOfLifeDate[tfm]!.Value;
				return true;
			}

			eolDate = null;
			return false;
		}

		private static IEnumerable<string> ParseRawTfm(string rawTfm) {
			if (string.IsNullOrWhiteSpace(rawTfm)) {
				throw new ArgumentNullException(nameof(rawTfm));
			}

			string[] tfms = rawTfm.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries);

			if (tfms.Length == 0) {
				throw new ArgumentException($"No Target Framework Monikers could be found in string {rawTfm}");
			}

			return tfms;
		}

		private static DateTime GetForecastedDateTime(TimeframeUnit timeframeUnit, byte timeframeAmount) {
			return timeframeUnit switch {
				TimeframeUnit.Day => DateTime.UtcNow.AddDays(timeframeAmount),
				TimeframeUnit.Week => DateTime.UtcNow.AddDays(7 * timeframeAmount),
				TimeframeUnit.Month => DateTime.UtcNow.AddMonths(timeframeAmount),
				TimeframeUnit.Year => DateTime.UtcNow.AddYears(timeframeAmount),
				_ => throw new ArgumentOutOfRangeException(nameof(timeframeUnit), timeframeUnit, null),
			};
		}
	}
}
