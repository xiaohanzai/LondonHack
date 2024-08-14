using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewVector3sEventChannel", menuName = "SOs/EventChannel/Vector3s Event Channel")]
public class Vector3sEventChannelSO : ScriptableObject
{
    public UnityAction<Vector3, Vector3, Vector3> OnEvtRaised;

    public void RaiseEvent(Vector3 v0, Vector3 v1, Vector3 v2)
    {
        if (OnEvtRaised != null)
        {
            OnEvtRaised.Invoke(v0, v1, v2);
        }
    }
}
