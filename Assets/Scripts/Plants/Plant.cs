using System;
using UnityEngine;

[Serializable]
public class Plant
{
    public string name;
    public int waterCost;
    public GameObject prefab;

    public Plant (string _name, int _waterCost, GameObject _prefab)
    {
        name = _name;
        waterCost = _waterCost;
        prefab = _prefab;
    }
}
