using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Event_TextChange : Game_Event
{
    public string txt;
    public TextMeshProUGUI target;
    public Color txtColor;
    public GameData GD;

    public bool now;
    public string now_col;
    public string now_fire;
    public string now_human;

    public bool last;
    public string last_colandfire;
    public string last_human;

    public override IEnumerator Event_Init()
    {

        yield return StartCoroutine(Event_Start());
    }

    protected override IEnumerator Event_Content()
    {
        if(now)
        {
            switch (GD.AccidentCase)
            {
                case "Col":
                    target.text = now_col;
                    break;
                case "Fire":
                    target.text = now_fire;
                    break;
                case "Human":
                    target.text = now_human;
                    break;
            }
        }
        else if(last)
        {
            switch (GD.AccidentCase)
            {
                case "Col":
                case "Fire":
                    target.text = last_colandfire;
                    break;
                case "Human":
                    target.text = last_human;
                    break;
            }
        }
        else
        {
            target.text = txt;
        }

        target.color = txtColor;
        yield return StartCoroutine(Event_Tieup());
    }

    protected override IEnumerator Event_Tieup()
    {

        Debug.Log("Timline finish");
        yield return StartCoroutine(Event_Finish());
    }
}
