using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableEnter : MonoBehaviour
{
    public List<GameObject> plateList;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Move>() && other.gameObject.GetComponent<Move>().isTouched == false)
        {
            foreach(GameObject plate in plateList)
            {
                if(plate.transform.position.x > -7 && plate.transform.position.x < 7)
                {
                    if(!plate.GetComponent<TableSlot>().occupied)
                    {
                        other.transform.position = new Vector3(plate.transform.position.x + 0.1f, plate.transform.position.y - 0.6f, -0.75f);
                        other.transform.parent = plate.transform;
                        plate.GetComponent<TableSlot>().occupied = true;
                    }
                }
            }
        }
    }
}
