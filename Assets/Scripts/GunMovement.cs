using UnityEngine;

public class GunMovement : MonoBehaviour
{
    [Header("Recoil Settings")]
    [SerializeField] private float recoilAmount = 5f; // How much the gun rotate
    [SerializeField] private float recoilRecoverySpeed = 5f; // How fast it returns to original position
    private bool recoiled;

    [Header("Reload Settings")]
    [SerializeField] private float reloadAmount = 45f; // How much the gun rotates
    [SerializeField] private float reloadSpeed = 40f; // How fast it "reloads"
    [SerializeField] private float reloadRecoverySpeed = 80f; // How fast it returns to original position
    private bool reloaded;

    private Vector3 originalRotation; // To store the initial rotation
    private Vector3 currentRotation;  // Current rotation being applied

    private void Start()
    {
        // Store the original local rotation of the gun
        originalRotation = transform.localEulerAngles;
        currentRotation = originalRotation;
    }

    private void Update()
    {
        // Smoothly return to original rotation

        if (reloaded == true)
        {
            currentRotation = Vector3.Lerp(currentRotation, originalRotation, reloadRecoverySpeed * Time.deltaTime);
            transform.localEulerAngles = currentRotation;
        }
        else if (recoiled == true)
        {
            currentRotation = Vector3.Lerp(currentRotation, originalRotation, recoilRecoverySpeed * Time.deltaTime);
            transform.localEulerAngles = currentRotation;
        }

        if (currentRotation == originalRotation)
        {
            reloaded = false;
            recoiled = false;
            return;
        }
    }

    public void ApplyRecoil()
    {
        // Add recoil by modifying the gun's rotation
        currentRotation += new Vector3(-recoilAmount, 0, 0);
        recoiled = true;
    }

    public void ApplyReloadMvmnt()
    {
        // Add reload animation by modifying the gun's rotation
        currentRotation += new Vector3(reloadAmount, 0, 0);
        reloaded = true;
    }
}
