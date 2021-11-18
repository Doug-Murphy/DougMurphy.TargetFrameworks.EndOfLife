using System;

namespace DougMurphy.TargetFrameworks.EndOfLife.Exceptions {
	public class TargetFrameworkUnknownException : Exception {
		public TargetFrameworkUnknownException(string message) : base(message) {
		}
	}
}
