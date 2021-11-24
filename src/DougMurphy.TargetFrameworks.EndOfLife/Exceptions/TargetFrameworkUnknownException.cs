using System;

namespace DougMurphy.TargetFrameworks.EndOfLife.Exceptions {
	/// <summary>
	/// Custom exception type for handling unknown TFMs.
	/// </summary>
	public class TargetFrameworkUnknownException : Exception {
		public TargetFrameworkUnknownException(string message) : base(message) {
		}
	}
}
