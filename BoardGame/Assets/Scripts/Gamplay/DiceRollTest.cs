using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceRollTest : MonoBehaviour
{
    public static Action<int> DiceResult;
    
    public void RollDice()
    {
        DiceResult?.Invoke(Random.Range(1, 7));
        this.gameObject.SetActive(false);
    }
}
