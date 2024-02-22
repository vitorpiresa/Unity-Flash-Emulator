namespace Lab5.Swf.Data
{
	public readonly struct CXFORM
	{
		public short RedMultTerm { get; init; }
		public short GreenMultTerm { get; init; }
		public short BlueMultTerm { get; init; }
		public short AlphaMultTerm { get; init; }
		public short RedAddTerm { get; init; }
		public short GreenAddTerm { get; init; }
		public short BlueAddTerm { get; init; }
		public short AlphaAddTerm { get; init; }

		public static CXFORM Identity => new() { RedMultTerm = 0x100, GreenMultTerm = 0x100, BlueMultTerm = 0x100, AlphaMultTerm = 0x100 };
		public static CXFORM Zero => default;
	}
}