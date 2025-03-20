using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider2D))]
public class HealthSystem : MonoBehaviour
{
    [Header("Health Configuration")]
    [SerializeField] private float maxHealth = 100;
    public float MaxHealth
    {
        get { return maxHealth; }
    }

    private float currentHealth;
    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    [SerializeField] private bool isPlayer = false;
    [SerializeField] private GameObject deathMenu;
    [Tooltip("If you want to destroy the gameobject after death click this")]
    [SerializeField] private bool destroyGameObject;

    [Header("Audio Configuration")]
    [SerializeField] private AudioClip deathSound;
    private AudioSource audioSource;

    private void Start()
    {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
    }

    // Method to apply damage to health
    public void TakeDamage(int damageAmount)
    {
        currentHealth = Mathf.Max(currentHealth - damageAmount, 0);
        if (currentHealth == 0)
        {
            Die();
        }
    }

    // Method to heal health
    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
    }

    // Handle the death of the entity
    private void Die()
    {
        if (isPlayer)
        {
            HandlePlayerDeath();
        }
        else
        {
            HandleNonPlayerDeath();
        }

        PlayDeathSound();
    }

    private void HandlePlayerDeath()
    {
        if (deathMenu != null)
        {
            Time.timeScale = 0f;
            deathMenu.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Death menu is not assigned.");
        }
    }

    private void HandleNonPlayerDeath()
    {
        if (destroyGameObject)
        {
            Destroy(gameObject);
        }
        else
        {
            Debug.Log($"{gameObject.name} has died.");
        }
    }

    private void PlayDeathSound()
    {
        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
        else
        {
            Debug.LogWarning("AudioSource or deathSound is missing.");
        }
    }
}
