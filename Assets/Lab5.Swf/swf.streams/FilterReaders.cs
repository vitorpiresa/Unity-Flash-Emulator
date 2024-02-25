namespace Lab5.Swf.Streams
{
	using Filters;
	using Interfaces;

	public static class FilterReaders
	{
		// Filter 0
		public static DROPSHADOWFILTER ReadDROPSHADOWFILTER(this ISwfReader reader)
		{
			var DropShadowColor = reader.ReadRGBA();
			var BlurX = reader.ReadFIXED();
			var BlurY = reader.ReadFIXED();
			var Angle = reader.ReadFIXED();
			var Distance = reader.ReadFIXED();
			var Strength = reader.ReadFIXED8();
			var Flags = reader.ReadUI8();
			return new(DropShadowColor, BlurX, BlurY, Angle, Distance, Strength, Flags);
		}

		// Filter 1
		public static BLURFILTER ReadBLURFILTER(this ISwfReader reader)
		{
			var BlurX = reader.ReadFIXED();
			var BlurY = reader.ReadFIXED();
			var Flags = reader.ReadUI8();
			return new(BlurX, BlurY, Flags);
		}

		// Filter 2
		public static GLOWFILTER ReadGLOWFILTER(this ISwfReader reader)
		{
			var GlowColor = reader.ReadRGBA();
			var BlurX = reader.ReadFIXED();
			var BlurY = reader.ReadFIXED();
			var Strength = reader.ReadFIXED8();
			var Flags = reader.ReadUI8();
			return new(GlowColor, BlurX, BlurY, Strength, Flags);
		}

		// Filter 3
		public static BEVELFILTER ReadBEVELFILTER(this ISwfReader reader)
		{
			var ShadowColor = reader.ReadRGBA();
			var HighlightColor = reader.ReadRGBA();
			var BlurX = reader.ReadFIXED();
			var BlurY = reader.ReadFIXED();
			var Angle = reader.ReadFIXED();
			var Distance = reader.ReadFIXED();
			var Strength = reader.ReadFIXED8();
			var Flags = reader.ReadUI8();
			return new(ShadowColor, HighlightColor, BlurX, BlurY, Angle, Distance, Strength, Flags);
		}

		// Filter 4
		public static GRADIENTGLOWFILTER ReadGRADIENTGLOWFILTER(this ISwfReader reader)
		{
			var NumColors = reader.ReadUI8();
			var GradientColors = reader.ReadMany(reader.ReadRGBA, NumColors);
			var GradientRatio = reader.ReadMany(reader.ReadUI8, NumColors);
			var BlurX = reader.ReadFIXED();
			var BlurY = reader.ReadFIXED();
			var Angle = reader.ReadFIXED();
			var Distance = reader.ReadFIXED();
			var Strength = reader.ReadFIXED8();
			var Flags = reader.ReadUI8();
			return new(NumColors, GradientColors, GradientRatio, BlurX, BlurY, Angle, Distance, Strength, Flags);
		}

		// Filter 5
		public static CONVOLUTIONFILTER ReadCONVOLUTIONFILTER(this ISwfReader reader)
		{
			var MatrixX = reader.ReadUI8();
			var MatrixY = reader.ReadUI8();
			var Divisor = reader.ReadFLOAT();
			var Bias = reader.ReadFLOAT();
			var Matrix = reader.ReadMany(reader.ReadFLOAT, MatrixX * MatrixY);
			var DefaultColor = reader.ReadRGBA();
			var Flags = reader.ReadUI8();
			return new(MatrixX, MatrixY, Divisor, Bias, Matrix, DefaultColor, Flags);
		}

		// Filter 6
		public static COLORMATRIXFILTER ReadCOLORMATRIXFILTER(this ISwfReader reader)
		{
			var Matrix = reader.ReadMany(reader.ReadFLOAT, 20);
			return new(Matrix);
		}

		// Filter 7
		public static GRADIENTBEVELFILTER ReadGRADIENTBEVELFILTER(this ISwfReader reader)
		{
			var NumColors = reader.ReadUI8();
			var GradientColors = reader.ReadMany(reader.ReadRGBA, NumColors);
			var GradientRatio = reader.ReadMany(reader.ReadUI8, NumColors);
			var BlurX = reader.ReadFIXED();
			var BlurY = reader.ReadFIXED();
			var Angle = reader.ReadFIXED();
			var Distance = reader.ReadFIXED();
			var Strength = reader.ReadFIXED8();
			var Flags = reader.ReadUI8();
			return new(NumColors, GradientColors, GradientRatio, BlurX, BlurY, Angle, Distance, Strength, Flags);
		}
	}
}