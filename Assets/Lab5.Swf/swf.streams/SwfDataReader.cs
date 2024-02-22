namespace Lab5.Swf.Streams
{
	using Data;
	using Enums;

	public class SwfDataReader : SwfReader
	{
		public SwfDataReader(System.IO.Stream input) : base(input) { }

		public FILEINFO ReadFILEINFO()
		{
			var Signature = (Signature)ReadUI24();
			var Version = ReadUI8();
			var FileLenght = ReadUI32();

			return new()
			{
				Signature = Signature,
				Version = Version,
				FileLenght = FileLenght
			};
		}

		public HEADER ReadHEADER()
		{
			return new()
			{
				Signature = (Signature)ReadUI24(),
				Version = ReadUI8(),
				FileLenght = ReadUI32(),
				FrameSize = ReadRECT(),
				FrameRate = ReadFIXED8(),
				FrameCount = ReadUI16(),
			};
		}

		public RECORDHEADER ReadRECORDHEADER()
		{
			var TagCodeAndLength = ReadUI16();
			var Type = (TagType)(TagCodeAndLength >> 6);
			var Lenght = (TagCodeAndLength & 0x3Fu) == 0x3Fu ? ReadUI32() : TagCodeAndLength & 0x3Fu;
			var Offset = BaseStream.Position;

			return new()
			{
				Type = Type,
				Length = Lenght,
				Offset = Offset,
			};
		}
	}
}