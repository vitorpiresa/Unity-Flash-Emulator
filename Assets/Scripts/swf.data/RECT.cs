namespace Lab5.Swf.Data
{
	public struct RECT
	{
		public int Xmin { get; init; }
		public int Xmax { get; init; }
		public int Ymin { get; init; }
		public int Ymax { get; init; }

		public override string ToString()
		{
			const string format = "(x={0}, y={1}, w={2}, h={3})";
			return string.Format(format, Xmin, Ymin, Xmax - Xmin, Ymax - Ymin);
		}
	}
}