using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    public float health;
    public Material damageFlash;
    public Material oMaterial;

    private void Start()
    {
        oMaterial = gameObject.GetComponent<MeshRenderer>().material;
    }
    // Update is called once per frame
    public void TookDamage(float damageDealt)
    {
        health -= damageDealt;
        gameObject.GetComponent<MeshRenderer>().material = damageFlash;
        CancelInvoke();
        Invoke(nameof(ChangeMaterialBack),0.2f);
        if (health < 1)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeMaterialBack()
    {
        gameObject.GetComponent<MeshRenderer>().material = oMaterial;
    }
}
