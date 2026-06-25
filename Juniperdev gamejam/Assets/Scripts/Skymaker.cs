using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skymaker : MonoBehaviour
{
    public GameObject SkyMid;
    public GameObject SkyBot;

    public int height;
    public int width;
    public float gridSize;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 50 * 2.5f, 0);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < width; y++)
            {
                if(y == 0)
                {
                    Instantiate(SkyBot, new Vector3(gridSize * x, gridSize * y, 20) + transform.position, Quaternion.identity, transform);
                }
                else
                {
                    Instantiate(SkyMid, new Vector3(gridSize * x, gridSize * y, 20) + transform.position, Quaternion.identity, transform);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
