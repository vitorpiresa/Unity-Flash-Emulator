using Lab5.Swf.Enums;
using Lab5.Swf.Interfaces;

namespace Lab5.Swf.Tags
{
	public readonly struct FileAttributes : ITag
	{
		public FileAttributeFlags Flags { get; init; }
	}
}