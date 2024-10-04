using UnityEngine;

public class Hitable : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;

    private HealthBar healthBar;

    private void Start()
    {
        healthBar = transform.Find("HealthBarUI/HealthBar").GetComponent<HealthBar>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        Debug.Log(currentHealth);
    }

    public void TakeDamage(float damageValue)
    {
        currentHealth -= damageValue;
        healthBar.SetHealth(currentHealth);

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
