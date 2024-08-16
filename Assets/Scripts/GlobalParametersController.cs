using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalParametersController : MonoBehaviour
{
    [SerializeField] private float cellSize = 0.5f;

    [Header("Broadcasting on")]
    [SerializeField] private GlobalParametersEventChannelSO globalParametersEventChannel;

    private bool hasBroadcasted;

    void Start()
    {
        Ray ray = new Ray(Camera.main.transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit)) // TODO: this might hit a chair
        {
            transform.position = hit.point;
        }
    }
    void Update()
    {
        if (!hasBroadcasted)
        {
            globalParametersEventChannel.RaiseEvent(transform.position, cellSize);
            hasBroadcasted = true;
        }
    }
}
