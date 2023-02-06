using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Patrol : StateMachineBehaviour
{
    private Vector2 leftEdge;
    private Vector2 rightEdge;

    Transform player;
    Rigidbody2D rb;
    Enemy enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float patrolDistanse;
    private Vector3 initScale;
    private bool movingLeft;

    [SerializeField] private float stoppingDistance; //відстань початку погоні

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        leftEdge = new Vector2(rb.transform.position.x - patrolDistanse, rb.transform.position.y);
        rightEdge = new Vector2(rb.transform.position.x + patrolDistanse, rb.transform.position.y);
        enemy = animator.GetComponent<Enemy>();
        initScale = enemy.transform.localScale;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (movingLeft)
        {
            if (enemy.transform.position.x >= leftEdge.x)
                MoveInDirection(1);
            else
            {
                idleTimer += Time.deltaTime;

                if (idleTimer > idleDuration)
                    movingLeft = !movingLeft;
            }

        }
        else
        {
            if (enemy.transform.position.x <= rightEdge.x)
                MoveInDirection(-1);
            else
            {
                idleTimer += Time.deltaTime;

                if (idleTimer > idleDuration)
                    movingLeft = !movingLeft;
            }
        }
        if (Vector2.Distance(player.position, rb.position) <= stoppingDistance)
        {
            animator.SetTrigger("Run");
        }
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;

        //Make enemy face direction
        enemy.transform.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
            initScale.y, initScale.z);

        //Move in that direction
        enemy.transform.position = new Vector3(enemy.transform.position.x + Time.deltaTime * _direction * speed,
            enemy.transform.position.y, enemy.transform.position.z);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Run");
    }
}
