using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float health = 500;

    public GameObject deathEffect;

    public bool isInvulnerable = false;

    public AudioClip MainMusic;

    private void Update()
    {
        if (health <= 100)
        {
            GetComponent<Animator>().SetBool("IsEnraged", true);
        }
    }

    public void TakeDamage(float damage)
    {
        if (isInvulnerable)
            return;

        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        SoundManager.Instance.PlayMusic(MainMusic);
    }

}
