using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public Image healthBarFill;
    private float maxSize;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBarFill = GameObject.Find("HealthFill").GetComponent<Image>();
    }

    public void Update()
    {
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log($"Take {damage} damage");
        if (currentHealth <= 0)
        {
            healthBarFill.fillAmount = 0;
            Die();
        }
    }
    public float getHealth()
    {
        return currentHealth;
    }
    private void Die()
    {
        Debug.Log("Press F to pay respect");
        // load scene
    }
}
