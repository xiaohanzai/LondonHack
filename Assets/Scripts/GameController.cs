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
    [SerializeField] private GameObject instructionPanel;

    [Header("Audio")]
    [SerializeField] private AudioSource level1BGM;
    [SerializeField] private AudioSource level2BGM;

    [Header("Broadcasting on")]
    [SerializeField] private VoidEventChannelSO newLevelEventChannel;
    [SerializeField] private QuestionEventChannelSO questionEventChannel;
    [SerializeField] private VoidEventChannelSO questionStartEventChannel;

    private int ind;

    private void Start()
    {
        SetUpOnboarding();
        level1BGM.Play();
    }

    private void SetUpOnboarding()
    {
        onboardingPage.SetActive(true);
        playButton.SetActive(true);
        SetUpPanel(onboardingPage);

        nextLevelPage.SetActive(false);
        nextLevelButton.SetActive(false);

        instructionPanel.SetActive(false);
    }

    public void StartGame()
    {
        onboardingPage.SetActive(false);
        playButton.SetActive(false);
        instructionPanel.SetActive(true);
        NextQuestion();
    }

    public void StartNextLevel()
    {
        HideNextLevelPage();
        NextQuestion();
    }

    private void ShowNextLevelPage()
    {
        instructionPanel.SetActive(false);
        nextLevelPage.SetActive(true);
        SetUpPanel(nextLevelPage);
        nextLevelButton.SetActive(true);
        level1BGM.Stop();
        level2BGM.Play();
    }

    private void HideNextLevelPage()
    {
        nextLevelPage.SetActive(false);
        nextLevelButton.SetActive(false);
        instructionPanel.SetActive(true);
    }

    private void SetUpPanel(GameObject panel)
    {
        panel.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 1f;
        panel.transform.position = new Vector3(panel.transform.position.x, 1.5f, panel.transform.position.z);
        Vector3 direction = Camera.main.transform.forward;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);
        panel.transform.rotation = Quaternion.Euler(0, rotation.eulerAngles.y, 0);
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
