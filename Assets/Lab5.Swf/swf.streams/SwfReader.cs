using System;
using System.IO;
using System.Text;
using Lab5.Swf.Data;
using Lab5.Swf.Enums;
using Lab5.Swf.Interfaces;

namespace Lab5.Swf
{
	public class SwfReader : IDisposable, ISwfReader
	{
		private Stream m_Stream;
		private Encoding m_Encoding;
		private byte m_BitIndex;
		private byte m_BitValue;

		public Stream BaseStream => m_Stream;
		public Encoding CurrentEncoding => m_Encoding;
		public bool EndOfStream => m_Stream.Position == m_Stream.Length;

		public SwfReader(Stream input)
		{
			m_Stream = input;
			m_Encoding = Encoding.UTF8;
			m_BitIndex = 0;
			m_BitValue = 0;
		}

		public virtual void Dispose()
		{
			m_Stream.Dispose();
		}

		private byte ReadByte()
		{
			var value = m_Stream.ReadByte();
			if (value == -1)
				throw new EndOfStreamException("Unable to read beyond the end of the stream.");
			return (byte)value;
		}

		// Integer types
		public sbyte ReadSI8()
		{
			unchecked
			{
				return (sbyte)ReadByte();
			}
		}

		public short ReadSI16()
		{
			unchecked
			{
				var b0 = ReadByte();
				var b1 = ReadByte();
				return (short)(b0 | b1 << 8);
			}
		}

		public int ReadSI24()
		{
			unchecked
			{
				var b0 = ReadByte();
				var b1 = ReadByte();
				var b2 = ReadByte();
				return b0 | b1 << 8 | b2 << 16;
			}
		}

		public int ReadSI32()
		{
			unchecked
			{
				var b0 = ReadByte();
				var b1 = ReadByte();
				var b2 = ReadByte();
				var b3 = ReadByte();
				return b0 | b1 << 8 | b2 << 16 | b3 << 24;
			}
		}

		public sbyte[] ReadSI8(long n)
		{
			unchecked
			{
				var value = new sbyte[n];
				for (long c = 0; c < n; c++)
					value[c] = (sbyte)ReadByte();
				return value;
			}
		}

		public short[] ReadSI16(long n)
		{
			unchecked
			{
				var value = new short[n];
				for (int c = 0; c < n; c++)
				{
					var b0 = ReadByte();
					var b1 = ReadByte();
					value[c] = (short)(b0 | b1 << 8);
				}
				return value;
			}
		}

		public byte ReadUI8()
		{
			unchecked
			{
				return ReadByte();
			}
		}

		public ushort ReadUI16()
		{
			unchecked
			{
				var b0 = ReadByte();
				var b1 = ReadByte();
				return (ushort)(b0 | b1 << 8);
			}
		}

		public uint ReadUI24()
		{
			unchecked
			{
				var b0 = ReadByte();
				var b1 = ReadByte();
				var b2 = ReadByte();
				return (uint)(b0 | b1 << 8 | b2 << 16);
			}
		}

		public uint ReadUI32()
		{
			unchecked
			{
				var b0 = ReadByte();
				var b1 = ReadByte();
				var b2 = ReadByte();
				var b3 = ReadByte();
				return (uint)(b0 | b1 << 8 | b2 << 16 | b3 << 24);
			}
		}

		public byte[] ReadUI8(long n)
		{
			unchecked
			{
				var value = new byte[n];
				for (long c = 0; c < n; c++)
					value[c] = ReadByte();
				return value;
			}
		}

		public ushort[] ReadUI16(long n)
		{
			unchecked
			{
				var value = new ushort[n];
				for (long c = 0; c < n; c++)
				{
					var b0 = ReadByte();
					var b1 = ReadByte();
					value[c] = (ushort)(b0 | b1 << 8);
				}
				return value;
			}
		}

		public uint[] ReadUI24(long n)
		{
			unchecked
			{
				var value = new uint[n];
				for (long c = 0; c < n; c++)
				{
					var b0 = ReadByte();
					var b1 = ReadByte();
					var b2 = ReadByte();
					value[c] = (uint)(b0 | b1 << 8 | b2 << 16);
				}
				return value;
			}
		}

