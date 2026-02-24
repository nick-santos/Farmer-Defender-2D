using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    public GameObject[] plantPrefabs;

    private int selectedPlant = 0;

    private void Awake()
    {
        main = this;
    }

    public GameObject GetSelectedPlant()
    {
        return plantPrefabs[selectedPlant];
    }

}
