namespace Lab5.Swf.Streams
{
	using Tags;
	using Enums;

	public class SwfTagReader : SwfDataReader
	{
		public SwfTagReader(System.IO.Stream input) : base(input) { }

		// 69 - FileAttributes
		public FileAttributes ReadFileAttributes()
		{
			var flags = (FileAttributeFlags)ReadUI32();

			return new()
			{
				Flags = flags
			};
		}
	}
}