using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Game_Event : MonoBehaviour
{
    public float startDelay;
    public float finishDelay;
    public abstract IEnumerator Event_Init();

    protected IEnumerator StartDelay()
    {
        float time = 0f;
        while (time < startDelay)
        {
            time += Time.deltaTime;
            Debug.Log(time);
            yield return null;
        }
    }

    protected IEnumerator Event_Start()
    {
        Debug.Log("Event Script start");
        Debug.Log("Event 이름" + gameObject.name);
        yield return StartCoroutine("StartDelay");
        yield return StartCoroutine("Event_Content");
    }

    

    protected abstract IEnumerator Event_Content();

    protected abstract IEnumerator Event_Tieup();

    protected IEnumerator FinishDelay()
    {
        float time = 0f;
        while (time < finishDelay)
        {
            time += Time.deltaTime;
            yield return null;
        }
    }

    protected IEnumerator Event_Finish()
    {
        yield return StartCoroutine(FinishDelay());
        Debug.Log("Event_Finish");
    }

    
}
