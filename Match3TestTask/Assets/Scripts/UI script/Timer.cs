using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text timerText;

    [SerializeField] LevelInfo lvlPref;

    private float timerTime;

    public static Action onGameOver;

    void Start()
    {
        timerTime = lvlPref.timeToScore;

        timerText.text = string.Concat("Timer: " + timerTime.ToString("F2"));
    }

    void Update()
    {
        timerTime -= Time.deltaTime;

        timerText.text = string.Concat("Timer: " + timerTime.ToString("F2"));

        if (timerTime <= 0)
        {
            onGameOver?.Invoke();
        }
    }
}
