using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_ObjectOnOff : Game_Event
{
    [Tooltip("선택시 Object를 활성화 / 비 선택시 Object를 비활성화")]
    public bool Object_On = true;
    [Tooltip("활성화/비활성화 할 Object 대상")]
    public List<GameObject> Target = new List<GameObject>();

    public override IEnumerator Event_Init()
    {
        yield return StartCoroutine(Event_Start());
    }

    protected override IEnumerator Event_Content() //collider on off 형식으로
    {
        int order = 0;
        while (Target[order] != null)
        {
            Target[order].SetActive(Object_On);
            order++;
        }
        yield return StartCoroutine(Event_Tieup());
    }

    protected override IEnumerator Event_Tieup()
    {
        yield return StartCoroutine(Event_Finish());
    }
}
