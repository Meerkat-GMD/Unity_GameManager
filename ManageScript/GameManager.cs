using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    private int order = 0;
    private string nextScene="";
    public bool readOnly = false;
    public List<EventManager> ScenePart_Index= new List<EventManager>();

    private void Start()
    {
        StartCoroutine(Execute_Part());
    }

    public IEnumerator Execute_Part()
    {
        while(order < ScenePart_Index.Count)
        {
            Debug.Log("Execute_Part");
            Debug.Log("현재 part" + order);
            yield return StartCoroutine(ScenePart_Index[order].Execute_Event());
            order++;
            Debug.Log("Execute_Part_Finish");
        }
            SceneManager.LoadScene(nextScene);
    }

    public void set_order(int n)
    {
        order = n;
        Debug.Log(order);
    }
    public int get_order()
    {
        return order;
    }

    public void Change_nextScene(string sceneName)
    {
        nextScene = sceneName;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    private GameManager myGameManager;
    int partSize;

    private void OnEnable()
    {
        myGameManager = (GameManager)target;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var list = serializedObject.FindProperty("ScenePart_Index");
        var readOnly = serializedObject.FindProperty("readOnly");

        if (GUILayout.Button("Auto"))
        {
            int child = myGameManager.transform.childCount;
            list.arraySize = 0;
            for (int i = 0; i < child; i++)
            {
                EventManager gamePart = myGameManager.transform.GetChild(i).GetComponent<EventManager>();
                list.InsertArrayElementAtIndex(i);
                list.GetArrayElementAtIndex(i).objectReferenceValue = gamePart;
            }
            
        }

        readOnly.boolValue = GUILayout.Toggle(readOnly.boolValue, "READ ONLY");

        if (readOnly.boolValue)
        {
            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.PropertyField(list, new GUIContent("Part"), true);
            EditorGUI.EndDisabledGroup();

        }
        else
        {
            EditorGUILayout.PropertyField(list, new GUIContent("Part"), true);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif