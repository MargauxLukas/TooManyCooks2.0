using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor : MonoBehaviour
{
    public AudioController ac;

    public int bpm;
    private bool isLeft;

    private float time;
    public int timeInt;

    private int random;

    public bool chooseSide1 = false;
    public bool chooseSide2 = false;
    public bool chooseSide3 = false;
    public bool chooseSpeed = false;

    void Start()
    {
        bpm = 60;
        isLeft = true;
        ac.SetBMP60();
        SetChangementOnAnimator();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeInt = (int)time;

        if(timeInt == 40)
        {
            SetSpeed();
            chooseSpeed = false;
            chooseSide1 = false;
            chooseSide2 = false;
            time = 0;
            timeInt = 0;
        }
        else if (timeInt == 30 && !chooseSide2)
        {
            chooseSide2 = false;
            SetSide();
        }
        else if(timeInt == 20 && !chooseSpeed)
        {
            chooseSpeed = true;
            SetSpeed();
        }
        else if(timeInt == 10 && !chooseSide1)
        {
            chooseSide1 = true;
            SetSide();
        }
    }

    public void SetSpeed()
    {
        random = Random.Range(1, 4);

        switch(random)
        {
            case 1:
                bpm = 60;
                ac.SetBMP60();
                break;

            case 2:
                bpm = 90;
                ac.SetBMP90();
                break;

            case 3:
                bpm = 120;
                ac.SetBMP120();
                break;
        }

        SetChangementOnAnimator();
    }

    public void SetSide()
    {
        random = Random.Range(1, 3);

        switch (random)
        {
            case 1:
                isLeft = true;
                break;

            case 2:
                isLeft = false;
                break;
        }

        SetChangementOnAnimator();
    }

    public void SetChangementOnAnimator()
    {
        GetComponent<Animator>().SetInteger("BPM", bpm);
        GetComponent<Animator>().SetBool("isLeft", isLeft);
    }
}
