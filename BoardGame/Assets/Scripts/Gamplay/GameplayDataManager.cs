using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "GameplayDataManager", menuName = "ScriptableObjects/GameplayDataManager", order = 1)]
public class GameplayDataManager : ScriptableObject
{
    public int PlayerCount = 0;

    public Categories[] Categorias;

    [SerializeField] [PreviewField(Height = 100)] private GameObject[] _playerPrefabs;
    
    public GameObject[] PlayerPrefabs
    {
        get => _playerPrefabs;
    } 
}
