using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public enum ConnectionType
{
    forward = 0,
    backward = 1
}

public class TilesGraph : MonoBehaviour
{
    [SerializeField] private float _distanceMultiplyeir = 1.5f;
    [SerializeField] private List<TilesGraph> _forwardConnections = new List<TilesGraph>();
    [SerializeField] private List<TilesGraph> _backwardsConnections = new List<TilesGraph>();
    public List<Player> CurrentPlayers;

    public bool HasMultipleRoutes { get => _forwardConnections.Count > 1; }
    
    public TileManager TileManager { get; private set; }

    public static List<TilesGraph> AllTiles { get; private set; }

    private void Awake()
    {
        CurrentPlayers = new List<Player>();
        TileManager = this.GetComponentInChildren<TileManager>();
        
        if (AllTiles == null)
        {
            AllTiles = new List<TilesGraph>();
        }

        AllTiles.Add(this);
    }

    public Tween ArrangePlayersInTile()
    {
        if (CurrentPlayers.Count == 0)
            return null;
        
        if (CurrentPlayers.Count == 1) 
        {
            return CurrentPlayers[0].MovePlayerInTile(Vector3.zero);
        }
        else
        {
            float angle = 360f / CurrentPlayers.Count;
            Tween aux = null;
                
            for (int i = 0; i < CurrentPlayers.Count; i++)
            {
               aux = CurrentPlayers[i].MovePlayerInTile(Quaternion.Euler(0, i * angle, 0) * (Vector3.forward * _distanceMultiplyeir));
            }

            return aux;
        }
    }

    public TilesGraph GetConnectedTile(int index, ConnectionType connectionType = ConnectionType.forward)
    {
        if (index < 0)
            return null;
        
        if (connectionType == ConnectionType.forward)
        {
            if (index >= _forwardConnections.Count)
                return null;
            else
                return _forwardConnections[index];
        }
        else
        {
            if (index >= _backwardsConnections.Count)
                return null;
            else
                return _backwardsConnections[index];
        }
    }

    public TilesGraph[] GetConnectedTile(ConnectionType connectionType = ConnectionType.forward)
    {
        if (connectionType == ConnectionType.forward)
        {
            return _forwardConnections.ToArray();
        }
        else
        {
            return _backwardsConnections.ToArray();
        }
    }

    public void AddConnection(TilesGraph tile, ConnectionType connectionType = ConnectionType.forward)
    {
        if (connectionType == ConnectionType.forward)
        {
            _forwardConnections.Add(tile);
        }
        else
        {
            _backwardsConnections.Add(tile);
        }
    }

    public void AddConnection(TilesGraph[] tiles, ConnectionType connectionType = ConnectionType.forward)
    {
        foreach (TilesGraph tile in tiles)
        {
            AddConnection(tile, connectionType);
        }
    }

    private void OnDrawGizmos()
    {
        if (_forwardConnections.Count > 0)
        {
            Gizmos.color = Color.green;
            foreach (var tile in _forwardConnections)
            {
                Gizmos.DrawLine(this.transform.position + (Vector3.right * 0.25f), tile.transform.position + (Vector3.right * 0.25f));
            }
        }

        if (_backwardsConnections.Count > 0)
        {
            Gizmos.color = Color.red;
            foreach (var tile in _backwardsConnections)
            {
                Gizmos.DrawLine(this.transform.position + (Vector3.left * 0.25f), tile.transform.position + (Vector3.left * 0.25f));
            }
        }
    }
}
