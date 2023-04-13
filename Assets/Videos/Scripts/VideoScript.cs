using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoScript : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RunTimeVideoData runTimeVideoData;
    public Text scriptText;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GameObject.Find("DocentVideoObject").GetComponent<VideoPlayer>();
        StartCoroutine(VideoScriptRoutine());
    }

    IEnumerator VideoScriptRoutine()
    {
        while (true)
        {
            for (int i = 0; i < runTimeVideoData.allVideoEvents.Count; i++)
            {
                if (i < runTimeVideoData.allVideoEvents.Count - 1)
                {
                    if (videoPlayer.time > runTimeVideoData.allVideoEvents[i].time & videoPlayer.time < runTimeVideoData.allVideoEvents[i + 1].time)
                        scriptText.text = runTimeVideoData.allVideoEvents[i].videoText[0].ToString();
                }
            }
            yield return null;
        }
    }
}
