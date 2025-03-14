using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPLeft : MonoBehaviour
{
    Damagable damagable;
    public GameObject player;
    int maxHealth;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        damagable = player.GetComponent<Damagable>();
        maxHealth = (int)damagable.health;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = damagable.health + " / " + maxHealth;
    }
}