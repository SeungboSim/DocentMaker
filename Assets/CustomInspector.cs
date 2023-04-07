using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System;
using UnityEngine.UI;

[Serializable]
public class DocentVideo
{
    [SerializeField] public VideoClip docentVideoClip;
    [SerializeField] public PixelSize pixelSize;
    [SerializeField] [EnumFlags] public EButtonOption buttonOption;
    [SerializeField] public GameObject stopButton;
    [SerializeField] public GameObject playButton;
    [SerializeField] public GameObject jumpButton;
    [SerializeField] public GameObject slider;
}

[Serializable]
public class Test
{

}

public enum ObjectType
{
    DocentVideo,
    Test
}

public enum EButtonOption
{
    Stop = 0x00000001,   // 0001
    Play = 0x00000002,   // 0010
    Jump = 0x00000004,   // 0100
    Slider = 0x00000008,   // 1000
}
public enum PixelSize
{
    [InspectorName("16:9")]
    a,
    [InspectorName("21:9")]
    b
}

public class CustomInspector : MonoBehaviour
{
    [HideInInspector] [SerializeField] ObjectType _objectType;
    [HideInInspector] [SerializeField] private DocentVideo _docentVideoInfo;
    [HideInInspector] [SerializeField] private Test _testInfo;

    public GameObject emptyQuadObject;
    public GameObject emptyCanvas;

    public void CreateDocentVideo()
    {
        GameObject docentObject = new GameObject("DocentObject");
        GameObject docentCanvas = Instantiate(emptyCanvas, docentObject.transform);
        docentCanvas.gameObject.name = "DocentCanvas";
        GameObject docentVideoObject = Instantiate(emptyQuadObject);
        docentVideoObject.gameObject.name = "DocentVideoObject";
        docentVideoObject.transform.parent = docentCanvas.transform;
        docentVideoObject.transform.localPosition = new Vector3(0, 0, 0);

        switch (_docentVideoInfo.pixelSize)
        {
            case PixelSize.a:
                {
                    docentVideoObject.transform.localScale = new Vector3(1600f, 900f, 1f);
                    break;
                }
            case PixelSize.b:
                {
                    docentVideoObject.transform.localScale = new Vector3(2100f, 900f, 1f);
                    break;
                }
        }

        VideoPlayer videoPlayer = docentVideoObject.AddComponent<VideoPlayer>();
        videoPlayer.clip = _docentVideoInfo.docentVideoClip;

        if (_docentVideoInfo.buttonOption != 0)
        {
            GameObject docentVideoButtonBox = new GameObject("DocentVideoButtonBox");
            docentVideoButtonBox.AddComponent<HorizontalLayoutGroup>();
            docentVideoButtonBox.transform.SetParent(docentCanvas.transform);
            docentVideoButtonBox.transform.localScale = new Vector3(1f, 1f, 1f);
            docentVideoButtonBox.GetComponent<RectTransform>().sizeDelta = new Vector2(docentVideoObject.transform.localScale.x, 100f);
            docentVideoButtonBox.transform.localPosition = new Vector3(0, -490, 0);
            docentVideoButtonBox.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;

            if ((_docentVideoInfo.buttonOption & EButtonOption.Stop) == EButtonOption.Stop)
                Instantiate(_docentVideoInfo.stopButton, docentVideoButtonBox.transform).transform.localScale = new Vector3(1f, 1f, 1f); ;
            if ((_docentVideoInfo.buttonOption & EButtonOption.Play) == EButtonOption.Play)
                Instantiate(_docentVideoInfo.playButton, docentVideoButtonBox.transform).transform.localScale = new Vector3(1f, 1f, 1f); ;
            if ((_docentVideoInfo.buttonOption & EButtonOption.Jump) == EButtonOption.Jump)
                Instantiate(_docentVideoInfo.jumpButton, docentVideoButtonBox.transform).transform.localScale = new Vector3(1f, 1f, 1f); ;
            if ((_docentVideoInfo.buttonOption & EButtonOption.Slider) == EButtonOption.Slider)
                Instantiate(_docentVideoInfo.slider, docentVideoButtonBox.transform).transform.localScale = new Vector3(1f, 1f, 1f); ;
        }
    }
}
