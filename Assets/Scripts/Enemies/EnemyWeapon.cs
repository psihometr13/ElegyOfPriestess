using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private int attackDamage = 10;
    [SerializeField] public Vector3 attackOffset;
    [SerializeField] public float attackRange = 1f;

    //[Header("Ranged Attack")]
    //[SerializeField] public Transform firepoint;
    //[SerializeField] private GameObject[] fireballs;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    public LayerMask attackMask;

    [Header("Idle Behaviour")]
    [SerializeField] private float attackCooldown; //перезарядка атаки
    private float cooldownTimer = 0;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;
    }

    //private void RangedAttack()
    //{
    //    fireballs[FindFireball()].transform.position = firepoint.position;
    //    fireballs[FindFireball()].GetComponent<EnemyProjectile>().ActivateProjectile();
    //}

    //private int FindFireball()
    //{
    //    for (int i = 0; i < fireballs.Length; i++)
    //    {
    //        if (!fireballs[i].activeInHierarchy)
    //            return i;
    //    }
    //    return 0;
    //}

    public void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (cooldownTimer >= attackCooldown)
                {
                    animator.SetTrigger("Attack");
                    Upd_PlayerControl.Instance.DamagePlayer(attackDamage);
                    cooldownTimer = 0;
                    Debug.Log("Attack from Melee Enemy!");
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * attackRange * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * attackRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
