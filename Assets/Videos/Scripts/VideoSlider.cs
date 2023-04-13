using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoSlider : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    Slider slider;
    public bool sliderControl = false;

    void Start()
    {
        videoPlayer = GameObject.Find("DocentVideoObject").GetComponent<VideoPlayer>();
        slider = GetComponent<Slider>();
        slider.maxValue = (float)videoPlayer.length;
        StartCoroutine(VideoSliderRoutine());
    }

    IEnumerator VideoSliderRoutine()
    {
        while (true)
        {
            if (!sliderControl)
                slider.value = (float)videoPlayer.time;
            yield return null;
        }
    }

    public void SliderHandleControl()
    {
        sliderControl = true;
        videoPlayer.time = (double)slider.value;
    }
    public void SliderHandleControlEnd()
    {
        sliderControl = false;
    }
}
