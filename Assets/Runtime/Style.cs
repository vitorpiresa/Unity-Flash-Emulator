using System.Collections.Generic;
using UnityEngine;
using Unity.VectorGraphics;

namespace Unity.Flash
{
	public class Style
	{
		IFill m_Fill;
		List<ContourAdapter> m_Contours;
		ContourAdapter m_CurrentContour;

		public Style(IFill fill)
		{
			m_Fill = fill;
			m_Contours = new();
			m_CurrentContour = null;
		}

		public void New()
		{
			m_CurrentContour = new();
		}

		public void Add0(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
		{
			m_CurrentContour.Segments.Insert(0, new() { P0 = p3, P1 = p2, P2 = p1, P3 = p0 });
		}

		public void Add1(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
		{
			m_CurrentContour.Segments.Add(new() { P0 = p0, P1 = p1, P2 = p2, P3 = p3 });
		}

		public void Close()
		{
			if(m_CurrentContour.Segments.Count > 0)
				m_Contours.Add(m_CurrentContour);
			m_CurrentContour = null;
		}

		public Shape ToShape()
		{
			var contours = new List<ContourAdapter>();

			while(m_Contours.Count > 0)
			{
				var mainContour = m_Contours[0];
				var find = true;
				m_Contours.RemoveAt(0);

				while(find && m_Contours.Count > 0)
				{
					find = false;
					for(int c = 0; !find && c < m_Contours.Count; c++)
					{
						find = mainContour.End == m_Contours[c].Begin;

						if(!find)
							continue;

						mainContour.Segments.AddRange(m_Contours[c].Segments);
						m_Contours.RemoveAt(c);
					}
				}

				contours.Add(mainContour);
			}

			var fcontours = new BezierContour[contours.Count];
			for(int c = 0; c < contours.Count; c++)
				fcontours[c] = contours[c].ToContour();
			
			var shape = new Shape();
			shape.Contours = fcontours;
			shape.Fill = m_Fill;
			shape.FillTransform = Matrix2D.identity;
			shape.PathProps = default;
			shape.IsConvex = false;

			return shape;
		}
	}
}