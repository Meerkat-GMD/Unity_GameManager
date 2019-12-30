using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class Event_DecideNextPart : Game_Event
{
    public GameManager GM;
    public GameData GD;
    public EventManager EM;
    public bool accident;
    public bool VHFCheck;
    public bool ending;
    public int num=0;
    public bool finish;
    public bool button;

    public Button vhf_col;
    public Button vhf_fire;
    public Button vhf_human;
    public Button vhf_check;

    public override IEnumerator Event_Init()
    {
        yield return StartCoroutine(Event_Start());
    }

    protected override IEnumerator Event_Content()
    {
        if(accident)
        {
            switch (GD.AccidentCase)
            {
                case "Check":
                    num = 0;
                    break;
                case "Col":
                    num = 1;
                    break;
                case "Fire":
                    num = 2;
                    break;
                case "Human":
                    num = 3;
                    break;
            }
        }
        else if(VHFCheck)
        {
            num = -2;
        }
        else if(ending)
        {
            num = 0;

            switch (GD.AccidentCase)
            {
                case "Col":
                    num = 2;
                    break;
                case "Fire":
                    num = 1;
                    break;
                case "Human":
                    num = 0;
                    break;
            }

            if (!GD.VHFDSC_Check)
            {
                num += 2;
            }
        }
        else if(finish)
        {
            num = -GM.get_order() + 3;
            if (!GD.VHFDSC_Check)
                GD.badending = true;
            if(GD.acc ==15)
            {
                num = 2;
            }
        }
        else if(button)
        {
            if(GD.badending)
            {
                vhf_col.interactable = false;
                vhf_fire.interactable = false;
                vhf_human.interactable = false;
                GD.badending = false;
            }
            else
            {
                if((GD.acc & 1)!=1)
                {
                    vhf_col.interactable = true;
                }
                if ((GD.acc & 2) != 1)
                {
                    vhf_col.interactable = true;
                }
                if ((GD.acc & 4) != 1)
                {
                    vhf_col.interactable = true;
                }

            }
        }


        Debug.Log(GM.get_order() + num);
        GM.set_order(GM.get_order() + num);
        
       
        yield return null;
        yield return StartCoroutine(Event_Tieup());
    }

    protected override IEnumerator Event_Tieup()
    {
        yield return StartCoroutine(Event_Finish());
    }
}
