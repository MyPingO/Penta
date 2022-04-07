using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GamePopUpManager : MonoBehaviour
{
    public GameObject gameOverMessageContainer;
    public GameObject congratulationsMessageContainer;
    public Streak streak;
    public Guess guesser;
    public Timer timer;
    public AnimatorManager animatorManager;

    public TMP_Text guessWord;
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
    public void LoadMainGame()
    {
        SceneManager.LoadScene("MainGame");
    }
    public void WindowPopUp(bool playerWon)
    {
        if (playerWon)
        {
            gameOverMessageContainer.SetActive(false);
            congratulationsMessageContainer.SetActive(true);
        }
        else
        {
            gameOverMessageContainer.SetActive(true);
            congratulationsMessageContainer.SetActive(false);
        }
        Time.timeScale = 0;
        guessWord.text = guesser.GetRandomWord();
        animatorManager.windowPopUpAnimator.SetBool("WindowPopUp", true);
    }
    public void PlayAgain() //resets the game
    {
        Time.timeScale = 1;
        animatorManager.windowPopUpAnimator.SetBool("WindowPopUp", false);
        ResetGame();
    }
    public void Continue() //resets the board only (this is done in Guess script)
    {
        timer.ResetTimer();
        Time.timeScale = 1;
        animatorManager.windowPopUpAnimator.SetBool("WindowPopUp", false);
    }

    public void ResetGame()
    {
        streak.ResetStreak();
        guesser.ResetGame();
        timer.ResetTimer();
    }
}
