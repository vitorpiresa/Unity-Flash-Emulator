using System;
using System.IO;
using System.Text;
using Lab5.Swf.Data;
using Lab5.Swf.Interfaces;

namespace Lab5.Swf
{
	public class SwfBasicReader : IDisposable, IBasicReader
	{
		private BinaryReader m_Reader;
		private byte m_BitIndex;
		private byte m_BitValue;

		public SwfBasicReader(Stream input)
		{
			m_Reader = new BinaryReader(input);
		}

		public virtual void Dispose()
		{
			m_Reader.Dispose();
		}

		// Integer types
		public sbyte ReadSI8()
		{
			return m_Reader.ReadSByte();
		}

		public short ReadSI16()
		{
			return m_Reader.ReadInt16();
		}

		public int ReadSI24()
		{
			return (int)(m_Reader.ReadByte() | m_Reader.ReadByte() << 8 | m_Reader.ReadByte() << 16);
		}

		public int ReadSI32()
		{
			return m_Reader.ReadInt32();
		}

		public sbyte[] ReadSI8(int n)
		{
			var value = new sbyte[n];
			for (int c = 0; c < n; c++)
				value[c] = m_Reader.ReadSByte();
			return value;
		}

		public short[] ReadSI16(int n)
		{
			var value = new short[n];
			for (int c = 0; c < n; c++)
				value[c] = m_Reader.ReadInt16();
			return value;
		}

		public byte ReadUI8()
		{
			return m_Reader.ReadByte();
		}

		public ushort ReadUI16()
		{
			return m_Reader.ReadUInt16();
		}

		public uint ReadUI24()
		{
			return (uint)(m_Reader.ReadByte() | m_Reader.ReadByte() << 8 | m_Reader.ReadByte() << 16);
		}

		public uint ReadUI32()
		{
			return m_Reader.ReadUInt32();
		}

		public byte[] ReadUI8(int n)
		{
			return m_Reader.ReadBytes(n);
		}

		public ushort[] ReadUI16(int n)
		{
			var value = new ushort[n];
			for (int c = 0; c < n; c++)
				value[c] = m_Reader.ReadUInt16();
			return value;
		}

		public uint[] ReadUI24(int n)
		{
			throw new NotImplementedException();
		}

		public uint[] ReadUI32(int n)
		{
			var value = new uint[n];
			for (int c = 0; c < n; c++)
				value[c] = m_Reader.ReadUInt32();
			return value;
		}

		public ulong[] ReadUI64(int n)
		{
			var value = new ulong[n];
			for (int c = 0; c < n; c++)
				value[c] = m_Reader.ReadUInt64();
			return value;
		}

		// Fixed-point numbers
		public float ReadFIXED()
		{
			return m_Reader.ReadInt32() / 65536f;
		}

		public float ReadFIXED8()
		{
			return m_Reader.ReadInt16() / 256f;
		}

		// Floating-point numbers
		public float ReadFLOAT16()
		{
			throw new NotImplementedException();
		}

		public float ReadFLOAT()
		{
			return m_Reader.ReadSingle();
		}

		public double ReadDOUBLE()
		{
			return m_Reader.ReadDouble();
		}

		// Encoded integers
		public uint ReadEncodedU32()
		{
			uint val = 0;
			var bt = m_Reader.ReadByte();
			val |= bt & 0x7fu;
			if ((bt & 0x80) == 0) return val;

			bt = m_Reader.ReadByte();
			val |= (bt & 0x7fu) << 7;
			if ((bt & 0x80) == 0) return val;

			bt = m_Reader.ReadByte();
			val |= (bt & 0x7fu) << 14;
			if ((bt & 0x80) == 0) return val;

			bt = m_Reader.ReadByte();
			val |= (bt & 0x7fu) << 21;
			if ((bt & 0x80) == 0) return val;

			bt = m_Reader.ReadByte();
			val |= (bt & 0x7fu) << 28;
			return val;
		}

		// Bit values
		public bool ReadBit()
		{
			var bitIndex = m_BitIndex & 0x07;
			if (bitIndex == 0)
				m_BitValue = m_Reader.ReadByte();
			m_BitIndex++;
			return ((m_BitValue << bitIndex) & 0x80) != 0;
		}

		public int ReadSB(long nBits)
		{
			if (nBits == 0)
				return 0;
			var sign = ReadBit();
			var res = sign ? uint.MaxValue : 0;
			nBits--;
			for (var i = 0; i < nBits; i++)
			{
				var bit = ReadBit();
				res = (res << 1 | (bit ? 1u : 0u));
			}
			return (int)res;
		}

		public uint ReadUB(long nBits)
		{
			if (nBits == 0)
				return 0;
			var res = 0u;
			for (var i = 0; i < nBits; i++)
			{
				var bit = ReadBit();
				res = (res << 1 | (bit ? 1u : 0u));
			}
			return res;
		}

		public float ReadFB(long nBits)
		{
			return ReadSB(nBits) / 65536f;
		}

		// Structured types
		public string ReadString()
		{
			var stream = new MemoryStream();
			byte bt = 1;
			while ((bt = m_Reader.ReadByte()) > 0)
			{
				if (bt > 0)
					stream.WriteByte(bt);
			}
			return Encoding.UTF8.GetString(stream.ToArray());
		}

