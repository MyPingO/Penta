using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField]
    ValidWordChecker validWordChecker;
    private Animator animator;
    private TMP_Text warningMessage;
    public GameObject warningMessageGO;
    public static string difficulty = "HARD";
    public Dictionary<int, char> greenLetterPositions = new Dictionary<int, char>();
    public static string yellowLetters = "";

    private void Start()
    {
        warningMessage = warningMessageGO.GetComponent<TMP_Text>();
        warningMessage.color = new Color32(255, 116, 116, 255);
        animator = warningMessageGO.GetComponent<Animator>();
    }
    // Update is called once per frame
    public void SetDifficulty(string difficulty)
    {
        DifficultyManager.difficulty = difficulty;
    }
    public bool GuessChecker(TMP_Text[] guess, string randomWord, Dictionary<char, int> letterCountInGuess)
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
            if (validWordChecker.IsValidGuess(guessWord.ToLower())) RevealLetters(guess, randomWord, letterCountInGuess);
            else
            {
                InvalidGuess("Invalid Word!");
                Debug.Log("Invalid Word!");
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
            if (validWordChecker.IsValidGuess(guessWord.ToLower()))
            {
                //check to make sure all hints are used in the guess, first check yellow characters, then green ones
                for (int i = 0; i < yellowLetters.Length; i++)
                    if (guessWord.Contains(yellowLetters[i]) == false)
                    {
                        InvalidGuess("Did Not Use All Hints!");
                        return false;
                    }
                //'key' represents an index
                foreach (int key in greenLetterPositions.Keys)
                    if (guessWord[key] != greenLetterPositions[key])
                    {
                        InvalidGuess("Did Not Use All Hints!");
                        return false;
                    }
                //if all checks are passed then reveal letters
                RevealLetters(guess, randomWord, letterCountInGuess);
            }
            else
            {
                InvalidGuess("Invalid Word!");
                return false;
            }
            return true;
        }
    }
    //function for showing red, green, or yellow letters for a guess
    private void RevealLetters(TMP_Text[] guess, string randomWord, Dictionary<char, int> letterCountInGuess)
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
    private void InvalidGuess(string message)
    {
        warningMessage.text = message;
        StartCoroutine("playWarningAnimation");
    }
    IEnumerator playWarningAnimation()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("warningMessage"))
        {
            animator.SetBool("TriggerWarning", true);
            yield return new WaitForSeconds(4);
            warningMessage.text = "";
            animator.SetBool("TriggerWarning", false); 
        }
    }

}

