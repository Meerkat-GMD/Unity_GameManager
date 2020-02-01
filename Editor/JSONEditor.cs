using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using LitJson;
using System.IO;

public class JSONScriptEditor : EditorWindow
{

    [MenuItem(itemName: "JSONEditorList", menuItem = "JSONEditor/JSON Script Editor")]
    public static void Init() { GetWindow<JSONScriptEditor>("JSON Script Editor", true); }

    Editor editor;

    [SerializeField] List<JSONScript> ScriptData = new List<JSONScript>();
    void OnGUI()
    {
        if (!editor) { editor = Editor.CreateEditor(this); }
        if (editor) { editor.OnInspectorGUI(); }
    }

    void OnInspectorUpdate() { Repaint(); }
}

[System.Serializable]
public class JSONScript
{
    public int PartNumber;
    public int ScriptOrder;
    public string Script;
    
}

[CustomEditor(typeof(JSONScriptEditor), true)]
public class JSONScriptEditorDrawer : Editor
{
    string Filename = "File Name : ";
    public override void OnInspectorGUI()
    {
        var list = serializedObject.FindProperty("ScriptData");
        EditorGUILayout.LabelField(Filename);

        if (GUILayout.Button("New"))
        {
            Filename = "";
            string path = EditorUtility.SaveFilePanel("Find Script JSON File", Application.dataPath, "", "json");
            Filename = path;
            System.IO.File.Create(Filename);
            //File.CreateText(Filename);
        }
        if (GUILayout.Button("Save"))
        {
            List<JSONScript> temp_jsonscript = new List<JSONScript>();
            for(int i =0; i < list.arraySize; i++)
            {
                SerializedProperty ScriptListRef = list.GetArrayElementAtIndex(i);
                SerializedProperty PartNumberRef = ScriptListRef.FindPropertyRelative("PartNumber");
                SerializedProperty ScriptOrderRef = ScriptListRef.FindPropertyRelative("ScriptOrder");
                SerializedProperty ScriptRef = ScriptListRef.FindPropertyRelative("Script");

                JSONScript temp = new JSONScript();
                temp.PartNumber = PartNumberRef.intValue;
                temp.ScriptOrder = ScriptOrderRef.intValue;
                temp.Script = ScriptRef.stringValue;

                temp_jsonscript.Add(temp);

            }

            JsonData ScriptJSON = JsonMapper.ToJson(temp_jsonscript);
            File.WriteAllText(Filename, ScriptJSON.ToString());
            /*
            for(int i=0; i< list.arraySize; i++)
            {
                JsonData ScriptJSON = JsonMapper.ToJson(list);
                File.WriteAllText(Filename, ScriptJSON.ToString());
            }
            */
            //string path = EditorUtility.OpenFolderPanel("Find Script JSON File", "", "txt");
        }
        if (GUILayout.Button("Load"))
        {
            string path = EditorUtility.OpenFilePanel("Find Script JSON File", "", "json");
            Filename = path;
            string JSONString = File.ReadAllText(path);

            JsonData ScriptData = JsonMapper.ToObject(JSONString);
            list.ClearArray();
            list.arraySize = ScriptData.Count;
            for(int i = 0; i<ScriptData.Count; i++)
            {
                SerializedProperty ScriptListRef = list.GetArrayElementAtIndex(i);
                SerializedProperty PartNumberRef = ScriptListRef.FindPropertyRelative("PartNumber");
                PartNumberRef.intValue = (int)ScriptData[i]["PartNumber"];
                SerializedProperty ScriptOrderRef = ScriptListRef.FindPropertyRelative("ScriptOrder");
                ScriptOrderRef.intValue = (int)ScriptData[i]["ScriptOrder"];
                SerializedProperty ScriptRef = ScriptListRef.FindPropertyRelative("Script");
                ScriptRef.stringValue = ScriptData[i]["Script"].ToString();
               
            }
        }

         EditorGUILayout.PropertyField(list, new GUIContent("Game Script JSON"), true);

        this.Repaint();
    }
}
