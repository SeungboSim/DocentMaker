using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(CustomInspector))]

public class ObjectInspectorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        // _objectType 필드를 가져옵니다.
        var objectType = (ObjectType)serializedObject.FindProperty("_objectType").intValue;

        // _objectType 필드를 Inspector에 노출 시켜줍니다.
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_objectType"));

        // Inspector에서 ObjectType Enum을 변경하게 되면 해당 타입에 맞게 노출시켜줄 필드를 정의해줍니다.
        switch (objectType)
        {
            case ObjectType.DocentVideo:
                {
                    GUIStyle style = EditorStyles.helpBox;
                    GUILayout.BeginVertical(style);

                    // DocentVideo는 클래스이므로 '_docentVideoInfo.name' 형태로 클래스 내부에 접근할 수 있습니다.
                    // 프로퍼티를 가져옴과 동시에 Inspector에 노출시켜 줍니다.
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_docentVideoInfo.docentVideoClip"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_docentVideoInfo.pixelSize"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_docentVideoInfo.buttonOption"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_docentVideoInfo.stopButton"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_docentVideoInfo.playButton"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_docentVideoInfo.jumpButton"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_docentVideoInfo.slider"));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_docentVideoInfo.scriptBox"));

                    var eButtonOptions = (EButtonOption)serializedObject.FindProperty("_docentVideoInfo.buttonOption").intValue;
                    if ((eButtonOptions & EButtonOption.Script) == EButtonOption.Script)
                        EditorGUILayout.PropertyField(serializedObject.FindProperty("_docentVideoInfo.scripts"));

                    CustomInspector customInspector = (CustomInspector)target;
                    GUILayout.FlexibleSpace(); // 고정된 여백을 넣습니다. ( 버튼이 가운데 오기 위함)
                    EditorGUILayout.Space();
                    if (GUILayout.Button("오브젝트 생성"))
                    {
                        //버튼 누를시 해당 명령을 구현해줍니다.
                        customInspector.CreateDocentVideo();
                    }
                    GUILayout.FlexibleSpace();  // 고정된 여백을 넣습니다.
                    EditorGUILayout.EndHorizontal();  // 가로 생성 끝
                }
                break;

            case ObjectType.Test:
                {                   
                    
                }
                break;
        }

        // 변경된 프로퍼티를 저장해줍니다.
        serializedObject.ApplyModifiedProperties();        
    }
}