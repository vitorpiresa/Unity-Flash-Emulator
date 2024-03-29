namespace Lab5.Swf.Data
{
	public readonly struct MATRIX
	{
		public float ScaleX { get; init; }
		public float ScaleY { get; init; }
		public float RotateSkew0 { get; init; }
		public float RotateSkew1 { get; init; }
		public int TranslateX { get; init; }
		public int TranslateY { get; init; }

		public static MATRIX Identity => new() { ScaleX = 1, ScaleY = 1 };
		public static MATRIX Zero => default;
	}
}