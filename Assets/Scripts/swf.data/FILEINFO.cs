using Lab5.Swf.Enums;

namespace Lab5.Swf.Data
{
	public readonly struct FILEINFO
	{
		public Signature Signature { get; init; }
		public byte Version { get; init; }
		public uint FileLenght { get; init; }
	}
}