using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldowns : MonoBehaviour
{
    public Image image;
    public GameObject body;
    Color color;
    Movement movement;
    public bool sliding;
    // Start is called before the first frame update
    void Start()
    {
        movement = body.GetComponent<Movement>();
        color.r = 255;
        color.g = 255;
        color.b = 255;
    }

    // Update is called once per frame
    void Update()
    {
        if (sliding)
        {
            if (movement.slideTimeStamp + movement.slideCooldown < Time.time || movement.sliding) color.a = 1;
            else color.a = 0.6f;
        }
        else if (!sliding)
        {
            if (movement.boostTimeStamp + movement.boostCooldown < Time.time) color.a = 1;
            else color.a = 0.6f;
        }
        image.color = color;
    }
}
