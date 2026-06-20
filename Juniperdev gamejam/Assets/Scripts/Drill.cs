using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    const float stoneChargeTax = .5f;
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
            if (collision.gameObject.layer == 6 && playermove.charge >= stoneChargeTax)
            {
                Destroy(collision.gameObject);
                playermove.charge -= stoneChargeTax;
            }
        }
    }
}
