using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float camSpeed = 0.5f;

    private float x;
    private float y;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        float mX = Input.GetAxis("Mouse X")*camSpeed;
        float mY = Input.GetAxis("Mouse Y")*camSpeed;

        y = Mathf.Clamp(y, -90f, 90f);

        x += mX;
        y += mY;

        //why tf is it y,x,z 
        transform.rotation = Quaternion.Euler(y,-x, 0);
    }
}