using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunSwapping : MonoBehaviour
{
    public GameObject firstSlot;
    public GameObject secondSlot;
    public GameObject thirdSlot;

    public int currentlyActive;

    public Image AR;
    public Image shotgun;
    public Image sniper;
    // Start is called before the first frame update
    void Start()
    {
        AR.enabled = true;
        shotgun.enabled = false;
        sniper.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Activate(true, false, false, 0, true, false, false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Activate(false, true, false, 1, false, true, false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Activate(false, false, true, 2, false, false, true);
        }
    }

    void Activate(bool FA, bool SA, bool TA, int num, bool FE, bool SE, bool TE)
    {
        firstSlot.SetActive(FA);
        secondSlot.SetActive(SA);
        thirdSlot.SetActive(TA);
        currentlyActive = num;
        AR.enabled = FE;
        shotgun.enabled = SE;
        sniper.enabled = TE;
    }
}
