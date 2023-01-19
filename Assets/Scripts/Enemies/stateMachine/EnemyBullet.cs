using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private Vector3 target;
    private float speed;
    private float damage;
    private Vector3 movementVector;
    //private SoundEnControler enemyControler;

    public void Init(Vector3 target, float speed, float damage)
    {
        this.target = target;
        this.speed = speed;
        this.damage = damage;

        movementVector = (this.target - transform.position).normalized * this.speed;
    }

    void Update()
    {
        transform.position += movementVector * Time.deltaTime;
        StartCoroutine("Destroy");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var player = collision.gameObject.GetComponent<Upd_PlayerControl>();
            player.DamagePlayer(damage);
            Destroy(gameObject);
        }
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2.5f);

        Destroy(gameObject);
    }
}