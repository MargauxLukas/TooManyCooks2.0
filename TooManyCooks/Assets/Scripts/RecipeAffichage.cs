using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeAffichage : MonoBehaviour
{
    public GameObject poulet;
    public GameObject porc;
    public GameObject fish;
    public GameObject boeuf;

    public void OpenRecette(int i)
    {
        if (transform.GetChild(i - 1).GetComponent<Image>().sprite.name.Contains("Porc"))
        {
            porc.SetActive(true);
        }
        else if (transform.GetChild(i - 1).GetComponent<Image>().sprite.name.Contains("Poulet"))
        {
            poulet.SetActive(true);
        }
        else if (transform.GetChild(i - 1).GetComponent<Image>().sprite.name.Contains("boeuf"))
        {
            boeuf.SetActive(true);
        }
        else if (transform.GetChild(i - 1).GetComponent<Image>().sprite.name.Contains("Poisson"))
        {
            fish.SetActive(true);
        }
    }

}
