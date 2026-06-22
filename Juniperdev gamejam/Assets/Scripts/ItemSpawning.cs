
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;

using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawning : MonoBehaviour
{
    public string[] itemNames;
    public GameObject[] itemSet;

    [Range(0, 100)]
    public int spawnChance;

    public int width;
    public int height;

    //private CaveGeneratorReal generatorReference;
    public GameObject items;
    public int itemChosen;
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
        Vector2 absolutePos = transform.TransformPoint(Vector2.zero);
        for (int x = 0; x < width; x++)
        {
            checkLocation.x = x + absolutePos.x;
            for (int y = 0; y < height; y++)
            {
                checkLocation.y = y + absolutePos.y;
                Collider2D collider = Physics2D.OverlapPoint(checkLocation);
                if (collider != null)
                {
                    //Debug.Log("block found at" + x + "," + y);
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
                if (y < height - 2 && y > 2)
                {
                    if (cavePointsClone[x, y - 1] == 1 && cavePointsClone[x, y + 1] == 0 && cavePointsClone[x, y] != 1)
                    {
                        if (randChoice.Next(0, 100) < spawnChance)
                        {
                            itemChosen = randChoice.Next(0, 1000000) % itemNames.Length;

                            GameObject child = Instantiate(itemSet[itemChosen], new Vector3(x, y, -5), Quaternion.identity, gameObject.transform);
                            child.transform.localPosition = new Vector3(x, y, -5);
                            child.transform.localRotation = Quaternion.identity;
                            //Debug.Log("A devil fruit has spawned at " + x + "," + y);
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
        // Debug.Log(cavePointsClone);
        Debug.Log("Start called");
        SpawnItems();
    }

    //Could mabye do updating spawning if it decreases lag like as the player gets closer but idk if that'd be an issue
}
