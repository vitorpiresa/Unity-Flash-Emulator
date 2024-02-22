namespace Lab5.Swf.Filters
{
	using Data;

	public record DROPSHADOWFILTER(RGBA DropShadowColor, float BlurX, float BlurY, float Angle, float Distance, float Strength, float Flags); // 0
	public record BLURFILTER(float BlurX, float BlurY, byte Passes); // 1
	public record GLOWFILTER(RGBA GlowColor, float BlurX, float BlurY, float Strength, byte Flags); // 2
	public record BEVELFILTER(RGBA ShadowColor, RGBA HighlightColor, float BlurX, float BlurY, float Angle, float Distance, float Strength, byte Flags); // 3
	public record GRADIENTGLOWFILTER(byte NumColors, RGBA[] GradientColors, byte[] GradientRatio, float BlurX, float BlurY, float Angle, float Distance, float Strength, byte Flags); // 4
	public record CONVOLUTIONFILTER(byte MatrixX, byte MatrixY, float Divisor, float Bias, float[] Matrix, RGBA DefaultColor, byte Flags); // 5
	public record COLORMATRIXFILTER(float[] Matrix); // 6
	public record GRADIENTBEVELFILTER(byte NumColors, RGBA[] GradientColors, byte[] GradientRatio, float BlurX, float BlurY, float Angle, float Distance, float Strength, byte Flags); // 7
}