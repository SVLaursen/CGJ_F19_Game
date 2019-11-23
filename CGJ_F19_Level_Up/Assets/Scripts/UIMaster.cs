using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMaster : MonoBehaviour
{
    [Header("Active UI")] 
    [SerializeField] private TextMeshProUGUI scoreText;
    
    [Header("Entry UI")] 
    [SerializeField] private GameObject entryUiHolder;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI nameInput;
    
    [Header("Highscore UI")]
    [SerializeField] private GameObject highscoreTable;
    [SerializeField] private GameObject highscoreText;

    private Scoreboard _scoreboard;

    private void Awake()
    {
        entryUiHolder.SetActive(false);
        highscoreTable.SetActive(false);
        highscoreText.SetActive(false);

        _scoreboard = GetComponent<Scoreboard>();
    }

    private void FixedUpdate()
    {
        if (scoreText.gameObject.activeSelf)
            scoreText.text = GameMaster.Instance.PlayerScore.ToString();
    }

    public void GameOverUI()
    {
        entryUiHolder.SetActive(true);
        highscoreTable.SetActive(true);
        highscoreText.SetActive(true);
        
        finalScoreText.text = GameMaster.Instance.PlayerScore.ToString();
    }

    public void SaveScore()
    {
        var entry = new ScoreboardDataEntry
        {
            entryName = nameInput.text,
            entryScore = GameMaster.Instance.PlayerScore
        };

        _scoreboard.AddEntry(entry);
        entryUiHolder.SetActive(false);
    }
}
