using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Streak : MonoBehaviour
{
    public static int currentStreak;
    public static int highestStreak;

    public static void AddStreak()
    {
        currentStreak++;
    }
    public static void ResetStreak()
    {
        if (currentStreak > highestStreak) highestStreak = currentStreak;
        currentStreak = 0;
    }
}
