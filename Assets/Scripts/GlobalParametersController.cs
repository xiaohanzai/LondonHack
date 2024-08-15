using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalParametersController : MonoBehaviour
{
    [SerializeField] private float cellSize = 0.5f;

    [Header("Broadcasting on")]
    [SerializeField] private GlobalParametersEventChannelSO globalParametersEventChannel;

    private bool hasBroadcasted;

    void Update()
    {
        if (!hasBroadcasted)
        {
            globalParametersEventChannel.RaiseEvent(transform.position, cellSize);
            hasBroadcasted = true;
        }
    }
}
