using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Guess : MonoBehaviour

{
    public Stats stats;
    public Streak streak;
    public GridBuilder gridBuilder;

    public int guessLength;
    private int guessNumber = 0;
    private int index = 0;
    public int tileGap = 1;

    public GameObject referenceTextTile;
    public TMP_Text[] firstGuess;
    public TMP_Text[] secondGuess;
    public TMP_Text[] thirdGuess;
    public TMP_Text[] fourthGuess;
    public TMP_Text[] fifthGuess;
    public TMP_Text[] sixthGuess;
    private TMP_Text[][] guesses = new TMP_Text[6][];

    public GameSceneManager gameSceneManager;
    public RandomWordPicker wordPicker;
    [SerializeField]
    private DifficultyManager difficultyManager;
    [SerializeField]
    private string randomWord;

    [SerializeField]
    private List<char> letters;

    public Dictionary<char, int> letterCountInGuess = new Dictionary<char, int>();
    // Start is called before the first frame update
    void Start()
    {
        randomWord = wordPicker.GetRandomWord();

        guessLength = DifficultyManager.guessLength;

        firstGuess = new TMP_Text[guessLength];
        secondGuess = new TMP_Text[guessLength];
        thirdGuess = new TMP_Text[guessLength];
        fourthGuess = new TMP_Text[guessLength];
        fifthGuess = new TMP_Text[guessLength];
        sixthGuess = new TMP_Text[guessLength];

        guesses[0] = firstGuess;
        guesses[1] = secondGuess;
        guesses[2] = thirdGuess;
        guesses[3] = fourthGuess;
        guesses[4] = fifthGuess;
        guesses[5] = sixthGuess;

        gridBuilder.GenerateGuessesGrid(guesses, guessLength, referenceTextTile);
        FillDictionaryWithWord(randomWord);

    }

    // Update is called once per frame
    void Update()
    {
        //get input from user and make sure its a letter to add to the guess
        foreach (char c in Input.inputString)
        {
            AddLetter(c.ToString().ToUpper());
        }
        //every time you enter in a guess
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ValidateGuess();
        }
        //delete character
        else if (Input.GetKeyDown(KeyCode.Backspace))
        {
            DeleteLastLetter();
        }

    }
    

    void FillDictionaryWithWord(string word)
    {
        foreach (char c in word)
        {
            char upperC = char.ToUpper(c);
            if (letterCountInGuess.ContainsKey(upperC)) letterCountInGuess[upperC]++;
            else letterCountInGuess.Add(char.ToUpper(upperC), 1);
        }
    }
    bool AreAllTextSameColor(TMP_Text[] textArray, Color color)
    {
        for (int i = 0; i < textArray.Length; i++) if (textArray[i].color != color) return false;
        return true;
    }
    public void AddLetter(string c)
    {
        if (char.IsLetter(c[0]) && index < guessLength && guessNumber < 6)
        {
            letters.Add(c[0]);
            guesses[guessNumber][index].text = c;
            index++;
        }
    }
    public void ValidateGuess()
    {
        if (letters.Count == guessLength && guessNumber < 6)
        {
            if (difficultyManager.GuessChecker(guesses[guessNumber], randomWord, letterCountInGuess))
            {
                //if player guessed the word
                if (AreAllTextSameColor(guesses[guessNumber], Color.green))
                {
                    gameSceneManager.WindowPopUp(true);
                    //reset text tiles and other stats
                    streak.AddStreak();
                    ResetBoard();
                }
                //reset the game if player ran out of guesses
                else if (guessNumber == 5 && AreAllTextSameColor(guesses[guessNumber], Color.green) == false)
                {
                    gameSceneManager.WindowPopUp(false);
                    ResetGame();   
                }
                //move on to next guess
                else
                {
                    letterCountInGuess.Clear();
                    FillDictionaryWithWord(randomWord);
                    guessNumber++;
                    index = 0;
                    letters.Clear();
                }
            }
            stats.SaveStats();
        }
    }
    public void DeleteLastLetter()
    {
        if (letters.Count > 0)
        {
            guesses[guessNumber][index - 1].text = "";
            index--;
            letters.RemoveAt(letters.Count - 1);
        }
    }
    public string GetRandomWord()
    {
        return randomWord;
    }
    public void ResetGame()
    {
        RandomWordPicker.currentWord = randomWord;
        streak.ResetStreak();
        ResetBoard();
    }
    public void ResetBoard()
    {
        index = 0; 
        guessNumber = 0; 
        letters.Clear(); 
        letterCountInGuess.Clear(); 
        randomWord = wordPicker.GetRandomWord();
        FillDictionaryWithWord(randomWord);
        DifficultyManager.yellowLetters = "";
        DifficultyManager.greenLetterPositions.Clear();
        //clear guesses[][]
        for (int i = 0; i < guesses.Length; i++)
        {
            for (int j = 0; j < guesses[i].Length; j++)
            {
                guesses[i][j].text = "";
                guesses[i][j].color = Color.black;
            }
        }
        //clear hint/known letters
        for (int i = 0; i < guessLength; i++) HintManager.knownLetterPositions[i].text = "";
        //clear keyboard colors
        for (int i = 0; i < 26; i++) difficultyManager.keyBoardLetters[i].color = Color.black; 

        stats.SaveStats();
    }
}
