using System.Collections.Generic;

namespace DougMurphy.TargetFrameworks.EndOfLife.Models {
	/// <summary>
	/// Holds the response from the EOL check to.
	/// </summary>
	/// <param name="EndOfLifeTargetFrameworks">The list of TFMs that were determined to be EOL.</param>
	public record TargetFrameworkCheckResponse(IReadOnlyList<string> EndOfLifeTargetFrameworks) {
		/// <summary>
		/// The list of TFMs that were determined to be EOL.
		/// </summary>
		public IReadOnlyList<string> EndOfLifeTargetFrameworks { get; } = EndOfLifeTargetFrameworks;
	}
}
