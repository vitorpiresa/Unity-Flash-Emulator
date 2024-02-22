using Lab5.Swf.Data;
using Lab5.Swf.Interfaces;

namespace Lab5.Swf.Tags
{
	public readonly struct Protect : ITag
	{
		public RECORDHEADER Header { get; init; }
		public byte[] Password { get; init; }
	}
}