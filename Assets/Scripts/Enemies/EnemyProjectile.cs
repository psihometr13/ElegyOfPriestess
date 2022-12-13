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
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hit = true;
        //OnTriggerEnter2D(coll); //Execute logic from parent script first
        coll.enabled = false;
        if (collision.gameObject.tag == "Player") DamagePlayer();
        Deactivate(); //When this hits any object deactivate arrow
        //if (anim != null)
        //    anim.SetTrigger("explode"); //When the object is a fireball explode it
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void DamagePlayer()
    {
            PlayerControl.Instance.DamagePlayer(damage);
    }
}
