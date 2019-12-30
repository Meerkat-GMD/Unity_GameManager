using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif // UNITY_EDITOR

public enum Delayfactor
{
    Time,
    Input,
    Execute
}

public class Event_WaitforSatisfaction : Game_Event
{
    public Delayfactor factor;
    //각 요소들에 대한 변수들 여기에 추가하기

    //Time
    public double Delaytime = 0;
	
    //Input
    public bool Satisfy = false;

    public override IEnumerator Event_Init()
    {
        Debug.Log("Event_Satisfaciton start");
        yield return StartCoroutine(Event_Start());
    }


    protected override IEnumerator Event_Content()
    {
        switch(factor)
        {
            case Delayfactor.Input:
                while(!Satisfy)
                {
                    yield return null;
                }
                break;

            case Delayfactor.Execute:
                break;
        }
        yield return StartCoroutine(Event_Tieup());
    }

    protected override IEnumerator Event_Tieup()
    {
        Satisfy = false;
        yield return StartCoroutine(Event_Finish());
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(Event_WaitforSatisfaction))]
public class MyScriptEditor : Editor
{

    override public void OnInspectorGUI()
    {
        serializedObject.Update();
        var myScript = target as Event_WaitforSatisfaction;
        var factor = serializedObject.FindProperty("factor");

        EditorGUILayout.PropertyField(factor);

        //각 요소들에 대한 변수 보여주는곳
        switch (myScript.factor)
        {

            case Delayfactor.Time:
                //myScript.Delaytime = EditorGUILayout.DoubleField("Delaytime", myScript.Delaytime);
                var delaytime = serializedObject.FindProperty("Delaytime");
                EditorGUILayout.PropertyField(delaytime);
                break;

            case Delayfactor.Input:
                break;
            case Delayfactor.Execute:
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
#endif // UNITY_EDITOR