using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TaskObject))]
public class TaskObjectEditor : Editor
{
    SerializedProperty headSP;
    SerializedProperty bodySP;
    SerializedProperty ornamentSP;

    HeadType head;
    BodyType body;
    OrnamentType ornament;

    TaskObject taskObject;

    private void OnEnable() {
        headSP = serializedObject.FindProperty("head");
        bodySP = serializedObject.FindProperty("body");
        ornamentSP = serializedObject.FindProperty("ornament");

        taskObject = (TaskObject)target;
    }

    public override void OnInspectorGUI() {


        serializedObject.Update();

        //head.enum
        //headSP.
        //body.DeleteArrayElementAtIndex(3);
        //ornamentSP.DeleteArrayElementAtIndex(3);

        //EditorGUILayout.PropertyField(headSP, true);
        //EditorGUILayout.PropertyField(bodySP, true);
        //EditorGUILayout.PropertyField(ornamentSP, true);

        /*taskObject.taskObjectType = (TaskObjectType)EditorGUILayout.EnumPopup("TaskObjectType:", taskObject.taskObjectType);
        switch (taskObject.taskObjectType) {
            case TaskObjectType.Head:
                taskObject.head = (Heads)EditorGUILayout.EnumPopup("Head type:", taskObject.head);
                taskObject.body = BodyType.None;
                taskObject.ornament = Ornaments.None;
                break;
            case TaskObjectType.Body:
                taskObject.head = Heads.None;
                taskObject.body = (BodyType)EditorGUILayout.EnumPopup("Body type:", taskObject.body);
                taskObject.ornament = Ornaments.None;
                break;
            case TaskObjectType.Ornament:
                taskObject.head = Heads.None;
                taskObject.body = BodyType.None;
                taskObject.ornament = (Ornaments)EditorGUILayout.EnumPopup("Ornament type:", taskObject.ornament);
                break;
        }

        serializedObject.ApplyModifiedProperties();*/
    }
}
