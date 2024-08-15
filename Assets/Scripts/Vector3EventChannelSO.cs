using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewVector3EventChannel", menuName = "SOs/EventChannel/Vector3 Event Channel")]
public class Vector3EventChannelSO : ScriptableObject
{
    public UnityAction<Vector3> OnEvtRaised;

    public void RaiseEvent(Vector3 v)
    {
        if (OnEvtRaised != null)
        {
            OnEvtRaised.Invoke(v);
        }
    }
}
