using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrapplingOn : MonoBehaviour
{
    public Image image;
    GrapplingMain grapplingMain;
    public GameObject anchor;
    Color color;
    // Start is called before the first frame update
    void Start()
    {
        grapplingMain = anchor.GetComponent<GrapplingMain>();
        color.r = 255;
        color.g = 255;
        color.b = 255;
    }

    // Update is called once per frame
    void Update()
    {
        if (grapplingMain.singleGrappling) color.a = 1;
        else if (!grapplingMain.singleGrappling) color.a = 0.6f;
        image.color = color;
    }
}
