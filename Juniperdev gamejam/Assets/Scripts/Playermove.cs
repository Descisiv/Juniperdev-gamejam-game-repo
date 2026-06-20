using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playermove : MonoBehaviour
{
    public Slider ChargeSlider;
    public float dizzy;
    public float charge;
    const float CHARGERATE = 10;
    const float NATRCHARGEDEC = 2;
    const float MAXCHARGE = 20;
    const float DIZZYRATE = 3;
    const float MAXDIZZY = 100;
    const float MAXANGLE = 45;
    const float TURNRATE = 30;
    float turn;
    string state = "static";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //might be a state machine
        if (Input.GetKey(KeyCode.Space)) {
            state = "charging";
        }else if(charge > 0)
        {
            state = "drilling";
        }
        else
        {
            state = "static";
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
            transform.position += new Vector3(Mathf.Sin(angle), -1 * Mathf.Cos(angle), 0) * charge * Time.deltaTime;
        }
        //charge naturally decays
        if (charge > 0 && state != "charging")
        {
            charge -= NATRCHARGEDEC * Time.deltaTime;
        }

        //set energy bar
        ChargeSlider.value = charge / MAXCHARGE;
    }
}