		public uint[] ReadUI32(long n)
		{
			unchecked
			{
				var value = new uint[n];
				for (int c = 0; c < n; c++)
				{
					var b0 = ReadByte();
					var b1 = ReadByte();
					var b2 = ReadByte();
					var b3 = ReadByte();
					value[c] = (uint)(b0 | b1 << 8 | b2 << 16 | b3 << 24);
				}
				return value;
			}
		}

		public ulong[] ReadUI64(long n)
		{
			unchecked
			{
				var value = new ulong[n];
				for (int c = 0; c < n; c++)
				{
					var lo = (uint)(ReadByte() | ReadByte() << 8 | ReadByte() << 16 | ReadByte() << 24);
					var hi = (uint)(ReadByte() | ReadByte() << 8 | ReadByte() << 16 | ReadByte() << 24);
					value[c] = ((ulong)hi) << 32 | lo;
				}
				return value;
			}
		}

		// Fixed-point numbers
		public float ReadFIXED()
		{
			unchecked
			{
				var b0 = ReadByte();
				var b1 = ReadByte();
				var b2 = ReadByte();
				var b3 = ReadByte();
				return (b0 | b1 << 8 | b2 << 16 | b3 << 24) / 65536f;
			}
		}

		public float ReadFIXED8()
		{
			unchecked
			{
				var b0 = ReadByte();
				var b1 = ReadByte();
				return (b0 << b1 | 8) / 256f;
			}
		}

		public float ReadFLOAT16()
		{
			unchecked
			{
				var b0 = ReadByte();
				var b1 = ReadByte();
				return HalfUtils.f16tof32((uint)(b0 | b1 << 8));
			}
		}

		public float ReadFLOAT()
		{
			unchecked
			{
				var b0 = ReadByte();
				var b1 = ReadByte();
				var b2 = ReadByte();
				var b3 = ReadByte();
				var value = b0 | b1 << 8 | b2 << 16 | b3 << 24;
				return BitConverter.Int32BitsToSingle(value);
			}
		}

		public double ReadDOUBLE()
		{
			unchecked
			{
				var lo = (uint)(ReadByte() | ReadByte() << 8 | ReadByte() << 16 | ReadByte() << 24);
				var hi = (uint)(ReadByte() | ReadByte() << 8 | ReadByte() << 16 | ReadByte() << 24);
				var value = ((long)hi) << 32 | lo;
				return BitConverter.Int64BitsToDouble(value);
			}
		}

		// Encoded integers
		public uint ReadEncodedU32()
		{
			uint val = 0;
			var bt = ReadByte();
			val |= bt & 0x7fu;
			if ((bt & 0x80) == 0) return val;

			bt = ReadByte();
			val |= (bt & 0x7fu) << 7;
			if ((bt & 0x80) == 0) return val;

			bt = ReadByte();
			val |= (bt & 0x7fu) << 14;
			if ((bt & 0x80) == 0) return val;

			bt = ReadByte();
			val |= (bt & 0x7fu) << 21;
			if ((bt & 0x80) == 0) return val;

			bt = ReadByte();
			val |= (bt & 0x7fu) << 28;
			return val;
		}

		// Bit values
		public bool ReadBit()
		{
			var bitIndex = m_BitIndex & 0x07;
			if (bitIndex == 0)
				m_BitValue = ReadByte();
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
		public string ReadSTRING()
		{
			var stream = new MemoryStream();
			byte bt;
			while ((bt = ReadByte()) > 0)
			{
				if (bt > 0)
					stream.WriteByte(bt);
			}
			return m_Encoding.GetString(stream.ToArray());
		}

		public LANGCODE ReadLANGCODE()
		{
			var languageCode = (LanguageCode)ReadUI8();
			return new()
			{
				LanguageCode = languageCode
			};
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
			var RotateSkew1 = HasRotate ? ReadFB(NRotateBits) : 0f;
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
			var RedMultTerm = HasMultTerms ? ReadSB(Nbits) : 0x100;
			var GreenMultTerm = HasMultTerms ? ReadSB(Nbits) : 0x100;
			var BlueMultTerm = HasMultTerms ? ReadSB(Nbits) : 0x100;
			var RedAddTerm = 0;
			var GreenAddTerm = 0;
			var BlueAddTerm = 0;

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