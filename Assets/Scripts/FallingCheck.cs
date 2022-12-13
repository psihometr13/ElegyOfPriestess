using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCheck : MonoBehaviour
{
    public float fallDistance = -30;
    public float fallDamage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            Debug.Log(PlayerControl.rb.velocity.y);
        }

        if (collision.gameObject.tag.Equals("Ground") && PlayerControl.rb.velocity.y < fallDistance)
        {
            Debug.Log("Falling damage!");
            PlayerControl.Instance.DamagePlayer(fallDamage);
        }
    }
}
