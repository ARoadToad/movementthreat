using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunbase : MonoBehaviour
{
    [Header("Reloading Stats")]
    bool reloading;
    public float reloadSpeed;

    [Header("Ammo Stats")]
    public int ammoCapacity;
    public int ammoCount;

    [Header("Firing Specs")]
    public float fireRate;
    public bool fullAuto;
    bool shooting;
    float timeShot;
    public Transform shotLocation;
    public float maxShotRange;
    public float damage;

    [Header("Shotgun Stats")]
    public int pellets;
    public float spread;

    [Header("Effects")]
    public ParticleSystem muzzleFlash;
    public GameObject impact;
    Vector3 deviation;
    public GameObject bulletTracer;
    public Transform barrelLocation;

    void Start()
    {
        reloading = false;
        ammoCount = ammoCapacity;
    }
    void Update()
    {
        if (fullAuto && Input.GetMouseButton(0)) Fire();
        else if (!fullAuto && Input.GetMouseButtonDown(0)) Fire();
        Debug.DrawRay(barrelLocation.position, barrelLocation.forward);
        if (Input.GetMouseButtonUp(0)) shooting = false;
        
        if (Input.GetKeyDown(KeyCode.R)) Reload();
    }
    void Fire()
    {
        if (ammoCount > 0 && !reloading && timeShot + fireRate < Time.time)
        {
            shooting = true;
            muzzleFlash.Play();
            for (int i = 0; i < pellets; i++)
            {
                deviation = barrelLocation.forward + barrelLocation.right * Random.Range(-spread, spread) + barrelLocation.up * Random.Range(-spread, spread);
                GameObject line = Instantiate(bulletTracer);
                LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
                lineRenderer.SetPosition(0, barrelLocation.position);
                lineRenderer.SetPosition(1, deviation.normalized * maxShotRange + barrelLocation.position);
                Destroy(line,0.05f);
                if (Physics.Raycast(shotLocation.position, shotLocation.forward + shotLocation.right * Random.Range(-spread, spread) + shotLocation.up * Random.Range(-spread, spread), out RaycastHit hitInfo, maxShotRange))
                {
                    Damagable damagable = hitInfo.transform.GetComponent<Damagable>();
                    if (damagable != null)
                    {
                        damagable.TookDamage(damage);
                    }
                    else
                    {
                        GameObject gameObject = Instantiate(impact, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                        Destroy(gameObject, 0.4f);
                    }
                }
            }
            ammoCount -= 1;
            timeShot = Time.time;
        }
    }

    void Reload()
    {
        if (!shooting && !reloading && ammoCount != ammoCapacity)
        {
            reloading = true;
            Invoke(nameof(RealReload), reloadSpeed);
        }
    }

    void RealReload()
    {
        ammoCount = ammoCapacity;
        reloading = false;
    }
}
