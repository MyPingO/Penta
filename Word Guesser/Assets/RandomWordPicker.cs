using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RandomWordPicker : MonoBehaviour
{
    private string[] words;
    private string filePath, fileName;
    public static string currentWord;
    void Start()
    {
        if (DifficultyManager.difficulty != "HARD") fileName = "Words.txt";
        else fileName = "HardWords.txt";
        filePath = Application.dataPath + "/" + fileName;
        words = File.ReadAllLines(filePath);
    }

    public string GetRandomWord()
    {
        return currentWord = words[Random.Range(0, words.Length)];
    }
}
