using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public event Action OnTimeIsOver;
    
    private float timeRemaining = 60;
    private float totalTime = 60;
    private bool timerIsRunning = false;
    private float bonusTime = 30f;

    [SerializeField] private TextMeshProUGUI timeText;

    private void Start()
    {
        timerIsRunning = true;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                OnTimeIsOver?.Invoke();
            }
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void AddBonusTime()
    {
        timeRemaining += bonusTime;
        timerIsRunning = true;
    }

    public float GetRemainingTime()
    {
        return totalTime - timeRemaining;
    }
}
