using System;
using System.IO;
using Lab5.Swf.Data;
using Lab5.Swf.Enums;
using SharpCompress.Compressors;
using SharpCompress.Compressors.LZMA;
using SharpCompress.Compressors.Deflate;

namespace Lab5.Swf
{
	public class SwfLoader : IDisposable
	{
		Stream m_Stream;
		SwfReader m_Reader;

		public SwfLoader(string path)
		{
			m_Stream = File.OpenRead(path);
			m_Reader = new SwfReader(m_Stream);

			var info = m_Reader.ReadFileInfo();

			if (info.Signature == Signature.FWS)
				m_Stream.Position = 0;
			else if (info.Signature == Signature.CWS)
				DecompressZlib(info);
			else if (info.Signature == Signature.ZWS)
				DecompressLzma(info);
			else
				throw new InvalidDataException("Unknown file signature");

			var header = m_Reader.ReadHEADER();
			UnityEngine.Debug.Log(header);

			m_Reader.ReadTag();
		}

		public void Dispose()
		{
			m_Reader.Dispose();
			m_Stream.Dispose();
		}

		private void DecompressZlib(HEADER info)
		{
			var lenght = (int)info.FileLenght;
			var offset = 8;
			var count = lenght - offset;
			var buffer = new byte[lenght];
			
			m_Stream.Position = 0;
			m_Stream.Read(buffer, 0, 8);

			using (var compressedStream = new ZlibStream(m_Stream, CompressionMode.Decompress))
			{
				var readed = compressedStream.Read(buffer, offset, count);
			}

			m_Stream.Dispose();
			m_Reader.Dispose();
			m_Stream = new MemoryStream(buffer);
			m_Reader = new SwfReader(m_Stream);
		}

		private void DecompressLzma(HEADER info)
		{
			var lenght = (int)info.FileLenght;
			var offset = 8;
			var count = lenght - offset;
			var buffer = new byte[lenght];

			m_Stream.Position = 0;
			m_Stream.Read(buffer, 0, 8);

			var properties = new byte[5];
			m_Stream.Position = offset + 4;
			m_Stream.Read(properties, 0, 5);

			using (var compressedStream = new LzmaStream(properties, m_Stream))
			{
				var readed = compressedStream.Read(buffer, offset, count);
			}
			
			m_Stream.Dispose();
			m_Reader.Dispose();
			m_Stream = new MemoryStream(buffer, false);
			m_Reader = new SwfReader(m_Stream);
		}
	}
}