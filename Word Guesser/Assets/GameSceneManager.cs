using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSceneManager : MonoBehaviour
{
    public GameObject gameOverMessageContainer;
    public GameObject congratulationsMessageContainer;
    public Streak streak;
    public Guess guesser;
    public Timer timer;
    public AnimatorManager animatorManager;

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
        Time.timeScale = 0;
        animatorManager.windowPopUpAnimator.SetBool("WindowPopUp", false);
    }

    public void ResetGame()
    {
        streak.ResetStreak();
        guesser.ResetGame();
        timer.ResetTimer();
    }
}
