using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Check Visibility")]
    public GameObject testRayer;
    public LayerMask pLayer;
    public float sightRange;
    public GameObject player;

    [Header("Shoot")]
    public GameObject gun;
    public GameObject bullet;
    public float bulletLifespan;
    public float bulletSpeed;
    public float fireRate;
    bool cooldown;
    public float bulletMass;

    [Header("Pathfinding")]
    public DestinationsCreator destinationsCreator;
    int xPos;
    int zPos;
    bool lateStart = true;
    bool looper;
    bool otherL;
    public float movementSpeed;
    bool up;
    bool down;
    bool left;
    bool right;
    int direction;

    // Update is called once per frame
    void Update()
    {
        if (lateStart)
        {
            looper = true;
            while (looper)
            {
                xPos = Random.Range(0, destinationsCreator.positionCount);
                zPos = Random.Range(0, destinationsCreator.positionCount);
                if (destinationsCreator.occupied[xPos, zPos] == false) looper = false;
            }
            destinationsCreator.Occupy(xPos, zPos);
            gameObject.transform.position = destinationsCreator.array[xPos, zPos].transform.position;
            gameObject.transform.position += new Vector3(0, 1, 0);
            NewDestination();
            lateStart = false;
        }

        if (CheckSight())
        {
            gun.transform.LookAt(player.transform);
            if (!cooldown) 
            {
                GameObject bulletClone = Instantiate(bullet, bullet.transform.position, bullet.transform.rotation);
                Rigidbody rb = bulletClone.AddComponent<Rigidbody>();
                rb.mass = bulletMass;
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                rb.AddForce(bulletClone.transform.forward * bulletSpeed,ForceMode.Impulse);
                bulletClone.AddComponent<SphereCollider>();
                Destroy(bulletClone, bulletLifespan);
                Invoke(nameof(StartCooldown),fireRate);
                cooldown = true;
            }
        }

        if (gameObject.transform.position.x == destinationsCreator.array[xPos, zPos].transform.position.x && gameObject.transform.position.z == destinationsCreator.array[xPos, zPos].transform.position.z) NewDestination();
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, destinationsCreator.array[xPos, zPos].transform.position, movementSpeed);
    }

    bool CheckSight()
    {
        testRayer.transform.LookAt(player.transform);
        if (Physics.Raycast(testRayer.transform.position, testRayer.transform.forward,sightRange, pLayer)) return true;
        else return false;
    }

    void StartCooldown()
    {
        cooldown = false;
    }

    void NewDestination()
    {
        otherL = true;
        destinationsCreator.Unoccupy(xPos, zPos);

        up = true;
        left = true;
        right = true;
        down = true;

        if (xPos != 0)
        {
            if (destinationsCreator.occupied[xPos - 1, zPos] == true) up = false;
        }
        if (xPos == 0 && up || !up) up = false;
        else up = true;

        if (xPos != destinationsCreator.positionCount-1)
        {
            if (destinationsCreator.occupied[xPos + 1, zPos] == true) down = false;
        }
        else if (xPos == destinationsCreator.positionCount - 1 && down || !down) down = false;
        else down = true;

        if (zPos != 0)
        {
            if (destinationsCreator.occupied[xPos, zPos - 1] == true) left = false;
        }
        else if (zPos == 0 && left || !left) left = false;
        else left = true;

        if (zPos != destinationsCreator.positionCount - 1)
        {
            if (destinationsCreator.occupied[xPos, zPos + 1] == true) right = false;
        }
        else if (zPos == destinationsCreator.positionCount - 1 && right || !right) right = false;
        else right = true;

        while (otherL)
        {
            direction = Random.Range(0, 4);
            if (direction == 0 && up)
            {
                xPos--;
                otherL = false;
            }
            else if (direction == 1 && left)
            {
                zPos--;
                otherL = false;
            }
            else if (direction == 2 && right)
            {
                zPos++;
                otherL = false;
            }
            else if (direction == 3 && down)
            {
                xPos++;
                otherL = false;
            }
            else if (!up && !left && !right && !down)
            {
                otherL = false;
            }
        }
        destinationsCreator.Occupy(xPos, zPos);
    }
}
