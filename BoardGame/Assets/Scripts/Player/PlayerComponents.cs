using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;

[System.Serializable]
public enum PlayerEvents
{
    Null = 0,
    Acertou, 
    AcertouX2,
    Errou,
    ErrouX2,
    Question
}

public class PlayerComponents : SerializedMonoBehaviour
{
    [SerializeField] private VisualEffect[] _trailEffects;
    [SerializeField] private VisualEffect[] _turboEffects;
    
    [SerializeField] private Dictionary<PlayerEvents, GameObject> _vfxDictionary;

    private bool _canContinue = false;
    
    private void Awake()
    {
        VfxEventListener.OnParticleStop += ParticleEnded;
        
        StopTurbo();
    }

    private void OnDestroy()
    {
        VfxEventListener.OnParticleStop -= ParticleEnded;
    }

    public void StartTurbo()
    {
        foreach (var trail in _trailEffects)
        {
            trail.Stop();
        }

        foreach (var turbo in _turboEffects)
        {
            turbo.Play();
        }
    }

    public void StopTurbo()
    {
        foreach (var trail in _trailEffects)
        {
            trail.Play();
        }

        foreach (var turbo in _turboEffects)
        {
            turbo.Stop();
        }
    }

    public IEnumerator PlayEffect(PlayerEvents trigger)
    {
        _canContinue = false;
        _vfxDictionary[trigger].SetActive(true);
        
        yield return new WaitUntil(() => _canContinue);
    }

    private void ParticleEnded()
    {
        _canContinue = true;
    }
}
