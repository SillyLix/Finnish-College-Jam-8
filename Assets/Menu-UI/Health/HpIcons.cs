using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HpIcons : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private HealthSystem healthSystem;
    public int PlayerHP;
    public int numOfHearts;

    // Icon related things
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Start()
    {
        healthSystem = player.GetComponent<HealthSystem>();
        PlayerHP = healthSystem.CurrentHealth;
        hearts = GetComponentsInChildren<Image>();
    }

    private void Update()
    {
        if (PlayerHP > numOfHearts)
        {
            PlayerHP = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < PlayerHP)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            hearts[i].enabled = i < numOfHearts;
        }
    }
}
