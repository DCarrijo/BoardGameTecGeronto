using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class VfxEventListener : MonoBehaviour
{
    public static Action OnParticleStop;
    
    [SerializeField] private VisualEffect _vfx;

    private bool _startedCorroutine = false;

    private void Awake()
    {
        if (_vfx == null)
        {
            _vfx = this.GetComponent<VisualEffect>();
        }
    }

    private void Update()
    {
        if (_vfx.aliveParticleCount > 0 && !_startedCorroutine)
        {
            _startedCorroutine = true;
            StartCoroutine(WaitFinish());
        }
    }

    private IEnumerator WaitFinish()
    {
        yield return new WaitUntil(()=>_vfx.aliveParticleCount <= 0);

        _startedCorroutine = false;
        OnParticleStop?.Invoke();
        this.gameObject.SetActive(false);
    }
}
