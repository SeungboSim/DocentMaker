using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using KaonMaker.TCP;


[CustomEditor(typeof(KXRNetwork))]
public class KXRNetworkEditor : Editor
{
    public KXRNetwork kaonMakerNetBase;
    public Texture2D logo;
    public NetEnum netEnum;


    private void OnEnable()
    {
        if (AssetDatabase.Contains(target))
        {
            kaonMakerNetBase = null;
        }
        else
        {
            kaonMakerNetBase = (KXRNetwork)target;
            logo = (Texture2D)Resources.Load("Textures/Logo");
        }
    }

    public override void OnInspectorGUI()
    {
        ////////////////////////////////////////////////////////////////////////////////////
        Color setColor = new Color(1.0f, 0.64f, 0.0f);
        GUIStyle allEditStyle = new GUIStyle(GUI.skin.label);
        allEditStyle.normal.textColor = setColor;
        allEditStyle.fontStyle = FontStyle.Bold;
        ////////////////////////////////////////////////////////////////////////////////////
        
        ////////////////////////////////////////////////////////////////////////////////////
        #region LogoImage
        GUILayout.BeginHorizontal("box");
        GUIStyle myStyle = new GUIStyle(GUI.skin.label);
        myStyle.alignment = TextAnchor.MiddleCenter;
        GUILayout.FlexibleSpace();
        GUILayout.Label(logo, myStyle, GUILayout.Height(60), GUILayout.Width(100));
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////////////////
        #region version Info
        GUILayout.BeginHorizontal("box");
        GUIStyle versionStyle = new GUIStyle(GUI.skin.label);
        versionStyle.alignment = TextAnchor.MiddleRight;
        EditorGUILayout.LabelField("kaonMaker [Version 0.1]", versionStyle);
        GUILayout.EndHorizontal();
        #endregion
        ////////////////////////////////////////////////////////////////////////////////////
    }

}
