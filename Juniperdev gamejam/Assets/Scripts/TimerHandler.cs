using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerHandler : MonoBehaviour
{
    float DepthOffset;
    float Depth;
    public bool playing;
    public Playermove playermove;
    public GameObject Player;
    public TMP_Text Depthometer;
    public TMP_Text Timer;
    public float TimeLeft = 30;
    string seconds;
    string centiseconds;
    // Start is called before the first frame update
    void Start()
    {
        DepthOffset = Player.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Depth = Mathf.Round(Player.transform.position.y * -1 + DepthOffset);

        Depthometer.text = Depth.ToString() + "m";
        if (playing)
        {
            if (playermove.TimeSinceCollision < 0.5f)
            {
                TimeLeft -= Time.deltaTime;
            }
            else
            {
                TimeLeft -= Time.deltaTime * 0.5f;
            }
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
