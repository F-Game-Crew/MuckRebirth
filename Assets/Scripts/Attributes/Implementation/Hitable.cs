using UnityEngine;

public class Hitable : MonoBehaviour
{
    public float maxHeatlh;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHeatlh;
    }

    public void TakeDamage(float damageValue)
    {
        currentHealth -= damageValue;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
