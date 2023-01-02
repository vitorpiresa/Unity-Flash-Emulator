using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwfLib.Data;

namespace Unity.Flash
{
	public static class Extensions
	{
		public static Rect ToUnityRect(this SwfRect rect)
		{
			return Rect.MinMaxRect(rect.XMin, rect.YMin, rect.XMax, rect.YMax);
		}

		public static Color ToUnityColor(this SwfRGB rgb)
		{
			return new Color(rgb.Red / 255f, rgb.Green / 255f, rgb.Blue / 255f, 1);
		}

		public static Color32 ToUnityColor32(this SwfRGB rgb)
		{
			return new Color32(rgb.Red, rgb.Green, rgb.Blue, 0xFF);
		}

		public static Color32 ToUnityColor32(this SwfRGBA rgba)
		{
			return new Color32(rgba.Red, rgba.Green, rgba.Blue, rgba.Alpha);
		}

		public static Color ToUnityColor(this SwfRGBA rgba)
		{
			return new Color(rgba.Red / 255f, rgba.Green / 255f, rgba.Blue / 255f, rgba.Alpha / 255f);
		}
	}
}