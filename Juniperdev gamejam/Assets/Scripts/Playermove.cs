using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playermove : MonoBehaviour
{
    bool frozen;
    public Animator Anim;
    public Slider DizzySlider;
    public Slider ChargeSlider;
    public float dizzy;
    public float charge;
    float speed = 30f;
    const float CHARGERATE = 100;
    const float NATRCHARGEDEC = 5;
    const float MAXCHARGE = 100;
    const float DIZZYRATE = 10;
    const float MAXDIZZY = 100;
    const float MAXANGLE = 90;
    const float TURNRATE = 120;
    const float NATRDIZZYDEC = 20;
    const float DIZZYMULT = 2;
    float turn;
    public string state = "static";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //might be a state machine
        if (Input.GetKey(KeyCode.Space) && !frozen) {
            state = "charging";
            Anim.SetInteger("state", 2);
        }else if(charge > 0 && !frozen)
        {
            state = "drilling";
            Anim.SetInteger("state", 1);
        }
        else if(!frozen)
        {
            state = "static";
            Anim.SetInteger("state", 0);
        }

        //hold a/d to change angle
        turn = Input.GetAxisRaw("Horizontal");
        if (transform.localEulerAngles.z < MAXANGLE || transform.localEulerAngles.z > 360 - MAXANGLE * 2 && turn == 1)
        {
            transform.localEulerAngles += new Vector3(0, 0, turn * TURNRATE * Time.deltaTime);
        }else if((transform.localEulerAngles.z > 360 - MAXANGLE || transform.localEulerAngles.z < 2 * MAXANGLE) && turn == -1)
        {
            transform.localEulerAngles += new Vector3(0, 0, turn * TURNRATE * Time.deltaTime);
        }
        

        //when holding space, drill velocity(charge) increases
        if (state == "charging" && charge < MAXCHARGE)
        {
            charge += CHARGERATE * Time.deltaTime;
        }

        //update position according to charge and angle
        if (state == "drilling")
        {
            float angle = transform.localEulerAngles.z * Mathf.PI/180;
            transform.position += new Vector3(Mathf.Sin(angle), -1 * Mathf.Cos(angle), 0) * charge / MAXCHARGE * speed * Time.deltaTime;
        }
        //charge naturally decays
        if (charge > 0 && state != "charging")
        {
            charge -= NATRCHARGEDEC * Time.deltaTime;
        }

        
        //animation speed proportional to charge
        Anim.SetFloat("DrillSpeed", charge / MAXCHARGE * 5);
        //update dizzy
        if (state == "drilling")
        {
            dizzy += DIZZYRATE * charge / MAXCHARGE * DIZZYMULT * Time.deltaTime;
        }
        else
        {
            dizzy -= NATRDIZZYDEC * Time.deltaTime;
        }
        //set bars
        ChargeSlider.value = charge / MAXCHARGE;
        DizzySlider.value = dizzy / MAXDIZZY;

        //stun if dizzy too high
        if(dizzy >= MAXDIZZY)
        {
            StartCoroutine(Stun());
        }
        if(dizzy < 0)
        {
            dizzy = 0;
        }
    }

    IEnumerator Stun()
    {
        frozen = true;
        charge = 0;
        dizzy = MAXDIZZY / 2;
        yield return new WaitForSeconds(2);
        frozen = false;
    }
}
