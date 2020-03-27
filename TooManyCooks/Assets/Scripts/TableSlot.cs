using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableSlot : MonoBehaviour
{
    public GameObject ingredient;
    public bool occupied = false;
    public GameObject prefab;

    void Start()
    {
        ingredient = IngredientManager.instance.SpawnIngredient(transform);

        if(ingredient != null)
        {
            occupied = true;
        }
        else
        {
            occupied = false;
        }

        /*GameObject ingredientSpawn = Instantiate(prefab, transform.position, Quaternion.identity, transform);

        foreach(Ingredient i in IngredientManager.instance.ingredientList)
        {
            Debug.Log(i);
        }
        Debug.Log(Random.Range(0, IngredientManager.instance.ingredientList.Count - 1));
        ingredientSpawn.GetComponent<IngredientInstance>().ingredient = IngredientManager.instance.ingredientList[Random.Range(0, IngredientManager.instance.ingredientList.Count-1)];

        ingredientSpawn.transform.GetChild(0).GetComponent<MeshRenderer>().material = ingredientSpawn.GetComponent<IngredientInstance>().ingredient.visual;

        ingredient = ingredientSpawn;*/
    }

    public void Spawn()
    {
        ingredient = IngredientManager.instance.SpawnIngredient(transform);

        if (ingredient != null)
        {
            occupied = true;
        }
        else
        {
            occupied = false;
        }
    }
}
