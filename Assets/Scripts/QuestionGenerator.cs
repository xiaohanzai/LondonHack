using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionGenerator : MonoBehaviour
{
    [SerializeField] private Target[] targetPrefabs;
    private Dictionary<Target.Type, Target> targetDict;

    private Vector3 gridOrigin;
    private float cellSize;

    [Header("Listening to")]
    [SerializeField] private GlobalParametersEventChannelSO globalParametersEventChannel;
    [SerializeField] private QuestionEventChannelSO questionEventChannel;

    [Header("Broadcasting on")]
    [SerializeField] private Vector3sEventChannelSO answersSetEventChannel;

    void Start()
    {
        globalParametersEventChannel.OnEvtRaised += SetParameters;
        questionEventChannel.OnEvtRaised += ReceiveQuestion;

        targetDict = new Dictionary<Target.Type, Target>();
        foreach (var item in targetPrefabs)
        {
            targetDict.Add(item.TargetType, item);
        }
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
        DestroyTargets();
        GenerateAnswers(question.xMinQ, question.xMaxQ, question.yMinQ, question.yMaxQ, question.zMinQ, question.zMaxQ);
    }

    private void GenerateAnswers(int xMin, int xMax, int yMin, int yMax, int zMin, int zMax)
    {
        int x = Random.Range(xMin, xMax);
        int y = Random.Range(yMin, yMax);
        int z = Random.Range(zMin, zMax);

        bool is2D = yMax == 0;
        Vector3 targetPos = new Vector3(x, y, z);
        InstantiateTarget(targetPos, is2D, true);
        Vector3 wrongAnswer1 = GenerateRandomWrongAnswer(xMin, xMax, yMin, yMax, zMin, zMax, x, y, z);
        InstantiateTarget(wrongAnswer1, is2D, false);
        Vector3 wrongAnswer2 = GenerateRandomWrongAnswer(xMin, xMax, yMin, yMax, zMin, zMax, x, y, z);
        InstantiateTarget(wrongAnswer2, is2D, false);

        answersSetEventChannel.RaiseEvent(targetPos, wrongAnswer1, wrongAnswer2);
    }

    private Vector3 GenerateRandomWrongAnswer(int xMin, int xMax, int yMin, int yMax, int zMin, int zMax, int x, int y, int z)
    {
        int newX = Random.Range(xMin, xMax);
        int newY = Random.Range(yMin, yMax);
        int newZ = Random.Range(zMin, zMax);
        while (newX == x && newY == y && newZ == z || Mathf.Approximately(newX / x, newZ / z))
        {
            newX = Random.Range(xMin, xMax);
            newY = Random.Range(yMin, yMax);
            newZ = Random.Range(zMin, zMax);
        }
        return new Vector3(newX, newY, newZ);
    }

    private void InstantiateTarget(Vector3 pos, bool is2D, bool isCorrectAnswer)
    {
        Target.Type type;
        if (is2D && isCorrectAnswer)
        {
            type = Target.Type.Mole;
        }
        else if (!is2D && isCorrectAnswer)
        {
            type = Target.Type.Ghost;
        }
        else
        {
            type = Target.Type.Empty;
        }
        Target target = Instantiate(targetDict[type], transform);
        target.transform.position = pos * cellSize + gridOrigin;
        Vector3 direction = gridOrigin - target.transform.position;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);
        target.transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
    }

    private void DestroyTargets()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
