using System.Collections.ObjectModel;
using Lab5.Swf.Data;
using Lab5.Swf.Interfaces;

namespace Lab5.Swf.Tags
{
	public readonly struct ExportAssets : ITag
	{
		public RECORDHEADER Header { get; init; }
		public ushort Count { get; init; }
		public ReadOnlyCollection<ushort> Tags { get; init; }
		public ReadOnlyCollection<string> Names { get; init; }
	}
}