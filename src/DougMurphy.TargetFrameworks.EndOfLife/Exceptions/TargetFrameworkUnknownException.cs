using System;

namespace DougMurphy.TargetFrameworks.EndOfLife.Exceptions {
	/// <summary>
	/// Custom exception type for handling unknown TFMs.
	/// </summary>
	public class TargetFrameworkUnknownException : Exception {
		/// <summary>
		/// Construct a new instance of <see cref="TargetFrameworkUnknownException"/> with a specified message.
		/// </summary>
		/// <param name="message">The message of the exception.</param>
		public TargetFrameworkUnknownException(string message) : base(message) {
		}
	}
}
