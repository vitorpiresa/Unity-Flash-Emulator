namespace Lab5.Swf.Data
{
	using Enums;

	public readonly struct RECORDHEADER
	{
		public TagType Type { get; init; }
		public uint Length { get; init; }
		public long Offset { get; init; }
	}
}