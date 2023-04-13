using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RunTime Video Data", menuName = "Scirptable Object/RunTime Video Data", order = int.MaxValue)]
public class RunTimeVideoData : ScriptableObject
{
	[System.Serializable]
	public struct VideoEvent
	{
		public float time; // �̺�Ʈ Ÿ��
		public int playbackSpeed;
		public int animatorNum;
		public string[] videoText; // �̺�Ʈ�� ������ �ؽ�Ʈ		 
	}

	public List<VideoEvent> allVideoEvents;
}
