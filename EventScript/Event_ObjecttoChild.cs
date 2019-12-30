using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_ObjecttoChild : Game_Event
{
    public GameObject Parent;
    public GameObject Child;

    public override IEnumerator Event_Init()
    {
        yield return StartCoroutine(Event_Start());
    }

    protected override IEnumerator Event_Content() //이유는 모르겟지만 꺼져있는 object를 부모로 삼으면 GameManager 실행하는데 있어서 오류가 발생함
    {
        if (Parent != null)
            Child.transform.parent = Parent.GetComponent<Transform>();
        else
            Child.transform.parent = null;
        yield return StartCoroutine(Event_Tieup());
    }

    protected override IEnumerator Event_Tieup()
    {
        yield return StartCoroutine(Event_Finish());
    }
}
