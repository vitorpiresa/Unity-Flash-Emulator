namespace Lab5.Swf.Interfaces
{
	using Data;

	public interface ISwfReader
	{

		// Signed types
		sbyte ReadSI8();
		short ReadSI16();
		int ReadSI24();
		int ReadSI32();
		sbyte[] ReadSI8(long n);
		short[] ReadSI16(long n);

		// Unsigned types
		byte ReadUI8();
		ushort ReadUI16();
		uint ReadUI32();
		byte[] ReadUI8(long n);
		ushort[] ReadUI16(long n);
		uint[] ReadUI24(long n);
		uint[] ReadUI32(long n);
		ulong[] ReadUI64(long n);

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
	}
}