using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationsCreator : MonoBehaviour
{
    [Header("Pillar Specs")]
    ObstacleGridCloning pillarClones;
    public GameObject pillar;

    [Header("Position")]
    public GameObject[,] array;
    public bool[,] occupied;
    public GameObject positions;
    public int positionCount;

    int xOffset;
    int zOffset;
    int xQuickFix;
    int zQuickFix;
    // Start is called before the first frame update
    void Start()
    {
        pillarClones = pillar.GetComponent<ObstacleGridCloning>();

        positionCount = pillarClones.countToSide * 2;
        occupied = new bool[positionCount, positionCount];
        for (int i = 0; i < positionCount; i++)
        {
            for (int j = 0; j < positionCount; j++)
            {
                occupied[i, j] = false;
            }
        }

        array = new GameObject[positionCount, positionCount];
        for (int i = 0; i < positionCount; i++)
        {
            for (int j = 0; j < positionCount; j++)
            {
                if (i < pillarClones.countToSide)
                {
                    xOffset = (int)pillarClones.spacing / -2;
                    xQuickFix = 1;
                }
                else
                {
                    xOffset = (int)pillarClones.spacing / 2;
                    xQuickFix = 0;
                }
                if (j < pillarClones.countToSide)
                {
                    zOffset = (int)pillarClones.spacing / -2;
                    zQuickFix = 1;
                }
                else
                {
                    zOffset = (int)pillarClones.spacing / 2;
                    zQuickFix = 0;
                }
                array[i, j] = Instantiate(positions, new Vector3((i - pillarClones.countToSide + xQuickFix) * pillarClones.spacing + xOffset, 0, (j - pillarClones.countToSide + zQuickFix) * pillarClones.spacing + zOffset), Quaternion.identity);
            }
        }
    }

    public void Occupy(int x, int z)
    {
        occupied[x, z] = true;
    }

    public void Unoccupy(int x, int z)
    {
        occupied[x, z] = false;
    }
}
