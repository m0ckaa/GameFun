using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    [Header("Gun Properties")]
    public float currentRange;
    public float maxRange;
    public float fireRate;
    public float cooldownTimer = 0f;
    public float damage;

    [Header("Ammo/Reload Properties")]
    public Vector2Int ammo;
    public bool isReloading;
    public float reloadCooldown;
    private float reloadTimer = 0f;

    [Header("Reference to External Objects/Scripts")]
    public GameObject player;
    private Interactalbe target;
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI reloadText;

    private void Start()
    {
        reloadText.gameObject.SetActive(false);
        UpdateAmmoText();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if(Input.GetMouseButtonDown(0))
        {
            Fire();
        }

        if(Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            if(ammo[0] != ammo[1])
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
            currentRange = GetCurrentRange();
            if (ammo[0] != 0 && !isReloading)
            {
                ammo[0] -= 1;
                cooldownTimer = 0f;
                UpdateAmmoText();
                if (currentRange <= maxRange)
                {
                    target = GetTarget();
                    if (target)
                    {
                        target.TakeDamage(damage);
                    }
                }
            }
        }
    }

    private void StartReload()
    {
        isReloading = true;
        reloadTimer = reloadCooldown;
        reloadText.gameObject.SetActive(true);
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

    private float GetCurrentRange()
    {
        return player.GetComponent<FPScontroller>().currentRange;
    }

    private Interactalbe GetTarget()
    {
        return player.GetComponent<FPScontroller>().lastFocusedObj;
    }
}
