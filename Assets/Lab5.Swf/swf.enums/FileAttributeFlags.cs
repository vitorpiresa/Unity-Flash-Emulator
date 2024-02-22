using System;

namespace Lab5.Swf.Enums
{
	[Flags]
	public enum FileAttributeFlags
	{
		UseDirectBlit = 0x40,
		UseGPU = 0x20,
		HasMetadata = 0x10,
		ActionScript3 = 0x08,
		NoCrossDomainCache = 0x04,
		UseNetwork = 0x01
	}
}