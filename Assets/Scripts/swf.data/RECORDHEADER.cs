using Lab5.Swf.Interfaces;

namespace Lab5.Swf.Data
{
	public readonly struct RECORDHEADER
	{
		public ushort Type { get; init; }
		public uint Lenght { get; init; }

		public static RECORDHEADER Read(ISwfReader reader)
		{
			var TagCodeAndLength = reader.ReadUI16();
			var Type = (ushort)(TagCodeAndLength >> 6);
			var Lenght = (TagCodeAndLength & 0x3Fu) == 0x3Fu ? reader.ReadUI32() : TagCodeAndLength & 0x3Fu;

			return new()
			{
				Type = Type,
				Lenght = Lenght
			};
		}
	}
}