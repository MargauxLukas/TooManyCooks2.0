using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Station : MonoBehaviour
{
    public enum StationType { KitchenCounter, CampFire };
    public StationType stationType;
    public enum StationFunction { CuttingBoard, Grill };
    public StationFunction stationFunction;

    public int stationCode = 0;
    public List<IngredientInstance> stationListIngredients;
    public List<Transform> stationSlots;

    public Button button;
    public GameObject canvas;

    private GameObject recipePrefab;
    public GameObject platDouteux;

    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("Collision : " + collision);
        if (collision.gameObject.GetComponent<IngredientInstance>())
        {
            stationListIngredients.Add(collision.gameObject.GetComponent<IngredientInstance>());
            //Debug.Log("StationListCount : " + stationListIngredients.Count);

            switch (stationCode)
            {
                case 11:
                    if (!collision.gameObject.GetComponent<IngredientInstance>().cutted && stationListIngredients.Count <= stationSlots.Count)
                    {
                        collision.GetComponent<Move>().isTouched = false;
                        GetPosition(collision);
                    }
                    else
                    {
                        stationListIngredients.RemoveAt(stationListIngredients.Count - 1);
                    }
                    break;

                case 23:
                    if (!collision.gameObject.GetComponent<IngredientInstance>().cooked && stationListIngredients.Count <= stationSlots.Count)
                    {
                        collision.GetComponent<Move>().isTouched = false;
                        GetPosition(collision);
                    }
                    else
                    {
                        stationListIngredients.RemoveAt(stationListIngredients.Count - 1);
                    }
                    break;
            }

            //Debug.Log("CutBool : " + collision.gameObject.GetComponent<Animator>().GetBool("Cut"));
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        //Debug.Log("Collision : " + collision);
        if (collision.gameObject.GetComponent<IngredientInstance>())
        {
            stationListIngredients.Remove(collision.gameObject.GetComponent<IngredientInstance>());

            if (collision.gameObject.GetComponent<IngredientInstance>().slot != null)
            {
                collision.gameObject.GetComponent<IngredientInstance>().slot.GetComponent<Slot>().occupied = false;
                collision.gameObject.GetComponent<IngredientInstance>().slot = null;
            }

            //Debug.Log("CutBool : " + collision.gameObject.GetComponent<Animator>().GetBool("Cut"));
            if(transform.parent.name.Contains("PlatDouteux") || transform.parent.name.Contains("IngredientsMix"))
            {
                collision.GetComponent<BoxCollider>().enabled = false;
            }

            SetButton();
        }
    }

    void GetPosition(Collider _collision)
    {
        Transform collisionTr = _collision.gameObject.transform;
        List<float> distList = new List<float>();
        List<Transform> trList = new List<Transform>();

        foreach (Transform tr in stationSlots)
        {
            if (!tr.GetComponent<Slot>().occupied)
            {
                float objToSlotDist = Vector3.Distance(_collision.transform.position, tr.transform.position);
                distList.Add(objToSlotDist);
                trList.Add(tr);
            }
        }

        float min = distList.Min();
        int index = distList.IndexOf(min);

        collisionTr.position = trList[index].position;

        foreach (Transform tr in stationSlots)
        {
            float dist;

            dist = Vector3.Distance(tr.position, trList[index].position);

            if (dist <= 0.01f)
            {
                tr.gameObject.GetComponent<Slot>().occupied = true;
                _collision.gameObject.GetComponent<IngredientInstance>().slot = tr.gameObject.GetComponent<Slot>();
            }
        }

        SetButton();
    }

    public void Cook()
    {
        button.gameObject.SetActive(false);
        int goodIngredient;
        bool goodRecette = false;

        //PlatDouteux
        foreach (Recipe recipe in RecipeManager.instance.recipesList)
        {
            goodIngredient = 0;
            if (!goodRecette)
            {
                foreach (Ingredient ingredientRecipe in recipe.ingredientsList)
                {
                    if (!goodRecette)
                    {
                        foreach (IngredientInstance ingredientI in stationListIngredients)
                        {
                            if (stationListIngredients[0].name != stationListIngredients[1].name)
                            {
                                if (ingredientRecipe.name == ingredientI.ingredient.name)
                                {
                                    //Bon ingredient
                                    goodIngredient++;
                                }

                                if (goodIngredient == 3)
                                {
                                    recipePrefab = recipe.prefab;
                                    goodRecette = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        if (goodRecette)
        {
            //BonPlat
            List<GameObject> listChild;
            foreach (IngredientInstance ingredient in stationListIngredients)
            {
                ingredient.cooked = true;
                listChild = UtilityFunctions.instance.GetAllChildren(ingredient.gameObject);
                ingredient.gameObject.SetActive(false);

                foreach (GameObject go in listChild)
                {
                    go.SetActive(false);
                }
            }

            GameObject recipeObtained = Instantiate(recipePrefab, new Vector3(5.30f, 0f, -1.32f), Quaternion.identity);

            foreach (IngredientInstance ingredient in stationListIngredients)
            {
                ingredient.transform.parent = recipeObtained.transform;
            }
            /*stationListIngredients.Clear();
            stationListIngredients.Add(ingredientMix.GetComponent<IngredientInstance>());*/

            recipeObtained.GetComponent<Move>().cookingGame = true;
            recipeObtained.GetComponent<Move>().upBar = canvas.transform.GetChild(0).gameObject;
            recipeObtained.GetComponent<Move>().downBar = canvas.transform.GetChild(1).gameObject;

            canvas.SetActive(true);
        }
        else
        {
            //PlatDouteux
            List<GameObject> listChild;
            foreach (IngredientInstance ingredient in stationListIngredients)
            {
                ingredient.cooked = true;
                listChild = UtilityFunctions.instance.GetAllChildren(ingredient.gameObject);
                ingredient.gameObject.SetActive(false);

                foreach (GameObject go in listChild)
                {
                    go.SetActive(false);
                }
            }

            platDouteux = Instantiate(StationsManager.instance.platDouteux, new Vector3(5.30f, 0f, -1.32f), Quaternion.identity);

            foreach (IngredientInstance ingredient in stationListIngredients)
            {
                ingredient.transform.parent = platDouteux.transform;
            }

            platDouteux.GetComponent<Move>().cookingGame = true;
            platDouteux.GetComponent<Move>().upBar = canvas.transform.GetChild(0).gameObject;
            platDouteux.GetComponent<Move>().downBar = canvas.transform.GetChild(1).gameObject;

            canvas.SetActive(true);
        }
    }

    public void SetButton()
    {
        if (stationType == StationType.CampFire)
        {
            if (stationListIngredients.Count == 3)
            {
                button.gameObject.SetActive(true);
            }
            else
            {
                button.gameObject.SetActive(false);
            }
        }
        if(stationType == StationType.KitchenCounter)
        {
            if (stationListIngredients.Count == 1)
            {
                button.gameObject.SetActive(true);
            }
            else
            {
                button.gameObject.SetActive(false);
            }
        }
    }

    public void Cut()
    {
        Debug.Log(stationListIngredients[0]);
        if (stationListIngredients[0] != null)
        {
            //stationListIngredients[0].gameObject.GetComponent<Animator>().SetBool("Cut", true);
            stationListIngredients[0].gameObject.GetComponent<Move>().cuttingGame = true;
            stationListIngredients[0].gameObject.GetComponent<IngredientInstance>().cutted = true;
            //stationListIngredients[0].gameObject.GetComponent<BoxCollider>().size = new Vector3(4f, 1.5f, 2.27f);
            button.gameObject.SetActive(false);
        }
    }

    public void ActivateFinger(GameObject go)
    {
        StartCoroutine("Finger", go);
    }

    IEnumerator Finger(GameObject go)
    {
        go.SetActive(true);
        yield return new WaitForSeconds(5f);
        go.SetActive(false);
    }
}
