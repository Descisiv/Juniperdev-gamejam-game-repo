using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerHandler : MonoBehaviour
{
    public bool playing;
    public GameObject Player;
    public TMP_Text Timer;
    public float TimeLeft = 30;
    string seconds;
    string centiseconds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            TimeLeft -= Time.deltaTime;
        }
        if(Mathf.Floor(TimeLeft) == 0)
        {
            seconds = "00";
        }
        else if (Mathf.Floor(TimeLeft) < 10)
        {
            seconds = "0" + (Mathf.Floor(TimeLeft)).ToString();
        }
        else
        {
            seconds = (Mathf.Floor(TimeLeft)).ToString();
        }

        if (Mathf.Round((TimeLeft % 1) * 100) == 0)
        {
            centiseconds = "00";
        }
        else if (Mathf.Round((TimeLeft % 1) * 100) < 10)
        {
            centiseconds = "0" + (Mathf.Round((TimeLeft % 1) * 100)).ToString();
        }
        else
        {
            centiseconds = (Mathf.Round((TimeLeft % 1) * 100)).ToString();
        }
        if (TimeLeft > 0)
        {
            Timer.text = seconds + ":" + centiseconds;
        }
        else
        {
            Timer.text = "00:00";
            Player.SetActive(false);
        }
    }
}
