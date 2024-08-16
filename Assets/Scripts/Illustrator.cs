using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Illustrator : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material rightAnswerMaterial;
    [SerializeField] private Material wrongAnswerMaterial;

    public void ApplyRightAnswerMat()
    {
        meshRenderer.material = rightAnswerMaterial;
        lineRenderer.material = rightAnswerMaterial;
    }

    public void ApplyWrongAnswerMat()
    {
        meshRenderer.material = wrongAnswerMaterial;
        lineRenderer.material = wrongAnswerMaterial;
    }

    public LineRenderer GetLineRenderer()
    {
        return lineRenderer;
    }
}
