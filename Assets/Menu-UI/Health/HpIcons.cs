using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HpIcons : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private HealthSystem healthSystem;
    public float PlayerHP;
    public int numOfHearts;

    // Icon related things
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Start()
    {
        healthSystem = player.GetComponent<HealthSystem>();
        hearts = GetComponentsInChildren<Image>();

        // Debug log to check if hearts array is populated correctly
        Debug.Log("Hearts array length: " + hearts.Length);
    }

    private void Update()
    {
        PlayerHP = healthSystem.CurrentHealth; // Update PlayerHP with the current health value

        if (PlayerHP > healthSystem.MaxHealth)
        {
            PlayerHP = healthSystem.MaxHealth; // Clamp PlayerHP to max health
        }

        if (PlayerHP == 0)
        {
            hearts[0].sprite = emptyHeart;
        }
        else if (PlayerHP < healthSystem.MaxHealth * 0.20)
        {
            hearts[1].sprite = emptyHeart;
        }
        else if (PlayerHP < healthSystem.MaxHealth * 0.40)
        {
            hearts[2].sprite = emptyHeart;
        }
        else if (PlayerHP < healthSystem.MaxHealth * 0.60)
        {
            hearts[3].sprite = emptyHeart;
        }
        else if (PlayerHP < healthSystem.MaxHealth * 0.80f)
        {
            hearts[4].sprite = emptyHeart;
        }
        else
        {
            Debug.LogError("Invalid health value: " + PlayerHP);
        }
    }


}
