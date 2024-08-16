using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Axis : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private float width = 0.04f;
    [SerializeField] private AxisLabel labelPrefab;
    [SerializeField] private TextMeshPro axisLabelText;
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
        float s = (vMax - vMin) * cellSize;
        if (direction == Direction.X)
        {
            cube.transform.localScale = new Vector3(s, width, width);
        }
        else if (direction == Direction.Y)
        {
            cube.transform.localScale = new Vector3(width, s, width);
        }
        else
        {
            cube.transform.localScale = new Vector3(width, width, s);
        }
        cube.transform.position = gridOrigin + directionDict[direction] * cellSize * (vMax + vMin) / 2f;
        Debug.Log(direction + " " + cube.transform.position.ToString());

        for (int i = vMin; i < vMax + 1; i++)
        {
            Vector3 pos = gridOrigin + directionDict[direction] * cellSize * i;
            AxisLabel label = Instantiate(labelPrefab, transform);
            label.transform.position = pos - new Vector3(0.1f * cellSize, 0.1f * cellSize, 0);
            if (i != 0 || direction == Direction.X)
            {
                label.SetText(i);
            }
            label.SetRotation(direction == Direction.Y);
        }

        arrow.transform.position = gridOrigin + directionDict[direction] * cellSize * vMax;
        axisLabelText.text = "X";
        if (direction == Direction.Y)
        {
            arrow.transform.rotation *= Quaternion.Euler(0, 90, 90);
            axisLabelText.text = "Z";
        }
        else if (direction == Direction.Z)
        {
            arrow.transform.rotation *= Quaternion.Euler(0, -90, 0);
            axisLabelText.text = "Y";
        }
    }
}
