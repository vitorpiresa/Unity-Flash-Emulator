using Lab5.Swf.Data;

namespace Lab5.Swf.Filters
{
	public readonly struct CONVOLUTIONFILTER
	{
		public readonly byte MatrixX;
		public readonly byte MatrixY;
		public readonly float Divisor;
		public readonly float Bias;
		public readonly float[,] Matrix;
		public readonly RGBA DefaultColor;
		public readonly byte Flags;
	}
}