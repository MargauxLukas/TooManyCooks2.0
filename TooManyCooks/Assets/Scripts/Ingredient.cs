using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/Ingredient", order = 2)]
public class Ingredient : ScriptableObject
{
    public string type;
    public Material visual;
    public GameObject prefab;

    /*public bool cutted;
    public bool cooked;
    public bool fried;
    public bool breaded;*/
}
