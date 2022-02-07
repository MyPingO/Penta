using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighestStreak : MonoBehaviour
{
    public TMP_Text highestStreakText;
    void Start()
    {
        highestStreakText.text = "The word was: " + RandomWordPicker.currentWord + "\n\n\n\n\n" + "Highest Streak: " + Streak.highestStreak;
    }

}
