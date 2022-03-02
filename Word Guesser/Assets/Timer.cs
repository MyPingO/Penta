using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public GameSceneManager gameSceneManager;
    public Guess guesser;
    private Animator animator;
    public GameObject timerGameObject;
    private float minutes;
    private float seconds;
    public TMP_Text countDownText;
    public float time;
    public byte[] redColor = new byte[4];
    void Start()
    {
        //adding 1 second to the timer so that the timer doesnt immediately start counting down
        time = DifficultyManager.countDown + 0.5f;
        if (time == 0.5f) timerGameObject.SetActive(false);
        animator = timerGameObject.GetComponent<Animator>();
    }

    // Update is called once per frame  
    void Update()
    {
        if (timerGameObject.activeSelf)
        {
            if (time > 0)
            {
                minutes = Mathf.FloorToInt(time / 60);
                seconds = Mathf.FloorToInt(time % 60);
                countDownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                time -= Time.deltaTime;
            }
            if (time < 15 && countDownText.color != Color.red)
            {
                countDownText.color = new Color32(redColor[0], redColor[1], redColor[2], redColor[3]);
                StartCoroutine("PlayWarningAnimation");

            }

            if (time < 0)
            {
                time = 0;
                gameSceneManager.GameOver();//guesser.ResetGame();
            }
        }
    }
    IEnumerator PlayWarningAnimation()
    {
        //if the "warningMessage" animation is not currently playing
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("TimeRunningOut"))
        {
            animator.SetBool("timerWarning", true);
            yield return new WaitForSeconds(15);
            animator.SetBool("timerWarning", false);
        }
    }
    public void ResetTimer()
    {
        if (timerGameObject.activeSelf) time = DifficultyManager.countDown + 0.5f;
    }
}
