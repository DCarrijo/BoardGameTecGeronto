using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private Vector3 _tileOffset;

    private List<GameObject> _tileList = new List<GameObject>();
    private GameObject _lastTile = null;

    private int _lastDirection = -1;

    [Button]
    public void SpawnTiles(int tilecount = 32)
    {
        for (int i = 0; i < tilecount; i++)
        {
            GameObject obj = Instantiate(_tilePrefab);
            
            if (_lastTile != null)
            {
                obj.transform.position = _lastTile.GetComponent<HexagonPoints>().Points[ChooseRandomDirection()];
                _lastTile.GetComponent<TilesGraph>().AddConnection(obj.GetComponent<TilesGraph>());
            }
            else
            {
                obj.transform.position = this.transform.position;
            }

            obj.transform.parent = this.transform;
            
            obj.GetComponent<HexagonPoints>().CalculatePoints();
            
            _lastTile = obj;
        }
    }

    [Button]
    public void ClearMap()
    {
        foreach (var tile in _tileList)
        {
            Destroy(tile);
        }
        
        _tileList.Clear();
    }

    private int ChooseRandomDirection() => Random.Range(0, 2) == 0 ? 4 : 5;
}
