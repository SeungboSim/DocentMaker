using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using KaonMaker;

[CustomEditor(typeof(KaonMaker.TCP.Client.KXRTCPClient))]
public class NetworkTCPClientEditor : Editor
{
    public KaonMaker.TCP.Client.KXRTCPClient kXRTCPClient;


    SerializedProperty msgString;

   

    private void OnEnable()
    {
        if (AssetDatabase.Contains(target))
        {
            kXRTCPClient = null;
        }
        else
        {
            kXRTCPClient = (KaonMaker.TCP.Client.KXRTCPClient)target;

            msgString = serializedObject.FindProperty("msgTest");
        }
    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Message test"))
        {
            kXRTCPClient.SendMeesageTest();
        }

    }
}
