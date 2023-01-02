using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VectorGraphics;
using SwfLib;
using SwfLib.Tags.ShapeTags;
using SwfLib.Shapes.Records;
using SwfLib.Shapes.FillStyles;

namespace Unity.Flash
{
	public class ShapeParser
	{
		int m_X;
		int m_Y;
		int m_ID;
		int m_Style0;
		int m_Style1;
		Rect m_Bounds;
		List<Style> m_AllStyles;
		Dictionary<int, Style> m_Styles;

		Style fillStyle0 => m_Styles[m_Style0];
		Style fillStyle1 => m_Styles[m_Style1];

		public void Parse(DefineShapeTag tag)
		{
			m_ID = tag.ShapeID;
			m_Bounds = tag.ShapeBounds.ToUnityRect();
			m_X = 0;
			m_Y = 0;
			m_Style0 = 0;
			m_Style1 = 0;
			m_Styles = new();
			m_Styles.Add(0, null);
			m_AllStyles = new();

			ParseFillStyles(tag.FillStyles);
			ParseShapeRecords(tag.ShapeRecords);
			Tessellate();
		}

		public void Parse(DefineShape2Tag tag)
		{
			m_ID = tag.ShapeID;
			m_Bounds = tag.ShapeBounds.ToUnityRect();
			m_X = 0;
			m_Y = 0;
			m_Style0 = 0;
			m_Style1 = 0;
			m_Styles = new();
			m_Styles.Add(0, null);
			m_AllStyles = new();

			ParseFillStyles(tag.FillStyles);
			ParseShapeRecords(tag.ShapeRecords);
			Tessellate();
		}

		public void Parse(DefineShape3Tag tag)
		{
			m_ID = tag.ShapeID;
			m_Bounds = tag.ShapeBounds.ToUnityRect();
			m_X = 0;
			m_Y = 0;
			m_Style0 = 0;
			m_Style1 = 0;
			m_Styles = new();
			m_Styles.Add(0, null);
			m_AllStyles = new();

			ParseFillStyles(tag.FillStyles);
			ParseShapeRecords(tag.ShapeRecords);
			Tessellate();
		}

		public void Parse(DefineShape4Tag tag)
		{
			m_ID = tag.ShapeID;
			m_Bounds = tag.ShapeBounds.ToUnityRect();
			m_X = 0;
			m_Y = 0;
			m_Style0 = 0;
			m_Style1 = 0;
			m_Styles = new();
			m_Styles.Add(0, null);
			m_AllStyles = new();

			ParseFillStyles(tag.FillStyles);
			ParseShapeRecords(tag.ShapeRecords);
			Tessellate();
		}

		// Parse Styles RGB

		void ParseFillStyles(IList<FillStyleRGB> fillStyles)
		{
			foreach (var fillStyle in fillStyles)
				if (fillStyle is SolidFillStyleRGB)
					m_Styles.Add(m_Styles.Count, new Style(Parse(fillStyle as SolidFillStyleRGB)));
				else
					m_Styles.Add(m_Styles.Count, new Style(DefaultFill()));
		}

		SolidFill Parse(SolidFillStyleRGB fillStyle)
		{
			return new SolidFill()
			{
				Color = fillStyle.Color.ToUnityColor(),
				Mode = FillMode.NonZero,
				Opacity = 1
			};
		}

		IFill DefaultFill()
		{
			return new SolidFill()
			{
				Color = Color.white,
				Mode = FillMode.NonZero,
				Opacity = 1
			};
		}

		// Parse Styles RGBA

		void ParseFillStyles(IList<FillStyleRGBA> fillStyles)
		{
			foreach(var fillStyle in fillStyles)
				if(fillStyle is SolidFillStyleRGBA)
					m_Styles.Add(m_Styles.Count, Parse(fillStyle as SolidFillStyleRGBA));
				else
					m_Styles.Add(m_Styles.Count, Parse(fillStyle));
		}

		Style Parse(SolidFillStyleRGBA fillStyle)
		{
			var fill = new SolidFill();
			fill.Color = fillStyle.Color.ToUnityColor();
			fill.Mode = FillMode.NonZero;
			fill.Opacity = fill.Color.a;
			return new Style(fill);
		}

