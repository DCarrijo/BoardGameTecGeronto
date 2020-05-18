using System.Collections;
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

    [BoxGroup("LookAtTween")] 
    public float LookAtDuration = 0.5f;

    [BoxGroup("LookAtTween")] 
    public AnimationCurve LookDirectionAnimationCurve;

    [BoxGroup("TileParams")] 
    public float TileFloatHightMultiplier = 1.5f;
}
