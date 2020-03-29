using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableEnter : MonoBehaviour
{
    public List<GameObject> plateList;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Move>() && other.gameObject.GetComponent<Move>().justDropped)
        {
            foreach(GameObject plate in plateList)
            {
                if (plate.transform.position.x > -7 && plate.transform.position.x < 7)
                {
                    if (!plate.GetComponent<TableSlot>().occupied)
                    {
                        Debug.Log(plate.gameObject.name);
                        other.GetComponent<IngredientInstance>().slotTable = plate.GetComponent<TableSlot>();
                        other.transform.position = new Vector3(plate.transform.position.x + 0.1f, plate.transform.position.y - 0.6f, -1.48f);
                        other.transform.parent = plate.transform;
                        plate.GetComponent<TableSlot>().occupied = true;
                        other.gameObject.GetComponent<Move>().justDropped = false;
                        return;
                    }
                }
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Move>() && other.gameObject.GetComponent<Move>().justDropped)
        {
            foreach (GameObject plate in plateList)
            {
                if (plate.transform.position.x > -7 && plate.transform.position.x < 7)
                {
                    if (!plate.GetComponent<TableSlot>().occupied)
                    {
                        Debug.Log(plate.gameObject.name);
                        other.GetComponent<IngredientInstance>().slotTable = plate.GetComponent<TableSlot>();
                        other.transform.position = new Vector3(plate.transform.position.x + 0.1f, plate.transform.position.y - 0.6f, -1.48f);
                        other.transform.parent = plate.transform;
                        plate.GetComponent<TableSlot>().occupied = true;
                        other.gameObject.GetComponent<Move>().justDropped = false;
                        return;
                    }
                }
            }
        }
    }
}
