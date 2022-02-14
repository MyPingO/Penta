using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StreakText : MonoBehaviour
{
    public TMP_Text currentStreakText;
    // Start is called before the first frame update
    void Start()
    {
        currentStreakText.text = "Current Streak: " + Streak.currentStreak;
    }
}

