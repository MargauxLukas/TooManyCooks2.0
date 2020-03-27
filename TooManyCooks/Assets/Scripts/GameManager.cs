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
    private int actualRecipeNum;
    public float timeBeforeNewRecipe = 30;


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
}
