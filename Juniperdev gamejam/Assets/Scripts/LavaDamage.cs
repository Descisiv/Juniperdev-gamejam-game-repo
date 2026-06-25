using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LavaDamage : MonoBehaviour
{
    public TimerHandler timer;
    public Playermove player;
    public TMP_Text timerText;

    private int inLava = 0;
    private float tempTime = 1;
    public bool immunity = false;
    public bool debounce = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inLava > 0 && !immunity && !debounce)
        {
            tempTime = player.timeSlow;
            player.timeSlow = 0.2f;
            timerText.color = new Vector4(255, 0, 0, 255);
            debounce = true;
        }
        else if (inLava <= 0 && debounce)
        {
            debounce = false;
            player.timeSlow = tempTime;
            timerText.color = new Vector4(0, 0, 0, 255);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lava") && inLava <= 0)
        {
            inLava ++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Lava") && inLava > 0)
        {
            inLava --;

        }
    }
}
