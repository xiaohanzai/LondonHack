using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewGlobalParametersEventChannel", menuName = "SOs/EventChannel/Global Parameters Event Channel")]
public class GlobalParametersEventChannelSO : ScriptableObject
{
    public UnityAction<Vector3, float> OnEvtRaised;

    public void RaiseEvent(Vector3 o, float s)
    {
        if (OnEvtRaised != null)
        {
            OnEvtRaised.Invoke(o, s);
        }
    }
}
