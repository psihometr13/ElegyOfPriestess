using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossReward : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerControl.Instance.AddExp(10);
            Destroy(gameObject);
        }
    }
}
