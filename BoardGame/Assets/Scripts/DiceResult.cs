using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiceResult : MonoBehaviour
{
    [SerializeField] private float _minVelocity = 0.05f;
    [SerializeField] private float _maxRaycastDistance = 10;
    [SerializeField] private LayerMask _faceLayerMasks;
    private Rigidbody _rigidbody;
    private bool _isRolling = false;

    public static Action<int> OnResult;

    private readonly Dictionary<int, Vector3> _numberVectorDict = new Dictionary<int, Vector3>
    {
        {1, Vector3.forward},
        {2, Vector3.left},
        {3, Vector3.down},
        {4, Vector3.up},
        {5, Vector3.right},
        {6, Vector3.back}
    };
    
    private void Awake()
    {
        _isRolling = false;
        ThrowDice.OnRoll += ListenOnRol;
        _rigidbody = this.GetComponent<Rigidbody>();
    }

    private void OnDestroy()
    {
        ThrowDice.OnRoll -= ListenOnRol;
    }

    private void Update()
    {
        CheckDice();
    }

    private void CheckDice()
    {
        if (!_isRolling) return;
        
        if (_rigidbody.velocity.magnitude <= _minVelocity)
        {
            OnResult?.Invoke(GetNumber());
        }
    }

    private int GetNumber()
    {
        int aux = 0;

        foreach (var pair in _numberVectorDict)
        {
            if (Physics.Raycast(this.transform.position, 
                                    this.transform.TransformDirection(pair.Value), 
                                        _maxRaycastDistance, _faceLayerMasks))
            {
                aux = pair.Key;
            }
        }

        _isRolling = false;
        return aux;
    }

    private void ListenOnRol()
    {
        _isRolling = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        
        foreach (var pair in _numberVectorDict)
        {
            Gizmos.DrawRay(this.transform.position, this.transform.TransformDirection(pair.Value));
        }
    }
}
