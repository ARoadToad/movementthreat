using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb; //loading rigidbody

    [Header("Run")]
    public float speed; //run variables
    float WSMovement;
    float ADMovement;
    Vector3 move;
    public float airInhibitor;
    float airVar;
    public Transform orient;

    [Header("Jump")]
    public float jumpPower; //jump variables

    [Header("Grounded Check")]
    bool grounded; //isGrounded variables
    public LayerMask mask;
    public Transform groundCheck;

    [Header("Drag")]
    public float groundDrag; //drag variables

    [Header("Crouch")]
    bool crouching; //crouch variables
    public float crouchingMultiplier;
    float crouchVar;
    public GameObject controller, body, cam, jumpChecker;

    [Header("Sprint")]
    bool sprinting; //run variables
    public float sprintingMultiplier;
    float sprintVar;

    [Header("Slide")]
    public bool sliding; //slide variables
    public float slideSpeed, slideDuration, slideCooldown, slidingDrag;
    public float slideTimeStamp;

    [Header("Boost")]
    public float boostPower;
    public float boostCooldown;
    public float boostTimeStamp;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mask = LayerMask.GetMask("Floor");

        crouching = false;
        sprinting = false;
        sliding = false;
        grounded = true;

        slideTimeStamp = -slideCooldown;
        boostTimeStamp = -boostCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();
        Sprint();
        Jump();
        Slide();
        Boost();
        Crouch();
        Multipliers();
        Drag();
        Run();
    }

    void Run()
    {
        WSMovement = Input.GetAxisRaw("Vertical");
        ADMovement = Input.GetAxisRaw("Horizontal");
        move = WSMovement * orient.forward + ADMovement * orient.right;

        rb.AddForce(speed * airVar * sprintVar * crouchVar * move.normalized);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded) rb.AddForce(transform.up * jumpPower, ForceMode.Impulse); //impluse since only 1 click so no deltatime
    }

    void IsGrounded()
    {
        if (Physics.Raycast(groundCheck.position,Vector3.down,0.25f, mask))
        {
            grounded = true;
        }
        else if (!Physics.Raycast(groundCheck.position, Vector3.down, 0.25f, mask))
        {
            grounded = false;
        }
    }

    void Drag()
    {
        if (!grounded)
        {
            rb.drag = 0;
        }
        else if (grounded)
        {
            if (sliding)
            {
                rb.drag = slidingDrag;
            }
            else rb.drag = groundDrag;
        }
    }

    void Crouch()
    { 
        if (Input.GetKeyDown(KeyCode.LeftControl) && !sliding && grounded) { 
            crouching = true;
            LowerCam();
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            crouching = false;
            RaiseCam();
        }
    }

    void LowerCam()
    {
        body.transform.localScale = new Vector3(1, 0.8f, 1);
        controller.transform.position = new Vector3(controller.transform.position.x, -0.2f, controller.transform.position.z);
        cam.transform.position = new Vector3(cam.transform.position.x, 2.12f, cam.transform.position.z);
        jumpChecker.transform.position = new Vector3(jumpChecker.transform.position.x, 0.2f, jumpChecker.transform.position.z);
    }

    void RaiseCam()
    {
        body.transform.localScale = new Vector3(1, 1, 1);
        cam.transform.position = new Vector3(cam.transform.position.x, 2.32f, cam.transform.position.z);
        controller.transform.position = new Vector3(controller.transform.position.x, 0, controller.transform.position.z);
        jumpChecker.transform.position = new Vector3(jumpChecker.transform.position.x, 0, jumpChecker.transform.position.z);
    }

    void Sprint() 
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sprinting = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprinting = false;
        }
    }

    void Slide() 
    { //bug where if user does this in air, glitches into ground;
        if (Input.GetKeyDown(KeyCode.LeftControl) && sprinting && grounded && slideTimeStamp + slideCooldown < Time.time)
        {
            sliding = true;
            slideTimeStamp = Time.time;
            rb.AddForce(orient.forward * slideSpeed, ForceMode.Impulse); 
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl) && sliding || slideTimeStamp + slideDuration < Time.time && sliding || !grounded && sliding)
        {
            sliding = false;
        }
    }

    void Boost()
    {
        if (Input.GetKeyDown(KeyCode.Q) && boostTimeStamp + boostCooldown < Time.time)
        {
            boostTimeStamp = Time.time;
            rb.AddForce(-1 * boostPower * orient.right, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.E) && boostTimeStamp + boostCooldown < Time.time)
        {
            boostTimeStamp = Time.time;
            rb.AddForce(orient.right * boostPower, ForceMode.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.F) && boostTimeStamp + boostCooldown < Time.time)
        {
            boostTimeStamp = Time.time;
            rb.AddForce(orient.forward * boostPower, ForceMode.Impulse);
        }
    }

    void Multipliers()
    {
        if (crouching) 
        {
            crouchVar = crouchingMultiplier;
        }else if(!crouching)
        {
            crouchVar = 1;
        }

        if (sprinting)
        {
            sprintVar = sprintingMultiplier;
        }
        else if (!sprinting)
        {
            sprintVar = 1;
        }

        if (!grounded)
        {
            airVar = airInhibitor;
        }
        else if (grounded)
        {
            airVar = 1;
        }
    }
}
