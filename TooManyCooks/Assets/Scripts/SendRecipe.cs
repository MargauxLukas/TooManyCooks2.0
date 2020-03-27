using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendRecipe : MonoBehaviour
{
    public List<GameObject> actualRecipes;
    public Transform actualRecipesGroup;

    public int score;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name.Contains("IngredientsMix"))
        {
            List<GameObject> ingredients = new List<GameObject>();
            ingredients = UtilityFunctions.instance.GetAllChildren(collision.gameObject);
            CheckIngredients(collision, ingredients);
            CompareScore(collision, ingredients);
        }
    }

    public void CheckIngredients(Collider _collision, List<GameObject> _collisionIngredients)
    { 
        actualRecipes = UtilityFunctions.instance.GetAllChildren(actualRecipesGroup.gameObject);

        foreach(GameObject recipe in actualRecipes)
        {
            List<GameObject> ingredients = new List<GameObject>();
            ingredients = UtilityFunctions.instance.GetAllChildren(recipe);

            foreach (GameObject _ingredient in _collisionIngredients)
            {
                string type = _ingredient.GetComponent<IngredientInstance>().ingredient.type;

                foreach(GameObject i in ingredients)
                {
                    if (type == i.GetComponent<IngredientReference>().ingredient.type)
                    {
                        CheckState(i, _ingredient);
                    }
                }
            }
        }
    }

    public void CheckState(GameObject reference, GameObject product)
    {
        if(reference.GetComponent<IngredientReference>().cutted == product.GetComponent<IngredientInstance>().cutted)
        {
            if(reference.GetComponent<IngredientReference>().cooked == product.GetComponent<IngredientInstance>().cooked)
            {
                if (reference.GetComponent<IngredientReference>().fried == product.GetComponent<IngredientInstance>().fried)
                {
                    if (reference.GetComponent<IngredientReference>().breaded == product.GetComponent<IngredientInstance>().breaded)
                    {
                        score++;
                    }
                }
            }
        }
    }

    public void CompareScore(Collider _collision, List<GameObject> _collisionIngredients)
    {
        if(score == _collisionIngredients.Count)
        {
            //WinPoints();
            SendPlate(_collision.gameObject);
        }
        else
        {
            //Strike();
            SendPlate(_collision.gameObject);
        }
    }

    public void SendPlate(GameObject plate)
    {
        Destroy(plate);
    }
}
