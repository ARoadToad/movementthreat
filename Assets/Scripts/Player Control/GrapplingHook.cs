using Unity.VisualScripting;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [Header("Specs")]
    public float hookSpeed;
    public float breakAngle;
    public float maxBeforeSnap;
    public float nonActiveLocation;

    [Header("GameObjects")]
    public GameObject muzzle;
    public GameObject hook;
    public GameObject rope;
    public Transform playerPosition;
    public GameObject playerChar;
    public GameObject enemy;
    public LayerMask playerMask;
    LineRenderer line;
    Rigidbody Rb;

    public bool connection = false;

    void Start()
    {
        line = rope.GetComponent<LineRenderer>();
        line.enabled = false;
        Physics.IgnoreCollision(hook.GetComponent<Collider>(), playerChar.GetComponent<Collider>(), true);
        Physics.IgnoreCollision(hook.GetComponent<Collider>(), enemy.GetComponent<Collider>(), true);
        Physics.IgnoreLayerCollision(9, 10);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!connection)
        {
            connection = true;
            print("connected");
        }
        Rb.velocity = Vector3.zero;
    }

    public void SetupGrappler(RaycastHit hit)
    {
        Rb = hook.AddComponent<Rigidbody>();
        hook.transform.position = muzzle.transform.position;
        hook.transform.LookAt(hit.point);
        muzzle.transform.LookAt(hit.point);
        line.enabled = true;
    }
    public void DisableHook()
    {
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, Vector3.zero);
        line.enabled = false;
        hook.transform.SetPositionAndRotation(new Vector3(0, nonActiveLocation, 0), Quaternion.identity);
        connection = false;
        Rb.velocity = Vector3.zero;
        Destroy(hook.GetComponent<Rigidbody>());
    }

    public void UpdateGrappler(RaycastHit hit)
    {
        line.SetPosition(0, muzzle.transform.position);
        line.SetPosition(1, hook.transform.position);
        hook.transform.LookAt(hit.point);
        muzzle.transform.LookAt(hit.point);
        if (!connection)
        {
            Rb.AddForce(hook.transform.forward * hookSpeed);
        }
        else if (connection)
        {
            hook.transform.position = hit.point;
        }
    }

    public bool LineSnap(RaycastHit hit)
    {
        if (Vector3.Distance(playerPosition.position, hit.point) > maxBeforeSnap) return true;
        else return false;
        // || Physics.Raycast(muzzle.transform.position,muzzle.transform.forward,Vector3.Distance(muzzle.transform.position,hook.transform.position)-1f,playerMask)
        //not using because makes wrapping around feel bad
        // || muzzle.transform.localRotation.eulerAngles.x > breakAngle & muzzle.transform.localRotation.eulerAngles.x < 360f - breakAngle || muzzle.transform.localRotation.eulerAngles.y > breakAngle && muzzle.transform.localRotation.eulerAngles.y < 360f - breakAngle
        //not using because inconsistent deactivation feel bad
    }
}
