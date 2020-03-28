using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public bool start = false;
    [HideInInspector]public float time = 0f;
    public AudioClip bpm60;
    public AudioClip bpm90;
    public AudioClip bpm120;

    public bool tempo60  = false;
    public bool tempo90  = false;
    public bool tempo120 = false;

    void Update()
    {
            time += Time.deltaTime;

            if (tempo60)
            {
                if (time > 1.0f)
                {
                    time = 0f;
                }
            }
            else if(tempo90)
            {
                if (time > 0.67f)
                {
                    time = 0f;
                }
            }
            else if(tempo120)
            {
                if (time > 0.5f)
                {
                    time = 0f;
                }
            }
    }

    public void SetBMP60()
    {
        if (!tempo60)
        {
            GetComponent<AudioSource>().clip = bpm60;
            ChangeBPMOnMiniGame();
            tempo60 = true;
            tempo90 = false;
            tempo120 = false;
        }
    }

    public void SetBMP90()
    {
        if (!tempo90)
        {
            GetComponent<AudioSource>().clip = bpm90;
            ChangeBPMOnMiniGame();
            tempo60 = false;
            tempo90 = true;
            tempo120 = false;
        }
    }

    public void SetBMP120()
    {
        if (!tempo120)
        {
            GetComponent<AudioSource>().clip = bpm120;
            ChangeBPMOnMiniGame();
            tempo60 = false;
            tempo90 = false;
            tempo120 = true;
        }
    }

    public void ChangeBPMOnMiniGame()
    {
        if (GetComponent<AudioSource>().enabled)
        {
            GetComponent<AudioSource>().enabled = false;
            GetComponent<AudioSource>().enabled = true;
        }
    }
}
