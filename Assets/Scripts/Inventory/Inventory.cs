using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory main;

    private Dictionary<PlantType, int> inventory = new Dictionary<PlantType, int>();

    private void Awake()
    {
        main = this;
        InitializeInventory();
    }

    void InitializeInventory()
    {
        foreach (PlantType plant in System.Enum.GetValues(typeof(PlantType)))
        {
            inventory[plant] = 2;
        }
    }

    public void AddItem(PlantType plant, int amount)
    {
        inventory[plant] += amount;
    }

    public bool UseItem(PlantType plant)
    {
        if (inventory[plant] > 0)
        {
            inventory[plant]--;
            return true;
        }

        return false;
    }

    public int GetAmount(PlantType plant)
    {
        return inventory[plant];
    }

}
