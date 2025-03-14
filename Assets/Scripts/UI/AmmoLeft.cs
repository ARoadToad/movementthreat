using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class AmmoLeft : MonoBehaviour
{
    public TextMeshProUGUI text;

    public GameObject gunParent;
    GunSwapping gunSwapping;
    public GameObject sniper;
    Gunbase sniperScript;
    public GameObject AR;
    Gunbase ARScript;
    public GameObject shotgun;
    Gunbase shotgunScript;
    // Start is called before the first frame update
    void Start()
    {
        gunSwapping = gunParent.GetComponent<GunSwapping>();
        sniperScript = sniper.GetComponent<Gunbase>();
        ARScript = AR.GetComponent<Gunbase>();
        shotgunScript = shotgun.GetComponent<Gunbase>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gunSwapping.currentlyActive == 0) text.text = ARScript.ammoCount + " / " + ARScript.ammoCapacity;
        else if (gunSwapping.currentlyActive == 1) text.text = shotgunScript.ammoCount + " / " + shotgunScript.ammoCapacity;
        else if (gunSwapping.currentlyActive == 2) text.text = sniperScript.ammoCount + " / " + sniperScript.ammoCapacity;
    }
}
