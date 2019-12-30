using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_TargetMove : Game_Event
{
    public GameObject Target;
    public GameObject Target_Pos;
    public float speed;
    public bool Teleport;

    public override IEnumerator Event_Init()
    {
        yield return StartCoroutine(Event_Start());
    }

    protected override IEnumerator Event_Content()
    {
        if (Teleport)
        {
            Target.GetComponent<Transform>().position = Target_Pos.GetComponent<Transform>().position;
            Target.GetComponent<Transform>().rotation = Target_Pos.GetComponent<Transform>().rotation;
        }
        else
        { //mathf.lerp로 수정해볼까? , rotation 나중에 추가....
            Vector3 dest = Target_Pos.transform.position - Target.transform.position;
            Vector3 dest_cal = dest;
            while (dest_cal.magnitude > 0.1f && Vector3.Dot(dest.normalized, dest_cal.normalized) != -1)
            {
                Target.GetComponent<Transform>().Translate(dest.normalized * Time.deltaTime * speed, Space.Self);
                dest_cal = Target_Pos.transform.position - Target.transform.position;
                yield return null;
            }

        }

        yield return StartCoroutine(Event_Tieup());
    }

    protected override IEnumerator Event_Tieup()
    {
        yield return StartCoroutine(Event_Finish());
    }
}