		Style Parse(FillStyleRGBA fillStyle)
		{
			var fill = new SolidFill();
			fill.Color = Color.white;
			fill.Mode = FillMode.NonZero;
			fill.Opacity = 1;
			return new Style(fill);
		}

		// Parse Shape Records RGB

		void ParseShapeRecords(IList<IShapeRecordRGB> records)
		{
			foreach (var record in records)
				if (record is StyleChangeShapeRecordRGB)
					Parse(record as StyleChangeShapeRecordRGB);
				else if (record is StraightEdgeShapeRecord)
					Parse(record as StraightEdgeShapeRecord);
				else if (record is CurvedEdgeShapeRecord)
					Parse(record as CurvedEdgeShapeRecord);
				else
					Parse(record as EndShapeRecord);
		}

		void Parse(StyleChangeShapeRecordRGB tag)
		{
			if (tag.StateMoveTo || tag.FillStyle0.HasValue)
				fillStyle0?.Close();

			if (tag.StateMoveTo || tag.FillStyle1.HasValue)
				fillStyle1?.Close();

			if (tag.StateMoveTo)
				(m_X, m_Y) = (tag.MoveDeltaX, tag.MoveDeltaY);

			if(tag.StateNewStyles)
				UpdateFillStyles(tag);

			if (tag.FillStyle0.HasValue)
				m_Style0 = (int)tag.FillStyle0.Value;

			if (tag.StateMoveTo || tag.FillStyle0.HasValue)
				fillStyle0?.New();

			if (tag.FillStyle1.HasValue)
				m_Style1 = (int)tag.FillStyle1.Value;

			if (tag.StateMoveTo || tag.FillStyle1.HasValue)
				fillStyle1?.New();
		}

		void Parse(StraightEdgeShapeRecord tag)
		{
			var begin = new Vector2(m_X, m_Y);
			var end = begin + new Vector2(tag.DeltaX, tag.DeltaY);

			fillStyle0?.Add0(begin, begin, end, end);
			fillStyle1?.Add1(begin, begin, end, end);

			m_X += tag.DeltaX;
			m_Y += tag.DeltaY;
		}

		void Parse(CurvedEdgeShapeRecord tag)
		{
			var begin = new Vector2(m_X, m_Y);
			var control = begin + new Vector2(tag.ControlDeltaX, tag.ControlDeltaY);
			var end = control + new Vector2(tag.AnchorDeltaX, tag.AnchorDeltaY);

			fillStyle0?.Add0(begin, begin, end, end);
			fillStyle1?.Add1(begin, begin, end, end);

			m_X += tag.ControlDeltaX + tag.AnchorDeltaX;
			m_Y += tag.ControlDeltaY + tag.AnchorDeltaY;
		}

		void Parse(EndShapeRecord tag)
		{
			fillStyle0?.Close();
			fillStyle1?.Close();
			m_AllStyles.AddRange(m_Styles.Values);
			m_Styles.Clear();
		}

		// Parse Shape Records RGBA

		void ParseShapeRecords(IList<IShapeRecordRGBA> records)
		{
			foreach (var record in records)
				if (record is StyleChangeShapeRecordRGBA)
					Parse(record as StyleChangeShapeRecordRGBA);
				else if (record is StraightEdgeShapeRecord)
					Parse(record as StraightEdgeShapeRecord);
				else if (record is CurvedEdgeShapeRecord)
					Parse(record as CurvedEdgeShapeRecord);
				else
					Parse(record as EndShapeRecord);
		}
		
		void Parse(StyleChangeShapeRecordRGBA tag)
		{
			if (tag.StateMoveTo || tag.FillStyle0.HasValue)
				fillStyle0?.Close();

			if (tag.StateMoveTo || tag.FillStyle1.HasValue)
				fillStyle1?.Close();

			if (tag.StateMoveTo)
				(m_X, m_Y) = (tag.MoveDeltaX, tag.MoveDeltaY);

			if(tag.StateNewStyles)
				UpdateFillStyles(tag);

			if (tag.FillStyle0.HasValue)
				m_Style0 = (int)tag.FillStyle0.Value;

			if (tag.StateMoveTo || tag.FillStyle0.HasValue)
				fillStyle0?.New();

			if (tag.FillStyle1.HasValue)
				m_Style1 = (int)tag.FillStyle1.Value;

			if (tag.StateMoveTo || tag.FillStyle1.HasValue)
				fillStyle1?.New();
		}

