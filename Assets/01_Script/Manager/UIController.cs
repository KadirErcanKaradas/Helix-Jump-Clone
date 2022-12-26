using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    private GameManager manager;
    [SerializeField] private GameObject StartPanel;
    [SerializeField] private GameObject FailPanel;
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject InGamePanel;
    [SerializeField] private TMP_Text scoreText;
    private int scoreCount = 0;

    private void OnEnable()
    {
        GameEvent.Score += ScorePlus;
        GameEvent.Fail += FailPanelOpen;
        GameEvent.Win += WinPanelOpen;
    }

    private void OnDisable()
    {
        GameEvent.Score -= ScorePlus;
        GameEvent.Fail -= FailPanelOpen;
        GameEvent.Win -= WinPanelOpen;
    }

    private void Start()
    {
        manager = GameManager.Instance;
        scoreText.text = "" + scoreCount;
    }

    public void TapToStartButton()
    {
        manager.SetGameStage(GameStage.Started);
        StartPanel.SetActive(false);
        InGamePanel.SetActive(true);
    }
    private void WinPanelOpen()
    {
        InGamePanel.SetActive(false);
        WinPanel.SetActive(true);
    }
    private void FailPanelOpen()
    {
        InGamePanel.SetActive(false);
        FailPanel.SetActive(true);
    }
    public void RetryButton()
    {
        SceneManager.LoadScene(0);
    }
    private void ScorePlus()
    {
        scoreCount += 50;
        scoreText.text = "" + scoreCount;
    }
}
