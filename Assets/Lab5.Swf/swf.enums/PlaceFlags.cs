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
		HasFilterList = 0x100,
		HasBlendMode = 0x200,
		CacheAsBitmap = 0x400,
		HasClassName = 0x800,
		HasImage = 0x1000,
		HasVisible = 0x2000,
		OpaqueBackground = 0x4000
	}
}