using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public bool occupied = false;

    private void Update()
    {
        if(transform.parent.GetComponent<Station>().stationListIngredients.Count == 0)
        {
            occupied = false;
        }
    }
}
