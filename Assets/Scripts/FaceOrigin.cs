using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceOrigin : MonoBehaviour
{
    [SerializeField] private Transform originTransform;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 direction = originTransform.position - transform.position;
        direction.y = 0;
        if (direction.sqrMagnitude > 0.0f)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
