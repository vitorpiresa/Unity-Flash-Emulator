using System;
using Lab5.Swf.Data;
using Int8 = System.SByte;
using UInt8 = System.Byte;
using Int24 = System.Int32;

namespace Lab5.Swf.Interfaces
{
	public interface ISwfReader
	{
		// Integer types
		Int8 ReadSI8();
		Int16 ReadSI16();
		Int24 ReadSI24();
		Int32 ReadSI32();
		Int8[] ReadSI8(long n);
		Int16[] ReadSI16(long n);
		UInt8 ReadUI8();
		UInt16 ReadUI16();
		UInt32 ReadUI32();
		UInt8[] ReadUI8(long n);
		UInt16[] ReadUI16(long n);
		UInt32[] ReadUI24(long n);
		UInt32[] ReadUI32(long n);
		UInt64[] ReadUI64(long n);

		// Fixed-point numbers
		Single ReadFIXED();
		Single ReadFIXED8();

		// Floating-point numbers
		Single ReadFLOAT16();
		Single ReadFLOAT();
		Double ReadDOUBLE();

		// Encoded integers
		UInt32 ReadEncodedU32();

		// Bit values
		Int32 ReadSB(long nBits);
		UInt32 ReadUB(long nBits);
		Single ReadFB(long nBits);

		// Structured types
		String ReadSTRING();
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