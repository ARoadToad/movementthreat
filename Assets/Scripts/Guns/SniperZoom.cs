using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperZoom : MonoBehaviour
{
    public int zoomFOV;
    public GameObject sniper;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Camera.main.fieldOfView = zoomFOV;
            sniper.SetActive(false);
        }
        if (Input.GetMouseButtonUp(1))
        {
            Camera.main.fieldOfView = 80; //add ui zoom
            sniper.SetActive(true);
        }
    }
}
