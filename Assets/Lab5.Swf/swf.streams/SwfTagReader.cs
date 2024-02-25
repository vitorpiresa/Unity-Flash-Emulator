namespace Lab5.Swf.Streams
{
	using Data;
	using Tags;
	using Enums;
	using System;

	public class SwfTagReader : SwfDataReader
	{
		public SwfTagReader(System.IO.Stream input) : base(input) { }

		// 0 - End
		public End ReadEnd()
		{
			return new();
		}

		// 1 - ShowFrame
		public ShowFrame ReadShowFrame()
		{
			return new();
		}

		// 4 - PlaceObject
		public PlaceObject ReadPlaceObject()
		{
			var characterID = ReadUI16();
			var depth = ReadUI16();
			var matrix = ReadMATRIX();
			var colorTransform = EndOfStream ? CXFORM.Identity : ReadCXFORM();

			return new PlaceObject()
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

		// 9 - SetBackgroundColor
		public SetBackgroundColor ReadSetBackgroundColor()
		{
			var backgroundColor = ReadRGB();
			return new()
			{
				BackgroundColor = backgroundColor
			};
		}

		// 24 - Protect
		public Protect ReadProtect()
		{
			var password = EndOfStream ? null : ReadMany(ReadUI8, (int)(BaseStream.Length - BaseStream.Position));
			return new()
			{
				Password = password
			};
		}

		// 26 - PlaceObject2
		public PlaceObject ReadPlaceObject2()
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

		// 43 - FrameLabel
		public FrameLabel ReadFrameLabel()
		{
			var name = ReadSTRING();
			var namedAnchor = EndOfStream ? (byte)0 : ReadUI8();

			return new()
			{
				Name = name,
				NamedAnchor = namedAnchor
			};
		}

		// 56 - ExportAssets
		public ExportAssets ReadExportAssets()
		{
			var count = ReadUI16();
			var tags = new ushort[count];
			var names = new string[count];
			for (int c = 0; c < count; c++)
			{
				tags[c] = ReadUI16();
				names[c] = ReadSTRING();
			}
			return new()
			{
				Count = count,
				Tags = Array.AsReadOnly(tags),
				Names = Array.AsReadOnly(names)
			};
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
		public PlaceObject ReadPlaceObject3()
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