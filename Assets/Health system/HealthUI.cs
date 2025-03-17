using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(HealthSystem))]
public class HealthUI : MonoBehaviour
{
    private HealthSystem healthSystem;
    [SerializeField] private Slider healthBarUI;
    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
    }

    private void Update()
    {
        healthBarUI.value = healthSystem.CurrentHealth / healthSystem.MaxHealth;
    }
}
