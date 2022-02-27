using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public Guess guesser;
    private float minutes;
    private float seconds;
    public TMP_Text countDownText;
    public float time;
    public byte[] redColor = new byte[4];
    void Start()
    {
        time = DifficultyManager.countDown + 1;
        if (time == 1) countDownText.gameObject.SetActive(false);
    }

    // Update is called once per frame  
    void Update()
    {
        if (time > 0)
        {
            minutes = Mathf.FloorToInt(time / 60);
            seconds = Mathf.FloorToInt(time % 60);
            countDownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            time -= Time.deltaTime;
        }
        if (time < 15 && countDownText.color != Color.red) countDownText.color = new Color32(redColor[0], redColor[1], redColor[2], redColor[3]);
        if (time <= 0) guesser.ResetGame();
    }
}
