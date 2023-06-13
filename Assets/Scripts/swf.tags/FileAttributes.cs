using Lab5.Swf.Enums;
using Lab5.Swf.Interfaces;

namespace Lab5.Swf.Tags
{
	public readonly struct FileAttributes : ITag
	{
		public FileAttributeFlags Flags { get; init; }

		public override string ToString()
		{
			return Flags.ToString();
		}

		public static FileAttributes Read(ISwfReader reader)
		{
			var Flags = reader.ReadUI32();
			return new()
			{
				Flags = (FileAttributeFlags)Flags
			};
		}
	}
}