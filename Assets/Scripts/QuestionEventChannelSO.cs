using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewQuestionEventChannel", menuName = "SOs/EventChannel/Question Event Channel")]
public class QuestionEventChannelSO : ScriptableObject
{
    public UnityAction<QuestionSO> OnEvtRaised;

    public void RaiseEvent(QuestionSO question)
    {
        if (OnEvtRaised != null)
        {
            OnEvtRaised.Invoke(question);
        }
    }
}
