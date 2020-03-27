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
                        //collision.gameObject.GetComponent<IngredientInstance>().GoToTable();
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
                        //collision.gameObject.GetComponent<IngredientInstance>().GoToTable();
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
            //Debug.Log("StationListCount : " + stationListIngredients.Count);

            collision.gameObject.GetComponent<IngredientInstance>().slot.occupied = false;

            //Debug.Log("CutBool : " + collision.gameObject.GetComponent<Animator>().GetBool("Cut"));

            CanButtonAppear();
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

        CanButtonAppear();
    }

    public void Cuire()
    {
        foreach (IngredientInstance ingredient in stationListIngredients)
        {
            ingredient.cooked = true;
        }
    }

    public void CanButtonAppear()
    {
        if (stationListIngredients.Count != 0)
        {
            button.gameObject.SetActive(true);
        }
        else
        {
            button.gameObject.SetActive(false);
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
            stationListIngredients[0].gameObject.GetComponent<BoxCollider>().size = new Vector3(4f, 1.5f, 2.27f);
            button.gameObject.SetActive(false);
        }
    }
}
