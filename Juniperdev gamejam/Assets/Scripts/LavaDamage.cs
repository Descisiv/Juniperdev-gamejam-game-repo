using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDamage : MonoBehaviour
{
    public TimerHandler timer;
    public Playermove player;

    private bool inLava = false;
    private bool debounce = false;
    private float tempTime = 1;
    public bool immunity = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inLava && debounce && !immunity)
        {
            tempTime = player.timeSlow;
            player.timeSlow = 0.1f;
            debounce = false;
        }
        else if (!inLava && !debounce)
        {
            player.timeSlow = tempTime;
            debounce = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Lava") && !inLava)
        {
            inLava = true;
            Debug.Log("IN LAVA!!!!!!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Lava") && inLava)
        {
            inLava = false;
            Debug.Log("Out of lava");

        }
    }
}
