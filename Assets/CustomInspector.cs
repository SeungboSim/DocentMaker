using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System;


[Serializable]
public class DocentVideo
{
    [SerializeField]
    public VideoClip docentVideoClip;
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

public class CustomInspector : MonoBehaviour
{
    [HideInInspector] [SerializeField] ObjectType _objectType;
    [HideInInspector] [SerializeField] private DocentVideo _docentVideoInfo;
    [HideInInspector] [SerializeField] private Test _testInfo;

    public void CreateDocentVideo()
    {
        Instantiate(gameObject);
        //구현내용
        //Debug.Log(_docentVideoInfo.docentVideoClip.name);
    }
}
