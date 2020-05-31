using UnityEngine;
using UnityEngine.UI;

public class MinimapSlider : MonoBehaviour
{
    [SerializeField] private Image[] _spaceshipImages;
    [SerializeField] private Slider[] _playerSliders;
    private int _playerCount = 0;
    private float[] _playersProgression;
    private Player[] _players;

    private void Awake()
    {
        foreach (Image image in _spaceshipImages)
        {
            image.enabled = false;
        }
    }

    public void SetupSpaceShips(ref Sprite[] spaceShipSprites, Player[] players)
    {
        _players = players;
        _playerCount = spaceShipSprites.Length; 
        
        _playersProgression = new float[_playerCount];

        for (int i = 0; i < _playerCount; i++)
        {
            _spaceshipImages[i].enabled = true;
            _spaceshipImages[i].sprite = spaceShipSprites[i];
            _playersProgression[i] = 0f;
        }

        this.enabled = true;
    }


    private void Update()
    {
        for (int i = 0; i < _playerCount; i++)
        {
            _playerSliders[i].value = _players[i].GetPercentageToWin();
        }
    }
}
