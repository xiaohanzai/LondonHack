using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewQuestionSO", menuName = "SOs/Question")]
public class QuestionSO : ScriptableObject
{
    [Header("Grid Min Max")]
    public int xMin;
    public int xMax;
    public int yMin;
    public int yMax;
    public int zMin;
    public int zMax;

    [Header("Question Min Max")]
    public int xMinQ;
    public int xMaxQ;
    public int yMinQ;
    public int yMaxQ;
    public int zMinQ;
    public int zMaxQ;
}
