using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintManager : MonoBehaviour
{
    public TMP_Text[] knownLetterPositions = new TMP_Text[5];
    public TMP_Text availableHints;
    public DifficultyManager difficultyManager;
    public Guess guesser;
    public string randomWord;
    public int hintCount = 3;
    private void Start()
    {
        SetAvailableHintsTMP();
        for (int i = 0; i < knownLetterPositions.Length; i++)
        {
            knownLetterPositions[i].text = "";
        }
    }
    public void RevealGreenLetter(int index, char letter)
    {
        if (knownLetterPositions[index].text == "") knownLetterPositions[index].text += letter;
    }
    public void UseHint()
    {
        randomWord = guesser.GetRandomWord().ToUpper();
        Dictionary<int, char> greenLetterPositions = difficultyManager.getGreenLetterPositions();
        if (hintCount > 0)
        {
            for (int i = 0; i < randomWord.Length; i++) if (!greenLetterPositions.ContainsValue(randomWord[i]))
                {
                    for (int j = 0; j < knownLetterPositions.Length; j++)
                        if (randomWord[j] == randomWord[i])
                        {
                            knownLetterPositions[j].text += randomWord[i];
                            greenLetterPositions.Add(i, randomWord[j]);
                        }
                    RemoveHint();
                    difficultyManager.setGreenLetterPositions(greenLetterPositions);
                    return;
                }
        }
        else difficultyManager.InvalidAction("You have no hints!");
    }
    public void AddHint() { hintCount++; SetAvailableHintsTMP(); }
    public void RemoveHint() { hintCount--; SetAvailableHintsTMP(); }
    public void SetAvailableHintsTMP()
    {
        availableHints.text = "Hints Left: " + hintCount;
    }
}
