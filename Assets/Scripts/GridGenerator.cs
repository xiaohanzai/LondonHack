using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private LineRenderer linePrefab;

    [Header("Listening to")]
    [SerializeField] private GlobalParametersEventChannelSO globalParametersEventChannel;
    [SerializeField] private QuestionEventChannelSO questionEventChannel;

    private float cellSize = 0.5f;
    private Vector3 gridOrigin = Vector3.zero;

    void Start()
    {
        globalParametersEventChannel.OnEvtRaised += SetParameters;
        questionEventChannel.OnEvtRaised += ReceiveQuestion;
    }

    private void OnDestroy()
    {
        globalParametersEventChannel.OnEvtRaised -= SetParameters;
        questionEventChannel.OnEvtRaised -= ReceiveQuestion;
    }

    private void SetParameters(Vector3 o, float s)
    {
        gridOrigin = o;
        cellSize = s;
    }

    private void ReceiveQuestion(QuestionSO question)
    {
        DrawGrid(question.xMin, question.xMax, question.yMin, question.yMax, question.zMin, question.zMax);
    }

    private void DrawGrid(int xMin, int xMax, int yMin, int yMax, int zMin, int zMax)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        for (int i = xMin; i < xMax + 1; i++)
        {
            for (int j = yMin; j < yMax + 1; j++)
            {
                LineRenderer line = Instantiate(linePrefab, transform);
                line.SetPosition(0, gridOrigin + new Vector3(i * cellSize, j * cellSize, zMin * cellSize));
                line.SetPosition(1, gridOrigin + new Vector3(i * cellSize, j * cellSize, zMax * cellSize));
                Material material = line.material;
                material.color = new Color(material.color.r, material.color.g, material.color.b, 1f / ((Mathf.Abs(i) + 1) * (Mathf.Abs(i) + 1) + (Mathf.Abs(j) + 1) * (Mathf.Abs(j) + 1)));
            }
        }

        for (int i = yMin; i < yMax + 1; i++)
        {
            for (int j = zMin; j < zMax + 1; j++)
            {
                LineRenderer line = Instantiate(linePrefab, transform);
                line.SetPosition(0, gridOrigin + new Vector3(xMin * cellSize, i * cellSize, j * cellSize));
                line.SetPosition(1, gridOrigin + new Vector3(xMax * cellSize, i * cellSize, j * cellSize));
                Material material = line.material;
                material.color = new Color(material.color.r, material.color.g, material.color.b, 1f / ((Mathf.Abs(i) + 1) * (Mathf.Abs(i) + 1) + (Mathf.Abs(j) + 1) * (Mathf.Abs(j) + 1)));
            }
        }

        for (int i = xMin; i < xMax + 1; i++)
        {
            for (int j = zMin; j < zMax + 1; j++)
            {
                LineRenderer line = Instantiate(linePrefab, transform);
                line.SetPosition(0, gridOrigin + new Vector3(i * cellSize, yMin * cellSize, j * cellSize));
                line.SetPosition(1, gridOrigin + new Vector3(i * cellSize, yMax * cellSize, j * cellSize));
                Material material = line.material;
                material.color = new Color(material.color.r, material.color.g, material.color.b, 1f / ((Mathf.Abs(i) + 1) * (Mathf.Abs(i) + 1) + (Mathf.Abs(j) + 1) * (Mathf.Abs(j) + 1)));
            }
        }
    }
}
