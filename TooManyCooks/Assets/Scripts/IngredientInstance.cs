using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientInstance : MonoBehaviour
{
    [HideInInspector] public Ingredient ingredient;

    public bool cutted;
    public bool cooked;
    public bool fried;
    public bool breaded;

    public Slot slot;
    public TableSlot slotTable;
    public WaitingSlot wSlot;
}
