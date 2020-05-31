using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.VFX;

[System.Serializable]
public enum PlayerEvents
{
    Null = 0,
    Acertou, 
    AcertouX2,
    Errou,
    ErrouX2,
    Question,
    Shield,
    Shock,
    PowerUp, 
    PowerDown
}

public class PlayerComponents : SerializedMonoBehaviour
{
    [SerializeField] private VisualEffect[] _trailEffects;
    [SerializeField] private VisualEffect[] _turboEffects;
    
    [SerializeField] private Dictionary<PlayerEvents, GameObject> _vfxDictionary;

    [SerializeField] private CinemachineVirtualCamera _personalCam;
    [SerializeField] private DOTweenAnimation _cameraAnimation;

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

    public void PlayShieldEffect()
    {
        _vfxDictionary[PlayerEvents.Shield].SetActive(true);
    }
    

    private void ParticleEnded()
    {
        _canContinue = true;
    }

    public void CloseUp()
    {
        _personalCam.Priority = 100;
        _cameraAnimation.DORestart();
    }

    public void ReturnCam()
    {
        _personalCam.Priority = -2;
        _cameraAnimation.DORewind();
    }
}
