using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private RecipeManager recipeManager;
    public GameObject recipeInstance;
    public Transform instanceGroup;
    public Recipe chosenRecipe;
    public List<GameObject> chosenRecipesList;

    public List<Image> UIImagesList;
    private int recipeNum = 3;
    public int actualRecipeNum;
    public float timeBeforeNewRecipe = 30;

    public int life = 3;
    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }

    void Start()
    {
        recipeManager = RecipeManager.instance;
        StartCoroutine(InstanciateRecipes());
    }

    IEnumerator InstanciateRecipes()
    {
        if (actualRecipeNum < recipeNum)
        {
            actualRecipeNum++;

            chosenRecipe = recipeManager.recipesList[Random.Range(0, recipeManager.recipesList.Count - 1)];
            GameObject instanciatedRecipe = Instantiate(recipeInstance, instanceGroup);
            instanciatedRecipe.GetComponent<RecipeInstance>().recipe = chosenRecipe;
            CreateRecipe(instanciatedRecipe);

            if (chosenRecipesList.Count < 3)
            {
                chosenRecipesList.Add(instanciatedRecipe);
            }

            UIImagesList[actualRecipeNum - 1].GetComponent<Image>().sprite = chosenRecipe.visual;
            UIImagesList[actualRecipeNum - 1].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            UIImagesList[actualRecipeNum - 1].transform.GetChild(0).gameObject.SetActive(true);
            UIImagesList[actualRecipeNum - 1].GetComponent<UIRecipeManager>().recipeInstance = instanceGroup.GetChild(actualRecipeNum - 1).GetComponent<RecipeInstance>();

            yield return new WaitForSeconds(timeBeforeNewRecipe);

            StartCoroutine(InstanciateRecipes());
        }
    }

    public void CreateRecipe(GameObject instRecipe)
    {
        switch(instRecipe.GetComponent<RecipeInstance>().recipe.name)
        {
            case "BeefSkewer":
                instRecipe.transform.GetChild(0).GetComponent<IngredientReference>().ingredient = IngredientManager.instance.ingredientList[5];
                instRecipe.transform.GetChild(0).GetComponent<IngredientReference>().cutted = true;
                instRecipe.transform.GetChild(0).GetComponent<IngredientReference>().cooked = true;
                instRecipe.transform.GetChild(1).GetComponent<IngredientReference>().ingredient = IngredientManager.instance.ingredientList[0];
                instRecipe.transform.GetChild(1).GetComponent<IngredientReference>().cutted = true;
                instRecipe.transform.GetChild(1).GetComponent<IngredientReference>().cooked = true;
                instRecipe.transform.GetChild(2).GetComponent<IngredientReference>().ingredient = IngredientManager.instance.ingredientList[1];
                instRecipe.transform.GetChild(2).GetComponent<IngredientReference>().cutted = true;
                instRecipe.transform.GetChild(2).GetComponent<IngredientReference>().cooked = true;
                break;

            case "ChickenSkewer":
                instRecipe.transform.GetChild(0).GetComponent<IngredientReference>().ingredient = IngredientManager.instance.ingredientList[5];
                instRecipe.transform.GetChild(0).GetComponent<IngredientReference>().cutted = true;
                instRecipe.transform.GetChild(0).GetComponent<IngredientReference>().cooked = true;
                instRecipe.transform.GetChild(1).GetComponent<IngredientReference>().ingredient = IngredientManager.instance.ingredientList[3];
                instRecipe.transform.GetChild(1).GetComponent<IngredientReference>().cutted = true;
                instRecipe.transform.GetChild(1).GetComponent<IngredientReference>().cooked = true;
                instRecipe.transform.GetChild(2).GetComponent<IngredientReference>().ingredient = IngredientManager.instance.ingredientList[6];
                instRecipe.transform.GetChild(2).GetComponent<IngredientReference>().cutted = true;
                instRecipe.transform.GetChild(2).GetComponent<IngredientReference>().cooked = true;
                break;

            case "FishSkewer":
                instRecipe.transform.GetChild(0).GetComponent<IngredientReference>().ingredient = IngredientManager.instance.ingredientList[5];
                instRecipe.transform.GetChild(0).GetComponent<IngredientReference>().cutted = true;
                instRecipe.transform.GetChild(0).GetComponent<IngredientReference>().cooked = true;
                instRecipe.transform.GetChild(1).GetComponent<IngredientReference>().ingredient = IngredientManager.instance.ingredientList[4];
                instRecipe.transform.GetChild(1).GetComponent<IngredientReference>().cutted = true;
                instRecipe.transform.GetChild(1).GetComponent<IngredientReference>().cooked = true;
                instRecipe.transform.GetChild(2).GetComponent<IngredientReference>().ingredient = IngredientManager.instance.ingredientList[2];
                instRecipe.transform.GetChild(2).GetComponent<IngredientReference>().cutted = true;
                instRecipe.transform.GetChild(2).GetComponent<IngredientReference>().cooked = true;
                break;

            case "PorkSkewer":
                instRecipe.transform.GetChild(0).GetComponent<IngredientReference>().ingredient = IngredientManager.instance.ingredientList[5];
                instRecipe.transform.GetChild(0).GetComponent<IngredientReference>().cutted = true;
                instRecipe.transform.GetChild(0).GetComponent<IngredientReference>().cooked = true;
                instRecipe.transform.GetChild(1).GetComponent<IngredientReference>().ingredient = IngredientManager.instance.ingredientList[7];
                instRecipe.transform.GetChild(1).GetComponent<IngredientReference>().cutted = true;
                instRecipe.transform.GetChild(1).GetComponent<IngredientReference>().cooked = true;
                instRecipe.transform.GetChild(2).GetComponent<IngredientReference>().ingredient = IngredientManager.instance.ingredientList[2];
                instRecipe.transform.GetChild(2).GetComponent<IngredientReference>().cutted = true;
                instRecipe.transform.GetChild(2).GetComponent<IngredientReference>().cooked = true;
                break;
        }
    }

    public void Strike()
    {
        life--;
        Debug.Log(life);
        if(life == 0)
        {
            //Perdu
        }
    }
}
