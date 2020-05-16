using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionChooser : MonoBehaviour
{
    public static int ChoosenDirection { get; private set; } = -1;

    private void OnEnable()
    {
        ChoosenDirection = -1;
    }

    public void ChooseDirection(int derectionIndex)
    {
        ChoosenDirection = derectionIndex;
        this.gameObject.SetActive(false);
    }
}
