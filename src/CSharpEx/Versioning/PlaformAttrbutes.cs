#if !NET5_0_OR_GREATER
using System;

namespace CSharpEx.Versioning
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
	public sealed class SupportedOSPlatformAttribute : Attribute
	{
		public SupportedOSPlatformAttribute(string platformName) { }
	}
}
#endif