using System.IO;
using UnityEngine;
using SwfLib;

namespace Unity.Flash
{
	[RequireComponent(typeof(FlashPlayer))]
	public class FlashLoader : MonoBehaviour
	{
		[SerializeField] string m_Path = "";
		[HideInInspector] FlashPlayer m_FlashPlayer;

		FlashPlayer flashPlayer => m_FlashPlayer ? m_FlashPlayer : GetComponent<FlashPlayer>();

		void Awake()
		{
			var path = Path.Combine(Application.streamingAssetsPath, m_Path);
			var file = SwfFile.ReadFrom(File.OpenRead(path));
			flashPlayer.file = file;
		}

#if UNITY_EDITOR
		void Reset()
		{
			TryGetComponent<FlashPlayer>(out m_FlashPlayer);
		}
#endif
	}
}