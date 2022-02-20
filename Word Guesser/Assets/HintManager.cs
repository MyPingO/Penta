using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HintManager : MonoBehaviour
{
    public Stats stats;
    public TMP_Text[] knownLetterPositions = new TMP_Text[DifficultyManager.guessLength];
    public TMP_Text availableHints;
    public DifficultyManager difficultyManager;
    public Guess guesser;
    public string randomWord;
    public static int hintCount;
    private void Start()
    {
        hintCount = stats.hintCount;
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
                            difficultyManager.ColorKeyBoard(randomWord[j], Color.green);
                        }
                    RemoveHint();
                    difficultyManager.setGreenLetterPositions(greenLetterPositions);
                    return;
                }
        }
        else difficultyManager.InvalidAction("You have no hints!");
    }
    public void AddHint() { hintCount++; stats.SaveStats(); SetAvailableHintsTMP(); }
    public void RemoveHint() { hintCount--; stats.SaveStats(); SetAvailableHintsTMP(); }
    public void SetAvailableHintsTMP()
    {
        availableHints.text = "Hints Left: " + stats.hintCount;
    }
}
