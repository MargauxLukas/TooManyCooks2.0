using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilityFunctions : MonoBehaviour
{
    public static UtilityFunctions instance;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }

    public List<GameObject> GetAllChildren(GameObject gameObject)
    {
        List<GameObject> list = new List<GameObject>();

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            list.Add(gameObject.transform.GetChild(i).gameObject);
        }

        return list;
    }
}
