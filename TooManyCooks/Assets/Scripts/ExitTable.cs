using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTable : MonoBehaviour
{
    public Animator tableAnimator;
    public bool toLeft = false;

    private void Update()
    {
        if (tableAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("D"))
        {
            toLeft = false;
        }
        else
        {
            toLeft = true;
        }

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name.Contains("IngredientsMix"))
        {
            Debug.Log("test");
        }

        if (!toLeft)
        {
            if (other.gameObject.transform.childCount > 0 && !other.gameObject.name.Contains("IngredientsMix"))
            {
                if (other.gameObject.GetComponent<TableSlot>())
                {
                    Destroy(other.gameObject.transform.GetChild(0).gameObject);
                    other.gameObject.GetComponent<TableSlot>().ingredient = null;
                    other.gameObject.GetComponent<TableSlot>().occupied = false;
                }
            }
        }
        else
        {
            if (other.gameObject.GetComponent<TableSlot>())
            {
                if (other.gameObject.GetComponent<TableSlot>().ingredient == null)
                {
                    other.gameObject.GetComponent<TableSlot>().Spawn(toLeft);
                }
            }
        }
    }
}
