using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.UI;

public class Event_UIScript : Game_Event 


{//part와 script order 몇부터 몇까지 정하는 기능 만들기
    public string Filename;
    public Text UI_Text;
    public int PartNumber;
    public int StartScriptorder;
    public int FinishScriptorder;
    JsonData ScriptData;
    bool nextButton;

    public override IEnumerator Event_Init()
    {
        string Jsonstring = File.ReadAllText(Application.dataPath + "/JSON/" + Filename + ".json");
        ScriptData = JsonMapper.ToObject(Jsonstring);
        
        yield return StartCoroutine(Event_Start());
    }


    protected override IEnumerator Event_Content() // 입력을 받앗을때만 다음거로 넘어가게
    {
        int pn=0;
        while ((int)ScriptData[pn]["PartNumber"] != PartNumber)
        {
            pn++;
        }

        for(int i = StartScriptorder-1; i<= FinishScriptorder-1; i++)
        {
            nextButton = false;
            UI_Text.text = ScriptData[pn + i]["Script"].ToString();
            while (!nextButton)
            {
               yield return null;
            }
        }

        yield return StartCoroutine( Event_Tieup());
    }

    protected override IEnumerator Event_Tieup()
    {
        yield return StartCoroutine(Event_Finish());
    }

    public void NextScript()
    {
        //nextButton = true;
    }
}
