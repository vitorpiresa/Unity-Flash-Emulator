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
			Debug.developerConsoleEnabled = true;
			Debug.developerConsoleVisible = true; 
		}

		void OnMouseUpAsButton() => Load();

		public void Load()
		{
			var path = Path.Combine(Application.streamingAssetsPath, m_Path);
			print(m_Path);
			print(path);

			//ILogger
			//ILogHandler

			using (var loader = new SwfLoader(path))
			{

			}
		}
	}
}