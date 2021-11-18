using System.Collections.Generic;

namespace DougMurphy.TargetFrameworks.EndOfLife.Models {
	public record TargetFrameworkCheckResponse(IReadOnlyList<string> EndOfLifeTargetFrameworks) {
		public IReadOnlyList<string> EndOfLifeTargetFrameworks { get; } = EndOfLifeTargetFrameworks;
	}
}
