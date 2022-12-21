using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public int attackDamage = 10;
    public int enragedAttackDamage = 30;

    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;

    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        //Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            Debug.Log(collision.gameObject.tag);
            if (collision.gameObject.tag == "Player")
            {
                PlayerControl.Instance.DamagePlayer(attackDamage);
                Debug.Log("Attack from Pontiff!");
            }
        }
    }

    public void EnragedAttack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        //Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        //if (colInfo != null)
        //{
        //    Debug.Log(colInfo.tag);
        //    if (colInfo.tag == "Player")
        //    {
        //        PlayerControl.Instance.DamagePlayer(attackDamage);
        //        Debug.Log("Enranged attack from Pontiff!");
        //    }
        //}
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
