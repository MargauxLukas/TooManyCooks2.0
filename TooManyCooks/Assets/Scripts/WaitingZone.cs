using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingZone : MonoBehaviour
{
    public List<WaitingSlot> waitingSlotList;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Move>() && other.gameObject.GetComponent<Move>().justDropped)
        {
            foreach (WaitingSlot waitSlot in waitingSlotList)
            {
                    if (!waitSlot.GetComponent<WaitingSlot>().occupied)
                    {
                        other.GetComponent<IngredientInstance>().wSlot = waitSlot.GetComponent<WaitingSlot>();
                        other.transform.position = new Vector3(waitSlot.transform.position.x -0.2f, waitSlot.transform.position.y, -1.48f);
                        other.transform.parent = waitSlot.transform;
                        waitSlot.GetComponent<WaitingSlot>().occupied = true;
                        other.gameObject.GetComponent<Move>().justDropped = false;
                        return;
                    }   
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Move>() && other.gameObject.GetComponent<Move>().justDropped)
        {
            foreach (WaitingSlot waitSlot in waitingSlotList)
            {
                if (!waitSlot.GetComponent<WaitingSlot>().occupied)
                {
                    other.GetComponent<IngredientInstance>().wSlot = waitSlot.GetComponent<WaitingSlot>();
                    other.transform.position = new Vector3(waitSlot.transform.position.x - 0.2f, waitSlot.transform.position.y, -1.48f);
                    other.transform.parent = waitSlot.transform;
                    waitSlot.GetComponent<WaitingSlot>().occupied = true;
                    other.gameObject.GetComponent<Move>().justDropped = false;
                    return;
                }
            }
        }
    }
}
