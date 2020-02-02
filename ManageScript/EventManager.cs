using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif



[System.Serializable]
public class EventManager : MonoBehaviour
{
    private int order = 0;
    public bool readOnly = false;
    public List<Game_Event> Event_Index = new List<Game_Event>();
    public IEnumerator Execute_Event()
    {
        Debug.Log("part 이름"+gameObject.name);
        while(order < Event_Index.Count)
        {
            Debug.Log("Execute_Event_Start");
            yield return StartCoroutine(Event_Index[order].Event_Init());
            order++;
            Debug.Log("Execute_Event_Finish");
        }
        order = 0;
    }

    public void Exit_Event()
    {
        order = Event_Index.Count;
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(EventManager))]
public class EventManagerEditor : Editor
{
    private EventManager myEventManager;

    private void OnEnable()
    {
        myEventManager = target as EventManager;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var list = serializedObject.FindProperty("Event_Index");
        var readOnly = serializedObject.FindProperty("readOnly");
        if (GUILayout.Button("Auto"))
        {
            int child = myEventManager.transform.childCount;
            list.arraySize = 0;
            for (int i = 0; i < child; i++)
            {
                Game_Event gameEvent = myEventManager.transform.GetChild(i).GetComponent<Game_Event>();
                list.InsertArrayElementAtIndex(i);
                list.GetArrayElementAtIndex(i).objectReferenceValue = gameEvent;
            }
        }

        readOnly.boolValue = GUILayout.Toggle(readOnly.boolValue, "READ ONLY");

        if (readOnly.boolValue)
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(list, true);
            EditorGUI.EndDisabledGroup();

        }
        else
        {
            EditorGUILayout.PropertyField(list, true);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif