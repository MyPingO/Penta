using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TMPro;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField]
    private ValidWordChecker validWordChecker;
    private Animator animator;
    private TMP_Text warningMessage;
    public Stats stats;
    public TMP_Text[] keyBoardLetters = new TMP_Text[26];
    public GameObject warningMessageGO;
    public HintManager hintManager;
    public Dictionary<int, char> greenLetterPositions = new Dictionary<int, char>();
    public Dictionary<char, Color32> keyBoardLetterColors = new Dictionary<char, Color32>();
    private Dictionary<int, string> dropdownIndexToDifficulty = new Dictionary<int, string>();
    public static string difficulty = "MEDIUM";
    public static string yellowLetters = "";
    public static int guessLength = 5;
    public static float countDown = 2;

    private void Start()
    {
        dropdownIndexToDifficulty.Add(0, "EASY");
        dropdownIndexToDifficulty.Add(1, "MEDIUM");
        dropdownIndexToDifficulty.Add(2, "HARD");
        if (SceneManager.GetActiveScene().name == "MainGame")
        {
            for (int i = 0; i < keyBoardLetters.Length; i++) keyBoardLetterColors.Add(keyBoardLetters[i].text[0], Color.black);
            warningMessage = warningMessageGO.GetComponent<TMP_Text>();
            warningMessage.color = new Color32(255, 116, 116, 255);
            animator = warningMessageGO.GetComponent<Animator>();
            stats.SaveStats(); // to save the difficulty once the game loads
        }
    }
    // Update is called once per frame
    public void SetDifficulty(int dropdownIndex)
    {
        difficulty = dropdownIndexToDifficulty[dropdownIndex];
        Debug.Log(difficulty);
    }
    public void SetGuessLength(int guessLength)
    {
        DifficultyManager.guessLength = guessLength + 4;
        Debug.Log(DifficultyManager.guessLength);
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
                InvalidAction("Invalid Word!");
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
                        InvalidAction("Did Not Use All Hints!");
                        return false;
                    }
                //'key' represents an index
                foreach (int key in greenLetterPositions.Keys)
                    if (guessWord[key] != greenLetterPositions[key])
                    {
                        InvalidAction("Did Not Use All Hints!");
                        return false;
                    }
                //if all checks are passed then reveal letters
                RevealLetters(guess, randomWord, letterCountInGuess);
            }
            else
            {
                InvalidAction("Invalid Word!");
                return false;
            }
            return true;
        }
    }
    //function for showing red, green, or yellow letters for a guess
    private void RevealLetters(TMP_Text[] guess, string randomWord, Dictionary<char, int> letterCountInGuess)
    {
        Color orange = new Color32(255, 200, 90, 255);
        //make a string out of the TMP_Text array and use that for checks
        string guessWord = "";
        for (int i = 0; i < guess.Length; i++) guessWord += guess[i].text;
        //check letters in guess
        for (int i = 0; i < guess.Length; i++)
        {
            if (guessWord[i] == char.ToUpper(randomWord[i]))
            {
                guess[i].color = Color.green;
                ColorKeyBoard(guess[i].text[0], Color.green);
                hintManager.RevealGreenLetter(i, guess[i].text[0]);
                letterCountInGuess[guessWord[i]]--;
                if (!greenLetterPositions.ContainsKey(i)) greenLetterPositions.Add(i, guessWord[i]);
            }
        }
        for (int i = 0; i < guess.Length; i++)
        {
            // if the letter 'i' in the guess is in the randomWord and if the count of the letter in the dictionary for that randomWord is more than 0
            // this is done to make sure only the appropriate amount of letters color gets changed
            if (randomWord.ToUpper().Contains(guessWord[i]) && letterCountInGuess[guessWord[i]] > 0 && guess[i].color != Color.green)
            {
                guess[i].color = orange;
                if (keyBoardLetterColors[guess[i].text[0]] != Color.green) ColorKeyBoard(guess[i].text[0], orange);
                letterCountInGuess[guessWord[i]]--;
                yellowLetters += guessWord[i];
            }
        }
        for (int i = 0; i < guess.Length; i++)
        {
            if (guess[i].color != Color.green && guess[i].color != orange)
            {
                guess[i].color = Color.red;
                if (keyBoardLetterColors[guess[i].text[0]] != Color.green && keyBoardLetterColors[guess[i].text[0]] != orange) ColorKeyBoard(guess[i].text[0], Color.red);
            }
        }
    }
    public void InvalidAction(string message)
    {
        warningMessage.text = message;
        StartCoroutine("PlayWarningAnimation");
    }
    IEnumerator PlayWarningAnimation()
    {
        //if the "warningMessage" animation is not currently playing
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("warningMessage"))
        {
            animator.SetBool("TriggerWarning", true);
            yield return new WaitForSeconds(2.7f);
            warningMessage.text = "";
            animator.SetBool("TriggerWarning", false); 
        }
    }
    public void setGreenLetterPositions(Dictionary<int, char> greenLetterPositions) { this.greenLetterPositions = greenLetterPositions; }
    public Dictionary<int, char> getGreenLetterPositions() { return greenLetterPositions; }
    public void ColorKeyBoard(char letter, Color color)
    {
        for (int i = 0; i < keyBoardLetters.Length; i++)
        {
            if (keyBoardLetters[i].text[0] == letter)
            {
                keyBoardLetters[i].color = color;
                keyBoardLetterColors[letter] = color;
            }
        }
    }

    public void setCountDown(int dropDownIndex)
    {
        countDown = dropDownIndex * 30;
    }
}

