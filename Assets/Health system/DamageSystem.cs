using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageSystem : MonoBehaviour
{
    [Header("Damage Configuration")]
    [SerializeField] private float damageAmount = 10f;
    [Tooltip("If you are using trigger to deal dmg click this")]
    [SerializeField] private bool isTriggerType;
    [SerializeField] private bool destroyAfterDMG = true;

    [Header("Audio Configuration")]
    [SerializeField] private AudioClip damageSound;

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTriggerType)
        {
            HandleDamage(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isTriggerType)
        {
            HandleDamage(collision.gameObject);
        }
    }

    private void HandleDamage(GameObject target)
    {
        HealthSystem healthSystem = target.GetComponent<HealthSystem>();
        if (healthSystem != null)
        {
            healthSystem.TakeDamage(damageAmount);
            Debug.Log($"Dealt {damageAmount} damage to {target.name}");

            PlayDamageSound(target.GetComponent<AudioSource>());
        }
        else
        {
            Debug.LogWarning($"No HealthSystem found on {target.name}");
        }
    }

    private void PlayDamageSound(AudioSource audioSource)
    {
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
            if (destroyAfterDMG)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false; // Disable the sprite renderer after playing the sound
                Destroy(gameObject, damageSound.length); // Destroy the object after the sound has played}
            }
            else
            {
                Debug.LogWarning("AudioSource or damageSound is missing");
            }
        }
    }
}
