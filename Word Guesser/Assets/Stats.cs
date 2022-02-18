using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
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
    }
}
