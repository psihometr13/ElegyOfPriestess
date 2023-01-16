using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ : MonoBehaviour
{
    public Transform player;

    public bool isFlipped = false;

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.rotation = Quaternion.Euler(0, 180, 0);
            isFlipped = true;
        }
    }
}
