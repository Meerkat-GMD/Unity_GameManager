using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_AudioChange : Game_Event
{
    public AudioSource audioSource;
    public AudioClip sound;

    public bool skip=true;
    public bool loop;
    public override IEnumerator Event_Init()
    {
        audioSource.clip = sound;
        audioSource.loop = loop;
        yield return StartCoroutine(Event_Start());
    }


    protected override IEnumerator Event_Content() //collider on off 형식으로
    {
        audioSource.Play();
        while(audioSource.isPlaying && !skip)
        {
            yield return null;
        }
        
        yield return StartCoroutine(Event_Tieup());
    }

    protected override IEnumerator Event_Tieup()
    {
        yield return StartCoroutine(Event_Finish());
    }
}
