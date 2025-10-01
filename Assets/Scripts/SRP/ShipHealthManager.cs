using UnityEngine;
using UnityEngine.SceneManagement; // optional if you want to reload scene on death

public class ShipHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private ShipUIManager uiManager;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        uiManager.ShowMessage("Health: " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        uiManager.ShowMessage("Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        uiManager.ShowMessage("Healed! Health: " + currentHealth);
    }

    private void Die()
    {
        isDead = true;
        uiManager.ShowMessage("You are destroyed! Restarting...");
        Invoke(nameof(RestartGame), 3f);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public int GetCurrentHealth() => currentHealth;
    public int GetMaxHealth() => maxHealth;
}
