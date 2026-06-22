using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectItems : MonoBehaviour
{
    public Playermove playermove;
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

            }
            Destroy(collision.gameObject);
        }
    }
}
