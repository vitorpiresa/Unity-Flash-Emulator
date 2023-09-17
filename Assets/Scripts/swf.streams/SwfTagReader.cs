namespace Lab5.Swf.Streams
{
	using Data;
	using Tags;
	using Enums;

	public class SwfTagReader : SwfDataReader
	{
		public SwfTagReader(System.IO.Stream input) : base(input) { }

		// 4 - PlaceObject
		public PlaceObjectTag ReadPlaceObject()
		{
			var characterID = ReadUI16();
			var depth = ReadUI16();
			var matrix = ReadMATRIX();
			var colorTransform = EndOfStream ? CXFORM.Identity : ReadCXFORM();

			return new PlaceObjectTag()
			{
				Flags = default,
				Depth = depth,
				ClassName = default,
				CharacterID = default,
				Matrix = matrix,
				ColorTransform = colorTransform,
				Ratio = default,
				Name = default,
				ClipDepth = default,
				// SurfaceFilterList
				// BlendMode
				// BitmapCache
				// Visible
				// BackgroundColor
				// ClipActions -> PlaceObject2
			};
		}

		// 5 - RemoveObject
		public RemoveObject ReadRemoveObject()
		{
			var characterID = ReadUI16();
			var depth = ReadUI16();

			return new(characterID, depth);
		}

		// 26 - PlaceObject2
		public PlaceObjectTag ReadPlaceObject2()
		{
			var flags = (PlaceFlags)ReadUI8();
			var depth = ReadUI16();
			var characterID = flags.HasFlag(PlaceFlags.HasCharacter) ? ReadUI16() : default;
			var matrix = flags.HasFlag(PlaceFlags.HasMatrix) ? ReadMATRIX() : default;
			var colorTransform = flags.HasFlag(PlaceFlags.HasColorTransform) ? ReadCXFORMWITHALPHA() : default;
			var ratio = flags.HasFlag(PlaceFlags.HasRatio) ? ReadUI16() : default;
			var name = flags.HasFlag(PlaceFlags.HasName) ? ReadSTRING() : default;
			var clipDepth = flags.HasFlag(PlaceFlags.HasClipDepth) ? ReadUI16() : default;

			return new()
			{
				Flags = flags,
				Depth = depth,
				CharacterID = characterID,
				Matrix = matrix,
				ColorTransform = colorTransform,
				Ratio = ratio,
				Name = name,
				ClipDepth = clipDepth,
			};
		}

		// 28 - RemoveObject2
		public RemoveObject ReadRemoveObject2()
		{
			var depth = ReadUI16();

			return new(default, depth);
		}

		// 69 - FileAttributes
		public FileAttributes ReadFileAttributes()
		{
			var flags = (FileAttributeFlags)ReadUI32();

			return new()
			{
				Flags = flags
			};
		}

		// 70 - PlaceObject3
		public PlaceObjectTag ReadPlaceObject3()
		{
			var flags = (PlaceFlags)ReadUI16();
			var depth = ReadUI16();
			var characterID = flags.HasFlag(PlaceFlags.HasCharacter) ? ReadUI16() : default;
			var matrix = flags.HasFlag(PlaceFlags.HasMatrix) ? ReadMATRIX() : default;
			var colorTransform = flags.HasFlag(PlaceFlags.HasColorTransform) ? ReadCXFORMWITHALPHA() : default;
			var ratio = flags.HasFlag(PlaceFlags.HasRatio) ? ReadUI16() : default;
			var name = flags.HasFlag(PlaceFlags.HasName) ? ReadSTRING() : default;
			var clipDepth = flags.HasFlag(PlaceFlags.HasClipDepth) ? ReadUI16() : default;

			return new()
			{
				Flags = flags,
				Depth = depth,
				CharacterID = characterID,
				Matrix = matrix,
				ColorTransform = colorTransform,
				Ratio = ratio,
				Name = name,
				ClipDepth = clipDepth,
			};
		}
	}
}