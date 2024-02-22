namespace Lab5.Swf.Data
{
	using Filters;

	public record CLIPACTIONS(ushort Reserved, CLIPEVENTFLAGS AllEventFlags, CLIPACTIONRECORD[] ClipActionRecords, uint ClipActionEndFlag);
	public record CLIPACTIONRECORD(CLIPEVENTFLAGS EventFlags, uint ActionRecordSize, byte KeyCode /*ACTIONRECORD[] Actions*/);
	public record FILTERLIST(byte NumberOfFilters, FILTER[] Filter);
	public record FILTER(byte FilterID, DROPSHADOWFILTER DropShadowFilter, BLURFILTER BlurFilter, GLOWFILTER GlowFilter, BEVELFILTER BevelFilter, GRADIENTGLOWFILTER GradientGlowFilter, CONVOLUTIONFILTER ConvolutionFilter, COLORMATRIXFILTER ColorMatrixFilter, GRADIENTBEVELFILTER GradientBevelFilter);
	public record CLIPEVENTFLAGS(uint Flags);
}