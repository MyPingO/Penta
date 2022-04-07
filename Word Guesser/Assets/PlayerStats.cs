using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerStats
{
    public int hintCount, currentStreak, highestStreak, guessLength;
    public string difficulty;
    
    public PlayerStats (Stats stats)
    {
        hintCount = stats.hintCount;
        currentStreak = stats.currentStreak;
        highestStreak = stats.highestStreak;
        difficulty = stats.difficulty;
        guessLength = stats.guessLength;
    }
}
