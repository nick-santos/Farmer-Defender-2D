using System;
using UnityEngine;

[Serializable]
public class Plant
{
    public PlantType plantType;
    public string name;
    public int waterCost;
    public GameObject prefab;

    public Plant (PlantType _plantType, string _name, int _waterCost, GameObject _prefab)
    {
        plantType = _plantType;
        name = _name;
        waterCost = _waterCost;
        prefab = _prefab;
    }
}
