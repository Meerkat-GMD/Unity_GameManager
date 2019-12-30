using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_GameDataInit : Game_Event
{
    public GameData GD;

    public override IEnumerator Event_Init()
    {
        yield return StartCoroutine(Event_Start());
    }

    protected override IEnumerator Event_Content()
    {
        GD.init();
        yield return StartCoroutine(Event_Tieup());
    }

    protected override IEnumerator Event_Tieup()
    {
        yield return StartCoroutine(Event_Finish());
    }
}
