using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Streak : MonoBehaviour
{
    public Stats stats;
    public HintManager hintManager;
    public static int currentStreak;
    public static int highestStreak;

    private void Start()
    {
        currentStreak = stats.currentStreak;
        highestStreak = stats.highestStreak;
    }

    public void AddStreak()
    {
        currentStreak++;
        if (currentStreak > highestStreak) highestStreak = currentStreak;
        Debug.Log("Current Streak: " + currentStreak);
        if (currentStreak % 3 == 0)
        {
            hintManager.AddHint();
            stats.SaveStats();
        }
        stats.SaveStats();
    }
    public void ResetStreak()
    {
        currentStreak = 0;
        stats.SaveStats();
    }
}
