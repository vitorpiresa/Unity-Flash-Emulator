using System;
using Lab5.Swf.Data;
using Int8 = System.SByte;
using UInt8 = System.Byte;

namespace Lab5.Swf.Interfaces
{
	public interface IBasicReader
	{
		// Integer types
		Int8 ReadSI8();
		Int16 ReadSI16();
		Int32 ReadSI32();
		Int8[] ReadSI8(int n);
		Int16[] ReadSI16(int n);
		UInt8 ReadUI8();
		UInt16 ReadUI16();
		UInt32 ReadUI32();
		UInt8[] ReadUI8(int n);
		UInt16[] ReadUI16(int n);
		UInt32[] ReadUI24(int n);
		UInt32[] ReadUI32(int n);
		UInt64[] ReadUI64(int n);

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
		String ReadString();
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