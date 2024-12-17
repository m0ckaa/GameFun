using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float maxDamage = 100f;
    public float minDamage = 100f;
    public float killRange = 5f;
    public float splashRange = 1.5f;

    private void OnCollisionEnter(Collision collision)
    {
        // Trigger explosion on any collision
        Explode();
    }

    private void Explode()
    {
        // Create an overlapping sphere to find nearby colliders within the splash range
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, splashRange);

        foreach (Collider collider in hitColliders)
        {
            // Check if the collider belongs to an enemy
            if (collider.CompareTag("Interactable"))
            {
                Interactalbe enemy = collider.GetComponent<Interactalbe>();

                if (enemy != null)
                {
                    float distance = Vector3.Distance(transform.position, collider.transform.position);

                    if (distance <= killRange)
                    {
                        // Apply max damage if within kill range
                        enemy.TakeDamage(maxDamage);
                        Debug.Log($"Enemy at {distance}m took MAX damage: {maxDamage}");
                    }
                    else
                    {
                        // Calculate scaled damage based on distance within splash range
                        float damage = CalculateSplashDamage(distance);
                        enemy.TakeDamage(damage);
                        Debug.Log($"Enemy at {distance}m took splash damage: {damage}");
                    }
                }
            }
        }

        // Destroy the rocket after explosion
        Destroy(gameObject);
    }

    private float CalculateSplashDamage(float distance)
    {
        // Linear interpolation of damage based on distance
        float normalizedDistance = (distance - killRange) / (splashRange - killRange);
        normalizedDistance = Mathf.Clamp01(normalizedDistance); // Clamp between 0 and 1
        float damage = Mathf.Lerp(maxDamage, minDamage, normalizedDistance);
        return damage;
    }

}
