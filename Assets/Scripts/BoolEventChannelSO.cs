using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewBoolEventChannel", menuName = "SOs/EventChannel/Bool Event Channel")]
public class BoolEventChannelSO : ScriptableObject
{
    public UnityAction<bool> OnEvtRaised;

    public void RaiseEvent(bool b)
    {
        if (OnEvtRaised != null)
        {
            OnEvtRaised.Invoke(b);
        }
    }
}
