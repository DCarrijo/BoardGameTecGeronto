using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Random = UnityEngine.Random;

public class DiceManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _dice;
    [SerializeField] private DOTweenAnimation _diceAnimation;
    public static Action<int> OnDiceNumberChoose;

    private bool _diceRolled = false;

    private readonly Dictionary<int, Vector3> _numberVectorDict = new Dictionary<int, Vector3>
    {
        {1, new Vector3(0,0,0)},
        {2, new Vector3(0,90,0)},
        {3, new Vector3(-90,0,0)},
        {4, new Vector3(90,0,0)},
        {5, new Vector3(0,-90,0)},
        {6, new Vector3(180,0,0)}
    };

    public IEnumerator StartDiceRoll()
    {
        _diceRolled = false;
        this.gameObject.SetActive(true);
        
        yield return new WaitUntil(() => _diceRolled);
        
        yield return new WaitForSeconds(1f);

        this.gameObject.SetActive(false);
    }
    
    public void DiceRoll()
    {
        _diceAnimation.DOPause();
        int diceNumber = Random.Range(1, 7);
        _text.text = diceNumber.ToString();
        _dice.transform.localRotation = Quaternion.Euler( _numberVectorDict[diceNumber]);
        OnDiceNumberChoose?.Invoke(diceNumber);
        _diceRolled = true;
    }
}
