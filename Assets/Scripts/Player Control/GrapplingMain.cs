using UnityEngine;
//remember to addcomponent cylinder collider
public class GrapplingMain : MonoBehaviour
{
    [Header("Specs")]
    public float maxDistance;
    public float playerGrapplingSpeed;

    [Header("GameObjects")]
    public Transform cameraOrigin;
    public GameObject leftHook;
    public GameObject rightHook;
    public GameObject playerModel;

    Rigidbody playerRb;
    public bool singleGrappling = false;
    public bool doubleGrappling = false;
    RaycastHit hit;
    GrapplingHook hookClassR;
    GrapplingHook hookClassL;

    void Start()
    {
        hookClassR = rightHook.GetComponent<GrapplingHook>();
        hookClassL = leftHook.GetComponent<GrapplingHook>();
        playerRb = playerModel.GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !singleGrappling && !doubleGrappling && Physics.Raycast(cameraOrigin.position, cameraOrigin.forward, out RaycastHit sHit, maxDistance)) {
            singleGrappling = true;
            hit = sHit;
            hookClassR.SetupGrappler(hit);
            hookClassL.SetupGrappler(hit);
        }
        else if (singleGrappling)
        {
            if (Input.GetKeyDown(KeyCode.C) || hookClassR.LineSnap(hit) || hookClassL.LineSnap(hit)) 
            {
                print("disengaged");
                singleGrappling = false;
                hookClassR.DisableHook();
                hookClassL.DisableHook();
            }
        }

        /* if (Input.GetKeyDown(KeyCode.Alpha2) && !singleGrappling && !doubleGrappling && DoubleGrappleAlgorithm()) doubleGrappling = true;
        if (Input.GetKeyDown(KeyCode.Alpha2) && doubleGrappling)
        {
            doubleGrappling = false;
        } */

        if (singleGrappling || doubleGrappling)
        {
            hookClassL.UpdateGrappler(hit);
            hookClassR.UpdateGrappler(hit);

            if (singleGrappling)
            {
                if (hookClassL.connection && hookClassR.connection)
                {
                    SingleGrapple();
                }
                
            }
            /*else if (doubleGrappling)
            {

            }
            */
        }
    }
    
    void SingleGrapple()
    {
        playerRb.AddForce(hookClassL.muzzle.transform.forward * playerGrapplingSpeed);
        playerRb.AddForce(hookClassR.muzzle.transform.forward * playerGrapplingSpeed);
    }

    /*
    void DoubleGrapple()
    {
        if hooks are still travelling, move hooks closer to destination
        if hooks are firmly in place, pull packs towards hooks 
    }

    bool DoubleGrappleAlgorithm()
    {
        //start 2 raycasts at predetermined angle going out with specified max and min range, if failed, scan again with increment a couple times

        return true; if successful
        return false; if unsuccessful
    }
    */
}
