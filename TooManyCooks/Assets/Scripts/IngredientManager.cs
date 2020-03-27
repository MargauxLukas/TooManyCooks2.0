using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    public static IngredientManager instance;

    public List<Ingredient> ingredientList;
    public GameObject prefab;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }

    public GameObject SpawnIngredient(Transform t)
    {
        int random = Random.Range(1, 5);

        if (random <= 3)
        {
            GameObject ingredientSpawn = Instantiate(prefab, new Vector3(t.position.x + 0.1f, t.position.y -0.6f, -0.75f), Quaternion.identity, t);

            ingredientSpawn.GetComponent<IngredientInstance>().ingredient = ingredientList[Random.Range(0, ingredientList.Count)];

            List<GameObject> childs = new List<GameObject>();
            Material mat = ingredientSpawn.GetComponent<IngredientInstance>().ingredient.visual;

            foreach (GameObject g in childs = UtilityFunctions.instance.GetAllChildren(ingredientSpawn))
            {
                g.GetComponent<MeshRenderer>().material = mat;
            }

            return ingredientSpawn;
        }
        else
        {
            return null;
        }

    }
}