using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class FPScontroller : MonoBehaviour
{
    //Character movement settings
    public Rigidbody rb;
    public float wSpeed;
    public float jumpStrength;
    public float xSens = 1;
    public float ySens = 1;
    public bool invertY;

    //Misc
    public bool isGrounded;
    private Transform camT;
    public Interactalbe lastFocusedObj;
    public float currentRange;

    //Input control
    private Vector3 moveInput;
    private bool jump;
    private float yaw;
    private float pitch;

    private void Start()
    {
        camT = transform.GetChild(0);
    }

    private void Update()
    {
        StoreInput();
        ApplyRotation();
    }

    /// <summary>
    /// Applies input rotation to player and camera
    /// </summary>

    private void ApplyRotation()
    {
        transform.rotation = Quaternion.Euler(0, yaw, 0);
        camT.localRotation = Quaternion.Euler(pitch, 0, 0);
    }

    /// <summary>
    /// Stores input values for movement and aim
    /// </summary>
    private void StoreInput()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.z = Input.GetAxis("Vertical");

        yaw += Input.GetAxis("Mouse X") * xSens;
        pitch += Input.GetAxis("Mouse Y") * ySens
            * (invertY ? -1 : 1);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {
        Quaternion rot = transform.rotation;
        rot.x = 0f;
        rot.z = 0f;

        Vector3 finalDir = rot * moveInput;

        rb.AddForce(finalDir * wSpeed);

        if(jump)
        {
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
            jump = false;
        }

        CheckGrounded();
        CheckInteract();
    }

    private void CheckGrounded()
    {
        Vector3 origin = transform.position - Vector3.up * 0.9f;
        float maxDist = 1f;
        if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, maxDist))
        {
            ///we hit something
            isGrounded = (hit.distance < 0.2f);
        }
        else
        {
            //we didnt hit something
            isGrounded = false;
        }

        Debug.DrawRay(origin, Vector3.down * 10, Color.magenta);
    }

    private void CheckInteract()
    {
        Vector3 origin = camT.transform.position + camT.transform.forward * 0.2f;
        float maxDist = 60f;
        if (Physics.Raycast(origin, camT.transform.forward, out RaycastHit hit, maxDist))
        {
            //we hit something
            if (hit.collider.tag == "Interactable")
            {
                Interactalbe newInteractable = hit.collider.GetComponent<Interactalbe>();
                currentRange = Vector3.Distance(camT.transform.position, hit.point);

                if (newInteractable != lastFocusedObj) // check if its a new interactableObj
                {
                    newInteractable.Focus();

                    if (lastFocusedObj)
                        lastFocusedObj.OnExitFocus();

                    lastFocusedObj = newInteractable;
                }
            }
            return;
        }

        if(lastFocusedObj)
        {
            lastFocusedObj.OnExitFocus();
            lastFocusedObj = null;
        }

        Debug.DrawRay(origin, transform.forward * 10, Color.magenta);
    }
}