		// Parse Shape Records EX

		void ParseShapeRecords(IList<IShapeRecordEx> records)
		{
			foreach (var record in records)
				if (record is StyleChangeShapeRecordEx)
					Parse(record as StyleChangeShapeRecordEx);
				else if (record is StraightEdgeShapeRecord)
					Parse(record as StraightEdgeShapeRecord);
				else if (record is CurvedEdgeShapeRecord)
					Parse(record as CurvedEdgeShapeRecord);
				else
					Parse(record as EndShapeRecord);
		}
		
		void Parse(StyleChangeShapeRecordEx tag)
		{
			if (tag.StateMoveTo || tag.FillStyle0.HasValue)
				fillStyle0?.Close();

			if (tag.StateMoveTo || tag.FillStyle1.HasValue)
				fillStyle1?.Close();

			if (tag.StateMoveTo)
				(m_X, m_Y) = (tag.MoveDeltaX, tag.MoveDeltaY);

			if(tag.StateNewStyles)
				UpdateFillStyles(tag);

			if (tag.FillStyle0.HasValue)
				m_Style0 = (int)tag.FillStyle0.Value;

			if (tag.StateMoveTo || tag.FillStyle0.HasValue)
				fillStyle0?.New();

			if (tag.FillStyle1.HasValue)
				m_Style1 = (int)tag.FillStyle1.Value;

			if (tag.StateMoveTo || tag.FillStyle1.HasValue)
				fillStyle1?.New();
		}

		// Style Override

		void UpdateFillStyles(StyleChangeShapeRecordRGB record)
		{
			m_AllStyles.AddRange(m_Styles.Values);
			m_Styles.Clear();
			m_Styles.Add(0, null);
			ParseFillStyles(record.FillStyles);
		}

		void UpdateFillStyles(StyleChangeShapeRecordRGBA record)
		{
			m_AllStyles.AddRange(m_Styles.Values);
			m_Styles.Clear();
			m_Styles.Add(0, null);
			ParseFillStyles(record.FillStyles);
		}

		void UpdateFillStyles(StyleChangeShapeRecordEx record)
		{
			m_AllStyles.AddRange(m_Styles.Values);
			m_Styles.Clear();
			m_Styles.Add(0, null);
			ParseFillStyles(record.FillStyles);
		}

		// Tesselation

		void Tessellate()
		{
			// Create Shape List
			var shapes = new List<Shape>(m_AllStyles.Count);
			for (int c = 0; c <  m_AllStyles.Count; c++)
				if (m_AllStyles[c] != null)
					shapes.Add(m_AllStyles[c].ToShape());

			// Create Scene Node
			var node = new SceneNode();
			node.Children = null;
			node.Shapes = shapes;
			node.Transform = Matrix2D.identity;
			node.Clipper = null;

			// Create Scene
			var scene = new Scene();
			scene.Root = node;

			// Create Tessellation Options
			var options = new VectorUtils.TessellationOptions();
			options.MaxCordDeviation = float.MaxValue;
			options.MaxTanAngleDeviation = 0.1f;
			options.SamplingStepSize = 0.01f;
			options.StepDistance = float.MaxValue;

			// Tessellate
			try
			{
				var geometry = VectorUtils.TessellateScene(scene, options);
				var atlas = VectorUtils.GenerateAtlasAndFillUVs(geometry, 32);
				// Debug Meshs
#if true
				var mesh = new Mesh();
				mesh.Clear();
				VectorUtils.FillMesh(mesh, geometry, 100);
				mesh.UploadMeshData(true);
				UnityEditor.AssetDatabase.CreateAsset(mesh, "Assets/Vectors/" + m_ID + ".asset");
#endif
			}
			catch (System.Exception exc)
			{
				Debug.LogException(exc);
			}
		}
	}
}