using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCheck : MonoBehaviour
{
    public float fallDistance = -10;
    public float fallDamage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            //Debug.Log(Upd_PlayerControl.body.velocity.y);
        }

        if (collision.gameObject.tag.Equals("Ground") && Upd_PlayerControl.body.velocity.y < fallDistance)
        {
            //Debug.Log("Falling damage!");
            Upd_PlayerControl.Instance.DamagePlayer(fallDamage * -Upd_PlayerControl.body.velocity.y);
        }
    }
}
