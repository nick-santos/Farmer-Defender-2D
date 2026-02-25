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
        if (Inventory.main.GetAmount(plantType) > 0)
        {
            Debug.Log("Item selecionado!");
            BuildManager.main.SetSelectedPlant(plantType);
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
