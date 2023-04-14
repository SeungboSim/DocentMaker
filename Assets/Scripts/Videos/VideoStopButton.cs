using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoStopButton : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GameObject.Find("DocentVideoObject").GetComponent<VideoPlayer>();
    }

    public void StopButtonPressed()
    {        
        videoPlayer.Stop();
    }
}
