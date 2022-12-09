using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;

    private void Start()
    {
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = PlayerControl.Instance.maxHealth;
        healthBar.value = PlayerControl.Instance.maxHealth;
    }

    public void SetHealth(int hp)
    {
        healthBar.value = hp;
    }
}
