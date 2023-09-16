using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Lab5.Swf;

namespace Lab5.Swf
{
	public class FlashPlayer : MonoBehaviour
	{
		[SerializeField] string m_Path;

		void Awake()
		{
			Application.targetFrameRate = 20;
		}

		void OnMouseUpAsButton() => Load();

		public void Load()
		{
			var path = Path.Combine(Application.streamingAssetsPath, m_Path);

			using (var loader = new SwfLoader(path))
			{
				
			}
		}
	}
}