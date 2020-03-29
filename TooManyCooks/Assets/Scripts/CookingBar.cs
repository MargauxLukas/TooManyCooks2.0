using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingBar : MonoBehaviour
{
    public Image timerBar;
    public float maxTime = 10f;
    float currentTime = 0f;

    Color green = new Color32(36, 200, 16, 255);
    Color yellow = new Color32(200, 165, 9, 255);
    Color red = new Color32(188, 43, 8, 255);

    public GameObject otherTimer;

    void Start()
    {
        timerBar = this.gameObject.GetComponent<Image>();
        timerBar.color = red;
    }

    void Update()
    {
        if(GameManager.instance.isWinter)
        {
            maxTime = 20f;
        }
        else
        {
            maxTime = 10f;
        }

        if(otherTimer.transform.position.y > transform.position.y)
        {
            currentTime += Time.deltaTime;
            timerBar.fillAmount = currentTime / maxTime;

            if (timerBar.fillAmount > 0.7f)
            {
                timerBar.color = green;
            }
            else if (timerBar.fillAmount > 0.3f)
            {
                timerBar.color = yellow;
            }
        }
    }
}
