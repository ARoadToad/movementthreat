using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class ObstacleGridCloning : MonoBehaviour
{
    public float spacing;
    public int countToSide;
    public GameObject pillarMesh;
    public Transform floor;
    // Start is called before the first frame update
    void Start()
    {
        floor.localScale += new Vector3(spacing * countToSide/ 5 + 2, 0, spacing * countToSide / 5 + 2);
        for (int i = -countToSide; i < countToSide+1; i++)
        {
            for (int j = -countToSide; j < countToSide+1; j++)
            {
                if (i == 0 && j == 0)
                {

                }
                else Instantiate(pillarMesh, new Vector3(i * spacing, 0, j * spacing), Quaternion.identity);
            }
        }
        gameObject.SetActive(false);
    }
}
