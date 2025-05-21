using System;
using UnityEngine;
using UnityEngine.UI;

public class UI_manager : MonoBehaviour
{

    public Text healthScoreText;

    public Text moneyScoreText;

    public float timer;
    public bool TimerOn = false;
    public Text TimerTxt;


    public void SetMoney(float money)
    {
        moneyScoreText.text = "Money: " + money;
    }

    public void SetHealth(float health)
    {
        healthScoreText.text = "Health:  " + health;
    }

    void Start()
    {
        TimerOn = true;
    }
    void Update()
    {
        if (TimerOn && timer > 0)
        {
            timer -= Time.deltaTime;
            updateTimer(timer);
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        TimerTxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
