using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Streak : MonoBehaviour
{
    public HintManager hintManager;
    public static int currentStreak;
    public static int highestStreak;

    public void AddStreak()
    {
        currentStreak++;
        if (currentStreak % 5 == 0)
        {
            hintManager.AddHint();
        }
    }
    public void ResetStreak()
    {
        if (currentStreak > highestStreak) highestStreak = currentStreak;
        currentStreak = 0;
    }
}
