using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private QuestionSO[] questions2D;
    [SerializeField] private QuestionSO[] questions3D;

    [Header("Broadcasting on")]
    [SerializeField] private QuestionEventChannelSO questionEventChannel;

    private int ind;

    public void NextQuestion()
    {
        if (ind < questions2D.Length)
        {
            questionEventChannel.RaiseEvent(questions2D[ind]);
            ind++;
        }
        else if (ind < questions3D.Length + questions2D.Length)
        {
            questionEventChannel.RaiseEvent(questions3D[ind - questions2D.Length]);
            ind++;
        }
    }
}
