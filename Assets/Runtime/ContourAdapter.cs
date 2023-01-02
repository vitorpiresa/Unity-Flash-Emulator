using System.Collections.Generic;
using UnityEngine;
using Unity.VectorGraphics;

namespace Unity.Flash
{
	public class ContourAdapter
	{
		List<BezierSegment> m_Segments;

		public List<BezierSegment> Segments => m_Segments;
		public Vector2 Begin => m_Segments[0].P0;
		public Vector2 End => m_Segments[m_Segments.Count - 1].P3;

		public ContourAdapter()
		{
			m_Segments = new();
		}

		public BezierContour ToContour()
		{
			var count = m_Segments.Count;
			var closed = m_Segments[0].P0 == m_Segments[count - 1].P3;
			var segments = new BezierPathSegment[closed ? count : count + 1];

			Debug.Log(closed);

			for (int c = 0; c < count; c++)
				segments[c] = new() { P0 = m_Segments[c].P0, P1 = m_Segments[c].P1, P2 = m_Segments[c].P2 };

			if (!closed)
				segments[count] = new() { P0 = m_Segments[count - 1].P0 };

			return new BezierContour()
			{
				Segments = segments,
				Closed = closed
			};
		}
	}
}