using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class ValidWordChecker : MonoBehaviour
{
    private string[] validGuesses;
    private string filePath, fileName;
    void Start()
    {
        fileName = DifficultyManager.guessLength + "LetterValidWords.txt";
        Debug.Log("ValidWordChecker chose the file: " + fileName);
        filePath = Application.dataPath + "/" + fileName;
        validGuesses = File.ReadAllLines(filePath);
    }

    // Update is called once per frame
    public bool IsValidGuess(string guess)
    {
        int left = 0, right = validGuesses.Length - 1;
        while (left <= right)
        {
            int midPoint = left + (right - left) / 2;
            int compare = guess.CompareTo(validGuesses[midPoint]);
            if (compare == 0) return true;
            else if (compare > 0) left = midPoint + 1;
            else right = midPoint - 1;
        }
        return false;
    }
}
