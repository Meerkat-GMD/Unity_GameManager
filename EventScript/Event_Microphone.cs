using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event_Microphone : Game_Event
{
    private int audioSampleRate = 44100;
    private string microphone;
    public AudioSource audioSource;
    public AudioClip volumeup;
    private int SuccessVol = 1000;

    public override IEnumerator Event_Init()
    {
        foreach (string device in Microphone.devices)
        {
            if (microphone == null)
            {
                //set default mic to first mic found.
                microphone = device;
            }
        }
        yield return StartCoroutine(Event_Start());
    }

    protected override IEnumerator Event_Content()
    {
        float time=0f;
        float volume = 0f;
        audioSource.Stop();
        //Start recording to audioclip from the mic
        audioSource.clip = Microphone.Start(microphone, true, 10, audioSampleRate);
        audioSource.loop = true;
        // Mute the sound with an Audio Mixer group becuase we don't want the player to hear it
        Debug.Log(Microphone.IsRecording(microphone).ToString());

        if (Microphone.IsRecording(microphone))
        { //check that the mic is recording, otherwise you'll get stuck in an infinite loop waiting for it to start
            while (!(Microphone.GetPosition(microphone) > 0))
            {
            } // Wait until the recording has started. 

            Debug.Log("recording started with " + microphone);

            // Start playing the audio source
            audioSource.Play();
        }
        else
        {
            //microphone doesn't work for some reason

            Debug.Log(microphone + " doesn't work!");
        }


        while (time<5f)
        {
            float temp = GetAveragedVolume();
            if (temp > volume)
                volume = temp; 
            time += Time.deltaTime;
            yield return null;
        }
        Debug.Log("볼륨 : " + volume);
        if (volume<0.05f)
        {
            audioSource.clip = volumeup;
            audioSource.loop = false;
            audioSource.Play();
        }


        yield return StartCoroutine(Event_Tieup());
    }

    protected override IEnumerator Event_Tieup()
    {
        yield return StartCoroutine(Event_Finish());
    }

    public float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        audioSource.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
       
        return a / 256;
    }
}
