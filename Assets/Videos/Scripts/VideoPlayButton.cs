using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayButton : MonoBehaviour
{
    public GameObject playButton;
    public GameObject pauseButton;

    public VideoPlayer videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
       videoPlayer = GameObject.Find("DocentVideoObject").GetComponent<VideoPlayer>();
    }

    public void PlayButtonPressed()
    {
        playButton.SetActive(false);
        pauseButton.SetActive(true);
        videoPlayer.Play();
    }
    public void PauseButtonPressed()
    {
        playButton.SetActive(true);
        pauseButton.SetActive(false);
        videoPlayer.Pause();
    }
}
