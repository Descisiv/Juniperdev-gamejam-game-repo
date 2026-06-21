using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveGeneratorGenerator : MonoBehaviour
{
    int i = 1;
    public float gridSize;
    int GenerationsCleared;
    public int height;
    public GameObject CaveGenerator;
    public Transform Player;
    GameObject Gen1;
    GameObject Gen2;
    GameObject Gen3;
    // Start is called before the first frame update
    void Start()
    {
        Gen1 = Instantiate(CaveGenerator, gridSize * (transform.position - new Vector3(0, height, 0) * GenerationsCleared), Quaternion.identity);
        Gen2 = Instantiate(CaveGenerator, gridSize * (transform.position - new Vector3(0, height, 0) * (GenerationsCleared + 1)), Quaternion.identity);
        Gen3 = Instantiate(CaveGenerator, gridSize * (transform.position - new Vector3(0, height, 0) * (GenerationsCleared + 2)), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.position.y < -1 * gridSize * height * (GenerationsCleared + 1))
        {
            switch (i)
            {
                case 1:
                    i++;
                    i = i % 3;
                    Destroy(Gen1);
                    GenerationsCleared++;
                    Gen1 = Instantiate(CaveGenerator, gridSize * (transform.position - new Vector3(0, height, 0) * (GenerationsCleared + 2)), Quaternion.identity);
                    break;
                case 2:
                    i++;
                    i = i % 3;
                    Destroy(Gen2);
                    GenerationsCleared++;
                    Gen2 = Instantiate(CaveGenerator, gridSize * (transform.position - new Vector3(0, height, 0) * (GenerationsCleared + 2)), Quaternion.identity);
                    break;
                case 0:
                    i++;
                    i = i % 3;
                    Destroy(Gen3);
                    GenerationsCleared++;
                    Gen3 = Instantiate(CaveGenerator, gridSize * (transform.position - new Vector3(0, height, 0) * (GenerationsCleared + 2)), Quaternion.identity);
                    break;
            }
        }
    }
}
