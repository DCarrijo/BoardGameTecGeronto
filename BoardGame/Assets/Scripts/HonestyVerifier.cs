using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class HonestyVerifier : MonoBehaviour
{
    private float _experimentTime = 0f;
    
    private List<int> _results;

    [SerializeField] private ThrowDice _dice;
    private int[] _partialResults = {0, 0, 0, 0, 0, 0};
    private float[] _proportions = {0f, 0f, 0f, 0f, 0f, 0f};

    [SerializeField] private TextMeshProUGUI _rolls;
    [SerializeField] private TextMeshProUGUI _proportionsText;
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private TextMeshProUGUI _experimentTimeText;

    private void Awake()
    {
        _results = new List<int>();

        _experimentTime = 0;

        DiceResult.OnResult += ListenResults;
        UpdateText();
    }

    private void OnDestroy()
    {
        DiceResult.OnResult -= ListenResults;
    }

    private void Start()
    {
        Roll();
    }

    private void Update()
    {
        _experimentTime += Time.deltaTime;
        _experimentTimeText.text = _experimentTime.ToString(CultureInfo.InvariantCulture);
    }

    private void Roll()
    {
        _dice.Roll();
    }

    private void ListenResults(int result)
    {
        _results.Add(result);
        UpdateResults(result);
        UpdateText();
        Roll();
    }

    private void UpdateResults(int lastResult)
    {
        _partialResults[lastResult - 1]++;

        for (int i = 0; i < 6; i++)
        {
            _proportions[i] = (float) _partialResults[i] / (float) _results.Count;
        }
    }

    private void UpdateText()
    {
        _countText.text = $"Total: {_results.Count}";
        
        string aux = "Rolls: \n";
        for(int i = 0; i < 6; i++)
        {
            aux += $"{i + 1} -> {_partialResults[i]}\n";
        }
        _rolls.text = aux;
        
        aux = "Proportions: \n";
        for(int i = 0; i < 6; i++)
        {
            aux += $"{i + 1} -> {_proportions[i]}\n";
        }
        _proportionsText.text = aux;
    }
}