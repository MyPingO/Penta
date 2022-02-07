using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StreakText : MonoBehaviour
{
    public TMP_Text currentStreak;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Streak.currentStreak);
        currentStreak.text = "Current Streak: " + Streak.currentStreak;
    }
}

