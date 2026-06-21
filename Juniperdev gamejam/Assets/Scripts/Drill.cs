using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    const float stoneChargeTax = 1.5f;
    const float dirtChargeTax = 0.5f;
    public Playermove playermove;
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
                playermove.charge -= stoneChargeTax;
            }else if(collision.gameObject.layer == 8 && playermove.charge >= dirtChargeTax && playermove.state == "drilling")
            {
                Destroy(collision.gameObject);
                playermove.charge -= dirtChargeTax;
            }
        }
    }
}
