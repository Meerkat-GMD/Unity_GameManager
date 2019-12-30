using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_ColliderOnoff : Game_Event
{
    //public Collider Target; // collider 있는 object
    public bool Collider_On = true;
    public List<Collider> Target = new List<Collider>();
  
    public override IEnumerator Event_Init()
    {
        yield return StartCoroutine(Event_Start());
    }


    protected override IEnumerator Event_Content() //collider on off 형식으로
    {
        int order = 0;
        while (Target[order] != null)
        {
            Target[order].enabled = Collider_On;
            order++;
        }
        yield return null;
        yield return StartCoroutine(Event_Tieup());
    }

    protected override IEnumerator Event_Tieup()
    {
        yield return StartCoroutine(Event_Finish());
    }
}
