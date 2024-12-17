using UnityEngine;
using TMPro;

public class RocketLauncher : MonoBehaviour
{
    [Header("RL Properties")]
    public float fireRate;
    public float cooldownTimer = 0f;
    public float force;
    public Transform spawnPosition;
    public GameObject rocketPrefab;

    [Header("Ammo/Reload Properties")]
    public Vector2Int ammo;
    public bool isReloading;
    public float reloadCooldown;
    private float reloadTimer = 0f;

    [Header("Reference to External Objects/Scripts")]
    public GameObject player;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI reloadText;
    public Transform camT;

    private void Start()
    {
        reloadText.gameObject.SetActive(false);
        UpdateAmmoText();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            if (ammo[0] != ammo[1])
            {
                StartReload();
            }
        }

        if (isReloading)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0f)
            {
                FinishReload();
            }
        }
    }

    private void Fire()
    {
        if (cooldownTimer >= fireRate)
        {
            if (ammo[0] != 0 && !isReloading)
            {
                ammo[0] -= 1;
                cooldownTimer = 0f;
                UpdateAmmoText();
                //GetComponent<RLMovement>()?.ApplyRecoil();

                GameObject rocket = Instantiate(rocketPrefab, spawnPosition.position, Quaternion.identity);
                rocket.GetComponent<Rigidbody>().AddForce(camT.forward * force, ForceMode.Impulse);

            }
        }
    }

    private void StartReload()
    {
        isReloading = true;
        reloadTimer = reloadCooldown;
        reloadText.gameObject.SetActive(true);
        //GetComponent<RLMovement>()?.ApplyReloadMvmnt();
    }

    private void FinishReload()
    {
        isReloading = false;
        reloadTimer = 0f;
        ammo[0] = ammo[1];
        UpdateAmmoText();
        reloadText.gameObject.SetActive(false);
    }

    private void UpdateAmmoText()
    {
        ammoText.text = ammo[0] + " / " + ammo[1];
    }

}
