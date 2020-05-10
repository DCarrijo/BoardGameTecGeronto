using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ConnectionType
{
    forward = 0,
    backward = 1
}

public class TilesGraph : MonoBehaviour
{
    [SerializeField] private List<TilesGraph> _forwardConnections = new List<TilesGraph>();
    [SerializeField] private List<TilesGraph> _backwardsConnections = new List<TilesGraph>();
    [SerializeField] private Transform _shipFloatingPoint;

    public GameObject GetConnectedTile(int index = 0, ConnectionType connectionType = ConnectionType.forward)
    {
        if (index <= 0)
            return null;
        
        if (connectionType == ConnectionType.forward)
        {
            if (index >= _forwardConnections.Count)
                return null;
            else
                return _forwardConnections[index].gameObject;
        }
        else
        {
            if (index >= _backwardsConnections.Count)
                return null;
            else
                return _backwardsConnections[index].gameObject;
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