		public LANGCODE ReadLANGCODE()
		{
			return (LANGCODE)ReadUI8();
		}

		public RGBA ReadRGB()
		{
			var red = ReadUI8();
			var green = ReadUI8();
			var blue = ReadUI8();

			return new()
			{
				Red = red,
				Green = green,
				Blue = blue,
				Alpha = 0xFF
			};
		}

		public RGBA ReadRGBA()
		{
			var red = ReadUI8();
			var green = ReadUI8();
			var blue = ReadUI8();
			var alpha = ReadUI8();

			return new()
			{
				Red = red,
				Green = green,
				Blue = blue,
				Alpha = alpha
			};
		}

		public RGBA ReadARGB()
		{
			var Alpha = ReadUI8();
			var Red = ReadUI8();
			var Green = ReadUI8();
			var Blue = ReadUI8();

			return new()
			{
				Red = Red,
				Green = Green,
				Blue = Blue,
				Alpha = Alpha
			};

		}

		public RECT ReadRECT()
		{
			var nBits = ReadUB(5);
			var Xmin = ReadSB(nBits);
			var Xmax = ReadSB(nBits);
			var Ymin = ReadSB(nBits);
			var Ymax = ReadSB(nBits);

			Aling();

			return new()
			{
				Xmin = Xmin,
				Xmax = Xmax,
				Ymin = Ymin,
				Ymax = Ymax
			};
		}

		public MATRIX ReadMATRIX()
		{
			var HasScale = ReadBit();
			var NScaleBits = HasScale ? ReadUB(5) : 0;
			var ScaleX = HasScale ? ReadFB(NScaleBits) : 1f;
			var ScaleY = HasScale ? ReadFB(NScaleBits) : 1f;
			var HasRotate = ReadBit();
			var NRotateBits = HasRotate ? ReadUB(5) : 0;
			var RotateSkew0 = HasRotate ? ReadFB(NRotateBits) : 0f;
			var RotateSkew1 = HasRotate ? ReadFB(NRotateBits) : 0f;;
			var NTranslateBits = ReadUB(5);
			var TranslateX = ReadSB(NTranslateBits);
			var TranslateY = ReadSB(NTranslateBits);

			Aling();

			return new()
			{
				ScaleX = ScaleX,
				ScaleY = ScaleY,
				RotateSkew0 = RotateSkew0,
				RotateSkew1 = RotateSkew1,
				TranslateX = TranslateX,
				TranslateY = TranslateY
			};
		}

		public CXFORM ReadCXFORM()
		{
			var HasAddTerms = ReadBit();
			var HasMultTerms = ReadBit();
			var Nbits = ReadUB(4);
			var RedMultTerm = 0x100;
			var GreenMultTerm = 0x100;
			var BlueMultTerm = 0x100;
			var RedAddTerm = 0;
			var GreenAddTerm = 0;
			var BlueAddTerm = 0;

			if (HasMultTerms)
			{
				RedMultTerm = ReadSB(Nbits);
				GreenMultTerm = ReadSB(Nbits);
				BlueMultTerm = ReadSB(Nbits);
			}
			if (HasAddTerms)
			{
				RedAddTerm = ReadSB(Nbits);
				GreenAddTerm = ReadSB(Nbits);
				BlueAddTerm = ReadSB(Nbits);
			}

			return new()
			{
				RedMultTerm = (short)RedMultTerm,
				GreenMultTerm = (short)GreenMultTerm,
				BlueMultTerm = (short)BlueMultTerm,
				AlphaMultTerm = 0x100,
				RedAddTerm = (short)RedAddTerm,
				GreenAddTerm = (short)GreenAddTerm,
				BlueAddTerm = (short)BlueAddTerm,
				AlphaAddTerm = 0
			};
		}

		public CXFORM ReadCXFORMWITHALPHA()
		{
			var HasAddTerms = ReadBit();
			var HasMultTerms = ReadBit();
			var Nbits = ReadUB(4);
			var RedMultTerm = 0x100;
			var GreenMultTerm = 0x100;
			var BlueMultTerm = 0x100;
			var AlphaMultTerm = 0x100;
			var RedAddTerm = 0;
			var GreenAddTerm = 0;
			var BlueAddTerm = 0;
			var AlphaAddTerm = 0;

			if (HasMultTerms)
			{
				RedMultTerm = ReadSB(Nbits);
				GreenMultTerm = ReadSB(Nbits);
				BlueMultTerm = ReadSB(Nbits);
				AlphaMultTerm = ReadSB(Nbits);
			}
			if (HasAddTerms)
			{
				RedAddTerm = ReadSB(Nbits);
				GreenAddTerm = ReadSB(Nbits);
				BlueAddTerm = ReadSB(Nbits);
				AlphaAddTerm = ReadSB(Nbits);
			}

			return new()
			{
				RedMultTerm = (short)RedMultTerm,
				GreenMultTerm = (short)GreenMultTerm,
				BlueMultTerm = (short)BlueMultTerm,
				AlphaMultTerm = (short)AlphaMultTerm,
				RedAddTerm = (short)RedAddTerm,
				GreenAddTerm = (short)GreenAddTerm,
				BlueAddTerm = (short)BlueAddTerm,
				AlphaAddTerm = (short)AlphaAddTerm
			};
		}

		public void Aling()
		{
			m_BitIndex = 0;
			m_BitValue = 0;
		}
	}
}