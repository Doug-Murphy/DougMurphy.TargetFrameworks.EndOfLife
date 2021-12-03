using System;
using System.Collections.Generic;

namespace DougMurphy.TargetFrameworks.EndOfLife.Models {
	/// <summary>Holds the response from the EOL check.</summary>
	/// <param name="EndOfLifeTargetFrameworks">The list of TFMs that were determined to be EOL with their respective EOL date.</param>
	public record TargetFrameworkCheckResponse(IReadOnlyDictionary<string, DateTime> EndOfLifeTargetFrameworks) {
		/// <summary>The list of TFMs that were determined to be EOL.</summary>
		public IReadOnlyDictionary<string, DateTime> EndOfLifeTargetFrameworks { get; } = EndOfLifeTargetFrameworks;
	}
}
