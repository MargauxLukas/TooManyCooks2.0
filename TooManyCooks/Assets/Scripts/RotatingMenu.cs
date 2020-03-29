using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotatingMenu : MonoBehaviour
{
    public GameObject scrollBar;
    public float scrollPos = 0;
    float[] pos;
    int position = 0;

    private void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1 / (pos.Length - 1);

        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }

        if (Input.touchCount > 0)
        {
            scrollPos = scrollBar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if(scrollPos < pos[i] + (distance/2) && scrollPos > pos[i] - (distance / 2))
                {
                    scrollBar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollBar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                    position = i;
                }
            }
        }

        /*for (int i = 0; i < pos.Length; i++)
        {
            if (scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1, 1), 0.1f);
            }

            for (int a = 0; a < pos.Length; a++)
            {
                if(a != i)
                {
                    transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale, new Vector2(0.8f, 0.8f), 0.1f);
                }
            }
        }*/
    }

    public void Next()
    {
        if(position < pos.Length - 1)
        {
            position++;
            scrollPos = pos[position];
        }
    }

    public void Prev()
    {
        if (position > 0)
        {
            position--;
            scrollPos = pos[position];
        }
    }
}
