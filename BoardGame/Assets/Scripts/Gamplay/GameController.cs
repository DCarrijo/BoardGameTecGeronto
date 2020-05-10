using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    private int _totalPlayerNumber;
    private int _currentPlayer = 0;
    private int _currentDiceNumber = -1;

    private List<Player> _players;

    [SerializeField] private TilesGraph _firstTile = null;
    [SerializeField] private TilesGraph _lastTile = null;

    [SerializeField] [AssetsOnly] private GameplayDataManager _gameplayData;

    [SerializeField] private GameObject _diceRollCanvas;

    private bool _roundFinished = false;
    
    private void Awake()
    {
        SetupGame();
        StartGame();
    }

    private void SetupGame()
    {
        _totalPlayerNumber = _gameplayData.PlayerCount;
        
        _players = new List<Player>();
            
        for (int i = 0; i < _totalPlayerNumber; i++)
        {
            GameObject playerShip = Instantiate(_gameplayData.PlayerPrefabs[i]);
            _players.Add(new Player(i, playerShip, _firstTile));
        }

        DiceRollTest.DiceResult += GetDiceRoll;
    }
    
    private void StartGame()
    {
        _currentPlayer = 0;
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        _currentPlayer = -1;

        do
        {
            _currentPlayer = _currentPlayer >= _totalPlayerNumber-1 ? 0 : _currentPlayer + 1;
            Debug.Log(_currentPlayer);
            StartCoroutine(PlayRound());
            
            yield return new WaitUntil(()=>_roundFinished);

        } while (!CheckVictory(_players[_currentPlayer]));
        
        Debug.Log(_currentPlayer + " Has won!");
        
        yield return null;
    }

    private IEnumerator PlayRound()
    {
        _roundFinished = false;

        _currentDiceNumber = 0;
        _diceRollCanvas.SetActive(true);
        
        yield return new WaitUntil(()=> _diceRollCanvas.activeInHierarchy == false);

        _players[_currentPlayer].MovePlayerForward(_currentDiceNumber);

        _roundFinished = true;
    }

    private bool CheckVictory(Player player)
    {
        return _lastTile.gameObject == player.CurentTile.gameObject;
    }

    private void GetDiceRoll(int diceRoll)
    {
        _currentDiceNumber = diceRoll;
    }
}
