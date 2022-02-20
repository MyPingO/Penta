using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{
    public TMP_Text currentStreakText;
    public TMP_Text highestStreakText;
    public TMP_Text difficultyText;
    public int guessLength = 5;
    public int hintCount = 0;
    public int currentStreak = 0;
    public int highestStreak = 0;
    public string difficulty;
    private void Start()
    {
        LoadStats();
    }
    public void SaveStats()
    {
        hintCount = HintManager.hintCount;
        currentStreak = Streak.currentStreak;
        highestStreak = Streak.highestStreak;
        difficulty = DifficultyManager.difficulty;
        guessLength = DifficultyManager.guessLength;
        StatsManager.SaveStats(this);
        LoadStats();
    }
    public void LoadStats()
    {
        PlayerStats playerStats = StatsManager.LoadStats();
        hintCount = playerStats.hintCount;
        currentStreak = playerStats.currentStreak;
        highestStreak = playerStats.highestStreak;
        difficulty = playerStats.difficulty;
        guessLength = playerStats.guessLength;
        SetTextStats();
    }
    private void SetTextStats()
    {
        Color lightGreen = new Color32(40, 255, 145, 255);
        Color lightOrange = new Color32(255, 200, 90, 255);
        Color lightRed = new Color32(255, 90, 100, 255);

        currentStreakText.text = "Streak: " + currentStreak.ToString();
        highestStreakText.text = "HighScore: " + highestStreak.ToString();
        difficultyText.text = difficulty;
        if (difficulty == "EASY") difficultyText.color = lightGreen;
        else if (difficulty == "MEDIUM") difficultyText.color = lightOrange;
        else difficultyText.color = lightRed;
    }
}
