using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuButton : MonoBehaviour
{
    public PlantType plantType;
    public TextMeshProUGUI quantity;

    public void OnClick()
    {
        bool used = Inventory.main.UseItem(plantType);

        if (used)
        {
            Debug.Log("Item usado!");
        }
        else
        {
            Debug.Log("Sem item suficiente!");
        }
    }

    private void Update()
    {
        quantity.text = Inventory.main.GetAmount(plantType).ToString();
    }
}
