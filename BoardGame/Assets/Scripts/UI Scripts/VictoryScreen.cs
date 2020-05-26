using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _winText;

    public void SetWin(int winner)
    {
        _winText.text = "Player " + winner + " venceu.";
        this.gameObject.SetActive(true);
    }
}
