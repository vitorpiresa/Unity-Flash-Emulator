namespace Lab5.Swf.Tags
{
	using Data;
	using Interfaces;

	public readonly struct SetBackgroundColor : ITag
	{
		public RECORDHEADER Header { get; init; }
		public RGBA BackgroundColor { get; init; }
	}
}