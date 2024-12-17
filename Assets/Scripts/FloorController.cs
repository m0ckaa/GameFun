using UnityEngine;

public class FloorController : MonoBehaviour
{
    public Vector3 currentRotation = Vector3.zero;
    private int maxRotation = 90;
    private float horzInput, vertInput;
    [SerializeField] private float rotationSpeed;

    void Update()
    {

        horzInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");

        if (currentRotation.z < maxRotation && horzInput < 0)
        {
            currentRotation.z += -horzInput * rotationSpeed * Time.deltaTime;
        }
        else if (currentRotation.z > -maxRotation && horzInput > 0)
        {
            currentRotation.z += -horzInput * rotationSpeed * Time.deltaTime;
        }

        
        if (currentRotation.x < maxRotation && -vertInput < 0)
        {
            currentRotation.x += vertInput * rotationSpeed * Time.deltaTime;
        }
        else if (currentRotation.x > -maxRotation && -vertInput > 0)
        {
            currentRotation.x += vertInput * rotationSpeed * Time.deltaTime;
        }

        transform.rotation = Quaternion.Euler(currentRotation.x, 0, currentRotation.z);

        /*
        if (Input.GetKey(KeyCode.W))
        {
            
                currentRotation.x += rotationSpeed * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if(currentRotation.z <= maxRotation)
            {
                currentRotation.z += rotationSpeed * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (currentRotation.x >= -maxRotation)
            {
                currentRotation.x -= rotationSpeed * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (currentRotation.z >= -maxRotation)
            {
                currentRotation.z -= rotationSpeed * Time.deltaTime;
            }
        }
        */
    }
}
