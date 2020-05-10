using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int _totalPlayerNumber;
    private int _currentPlayer = 0;

    private List<Player> _players;

    [SerializeField] private TilesGraph _firstTile = null;
    [SerializeField] private TilesGraph _lastTile = null;

    [SerializeField] [AssetsOnly] private GameplayDataManager _gameplayData;

    private void Awake()
    {
        SetupGame();
    }

    private void SetupGame()
    {
        _totalPlayerNumber = _gameplayData.PlayerCount;
        _currentPlayer = 1;

        _players = new List<Player>();
            
        for (int i = 0; i < _totalPlayerNumber; i++)
        {
            GameObject playerShip = Instantiate(_gameplayData.PlayerPrefabs[i]);
            _players.Add(new Player(i, playerShip, _firstTile));
        }
    }
    
    private void StartGame()
    {
        
    }
}
