using System.Collections.Generic;
using Lab5.Swf.Data;
using Lab5.Swf.Interfaces;

namespace Lab5.Swf
{
	public readonly struct SwfFile
	{
		public HEADER Header { get; init; }
		public IReadOnlyList<ITag> Tags { get; init; }
	}
}