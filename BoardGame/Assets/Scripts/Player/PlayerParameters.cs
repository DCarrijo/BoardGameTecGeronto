﻿using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerParameters", menuName = "ScriptableObjects/PlayerParameters", order = 1)]
public class PlayerParameters : ScriptableObject
{
    [BoxGroup("MovementTween")]
    public float MovementDuretion = 1f;

    [BoxGroup("MovementTween")] 
    public AnimationCurve MovementAnimationCurve;
}
