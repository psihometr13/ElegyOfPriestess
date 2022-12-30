using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Run : StateMachineBehaviour
{
    public float speed = 5f;
    public float fightRange = 30f;
    public float attackRange = 7f;
    [SerializeField] private float attackCooldown; //перезарядка атаки

    Transform player;
    Rigidbody2D rb;
    Enemy enemy;

    private Vector3 initScale;

    public AudioClip BattleMusic;
    private float cooldownTimer = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        enemy = animator.GetComponent<Enemy>();
        initScale = enemy.transform.localScale;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        cooldownTimer += Time.deltaTime;

        enemy.LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, rb.position.y);

        if (Vector2.Distance(target, rb.gameObject.transform.position) <= fightRange)
        {
            //SoundManager.Instance.PlayMusic(BattleMusic);

            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            if (Vector2.Distance(player.position, rb.position) <= attackRange)
            {
                if (cooldownTimer >= attackCooldown)
                {
                    animator.SetTrigger("Attack");
                    cooldownTimer = 0;
                }
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
