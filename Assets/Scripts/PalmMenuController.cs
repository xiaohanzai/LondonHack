using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalmMenuController : MonoBehaviour
{
    [SerializeField] private GameObject palmMenuObject;
    [SerializeField] private Transform leftHandTransform;

    [SerializeField] private GameObject nextQuestionButton;

    [SerializeField] private Transform[] buttonTransforms;
    [SerializeField] private GameObject[] answerButtons;

    [Header("Listening to")]
    [SerializeField] private VoidEventChannelSO newLevelEventChannel;
    [SerializeField] private VoidEventChannelSO questionStartEventChannel;
    [SerializeField] private VoidEventChannelSO questionCompleteEventChannel;

    void Start()
    {
        FollowLeftHand();
        HideAnswerButtons();
        HideQuestionButton();

#if UNITY_EDITOR
#else
        //palmMenuObject.SetActive(false);
#endif
        newLevelEventChannel.OnEvtRaised += HideQuestionButton;
        newLevelEventChannel.OnEvtRaised += HideAnswerButtons;
        questionCompleteEventChannel.OnEvtRaised += ShowQuestionButton;
        questionStartEventChannel.OnEvtRaised += HideQuestionButton;
        questionStartEventChannel.OnEvtRaised += ShowAnswerButtons;
        questionStartEventChannel.OnEvtRaised += ShuffleButtonPositions;
    }

    private void OnDestroy()
    {
        newLevelEventChannel.OnEvtRaised -= HideQuestionButton;
        newLevelEventChannel.OnEvtRaised -= HideAnswerButtons;
        questionCompleteEventChannel.OnEvtRaised -= ShowQuestionButton;
        questionStartEventChannel.OnEvtRaised -= HideQuestionButton;
        questionStartEventChannel.OnEvtRaised -= ShowAnswerButtons;
        questionStartEventChannel.OnEvtRaised -= ShuffleButtonPositions;
    }

    private void ShuffleButtonPositions()
    {
        int[,] inds = new int[,]
        {
            {0, 1, 2},
            {0, 2, 1},
            {1, 0, 2},
            {2, 0, 1},
            {1, 2, 0},
            {2, 1, 0},
        };
        int n = Random.Range(0, 6);
        answerButtons[0].transform.position = buttonTransforms[inds[n, 0]].position;
        answerButtons[1].transform.position = buttonTransforms[inds[n, 1]].position;
        answerButtons[2].transform.position = buttonTransforms[inds[n, 2]].position;
    }

    public void ActivatePalmMenu()
    {
        palmMenuObject.SetActive(true);
    }

    public void DeactivatePalmMenu()
    {
        palmMenuObject.SetActive(false);
    }

    private void FollowLeftHand()
    {
        transform.parent = leftHandTransform;
        transform.localPosition = new Vector3(0, 0.1f, 0);
        //transform.localRotation *= Quaternion.Euler(0, 180, 0);
    }

    private void ShowQuestionButton()
    {
        nextQuestionButton.SetActive(true);
    }

    private void HideQuestionButton()
    {
        nextQuestionButton.SetActive(false);
    }

    private void ShowAnswerButtons()
    {
        foreach (var item in answerButtons)
        {
            item.SetActive(true);
        }
    }

    private void HideAnswerButtons()
    {
        foreach (var item in answerButtons)
        {
            item.SetActive(false);
        }
    }
}
