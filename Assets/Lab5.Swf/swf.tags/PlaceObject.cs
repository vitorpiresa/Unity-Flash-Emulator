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
		// SurfaceFilterList
		// BlendMode
		// BitmapCache
		// Visible
		// BackgroundColor
		// ClipActions -> PlaceObject2
	}
}

public record Placer();