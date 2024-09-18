using System;
using UnityEngine;

[Serializable]
public class Tower 
{
    public string name;
    public int cost;
    public GameObject prefabs;

    public Tower(string _name, int _cost,GameObject _prefabs)
    {
        name = _name;
        cost = _cost;
        prefabs = _prefabs;
    }
}
