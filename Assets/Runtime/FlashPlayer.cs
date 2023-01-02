using System;
using UnityEngine;
using SwfLib;

namespace Unity.Flash
{
	public class FlashPlayer : MonoBehaviour
	{
		[SerializeField] RenderTexture m_Target;

		[NonSerialized] SwfFile m_File;

		public SwfFile file
		{
			get => m_File;
			set
			{
				m_File = value;
				Initialize();
			}
		}

		void Initialize()
		{
			if (m_File == null)
				return;
			new FlashParser().Parse(file);
		}

		void OnEnable()
		{

		}

		void OnDisable()
		{

		}

		void Update()
		{

		}
	}
}