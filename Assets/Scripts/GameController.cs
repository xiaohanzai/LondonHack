using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private QuestionSO[] questions2D;
    [SerializeField] private QuestionSO[] questions3D;

    [Header("UI")]
    [SerializeField] private GameObject onboardingPage;
    [SerializeField] private GameObject nextLevelPage;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject nextLevelButton;

    [Header("Broadcasting on")]
    [SerializeField] private VoidEventChannelSO newLevelEventChannel;
    [SerializeField] private QuestionEventChannelSO questionEventChannel;
    [SerializeField] private VoidEventChannelSO questionStartEventChannel;

    private int ind;

    private void Start()
    {
        SetUpOnboarding();
    }

    private void SetUpOnboarding()
    {
        onboardingPage.SetActive(true);
        playButton.SetActive(true);
        SetUpPanel(onboardingPage);

        nextLevelPage.SetActive(false);
        nextLevelButton.SetActive(false);
    }

    public void StartGame()
    {
        onboardingPage.SetActive(false);
        playButton.SetActive(false);
        NextQuestion();
    }

    public void StartNextLevel()
    {
        HideNextLevelPage();
        NextQuestion();
    }

    private void ShowNextLevelPage()
    {
        SetUpPanel(nextLevelPage);
        nextLevelPage.SetActive(true);
        nextLevelButton.SetActive(true);
    }

    private void HideNextLevelPage()
    {
        nextLevelPage.SetActive(false);
        nextLevelButton.SetActive(false);
    }

    private void SetUpPanel(GameObject panel)
    {
        panel.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1f;
        panel.transform.position = new Vector3(panel.transform.position.x, 1.5f, panel.transform.position.z);
        Vector3 direction = Camera.main.transform.forward;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);
        panel.transform.rotation = Quaternion.Euler(0, rotation.y, 0);
    }

    public void NextQuestion()
    {
        if (ind < questions2D.Length)
        {
            questionEventChannel.RaiseEvent(questions2D[ind]);
            questionStartEventChannel.RaiseEvent();
            ind++;
        }
        else if (ind == questions2D.Length)
        {
            ShowNextLevelPage();
            newLevelEventChannel.RaiseEvent();
            ind++;
        }
        else if (ind < questions3D.Length + questions2D.Length + 1)
        {
            questionEventChannel.RaiseEvent(questions3D[ind - questions2D.Length - 1]);
            questionStartEventChannel.RaiseEvent();
            ind++;
        }
    }
}
