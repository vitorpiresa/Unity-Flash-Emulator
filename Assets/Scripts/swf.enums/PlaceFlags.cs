using System;

namespace Lab5.Swf.Enums
{
	[Flags]
	public enum PlaceFlags
	{
		Move = 0x01,
		HasCharacter = 0x02,
		HasMatrix = 0x04,
		HasColorTransform = 0x08,
		HasRatio = 0x10,
		HasName = 0x20,
		HasClipDepth = 0x40,
		HasClipActions = 0x80,
	}
}