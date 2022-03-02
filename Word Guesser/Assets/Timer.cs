using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public GameSceneManager gameSceneManager;
    public  AnimatorManager animatorManager;
    public Guess guesser;
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
                StartCoroutine("PlayLowTimeAnimation");

            }

            if (time < 0)
            {
                time = 0;
                gameSceneManager.WindowPopUp(false);//guesser.ResetGame();
            }
        }
    }
    IEnumerator PlayLowTimeAnimation()
    {
        //if the "warningMessage" animation is not currently playing
        if (!animatorManager.timerAnimator.GetCurrentAnimatorStateInfo(0).IsName("TimeRunningOut"))
        {
            animatorManager.timerAnimator.SetBool("timerWarning", true);
            yield return new WaitForSeconds(15);
            animatorManager.timerAnimator.SetBool("timerWarning", false);
        }
    }
    public void ResetTimer()
    {
        if (timerGameObject.activeSelf) time = DifficultyManager.countDown + 0.5f;
    }
}
