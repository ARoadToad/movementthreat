/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationsCreator : MonoBehaviour
{
    [Header("Pillar Specs")]
    ObstacleGridCloning pillarClones;
    public GameObject pillar;

    [Header("Position")]
    GameObject[,] array;
    public GameObject positions;

    int xOffset;
    int zOffset;
    int xQuickFix;
    int zQuickFix;
    // Start is called before the first frame update
    void Start()
    {
        pillarClones = pillar.GetComponent<ObstacleGridCloning>();
        array = new GameObject[pillarClones.countToSide * 2, pillarClones.countToSide * 2];
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; i++)
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
                //array[i, j] =
                Instantiate(positions, new Vector3((i - pillarClones.countToSide + xQuickFix) * pillarClones.spacing + xOffset, 0, (j - pillarClones.countToSide + zQuickFix) * pillarClones.spacing + zOffset), Quaternion.identity);
            }
        }
        print("bruh");
    }
}
*/