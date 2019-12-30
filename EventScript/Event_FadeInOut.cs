using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Event_FadeInOut : Game_Event
{
    public bool FadeIn = true;
    public float FadeTime = 2f;
    public GameObject FadeUI;
    private float time =0f;
    private float start=0f;
    private float end=1f;

    public Image FadeImg;

    public override IEnumerator Event_Init()
    {
        if(FadeImg != null)
        FadeImg.enabled = true;
        if(FadeUI != null)
        FadeUI.SetActive(true);
       
        yield return StartCoroutine(Event_Start());
    }

    protected override IEnumerator Event_Content() //collider on off 형식으로
    {
        Color fadecolor = FadeImg.color;

        if (FadeIn)
        {
            time = 1f;
            while (fadecolor.a >0f)
            {
                time -= Time.deltaTime / FadeTime;

                fadecolor.a = Mathf.Lerp(start, end, time);
                //Debug.Log(fadecolor.a);

                FadeImg.color = fadecolor;
                yield return null;
            }
        }
        else
        {
            time = 0f;
            while (fadecolor.a <1f)
            {
                time += Time.deltaTime / FadeTime;

                fadecolor.a = Mathf.Lerp(start, end, time);
                //Debug.Log(fadecolor.a);

                FadeImg.color = fadecolor;
                yield return null;
            }
        }

        yield return StartCoroutine(Event_Tieup());
    }

    protected override IEnumerator Event_Tieup()
    {
        if (FadeIn)
        {
            if (FadeUI != null)
                FadeUI.SetActive(false);
            if (FadeImg != null)
                FadeImg.enabled = false;
        }
        yield return StartCoroutine(Event_Finish());
    }
}
