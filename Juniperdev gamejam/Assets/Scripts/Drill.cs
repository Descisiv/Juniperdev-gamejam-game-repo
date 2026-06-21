using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    const float stoneChargeTax = 1.5f;
    const float dirtChargeTax = 0.5f;
    const float diamondChargeTax = 5;
    const float uraniumChargeTax = 20;
    public CircleCollider2D UraniumCollider;
    bool UraniumBuffActive;
    public Playermove playermove;
    public TimerHandler timerhandler;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (playermove.state == "drilling")
        {
            if (collision.gameObject.layer == 6 && playermove.charge >= stoneChargeTax && playermove.state == "drilling")
            {
                Destroy(collision.gameObject);
                if (!UraniumBuffActive)
                {
                    playermove.charge -= stoneChargeTax;
                }
            }
            else if(collision.gameObject.layer == 8 && playermove.charge >= dirtChargeTax && playermove.state == "drilling")
            {
                Destroy(collision.gameObject);
                if (!UraniumBuffActive)
                {
                    playermove.charge -= dirtChargeTax;
                }
            }
            else if (collision.gameObject.layer == 9 && playermove.charge >= dirtChargeTax && playermove.state == "drilling")
            {
                Destroy(collision.gameObject);
                if (!UraniumBuffActive)
                {
                    playermove.charge -= diamondChargeTax;
                }
                timerhandler.TimeLeft += 5;
            }else if (collision.gameObject.layer == 10 && playermove.charge >= dirtChargeTax && playermove.state == "drilling")
            {
                Destroy(collision.gameObject);
                if (!UraniumBuffActive)
                {
                    playermove.charge -= uraniumChargeTax;
                }
                StartCoroutine(OnMineUranium());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playermove.state == "drilling")
        {
            if (collision.gameObject.layer == 6 && playermove.charge >= stoneChargeTax && playermove.state == "drilling")
            {
                Destroy(collision.gameObject);
                if (!UraniumBuffActive)
                {
                    playermove.charge -= stoneChargeTax;
                }
            }
            else if (collision.gameObject.layer == 8 && playermove.charge >= dirtChargeTax && playermove.state == "drilling")
            {
                Destroy(collision.gameObject);
                if (!UraniumBuffActive)
                {
                    playermove.charge -= dirtChargeTax;
                }
            }
            else if (collision.gameObject.layer == 9 && playermove.charge >= dirtChargeTax && playermove.state == "drilling")
            {
                Destroy(collision.gameObject);
                if (!UraniumBuffActive)
                {
                    playermove.charge -= diamondChargeTax;
                }
                timerhandler.TimeLeft += 5;
            }
            else if (collision.gameObject.layer == 10 && playermove.charge >= dirtChargeTax && playermove.state == "drilling")
            {
                Destroy(collision.gameObject);
                if (!UraniumBuffActive)
                {
                    playermove.charge -= uraniumChargeTax;
                }
                StartCoroutine(OnMineUranium());
            }
        }
    }

    IEnumerator OnMineUranium()
    {
        UraniumBuffActive = true;
        playermove.speed += 30;
        UraniumCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        UraniumCollider.enabled = false;
        yield return new WaitForSeconds(2);
        print("diddy");
        playermove.speed -= 30;
        UraniumBuffActive = false;
    }
}
