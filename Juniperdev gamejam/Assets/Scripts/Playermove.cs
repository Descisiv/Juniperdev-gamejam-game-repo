using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Playermove : MonoBehaviour
{
    //player stats that can be improved with items
    public float timeSlow = 1;
    public float speed = 10f;
    public float CHARGERATE = 100;
    public float NATRCHARGEDEC = 5;
    public float MAXCHARGE = 100;
    public float airborneTimeSave = 2;
    public float chargeTaxDecrease = 0;
    public float depthScalingSpeed = 0;
    public float chainsawLength = 0;

    bool InvertedControls;
    public float DepthOffset;
    public float Depth;
    public float TimeSinceCollision;
    public CaveGeneratorReal caveGen;
    public bool frozen;
    public Animator Anim;
    public Slider ChargeSlider;
    public float charge;
    const float MAXANGLE = 90;
    const float TURNRATE = 150;
    float turn;
    public string state = "static";
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(caveGen.width / 2 * caveGen.gridSize, caveGen.height * caveGen.gridSize + 2.5f);
        DepthOffset = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            InvertedControls = !InvertedControls;
        }
        Depth = transform.position.y * -1 + DepthOffset;

        TimeSinceCollision += Time.deltaTime;

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
        if (InvertedControls)
        {
            if (transform.localEulerAngles.z < MAXANGLE || transform.localEulerAngles.z > 360 - MAXANGLE * 2 && turn == -1)
            {
                transform.localEulerAngles -= new Vector3(0, 0, turn * TURNRATE * Time.deltaTime);
            }
            else if ((transform.localEulerAngles.z > 360 - MAXANGLE || transform.localEulerAngles.z < 2 * MAXANGLE) && turn == 1)
            {
                transform.localEulerAngles -= new Vector3(0, 0, turn * TURNRATE * Time.deltaTime);
            }
        }
        else
        {
            if (transform.localEulerAngles.z < MAXANGLE || transform.localEulerAngles.z > 360 - MAXANGLE * 2 && turn == 1)
            {
                transform.localEulerAngles += new Vector3(0, 0, turn * TURNRATE * Time.deltaTime);
            }
            else if ((transform.localEulerAngles.z > 360 - MAXANGLE || transform.localEulerAngles.z < 2 * MAXANGLE) && turn == -1)
            {
                transform.localEulerAngles += new Vector3(0, 0, turn * TURNRATE * Time.deltaTime);
            }
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
            transform.position += new Vector3(Mathf.Sin(angle), -1 * Mathf.Cos(angle), 0) * charge / MAXCHARGE * (speed + depthScalingSpeed * Depth / 100) * Time.deltaTime;
        }
        //charge naturally decays
        if (charge > 0 && state != "charging")
        {
            charge -= NATRCHARGEDEC * Time.deltaTime;
        }

        
        //animation speed proportional to charge
        Anim.SetFloat("DrillSpeed", charge / MAXCHARGE * 5);
        //set bar
        ChargeSlider.value = charge / MAXCHARGE;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        TimeSinceCollision = 0;
    }
}
