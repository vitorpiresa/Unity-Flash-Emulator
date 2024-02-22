using Lab5.Swf.Filters;

namespace Lab5.Swf.Data
{
	public readonly struct FILTER
	{
		public readonly byte FilterID;
		public readonly CONVOLUTIONFILTER ConvolutionFilter; // 5
		public readonly COLORMATRIXFILTER ColorMatrixFilter; // 6
	}
}