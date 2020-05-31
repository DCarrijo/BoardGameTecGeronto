using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenOnAnswerEvent : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        ShowQuestion.OnAnswerCallBack += ListenOnAnswerCallback;
        _particleSystem = this.GetComponent<ParticleSystem>();
    }

    private void OnDestroy()
    {
        ShowQuestion.OnAnswerCallBack -= ListenOnAnswerCallback;
    }
    

    private void ListenOnAnswerCallback()
    {
        _particleSystem.Stop();
    }
    
}
