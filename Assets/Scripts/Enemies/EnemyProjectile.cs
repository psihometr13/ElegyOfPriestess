using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;
    //private Animator anim;
    private BoxCollider2D coll;

    private bool hit;

    private void Awake()
    {
        //anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(-movementSpeed * transform.parent.localScale.x, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hit = true;
        coll.enabled = false;
        if (collision.gameObject.tag == "Player" && gameObject.tag != "Fireball")
        {
            DamagePlayer();
            Debug.Log("Attack from Ranged Enemy!");
        }
        if (collision.gameObject.tag == "MeleeEnemy")
            collision.transform.GetComponent<Enemy>().TakeDamage(damage);
        if (collision.gameObject.tag == "Boss")
            collision.gameObject.GetComponent<BossHealth>().TakeDamage(damage);

        Deactivate(); //When this hits any object deactivate arrow
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void DamagePlayer()
    {
        Upd_PlayerControl.Instance.DamagePlayer(damage);
    }
}
