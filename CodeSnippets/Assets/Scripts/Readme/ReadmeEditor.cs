#if UNITY_EDITOR

using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(Readme))]
public class ReadmeEditor : Editor
{
    private bool isEditing;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty textProperty = serializedObject.FindProperty("text");

        EditorGUILayout.Space();

        if (isEditing)
        {
            EditorGUILayout.LabelField("Readme", EditorStyles.boldLabel);

            GUIStyle textAreaStyle = new GUIStyle(EditorStyles.textArea)
            {
                wordWrap = true,
                fontSize = 16 // adjust as needed
            };

            textProperty.stringValue = EditorGUILayout.TextArea(
                textProperty.stringValue,
                textAreaStyle,
                GUILayout.MinHeight(120)
            );

            EditorGUILayout.Space();

            if (GUILayout.Button("Save"))
            {
                isEditing = false;
                GUI.FocusControl(null);
            }
        }
        else
        {
            DrawFormattedReadme(textProperty.stringValue);

            EditorGUILayout.Space();

            if (GUILayout.Button("Edit"))
            {
                isEditing = true;
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawFormattedReadme(string text)
    {
        GUIStyle displayStyle = new GUIStyle(EditorStyles.helpBox)
        {
            richText = true,
            wordWrap = true,
            fontSize = 16,
            padding = new RectOffset(10, 10, 8, 8)
        };

        EditorGUILayout.LabelField(text, displayStyle);
    }
}

public static class ReadmeEditorUtility
{
    public static void MoveReadmeBelowTransform(Readme readme)
    {
        if (readme == null)
        {
            return;
        }

        EditorApplication.delayCall += () =>
        {
            if (readme == null)
            {
                return;
            }

            while (ComponentUtility.MoveComponentUp(readme))
            {
                // Moves as high as Unity permits.
                // Transform will remain above it.
            }
        };
    }
}

#endif