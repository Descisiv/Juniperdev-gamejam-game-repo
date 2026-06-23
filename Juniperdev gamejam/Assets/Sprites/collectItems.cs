using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectItems : MonoBehaviour
{
    public Playermove playermove;
    public TimerHandler timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer >= 11 && collision.gameObject.layer <= 19)
        {
            switch (collision.gameObject.layer)
            {
                case 11:
                    //battery
                    playermove.MAXCHARGE += 20;
                    break;
                case 12:
                    //pickaxe
                    playermove.chargeTaxDecrease += 0.25f;
                    break;
                case 13:
                    //wd40
                    playermove.speed += 2;
                    break;
                case 14:
                    //hourglass
                    playermove.timeSlow *= 1.25f;
                    break;
                case 15:
                    //valve
                    playermove.depthScalingSpeed += 0.1f;
                    break;
                case 16:
                    //parachute
                    playermove.airborneTimeSave *= 1.25f;
                    break;
                case 17:
                    //charger
                    playermove.CHARGERATE *= 1.2f;
                    break;
                case 18:
                    //chainsaw
                    playermove.chainsawLength++;
                    playermove.NATRCHARGEDEC *= 1.5f;
                    break;
                case 19:
                    //insulator
                    playermove.NATRCHARGEDEC *= .8f;
                    break;
            }
            Destroy(collision.gameObject);
        }
    }
}
