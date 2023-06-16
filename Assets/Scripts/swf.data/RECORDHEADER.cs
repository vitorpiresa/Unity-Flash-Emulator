namespace Lab5.Swf.Data
{
	public readonly struct RECORDHEADER
	{
		public ushort Type { get; init; }
		public uint Length { get; init; }
		public long Offset { get; init; }
	}
}