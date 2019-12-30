using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Event_Timeline : Game_Event
{
    public PlayableDirector Timeline;
    public Animation Ani;
    public override IEnumerator Event_Init()
    {
        if(Ani  != null)
            Ani.enabled = true;

        if(Timeline!=null)
            Timeline.enabled = true;

        yield return StartCoroutine(Event_Start());
    }

    protected override IEnumerator Event_Content()
    {
        while (Ani != null && Ani.isPlaying)
            yield return null;
        while (Timeline != null && Timeline.state == PlayState.Playing)
             yield return null;
        
        yield return StartCoroutine(Event_Tieup());
    }

    protected override IEnumerator Event_Tieup()
    {
        if (Ani != null)
            Ani.enabled = false;

        if (Timeline != null)
            Timeline.enabled = false;


        Debug.Log("Timline finish");
        yield return StartCoroutine(Event_Finish());
    }
}
