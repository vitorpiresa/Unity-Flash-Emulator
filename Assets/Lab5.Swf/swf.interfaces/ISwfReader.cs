namespace Lab5.Swf.Interfaces
{
	using System;
	using Data;

	public interface ISwfReader
	{

		// Signed types
		sbyte ReadSI8();
		short ReadSI16();
		int ReadSI24();
		int ReadSI32();
		long ReadSI64();

		// Unsigned types
		byte ReadUI8();
		ushort ReadUI16();
		uint ReadUI24();
		uint ReadUI32();
		ulong ReadUI64();

		// Fixed-point numbers
		float ReadFIXED();
		float ReadFIXED8();

		// Floating-point numbers
		float ReadFLOAT16();
		float ReadFLOAT();
		double ReadDOUBLE();

		// Encoded integers
		uint ReadEncodedU32();

		// Bit values
		void Aling();
		int ReadSB(long nBits);
		uint ReadUB(long nBits);
		float ReadFB(long nBits);

		// Structured types
		string ReadSTRING();
		LANGCODE ReadLANGCODE();
		RGBA ReadRGB();
		RGBA ReadRGBA();
		RGBA ReadARGB();
		RECT ReadRECT();
		MATRIX ReadMATRIX();
		CXFORM ReadCXFORM();
		CXFORM ReadCXFORMWITHALPHA();

		// Misc
		T[] ReadMany<T>(Func<T> reader, int count);
	}
}