using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewVoidEventChannel", menuName = "SOs/EventChannel/Void Event Channel")]
public class VoidEventChannelSO : ScriptableObject
{
    public UnityAction OnEvtRaised;

    public void RaiseEvent()
    {
        if (OnEvtRaised != null)
        {
            OnEvtRaised.Invoke();
        }
    }
}
