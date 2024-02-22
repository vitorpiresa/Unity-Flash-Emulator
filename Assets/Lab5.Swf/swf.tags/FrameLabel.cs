namespace Lab5.Swf.Tags
{
	using Data;
	using Interfaces;

	public readonly struct FrameLabel : ITag
	{
		public RECORDHEADER Header { get; init; }
		public string Name { get; init; }
		public byte NamedAnchor { get; init; }
	}
}