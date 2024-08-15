using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AxisLabel : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMeshPro;

    public void SetText(int n)
    {
        textMeshPro.text = n.ToString();
    }

    public void SetRotation(bool isVerticalAxis)
    {
        if (isVerticalAxis)
        {
            textMeshPro.transform.localRotation = Quaternion.identity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        FollowCamera();
    }

    private void FollowCamera()
    {
        Vector3 direction = Camera.main.transform.position - transform.position;
        direction.y = 0;
        if (direction.sqrMagnitude > 0.0f)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Euler(0, rotation.y, 0);
        }
    }
}
