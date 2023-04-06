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

        // _objectType �ʵ带 �����ɴϴ�.
        var objectType = (ObjectType)serializedObject.FindProperty("_objectType").intValue;
        // _objectType �ʵ带 Inspector�� ���� �����ݴϴ�.
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_objectType"));

        // Inspector���� ObjectType Enum�� �����ϰ� �Ǹ� �ش� Ÿ�Կ� �°� ��������� �ʵ带 �������ݴϴ�.
        switch (objectType)
        {
            case ObjectType.DocentVideo:
                {
                    GUIStyle style = EditorStyles.helpBox;
                    GUILayout.BeginVertical(style);

                    // DocentVideo�� Ŭ�����̹Ƿ� '_docentVideoInfo.name' ���·� Ŭ���� ���ο� ������ �� �ֽ��ϴ�.
                    // ������Ƽ�� �����Ȱ� ���ÿ� Inspector�� ������� �ݴϴ�.
                    EditorGUILayout.PropertyField(serializedObject.FindProperty("_docentVideoInfo.docentVideoClip"));

                    CustomInspector customInspector = (CustomInspector)target;
                    GUILayout.FlexibleSpace(); // ������ ������ �ֽ��ϴ�. ( ��ư�� ��� ���� ����)
                    
                    if (GUILayout.Button("������Ʈ ����"))
                    {
                        //��ư ������ �ش� ������ �������ݴϴ�.
                        customInspector.CreateDocentVideo();
                    }
                    GUILayout.FlexibleSpace();  // ������ ������ �ֽ��ϴ�.
                    EditorGUILayout.EndHorizontal();  // ���� ���� ��
                }
                break;

            case ObjectType.Test:
                {                   
                    
                }
                break;
        }

        // ����� ������Ƽ�� �������ݴϴ�.
        serializedObject.ApplyModifiedProperties();        
    }
}