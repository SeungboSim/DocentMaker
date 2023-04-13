using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RunTime Video Data", menuName = "Scirptable Object/RunTime Video Data", order = int.MaxValue)]
public class RunTimeVideoData : ScriptableObject
{
	[System.Serializable]
	public struct VideoEvent
	{
		public float time; // 이벤트 타임
		public int playbackSpeed;
		public int animatorNum;
		public string[] videoText; // 이벤트시 보여질 텍스트		 
	}

	public List<VideoEvent> allVideoEvents;
}
