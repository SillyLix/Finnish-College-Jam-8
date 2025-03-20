using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HpIcons : MonoBehaviour
{

    [SerializeField] HealthSystem healthSystem;
    public float placeholderHP;
    public int numOfHearts;

    // Icon related things
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Update()
    {
        placeholderHP = healthSystem.CurrentHealth;
        if (placeholderHP > numOfHearts)
        {
            placeholderHP = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < placeholderHP)
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
