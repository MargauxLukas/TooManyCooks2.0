using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTable : MonoBehaviour
{
    public Animator tableAnimator;
    public bool ToLeft = false;

    private void Update()
    {
        if(tableAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("D"))
        {
            ToLeft = true;
        }
        else
        {
            ToLeft = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!ToLeft)
        {
            if (other.gameObject.GetComponent<TableSlot>())
            {
                if (other.gameObject.GetComponent<TableSlot>().ingredient == null)
                {
                    other.gameObject.GetComponent<TableSlot>().Spawn();
                }
            }
        }
        else
        {
            if (other.gameObject.GetComponent<TableSlot>())
            {
                Destroy(other.gameObject.transform.GetChild(0).gameObject);
                other.gameObject.GetComponent<TableSlot>().ingredient = null;
            }
        }
    }
}
