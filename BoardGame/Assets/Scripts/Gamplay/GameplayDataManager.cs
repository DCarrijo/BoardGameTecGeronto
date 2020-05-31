using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "GameplayDataManager", menuName = "ScriptableObjects/GameplayDataManager", order = 1)]
public class GameplayDataManager : ScriptableObject
{
    public int PlayerCount = 0;

    public Categories[] Categorias;

    [SerializeField] [PreviewField(Height = 100)] private GameObject[] _playerPrefabs;
    
    [SerializeField] [PreviewField(Height = 100)] private List<Sprite> _playerMiniMapImages;

    public GameObject[] PlayerPrefabs
    {
        get => _playerPrefabs;
    }
    
    public Sprite[] GetImages(int howMany) => _playerMiniMapImages.GetRange(0, howMany).ToArray();
}
