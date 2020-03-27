using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour
{
    public static RecipeManager instance;

    public List<Recipe> recipesList;

    private void Awake()
    {
        Init();
    }

    public virtual void Init()
    {
        instance = this;
    }
}
