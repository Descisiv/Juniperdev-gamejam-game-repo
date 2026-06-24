using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectItems : MonoBehaviour
{
    public Playermove playermove;
    public TimerHandler timer;
    public GameObject[] itemBanners;
    public Transform canvas;
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
        if (collision.gameObject.layer >= 11 && collision.gameObject.layer <= 19)
        {
            switch (collision.gameObject.layer)
            {
                case 11:
                    //battery
                    Instantiate(itemBanners[0], canvas);
                    playermove.MAXCHARGE += 20;
                    break;
                case 12:
                    //pickaxe
                    Instantiate(itemBanners[4], canvas);
                    playermove.chargeTaxDecrease += 0.25f;
                    break;
                case 13:
                    //wd40
                    Instantiate(itemBanners[8], canvas);
                    playermove.speed += 2;
                    break;
                case 14:
                    //hourglass
                    Instantiate(itemBanners[2], canvas);
                    playermove.timeSlow *= 1.25f;
                    break;
                case 15:
                    //valve
                    Instantiate(itemBanners[6], canvas);
                    playermove.depthScalingSpeed += 0.1f;
                    break;
                case 16:
                    //parachute
                    Instantiate(itemBanners[3], canvas);
                    playermove.airborneTimeSave *= 1.25f;
                    break;
                case 17:
                    //charger
                    Instantiate(itemBanners[5], canvas);
                    playermove.CHARGERATE *= 1.2f;
                    break;
                case 18:
                    //chainsaw
                    Instantiate(itemBanners[1], canvas);
                    playermove.chainsawLength++;
                    playermove.NATRCHARGEDEC *= 1.5f;
                    break;
                case 19:
                    //insulator
                    Instantiate(itemBanners[7], canvas);
                    playermove.NATRCHARGEDEC *= .8f;
                    break;
            }
            Destroy(collision.gameObject);
        }
    }
}
