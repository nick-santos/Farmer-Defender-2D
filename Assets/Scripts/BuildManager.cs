using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [Header("References")]
    public Plant[] plants;

    private int selectedPlant = 0;

    private void Awake()
    {
        main = this;
    }

    public Plant GetSelectedPlant()
    {
        return plants[selectedPlant];
    }

}
