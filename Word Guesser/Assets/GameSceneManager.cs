using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSceneManager : MonoBehaviour
{
    public Streak streak;
    public Guess guesser;
    public Timer timer;
    public GameObject gameOverGameObject;
    private Animator animator;

    public void Start()
    {
        animator = gameOverGameObject.GetComponent<Animator>();
    }
    public void LoadMainGame()
    {
        SceneManager.LoadScene("MainGame");
    }
    public void GameOver()
    {
        //play the game over animation
        Time.timeScale = 0;
        animator.SetBool("GameOverTrigger", true);
    }
    public void PlayAgain()
    {
        Time.timeScale = 1;
        animator.SetBool("GameOverTrigger", false);
        Debug.Log("GameOver reversed");
        ResetGame();
    }

    public void ResetGame()
    {
        streak.ResetStreak();
        guesser.ResetGame();
        timer.ResetTimer();
    }
}
