using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;
    [SerializeField] private float stoppingDistance; //відстань початку погоні

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    bool patrol = false;
    bool chase = false;

    //[Header("Enemy Animator")]
    //[SerializeField] private Animator anim;

    private void Awake()
    {
        initScale = enemy.localScale;
    }
    private void OnDisable()
    {
        //anim.SetBool("moving", false);
    }

    private void Update()
    {
        if (Vector2.Distance(this.transform.position, PlayerControl.Instance.transform.position) > stoppingDistance && chase == false)
        {
            patrol = true;
        }
        else if (Vector2.Distance(this.transform.position, PlayerControl.Instance.transform.position) <= stoppingDistance)
        {
            chase = true;
            patrol = false;
        }

        if (patrol == true)
        {
            Patrol();
        }
        else if (chase == true)
        {
            ChasePlayer();
        }
    }

    private void DirectionChange()
    {
        //anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        //anim.SetBool("moving", true);

        //Make enemy face direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
            initScale.y, initScale.z);

        //Move in that direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }

    private void ChasePlayer()
    {
        Debug.Log("Chase!");

        idleTimer = 0;

        enemy.position = new Vector2(PlayerControl.Instance.transform.position.x + Time.deltaTime * speed * 2, PlayerControl.Instance.transform.position.y);
    }

    private void Patrol()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }
    }
}
