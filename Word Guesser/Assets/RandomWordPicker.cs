using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RandomWordPicker : MonoBehaviour
{
    private string[] words;
    public static string currentWord;
    private string filePath, fileName;
    void Awake()
    {

        fileName = "Words.txt";
        filePath = Application.dataPath + "/" + fileName;
        words = File.ReadAllLines(filePath);
    }

    public string GetRandomWord()
    {
        return currentWord = words[Random.Range(0, words.Length)];
    }
}
