using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Event_GameDataInit : Game_Event
{
    public GameData GD;
    public PlayableDirector Timeline;

    public override IEnumerator Event_Init()
    {
        yield return StartCoroutine(Event_Start());
    }

    protected override IEnumerator Event_Content()
    {
        Timeline.enabled = true;
        Timeline.Stop();
        Timeline.Evaluate();
        Timeline.enabled = false;
        yield return StartCoroutine(Event_Tieup());
    }

    protected override IEnumerator Event_Tieup()
    {
        yield return StartCoroutine(Event_Finish());
    }
}
