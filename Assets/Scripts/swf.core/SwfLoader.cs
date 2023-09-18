using System;
using System.IO;
using System.Collections.Generic;
using Lab5.Swf.Data;
using Lab5.Swf.Enums;
using Lab5.Swf.Streams;
using Lab5.Swf.Interfaces;
using SharpCompress.Compressors;
using SharpCompress.Compressors.LZMA;
using SharpCompress.Compressors.Deflate;

namespace Lab5.Swf
{
	public class SwfLoader : IDisposable
	{
		SwfTagReader m_Reader;
		RECORDHEADER[] m_Records;
		ITag[] m_Tags;
		Dictionary<int, IDefinitionTag> m_Dictionary;

		public SwfLoader(string path)
		{
			m_Reader = new SwfTagReader(File.OpenRead(path));

			var info = m_Reader.ReadFILEINFO();

			if (info.Signature == Signature.FWS)
				m_Reader.BaseStream.Position = 0;
			else if (info.Signature == Signature.CWS)
				DecompressZlib(info);
			else if (info.Signature == Signature.ZWS)
				DecompressLzma(info);
			else
				throw new InvalidDataException("Unknown file signature");

			var header = m_Reader.ReadHEADER();
			ReadRecords();
			ReadTags();
		}

		private void ReadRecords()
		{
			var records = new List<RECORDHEADER>();
			while (!m_Reader.EndOfStream)
			{
				var record = m_Reader.ReadRECORDHEADER();
				records.Add(record);

				var next = m_Reader.BaseStream.Position + record.Length;

				if (next <= m_Reader.BaseStream.Length)
					m_Reader.BaseStream.Position = next;
				else
					return;
			}
			m_Records = records.ToArray();
		}

		private void ReadTags()
		{
			m_Tags = new ITag[m_Records.Length];
			m_Dictionary = new();

			for (var index = 0; index < m_Records.Length; index++)
			{
				var record = m_Records[index];
				var length = (int)record.Length;
				var buffer = new byte[length];

				m_Reader.BaseStream.Position = record.Offset;
				m_Reader.BaseStream.Read(buffer, 0, length);

				var reader = new SwfTagReader(new MemoryStream(buffer));
				var tag = ReadTag(reader, record);

				if (tag is null)
				{
					UnityEngine.Debug.LogWarning($"Unknown tag type: {record.Type}");
					continue;
				}

				if (!reader.EndOfStream)
					UnityEngine.Debug.LogWarning($"Uncomplete tag reading on tag {tag.GetType().Name} number {index}.");

				if (tag is IDefinitionTag defineTag)
					m_Dictionary.Add(defineTag.ID, defineTag);

				m_Tags[index] = tag;
			}
		}

		private ITag ReadTag(SwfTagReader reader, RECORDHEADER header)
		{
			try
			{
				if (header.Type == 1)
					return reader.ReadShowFrame();
				else if (header.Type == 4)
					return reader.ReadPlaceObject();
				else if (header.Type == 5)
					return reader.ReadRemoveObject();
				else if (header.Type == 26)
					return reader.ReadPlaceObject2();
				else if (header.Type == 28)
					return reader.ReadRemoveObject2();
				else if (header.Type == 69)
					return reader.ReadFileAttributes();
				else if (header.Type == 70)
					return reader.ReadPlaceObject3();
				else
					return null;
			}
			catch
			{
				UnityEngine.Debug.Log("Corrupted tag reading");
				return null;
			}
		}

		public void Dispose()
		{
			m_Reader.Dispose();
		}

		private void DecompressZlib(FILEINFO info)
		{
			var lenght = (int)info.FileLenght;
			var offset = 8;
			var count = lenght - offset;
			var buffer = new byte[lenght];

			m_Reader.BaseStream.Position = 0;
			m_Reader.BaseStream.Read(buffer, 0, 8);

			using (var compressedStream = new ZlibStream(m_Reader.BaseStream, CompressionMode.Decompress))
			{
				var readed = compressedStream.Read(buffer, offset, count);
			}

			m_Reader.Dispose();
			m_Reader = new SwfTagReader(new MemoryStream(buffer));
		}

		private void DecompressLzma(FILEINFO info)
		{
			var lenght = (int)info.FileLenght;
			var offset = 8;
			var count = lenght - offset;
			var buffer = new byte[lenght];

			m_Reader.BaseStream.Position = 0;
			m_Reader.BaseStream.Read(buffer, 0, 8);

			var properties = new byte[5];
			m_Reader.BaseStream.Position = offset + 4;
			m_Reader.BaseStream.Read(properties, 0, 5);

			using (var compressedStream = new LzmaStream(properties, m_Reader.BaseStream))
			{
				var readed = compressedStream.Read(buffer, offset, count);
			}

			m_Reader.Dispose();
			m_Reader = new SwfTagReader(new MemoryStream(buffer));
		}
	}
}