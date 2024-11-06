using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoOnEnable : MonoBehaviour
{
    public UnityEvent OnEnableEvent;

    public UnityEvent OnDisableEvent;

    void OnEnable()
    {
        OnEnableEvent?.Invoke();
    }

    private void OnDisable()
    {
        OnDisableEvent?.Invoke();
    }


}
