using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionGenerator : MonoBehaviour
{
    [SerializeField] private Target targetPrefab;
    private Target target;

    private Vector3 gridOrigin;
    private float cellSize;

    private Vector3 targetPos;

    [Header("Listening to")]
    [SerializeField] private GlobalParametersEventChannelSO globalParametersEventChannel;
    [SerializeField] private QuestionEventChannelSO questionEventChannel;

    [Header("Broadcasting on")]
    [SerializeField] private Vector3sEventChannelSO answersSetEventChannel;

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
        GenerateAnswers(question.xMinQ, question.xMaxQ, question.yMinQ, question.yMaxQ, question.zMinQ, question.zMaxQ);
    }

    private void GenerateAnswers(int xMin, int xMax, int yMin, int yMax, int zMin, int zMax)
    {
        int x = Random.Range(xMin, xMax);
        int y = Random.Range(yMin, yMax);
        int z = Random.Range(zMin, zMax);

        targetPos = new Vector3(x, y, z);
        InstantiateTarget();
        answersSetEventChannel.RaiseEvent(targetPos, GenerateRandomWrongAnswer(xMin, xMax, yMin, yMax, zMin, zMax), GenerateRandomWrongAnswer(xMin, xMax, yMin, yMax, zMin, zMax));
    }

    private Vector3 GenerateRandomWrongAnswer(int xMin, int xMax, int yMin, int yMax, int zMin, int zMax)
    {
        return new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), Random.Range(zMin, zMax));
    }

    private void InstantiateTarget()
    {
        target = Instantiate(targetPrefab, targetPos * cellSize + gridOrigin, Quaternion.identity);
    }
}
