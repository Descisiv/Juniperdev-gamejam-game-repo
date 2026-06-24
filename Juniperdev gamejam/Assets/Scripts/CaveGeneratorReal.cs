using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveGeneratorReal : MonoBehaviour
{
    public float gridSize;
    public int width;
    public int height;
    public int smoothCycles;
    public int stoneDirtThreshold;
    public int stoneDirtCycles;

    private int[,] cavePoints;

    [Range(0, 100)]
    public int randFillPercent;
    //dirt takes difference between it and stone fill
    [Range(0, 100)]
    public int randDirtPercent;
    //ores are randomly generated after cave generation, considering all filled in blocks
    [Range(0, 1)]
    public float randDiamondPercent;
    [Range(0, 1)]
    public float randUraniumPercent;
    [Range(0, 1)]
    public float randRubyPercent;
    [Range(0, 8)]
    public int threshold;
    [Range(0, 1)]
    public float randLavaPercent;
    [Range(0, 8)]
    public int lavaThreshold;
    public int lavaSmoothCycles;

    public GameObject stone;
    public GameObject dirt;
    public GameObject Diamond;
    public GameObject Uranium;
    public GameObject Lava;
    public GameObject Ruby;
    private void Awake()
    {
        GenerateCave();
    }
    void Start()
    {
        PlaceGrid();
    }

    private void GenerateCave()
    {
        cavePoints = new int[width, height];
        int seed = Random.Range(0, 100000);
        System.Random randChoice = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                {
                    cavePoints[x, y] = 1;
                }
                else if (randChoice.Next(0, 100) < randFillPercent)
                {
                    cavePoints[x, y] = 1;
                }
                else
                {
                    cavePoints[x, y] = 0;
                }
            }
        }
        //discern between filled and empty
        for (int i = 0; i < smoothCycles; i++)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int neighboringWalls = GetNeighbors(x, y, 1);

                    if (neighboringWalls > threshold)
                    {
                        cavePoints[x, y] = 1;
                    }
                    else if (neighboringWalls < threshold)
                    {
                        cavePoints[x, y] = 0;
                    }
                }
            }
        }
        //randomize between dirt and stone
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (!(cavePoints[x, y] == 0))
                {
                    if (randChoice.Next(0, 100) < randDirtPercent)
                    {
                        cavePoints[x, y] = 1;
                    }
                    else
                    {
                        cavePoints[x, y] = 2;
                    }
                }
            }
        }
        //smooth between dirt and stone
        for (int i = 0; i < stoneDirtCycles; i++)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int neighboringWalls = GetNeighbors(x, y, 2);

                    if (neighboringWalls > threshold)
                    {
                        cavePoints[x, y] = 2;
                    }
                    else if (neighboringWalls < threshold)
                    {
                        cavePoints[x, y] = 1;
                    }
                }
            }
        }

        //generate ores
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (!(cavePoints[x, y] == 0))
                {
                    if (randChoice.NextDouble() <= randLavaPercent && !(cavePoints[x, y] == 0) && x >= 5 && x < width - 5 && y >= 5 && y < height - 5)
                    {
                        cavePoints[x, y] = 5;
                        Debug.Log("Lava created");
                    }
                    if (randChoice.NextDouble() <= randDiamondPercent)
                    {
                        cavePoints[x, y] = 3;
                    }
                    if (randChoice.NextDouble() <= randUraniumPercent)
                    {
                        cavePoints[x, y] = 4;
                    }

                    if (randChoice.NextDouble() <= randRubyPercent)
                    {
                        cavePoints[x, y] = 6;
                    }

                }

            }
        }

        //generate lava patches (could probably add this to the above for loop but)

        for (int i = 0; i < lavaSmoothCycles; i++)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int neighboringLava = GetNeighbors(x, y, 5);
                    if (neighboringLava > lavaThreshold && !(cavePoints[x, y] == 0) && x >= 5 && x < width - 5 && y >= 5 && y < height - 5)
                    {
                        Debug.Log("Lava made in cycle " + i);
                        cavePoints[x, y] = 5;
                    }
                    else if (neighboringLava < lavaThreshold && cavePoints[x, y] == 5)
                    {
                        cavePoints[x, y] = 1;
                    }
                }
            }
        }

    }


    private int GetNeighbors(int pointX, int pointY, int initial)
    {
        int wallNeighbors = 0;
        for (int x = pointX - 1; x <= pointX + 1; x++)
        {
            for (int y = pointY - 1; y <= pointY + 1; y++)
            {
                if (x >= 0 && x < width && y >= 0 && y < height)
                {
                    if (x != pointX || y != pointY)
                    {
                        if (cavePoints[x, y] == initial)
                        {
                            wallNeighbors++;
                        }
                    }
                }
                else
                {
                    wallNeighbors++;
                }
            }
        }
        return wallNeighbors;
    }

    private void PlaceGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (cavePoints[x, y] == 1)
                {
                    Instantiate(stone, new Vector3(gridSize * x, gridSize * y, 5) + transform.position, Quaternion.identity, gameObject.transform);
                }
                else if (cavePoints[x, y] == 2)
                {
                    Instantiate(dirt, new Vector3(gridSize * x, gridSize * y, 5) + transform.position, Quaternion.identity, gameObject.transform);
                }
                else if (cavePoints[x, y] == 3)
                {
                    Instantiate(Diamond, new Vector3(gridSize * x, gridSize * y, 5) + transform.position, Quaternion.identity, gameObject.transform);
                }
                else if (cavePoints[x, y] == 4)
                {
                    Instantiate(Uranium, new Vector3(gridSize * x, gridSize * y, 5) + transform.position, Quaternion.identity, gameObject.transform);

                }
                else if (cavePoints[x, y] == 5)
                {
                    Instantiate(Lava, new Vector3(gridSize * x, gridSize * y, 5) + transform.position, Quaternion.identity, gameObject.transform);
                }
                else if (cavePoints[x, y] == 6)
                {
                    Instantiate(Ruby, new Vector3(gridSize * x, gridSize * y, 5) + transform.position, Quaternion.identity, gameObject.transform);
                }
            }
        }
    }
}