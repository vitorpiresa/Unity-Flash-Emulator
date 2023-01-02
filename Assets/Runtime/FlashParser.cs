using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwfLib;
using SwfLib.Tags.ShapeTags;
using System.Text;

namespace Unity.Flash
{
	public class FlashParser
	{
		public void Parse(SwfFile file)
		{
			LogHeader(file);
			LogTags(file);

			foreach (var tag in file.Tags)
			{
				try
				{
					if (tag is DefineShapeTag)
						new ShapeParser().Parse(tag as DefineShapeTag);
					else if (tag is DefineShape2Tag)
						new ShapeParser().Parse(tag as DefineShape2Tag);
					else if(tag is DefineShape3Tag)
						new ShapeParser().Parse(tag as DefineShape3Tag);
					else if(tag is DefineShape4Tag)
						new ShapeParser().Parse(tag as DefineShape4Tag);
				}
				catch(System.Exception exception)
				{
					Debug.LogException(exception);
				}
			}
		}

		void LogHeader(SwfFile file)
		{
			var sb = new StringBuilder();
			sb.Append("Signature: ");
			sb.Append(file.FileInfo.Format);
			sb.AppendLine();
			sb.Append("Version: ");
			sb.Append(file.FileInfo.Version);
			sb.AppendLine();
			sb.Append("File length: ");
			sb.Append(file.FileInfo.FileLength);
			sb.AppendLine();
			sb.Append("Frame size: ");
			sb.Append(file.Header.FrameSize);
			sb.AppendLine();
			sb.Append("Frame rate: ");
			sb.Append(file.Header.FrameRate);
			sb.AppendLine();
			sb.Append("Frame count: ");
			sb.Append(file.Header.FrameCount);
			sb.AppendLine();
			Debug.Log(sb.ToString());
		}

		void LogTags(SwfFile file)
		{
			var sb = new StringBuilder();
			foreach (var tag in file.Tags)
				sb.AppendLine(tag.TagType.ToString());
			Debug.Log(sb);
		}
	}
}