using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    public GameObject placeholder;
    public Plant[] plants;

    private int selectedPlant = 0;

    private void Awake()
    {
        main = this;
        InitializeList();
    }

    void InitializeList()
    {
        int aux = 0;
        foreach (PlantType plantT in System.Enum.GetValues(typeof(PlantType)))
        {
            plants[aux].plantType = plantT;
            aux++;
        }
    }

    public void SetSelectedPlant(PlantType plantT)
    {
        int aux = 0;
        foreach (Plant plant in plants)
        {
            if (plants[aux].plantType == plantT)
            {
                selectedPlant = aux;
            }
            aux++;
        }
        Debug.Log(selectedPlant);
        placeholder.GetComponent<MousePositionBuilder>().enabled = true;
    }

    public Plant GetSelectedPlant()
    {
        return plants[selectedPlant];
    }

}
