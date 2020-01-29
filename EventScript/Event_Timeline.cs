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
        {
            Timeline.enabled = true;
            Timeline.Play();
        }
            

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
        {
            Ani.enabled = false;
            // 여기도 초기화 해주는 코드 필요?
            Ani.Stop();
        }

        if (Timeline != null)
        {
            Timeline.time = 0;
            Timeline.Stop();
            Timeline.enabled = false;
            
        }
            
        Debug.Log("Timline finish");
        yield return StartCoroutine(Event_Finish());
    }
}
