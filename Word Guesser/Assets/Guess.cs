using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Guess : MonoBehaviour

{
    public TMP_Text[] firstGuess = new TMP_Text[5];
    public TMP_Text[] secondGuess = new TMP_Text[5];
    public TMP_Text[] thirdGuess = new TMP_Text[5];
    public TMP_Text[] fourthGuess = new TMP_Text[5];
    public TMP_Text[] fifthGuess = new TMP_Text[5];
    public TMP_Text[] sixthGuess = new TMP_Text[5];
    private TMP_Text[][] guesses = new TMP_Text[6][];

    private int guessNumber = 0;
    private int index = 0;

    [SerializeField]
    private RandomWordPicker wordPicker;
    [SerializeField]
    private string randomWord;

    [SerializeField]
    private List<char> letters;

    public Dictionary<char, int> letterCountInGuess = new Dictionary<char, int>();
    // Start is called before the first frame update
    void Start()
    {
        randomWord = wordPicker.GetRandomWord();
        guesses[0] = firstGuess;
        guesses[1] = secondGuess;
        guesses[2] = thirdGuess;
        guesses[3] = fourthGuess;
        guesses[4] = fifthGuess;
        guesses[5] = sixthGuess;

        FillDictionaryWithWord(randomWord);

    }

    // Update is called once per frame
    void Update()
    {
        //get input from user and make sure its a letter to add to the guess
        foreach (char c in Input.inputString)
        {
            if (char.IsLetter(c) && index < 5 && guessNumber < 6)
            {
                letters.Add(char.ToUpper(c));
                guesses[guessNumber][index].text = c.ToString().ToUpper();
                index++;
            }
        }
        //every time you enter in a guess
        if (Input.GetKeyDown(KeyCode.Return) && letters.Count == 5 && guessNumber < 6)
        {
            if (DifficultyManager.GuessChecker(guesses[guessNumber], randomWord, letterCountInGuess))//else / else if where?
            {
                //if player guessed the word
                if (AreAllTextSameColor(guesses[guessNumber], Color.green))
                {
                    //reset text tiles and other stats
                    Streak.AddStreak();
                    for (int i = 0; i < guesses.Length; i++)
                        for (int j = 0; j < guesses[i].Length; j++)
                        {
                            guesses[i][j].text = "";
                            guesses[i][j].color = Color.black;
                        }
                    guessNumber = 0; index = 0; letters.Clear(); letterCountInGuess.Clear(); randomWord = wordPicker.GetRandomWord(); DifficultyManager.greenLetterPositions.Clear(); DifficultyManager.yellowLetters = "";
                    FillDictionaryWithWord(randomWord);
                    SceneManager.LoadScene("Congrats");

                }
                //reset the game if player ran out of guesses
                else if (guessNumber == 5 && AreAllTextSameColor(guesses[guessNumber], Color.green) == false)
                {
                    RandomWordPicker.currentWord = randomWord;
                    Streak.ResetStreak();
                    SceneManager.LoadScene("GameOver");
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
        }
        //delete character
        else if (Input.GetKeyDown(KeyCode.Backspace) && letters.Count > 0)
        {
            guesses[guessNumber][index - 1].text = "";
            index--;
            letters.RemoveAt(letters.Count - 1);
        }

    }

    bool AreAllTextSameColor(TMP_Text[] textArray, Color color)
    {
        for (int i = 0; i < textArray.Length; i++) if (textArray[i].color != color) return false;
        return true;
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
}
