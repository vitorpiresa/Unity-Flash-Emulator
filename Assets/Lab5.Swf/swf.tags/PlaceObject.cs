using Lab5.Swf.Data;
using Lab5.Swf.Enums;
using Lab5.Swf.Interfaces;

namespace Lab5.Swf.Tags
{
	// PlaceObject 	=> 4
	// PlaceObject2 => 26
	// PlaceObject3 => 70
	public readonly struct PlaceObject : ITag
	{
		public PlaceFlags Flags { get; init; }
		public ushort Depth { get; init; }
		public string ClassName { get; init; }
		public ushort CharacterID { get; init; }
		public MATRIX Matrix { get; init; }
		public CXFORM ColorTransform { get; init; }
		public ushort Ratio { get; init; }
		public string Name { get; init; }
		public ushort ClipDepth { get; init; }
		public FILTERLIST SurfaceFilterList { get; init; }
		public BlendMode BlendMode { get; init; }
		public byte BitmapCache { get; init; }
		public byte Visible { get; init; }
		public RGBA BackgroundColor { get; init; }
		public CLIPACTIONS ClipActions { get; init; }
	}
}