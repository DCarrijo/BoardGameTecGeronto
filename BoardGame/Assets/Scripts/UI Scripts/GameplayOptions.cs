using System.Collections.Generic;
using System.Linq;
using TMPro.SpriteAssetUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayOptions : MonoBehaviour
{
    [SerializeField] private GameplayDataManager _gameplayData;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Toggle[] _categories;
    private int _playerCount = 2;

    private bool _selectedThreeCategories = false;

    private void Update()
    {
        _startGameButton.interactable = _selectedThreeCategories;
    }

    public void SetPlayerCount(int number)
    {
        _playerCount = number;
    }

    public void OnToggle()
    {
        int count = 0;
        foreach (var tog in _categories)
        {
            if (tog.isOn)
            {
                count++;
            }
        }

        if (count >= 3)
        {
            foreach (var tog in _categories.Where(toggle => !toggle.isOn))
            {
                tog.interactable = false;
            }

            _selectedThreeCategories = true;
        }
        else
        {
            foreach (var tog in _categories)
            {
                tog.interactable = true;
            }

            _selectedThreeCategories = false;
        }
    }

    public void SetOptionsToFile()
    {
        var aux = new List<Categories>();
        foreach (Toggle tog in _categories.Where(toggle => toggle.isOn))
        {
            aux.Add(tog.gameObject.GetComponent<ToggleCategorie>().Categorie);
        }

        _gameplayData.Categorias= aux.ToArray();

        _gameplayData.PlayerCount = _playerCount;
        
        StartGame();
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Mapa01");
    }
}

