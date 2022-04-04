using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    private float time = 0f;
    private float timeBeforePause = 0f;
    private int displayTimeSec = 0;
    private int displayTimeMin = 0;
    private bool timerRunning = true;
    public Text timeText;

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (timerRunning)
        {
            if (Mathf.FloorToInt(time) >= 1)
            {
                time = 0f;
                displayTimeSec++;
                if(displayTimeSec/60 >= 1)
                {
                    displayTimeSec = 0;
                    displayTimeMin++;
                }

                UpdateTimeUI();
            }
        }
    }

    //updates UI for time
    private void UpdateTimeUI()
    {
        string formatText = "";
        if (displayTimeMin < 10)
            formatText += "0" + displayTimeMin;
        else
            formatText += displayTimeMin;

        if (displayTimeSec < 10)
            formatText += ":0" + displayTimeSec;
        else
            formatText += ":" + displayTimeSec;

        timeText.text = formatText;
    }
    
    //pauses the game time
    public void PauseTime()
    {
        timerRunning = false;
        timeBeforePause = time;
    }

    //unpause the game time
    public void UnPauseTime()
    {
        timerRunning = true;
        time = timeBeforePause;
    }
}
