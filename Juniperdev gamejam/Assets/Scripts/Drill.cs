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
    public GameObject Player;
    public TimerHandler timerhandler;
    public LayerMask LAYERMASK;
    public LavaDamage lava;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playermove.state == "drilling")
        {
            float angle = Player.transform.localEulerAngles.z * Mathf.PI / 180;
            RaycastHit2D ChainsawRay = Physics2D.Raycast(transform.position, new Vector3(Mathf.Sin(angle), -1 * Mathf.Cos(angle), 0), playermove.chainsawLength, LAYERMASK);

            Debug.DrawRay(transform.position, new Vector3(Mathf.Sin(angle), -1 * Mathf.Cos(angle), 0) * playermove.chainsawLength, Color.red);

            if (ChainsawRay != false)
            {
                GameObject rock = ChainsawRay.collider.gameObject;

                if (rock.layer == 9)
                {
                    playermove.charge -= diamondChargeTax - playermove.chargeTaxDecrease;
                    timerhandler.TimeLeft += 5;
                }
                else if (rock.layer == 10)
                {
                    if (!UraniumBuffActive)
                    {
                        playermove.charge -= uraniumChargeTax - playermove.chargeTaxDecrease;
                        playermove.charge -= uraniumChargeTax;
                    }
                    StartCoroutine(OnMineUranium());
                }
                else if (rock.layer == 6)
                {
                    playermove.charge -= stoneChargeTax - playermove.chargeTaxDecrease;
                }
                else if (rock.layer == 8)
                {
                    playermove.charge -= dirtChargeTax - playermove.chargeTaxDecrease;
                }
                Destroy(rock);
            }
        }
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
                    playermove.charge -= stoneChargeTax - playermove.chargeTaxDecrease;
                }
            }
            else if (collision.gameObject.layer == 8 && playermove.charge >= dirtChargeTax && playermove.state == "drilling")
            {
                Destroy(collision.gameObject);
                if (!UraniumBuffActive)
                {
                    playermove.charge -= dirtChargeTax - playermove.chargeTaxDecrease;
                }
            }
            else if (collision.gameObject.layer == 9 && playermove.charge >= dirtChargeTax && playermove.state == "drilling")
            {
                Destroy(collision.gameObject);
                if (!UraniumBuffActive)
                {
                    playermove.charge -= diamondChargeTax - playermove.chargeTaxDecrease;
                }
                timerhandler.TimeLeft += 5;
            }
            else if (collision.gameObject.layer == 10 && playermove.charge >= dirtChargeTax && playermove.state == "drilling")
            {
                Destroy(collision.gameObject);
                if (!UraniumBuffActive)
                {
                    playermove.charge -= uraniumChargeTax - playermove.chargeTaxDecrease;
                }
                StartCoroutine(OnMineUranium());
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Ruby + Lava (I only did it for OnTriggerEnter2D idk if that's a problem and it needs to be added to the other ones but it's ok)
        if (collision.gameObject.layer == 20 && playermove.charge >= stoneChargeTax && playermove.state == "drilling")
        {
            Destroy(collision.gameObject);
            if (!UraniumBuffActive)
            {
                playermove.charge -= stoneChargeTax;
            }
            StartCoroutine(OnMineRuby());
        }
        if (collision.CompareTag("Lava") && lava.immunity && playermove.charge >= stoneChargeTax && playermove.state == "drilling")
        {
            Destroy(collision.gameObject);
            if (!UraniumBuffActive)
            {
                playermove.charge -= stoneChargeTax;
            }
        }
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

    IEnumerator OnMineUranium()
    {
        UraniumBuffActive = true;
        playermove.speed += 30;
        UraniumCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        UraniumCollider.enabled = false;
        yield return new WaitForSeconds(2);
        playermove.speed -= 30;
        UraniumBuffActive = false;
    }

    IEnumerator OnMineRuby()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Ruby function called");
        lava.immunity = true;
        yield return new WaitForSeconds(10);
        lava.immunity = false;
    }
}