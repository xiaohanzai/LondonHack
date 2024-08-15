using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axis : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private AxisLabel labelPrefab;
    [SerializeField] private GameObject arrow;

    public enum Direction
    {
        X,
        Y,
        Z
    }
    private Direction direction;

    private int vMin;
    private int vMax;
    private Vector3 gridOrigin;
    private float cellSize;

    private Dictionary<Direction, Vector3> directionDict = new Dictionary<Direction, Vector3>
    {
        { Direction.X, Vector3.right},
        { Direction.Y, Vector3.up },
        { Direction.Z, Vector3.forward }
    };

    public void SetUpAxis(Vector3 o, float s, int vmin, int vmax, Direction dir)
    {
        vMin = vmin;
        vMax = vmax;
        gridOrigin = o;
        cellSize = s;
        direction = dir;
    }

    public void CreateAxisAndLabels()
    {
        lineRenderer.positionCount = vMax - vMin + 1;
        for (int i = vMin; i < vMax + 1; i++)
        {
            Vector3 pos = gridOrigin + directionDict[direction] * cellSize * i;
            lineRenderer.SetPosition(i - vMin, pos);
            AxisLabel label = Instantiate(labelPrefab, transform);
            label.transform.position = pos - new Vector3(0.1f * cellSize, 0.1f * cellSize, 0);
            if (i != 0 || direction == Direction.X)
            {
                label.SetText(i);
            }
            label.SetRotation(direction == Direction.Y);
        }

        arrow.transform.position = gridOrigin + directionDict[direction] * cellSize * vMax;
        if (direction == Direction.Y)
        {
            arrow.transform.rotation *= Quaternion.Euler(0, 90, 90);
        }
        else if (direction == Direction.Z)
        {
            arrow.transform.rotation *= Quaternion.Euler(0, -90, 0);
        }
    }
}
