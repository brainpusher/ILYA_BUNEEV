using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTimerView : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private Text timerText;

    private TimeSpan timeSpan;
    
    private void OnEnable()
    {
        timer.OnChangeTime += UpdateTimeText;
    }

    private void UpdateTimeText(float timeInSeconds)
    {
        timeSpan = TimeSpan.FromSeconds(timeInSeconds);
        timerText.text = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
    }
    
    private void OnDisable()
    {
        timer.OnChangeTime -= UpdateTimeText;
    }
}
