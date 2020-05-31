using UnityEngine;

public class ParticleEventListener : MonoBehaviour
{
    private void OnParticleSystemStopped()
    {
        this.gameObject.SetActive(false);
        VfxEventListener.OnParticleStop?.Invoke();        
    }
}
