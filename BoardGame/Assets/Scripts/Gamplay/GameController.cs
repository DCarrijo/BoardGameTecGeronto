using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.iOS;
using UnityEngine.WSA;

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

    [SerializeField] private Material[] _tileMaterials;
    [SerializeField] private Material[] _glowMaterials;

    [SerializeField] private Material _firstTileMaterial;
    [SerializeField] private Material _firstGlowMaterial;
    [SerializeField] private Material _lastTileMaterial;
    [SerializeField] private Material _lastGlowMaterial;

    [SerializeField] private ShowQuestion _questionShower;

    private void Awake()
    {
        StartCoroutine(GameSetup());
    }

    private IEnumerator GameSetup()
    {
        yield return StartCoroutine(SetupPlayers());
        yield return StartCoroutine(SetupMap());
    }

    private IEnumerator SetupPlayers()
    {
        _totalPlayerNumber = _gameplayData.PlayerCount;
        
        _players = new List<Player>();
            
        for (int i = 0; i < _totalPlayerNumber; i++)
        {
            GameObject playerShip = Instantiate(_gameplayData.PlayerPrefabs[i]);
            _players.Add(new Player(i, playerShip, _firstTile));
            yield return null;
        }

        DiceRollTest.DiceResult += GetDiceRoll;
        yield return null;
    }

    private IEnumerator SetupMap()
    {
        Queue<TilesGraph> tilesGraphs = new Queue<TilesGraph>();
        tilesGraphs.Enqueue(_firstTile);
        Queue<TilesGraph> tilesQueue = new Queue<TilesGraph>();
        
        while (tilesGraphs.Count > 0)
        {
            TilesGraph tile = tilesGraphs.Dequeue();

            foreach (var t in tile.GetConnectedTile())
            {
                tilesGraphs.Enqueue(t);

                if (!tilesQueue.Contains(t))
                {
                    tilesQueue.Enqueue(t);
                }
                
                yield return null;
            }

            yield return null;
        }

        int catCounter = 0;
        while (tilesQueue.Count > 0)
        {
            if (catCounter >= _gameplayData.Categorias.Length)
            {
                catCounter = 0;
            }

            tilesQueue.Dequeue().TileManager.SetTile(_gameplayData.Categorias[catCounter], _tileMaterials[catCounter], _glowMaterials[catCounter]);
            catCounter++;
        }
        
        _firstTile.TileManager.SetFirstTile(_firstTileMaterial, _firstGlowMaterial);
        _lastTile.TileManager.SetLastTile(_lastTileMaterial, _lastGlowMaterial);
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

        TilesGraph lastTile = _players[_currentPlayer].CurentTile;
        
        _players[_currentPlayer].MovePlayerForward(_currentDiceNumber);

        _players[_currentPlayer].CurentTile.TileManager.
        
        _questionShower.StartQuestion();
        
        yield return new WaitUntil(()=>_questionShower.gameObject.activeInHierarchy == false);

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
