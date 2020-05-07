using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesGraph : MonoBehaviour
{
    [SerializeField] private List<TilesGraph> _connections = new List<TilesGraph>();
    [SerializeField] private Transform _shipFloatingPoint;

    public void AddConnection(TilesGraph tile)
    {
        _connections.Add(tile);
    }

    public void AddConnection(TilesGraph[] tiles)
    {
        foreach (TilesGraph tile in tiles)
        {
            AddConnection(tile);
        }
    }
}
