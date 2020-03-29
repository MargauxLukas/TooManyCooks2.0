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

    public bool meatMania = false;
    public bool isWinter = false;

    public int nbPlat;
    public int nbPlatVoulu = 2;

    public bool needRecipe = false;

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

    private void Update()
    {
        if(nbPlat == nbPlatVoulu)
        {
            Event();
            nbPlatVoulu = 4;
            nbPlat = 0;
        }
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

            for (int i = 0; i < UIImagesList.Count; i++)
            {
                if (UIImagesList[i].GetComponent<Image>().sprite == null)
                {
                    UIImagesList[i].GetComponent<Image>().sprite = chosenRecipe.visual;
                    UIImagesList[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                    UIImagesList[i].transform.GetChild(0).gameObject.SetActive(true);
                    UIImagesList[i].GetComponent<UIRecipeManager>().recipeInstance = instanceGroup.GetChild(i).GetComponent<RecipeInstance>();
                    break;
                }
            }

            yield return new WaitForSeconds(timeBeforeNewRecipe);

            StartCoroutine(InstanciateRecipes());  
        }
        else
        {
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
        if(life == 2)
        {
            transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color32(217, 44, 56, 255);
        }
        if(life == 1)
        {
            transform.GetChild(0).transform.GetChild(1).GetComponent<SpriteRenderer>().color = new Color32(217, 44, 56, 255);
        }
        if(life == 0)
        {
            transform.GetChild(0).transform.GetChild(2).GetComponent<SpriteRenderer>().color = new Color32(217,44,56,255);
            //Perdu
        }
    }

    public void Event()
    {
        if (meatMania || isWinter)
        {
            meatMania = false;
            isWinter = false;
        }
        else
        {
            int random = Random.Range(0, 2);

            if (random == 0)
            {
                //Timer + court
                Debug.Log("meat");
                meatMania = true;
            }
            if (random == 1)
            {
                //Chauffer = plus lent
                Debug.Log("winter");
                isWinter = true;

            }
        }
    }
}
