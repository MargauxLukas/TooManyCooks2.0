using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/Recipe", order = 1)]
public class Recipe : ScriptableObject
{
    public string type;
    public Sprite visual;
    public List<Ingredient> ingredientsList;
    public GameObject prefab;
}
