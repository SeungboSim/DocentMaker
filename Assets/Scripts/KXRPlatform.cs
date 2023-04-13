using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KXRPlatform : MonoBehaviour
{
    private static KXRPlatform Instance = null;

    public static KXRPlatform instance
    {
        get
        {
            if (null == Instance)
            {
                return null;
            }
            return Instance;
        }
    }

    /// <summary>
    /// Class Declaration
    /// </summary>
    #region Managed Class Declaration

    #endregion

    /// <summary>
    /// Global Variable Declaration
    /// </summary>
    #region Global Variable Declaration

    #endregion

    private void Awake()
    {
        if (null == Instance)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }

        Init();
    }

    public CustomInspector CustomInspector;

    private bool Init()
    {
        CustomInspector = GameObject.Find("CustomInspector").GetComponent<CustomInspector>();

        return true;
    }
    #region Member Method Declaratives
#if UNITY_STANDALONE


#elif UNITY_ANDROID
    /// <summary>
    /// This function makes it possible to call when building a fixed screen.
    /// You can choose from a drop-down format in the editor, and you can call it from anywhere.
    /// </summary>
    /// <param name="orName"></param>
    public void OrientationChanged(string orName)
    {
        if (orName == "Portrait")
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
        else if (orName == "LandscapeLeft")
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
        else if (orName == "LandscapeRight")
        {
            Screen.orientation = ScreenOrientation.LandscapeRight;
        }
        else
        {
            Debug.Log("Chacking your Orientations Name");
        }
    }

#endif
    #endregion
}