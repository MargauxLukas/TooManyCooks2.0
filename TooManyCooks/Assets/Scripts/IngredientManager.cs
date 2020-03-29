using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    public static IngredientManager instance;

    public List<Ingredient> ingredientList;
    private Ingredient ingredient;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }

    public GameObject SpawnIngredient(Transform t, bool toLeft)
    {
        int random = Random.Range(1, 5);

        if (random <= 3)
        {
            ingredient = ingredientList[Random.Range(0, ingredientList.Count)];

            GameObject ingredientSpawn = Instantiate(ingredient.prefab, new Vector3(t.position.x + 0.1f, t.position.y - 0.6f, -1.40f), Quaternion.identity, t);
            ingredientSpawn.GetComponent<IngredientInstance>().slotTable = t.gameObject.GetComponent<TableSlot>();
            ingredientSpawn.GetComponent<IngredientInstance>().ingredient = ingredient;

            /*if (toLeft)
            {
                ingredientSpawn.transform.rotation = Quaternion.Euler(0f, -180f, -26.501f);
            }
            else
            {
                ingredientSpawn.transform.rotation = Quaternion.Euler(0f, -180f, +26f);
            }*/

            return ingredientSpawn;
        }
        else
        {
            return null;
        }

    }
}