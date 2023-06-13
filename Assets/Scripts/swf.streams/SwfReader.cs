using System.IO;
using Lab5.Swf.Data;
using Lab5.Swf.Enums;
using Lab5.Swf.Interfaces;
using FileAttributes = Lab5.Swf.Tags.FileAttributes;

namespace Lab5.Swf
{
	public class SwfReader : SwfBasicReader, ISwfReader
	{
		public SwfReader(Stream input) : base(input) { }

		public HEADER ReadFileInfo()
		{
			var Signature = ReadSignature();
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
				Signature = ReadSignature(),
				Version = ReadUI8(),
				FileLenght = ReadUI32(),
				FrameSize = ReadRECT(),
				FrameRate = ReadFIXED8(),
				FrameCount = ReadUI16(),
			};
		}

		public ITag ReadTag()
		{
			var header = RECORDHEADER.Read(this);

			if(header.Type == 69)
			{
				var tag = FileAttributes.Read(this);
				UnityEngine.Debug.Log(tag);
			}

			return default;
		}

		private Signature ReadSignature()
		{
			var Signature1 = ReadUI8();
			var Signature2 = ReadUI8();
			var Signature3 = ReadUI8();

			if (Signature2 == 0x57 && Signature3 == 0x53)
				if (Signature1 == 0x46) // FWS
					return Signature.FWS;
				else if (Signature1 == 0x43) // CWS
					return Signature.CWS;
				else if (Signature1 == 0x5A) // ZWS
					return Signature.ZWS;
			return Signature.Unknown;
		}
	}
}