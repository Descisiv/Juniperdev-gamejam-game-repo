
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawning : MonoBehaviour
{
    [Range(0, 100)]
    public int spawnChance;

    public int width;
    public int height;

    //private CaveGeneratorReal generatorReference;
    public GameObject items;
    private int[,] cavePointsClone;
    private Vector2 checkLocation;

    /*    private void Awake()
        {
            generatorReference = GetComponent<CaveGeneratorReal>();
            width = generatorReference.width;
            height = generatorReference.height;
        }
    */
    private void CheckPoints()
    {
        cavePointsClone = new int[width, height];

        for (int x = 0; x < width; x++)
        {
            checkLocation.x = x;
            for (int y = 0; y < height; y++)
            {
                checkLocation.y = y;
                Collider2D collider = Physics2D.OverlapPoint(checkLocation);
                if (collider != null)
                {
                    Debug.Log("block found at" + x + "," + y);
                    cavePointsClone[x, y] = 1;
                }
                else
                {
                    cavePointsClone[x, y] = 0;
                }
            }
        }
    }

    private void SpawnItems()
    {
        int seed = Random.Range(0, 100000);
        System.Random randChoice = new System.Random(seed.GetHashCode());
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (y != 0 && y != 399 && y != 400)
                {
                    if (cavePointsClone[x, y - 1] == 1 && cavePointsClone[x, y + 1] == 0 && cavePointsClone[x, y] != 1)
                    {
                        if (randChoice.Next(0, 100) < spawnChance)
                        {
                            Instantiate(items, new Vector3(x, y, 5), Quaternion.identity, gameObject.transform);
                            Debug.Log("A devil fruit has spawned at " + x + "," + y);
                        }
                    }
                }

            }
        }
    }

    /*public void Awake()
    {
       
    }
    */
    void Start()
    {
        CheckPoints();
        Debug.Log("Points checked");
        Debug.Log(cavePointsClone);
        Debug.Log("Start called");
        SpawnItems();
    }

    //Could mabye do updating spawning if it decreases lag like as the player gets closer but idk if that'd be an issue
}
