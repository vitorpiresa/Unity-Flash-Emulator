using Lab5.Swf.Enums;

namespace Lab5.Swf.Data
{
	public readonly struct HEADER
	{
		public Signature Signature { get; init; }
		public byte Version { get; init; }
		public uint FileLenght { get; init; }
		public RECT FrameSize { get; init; }
		public float FrameRate { get; init; }
		public int FrameCount { get; init; }
	}
}