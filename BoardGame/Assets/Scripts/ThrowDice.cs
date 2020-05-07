using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using Rewired.Data;
using Random = UnityEngine.Random;

public class ThrowDice : MonoBehaviour
{
    [SerializeField] private Vector2 _forceY = new Vector2(10, 30);
    [SerializeField] private Vector2 _forceX = new Vector2(-10, 10);
    [SerializeField] private Vector2 _forceZ = new Vector2(-10, 10);
    [SerializeField] private Vector2 _rotationX = new Vector2(-30, 30);
    [SerializeField] private Vector2 _rotationY = new Vector2(-30, 30);
    [SerializeField] private Vector2 _rotationZ = new Vector2(-30, 30);

    [SerializeField] private Material _material;
    private Tween _blink;

    private Rigidbody _rigidbody;
    private Collider _collider;

    private int _currentContactCount = 0;

    public static Action OnRoll;

    private void Awake()
    {
        this._rigidbody = this.GetComponent<Rigidbody>();
        this._collider = this.GetComponent<Collider>();
        _blink = DOTween.To(()=>_material.GetColor("_Emission"), x =>_material.SetColor("_Emission", x), Color.black, 0.2f).SetAutoKill(false).From(Color.white).SetEase(Ease.InExpo);
        _rigidbody.maxAngularVelocity = float.MaxValue;
    }

    private void Update()
    {
        if (_rigidbody.velocity.y < 0)
        {
            _collider.enabled = true;
        }
    }

    [Button(ButtonSizes.Gigantic)]
    public void Roll()
    {
        OnRoll?.Invoke();
        _collider.enabled = false;
        this.transform.rotation = Random.rotationUniform;
        _rigidbody.velocity = (new Vector3(Random.Range(_forceX.x, _forceX.y),Random.Range(_forceY.x, _forceY.y), Random.Range(_forceZ.x, _forceZ.y)));
        _rigidbody.angularVelocity = (new Vector3(Random.Range(_rotationX.x, _rotationX.y) * (Random.Range(-1, 2) == 0 ? 1 : -1),Random.Range(_rotationY.x, _rotationY.y) * (Random.Range(-1, 2)), Random.Range(_rotationZ.x, _rotationZ.y) * (Random.Range(-1, 1) == 0 ? 1 : -1) ));
    }
    
    private void OnCollisionEnter(Collision other)
    {
        _blink.Restart();
        _currentContactCount = other.contacts.Length;
    }

    private void OnCollisionStay(Collision other)
    {
        int aux = other.contacts.Length;

        if (aux == _currentContactCount) return;

        _blink.Restart();
        _currentContactCount = aux;
    }
}
