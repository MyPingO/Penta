using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DifficultyManager : MonoBehaviour
{
    public static string difficulty = "EASY";
    public static Dictionary<int, char> greenLetterPositions = new Dictionary<int, char>();
    public static string yellowLetters = "";

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    // Update is called once per frame
    public static void SetDifficulty(string difficulty)
    {
        DifficultyManager.difficulty = difficulty;
    }
    public static bool GuessChecker(TMP_Text[] guess, string randomWord, Dictionary<char, int> letterCountInGuess)
    {
        if (difficulty == "EASY")
        {
            RevealLetters(guess, randomWord, letterCountInGuess);
            return true;
        }
        else if (difficulty == "MEDIUM")
        {
            //make a string out of the TMP_Text array and if it's a valid guess, reveal the characters
            string guessWord = "";
            for (int i = 0; i < guess.Length; i++) guessWord += guess[i].text;
            if (ValidWordChecker.IsValidGuess(guessWord.ToLower())) RevealLetters(guess, randomWord, letterCountInGuess);
            else
            {
                Debug.Log("Invalid Word!"); //create function to say guess is an invalid word
                return false;            
            }
            return true;
        }
        else //if difficulty is set to HARD
        {
            //make a string out of the TMP_Text array and use that for checks
            string guessWord = "";
            for (int i = 0; i < guess.Length; i++) guessWord += guess[i].text;
            //first check if the guess is a valid word
            if (ValidWordChecker.IsValidGuess(guessWord.ToLower()))
            {
                //check to make sure all hints are used in the guess, first check yellow characters, then green ones
                for (int i = 0; i < yellowLetters.Length; i++)
                    if (guessWord.Contains(yellowLetters[i]) == false)
                    {
                        Debug.Log("Did not use all hints! " + yellowLetters[i] + " is not in " + guessWord); //TODO Create a function to say you need to use all hints cause of hard mode
                        return false;
                    }
                //'key' represents an index
                foreach (int key in greenLetterPositions.Keys)
                    if (guessWord[key] != greenLetterPositions[key])
                    {
                        Debug.Log("Did not use all hints! " + guessWord[key] + " is not " + greenLetterPositions[key]); //TODO Create a function to say you need to use all hints cause of hard mode
                        return false;
                    }
                //if all checks are passed then reveal letters
                RevealLetters(guess, randomWord, letterCountInGuess);
            }
            else
            {
                Debug.Log("Invalid Word!"); //create function to say guess is an invalid word
                return false;
            }
            return true;
        }
    }
    //function for showing red, green, or yellow letters for a guess
    private static void RevealLetters(TMP_Text[] guess, string randomWord, Dictionary<char, int> letterCountInGuess)
    {
        //make a string out of the TMP_Text array and use that for checks
        string guessWord = "";
        for (int i = 0; i < guess.Length; i++) guessWord += guess[i].text;
        //check letters in guess
        for (int i = 0; i < guess.Length; i++)
        {
            if (guessWord[i] == char.ToUpper(randomWord[i]))
            {
                guess[i].color = Color.green;
                letterCountInGuess[guessWord[i]]--;
                if (greenLetterPositions.ContainsKey(i) == false) greenLetterPositions.Add(i, guessWord[i]);
            }
        }
        for (int i = 0; i < guess.Length; i++)
        {
            // if the letter 'i' in the guess is in the randomWord and if the count of the letter in the dictionary for that randomWord is more than 0
            // this is done to make sure only the appropriate amount of letters color gets changed
            if (randomWord.ToUpper().Contains(guessWord[i]) && letterCountInGuess[guessWord[i]] > 0 && guess[i].color != Color.green)
            {
                guess[i].color = Color.yellow;
                letterCountInGuess[guessWord[i]]--;
                yellowLetters += guessWord[i];
            }
        }
        for (int i = 0; i < guess.Length; i++)
        {
            if (guess[i].color != Color.green && guess[i].color != Color.yellow)
            {
                guess[i].color = Color.red;
            }
        }
    }
}

