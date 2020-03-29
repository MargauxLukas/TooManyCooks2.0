using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Image timerBar;
    public float maxTime = 50f;
    float timeLeft;

    Color green = new Color32(36,200,16,255);
    Color yellow = new Color32(200,165,9,255);
    Color red = new Color32(188,43,8,255);

    // Start is called before the first frame update
    void Start()
    {
        timerBar = this.gameObject.GetComponent<Image>();
        timeLeft = maxTime;
        timerBar.color = green;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.meatMania)
        {
            maxTime = 60f;
        }
        else
        {
            maxTime = 100f;
        }


        if(this.enabled)
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                timerBar.fillAmount = timeLeft / maxTime;

                if(timerBar.fillAmount < 0.3)
                {
                    timerBar.color = red;
                }
                else if(timerBar.fillAmount < 0.7)
                {
                    timerBar.color = yellow;
                }
            }
            else
            {
                transform.parent.gameObject.SetActive(false);
                timerBar.fillAmount = 1f;
                timerBar.color = green;
                timeLeft = maxTime;

                transform.parent.transform.parent.GetComponent<Image>().sprite = null;
                transform.parent.transform.parent.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
                GameManager.instance.chosenRecipesList.Remove(transform.parent.transform.parent.GetComponent<UIRecipeManager>().recipeInstance.gameObject);
                Destroy(transform.parent.transform.parent.GetComponent<UIRecipeManager>().recipeInstance.gameObject);
                transform.parent.transform.parent.GetComponent<UIRecipeManager>().recipeInstance = null;
                GameManager.instance.actualRecipeNum--;

                GameManager.instance.Strike();
            }
        }
    }
}
