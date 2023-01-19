using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("States")]
    private StateMachine stateMachine;
    public SimpleAtack simpleAttack;
    public LongAttack longAttack;
    public EnemyData enemyData;
    public Ground ground;
    public Idle idle;
    public Hit hit;
    public Dead dead;
    public Patrol patrol;
    public Run run;
    public Ding ding;
    public Pray pray;

    [Header("Variables")]
    public Animator anim;
    public Transform checkPosition;
    public Vector2 checkSize;
    public LayerMask Ground;
    public bool attack;
    Vector3 spawnPos;
    public float currentHP;
    public float cooldownTimer = 0;
    public bool isFlipped = false;
    [SerializeField]
    public UnityEngine.Object enemyRef;
    public Transform player;
    private int direct;

    public bool isHitted = false;
    public bool isDead = false;
    public bool isPatroling = false;
    public bool isRunning = false;
    public bool isDing = false;
    public bool isPraying = false;
    public bool strongAttack;

    [Header("Patrol parameters")]
    public Transform leftEdge;
    public Transform rightEdge;

    public bool isOnEdge = false;

    [SerializeField] private float speed;
    [SerializeField] private float patrolDistanse;
    private Vector3 initScale;
    private bool movingLeft = false;

    [SerializeField] private float stoppingDistance; //відстань початку погоні

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [SerializeField] public Object bulletPrefab;
    [SerializeField] public Object bulletPrefab1;
    [SerializeField] public Object bulletPrefab2;
    [SerializeField] public GameObject actualShootPoint;
    [SerializeField] public float bulletVelocity;

    [SerializeField] private float attackCooldown;

    private void Awake()
    {
        stateMachine = new StateMachine();
        idle = new Idle(this, stateMachine, enemyData, "Idle");
        ground = new Ground(this, stateMachine, enemyData, "Ground");
        simpleAttack = new SimpleAtack(this, stateMachine, enemyData, "Attack");
        longAttack = new LongAttack(this, stateMachine, enemyData, "LongAttack");
        hit = new Hit(this, stateMachine, enemyData, "Hit");
        dead = new Dead(this, stateMachine, enemyData, "Dead");
        patrol = new Patrol(this, stateMachine, enemyData, "Patrol");
        run = new Run(this, stateMachine, enemyData, "Run");
        ding = new Ding(this, stateMachine, enemyData, "Ding");
        pray = new Pray(this, stateMachine, enemyData, "Pray");
        player = GameObject.Find("Main_Hero_NoWeapon").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        spawnPos = transform.position;
        anim = GetComponent<Animator>();
        stateMachine.Inizialization(idle);
        currentHP = enemyData.maxHealth;
        //NEW PARAM
        //leftEdge = new Vector2(gameObject.transform.position.x - patrolDistanse, gameObject.transform.position.y);
        //rightEdge = new Vector2(gameObject.transform.position.x + patrolDistanse, gameObject.transform.position.y);
        initScale = gameObject.transform.localScale;
        if (enemyData.demon || enemyData.spirit || enemyData.mosquito) isPatroling = true;
        bulletPrefab = bulletPrefab1;
        strongAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (player.transform.position.x < gameObject.transform.position.x)
        {
            direct = -1;
        }
        else
        {
            direct = 1;
        }
            stateMachine.currentState.LogicUpdate();
        cooldownTimer += Time.deltaTime;
        if (enemyData.rooted)
        {
            LookAtPlayer();
        }
       //Debug.Log(isOnEdge);
        if (Vector2.Distance(player.position, gameObject.transform.position) < stoppingDistance && enemyData.demon && !isOnEdge)
        {
            isPatroling = false;
            if(enemyData.demon) isRunning = true;
        }
        else if (Vector2.Distance(player.position, gameObject.transform.position) > stoppingDistance && enemyData.demon)
        {
            if (enemyData.demon) isRunning = false;
            isPatroling = true;
            isOnEdge = false;
        }
    }
    public void ChangeAttack()
    {
        if(Upd_PlayerControl.Instance.maxEnergy <= 40)
        {
            isPraying = false;
            isDing = true;
        }
        else
        {
            isPraying = true;
            isDing = false;
        }
    }
    public void Dinging()
    {
        Rotate(direct);
        if (cooldownTimer >= attackCooldown)
        {
            player.GetComponent<Upd_PlayerControl>().ChangeHealth(10);
            cooldownTimer= 0;
        }
    }
    public void Praying()
    {
        Rotate(direct);
        if (cooldownTimer >= attackCooldown)
        {
            player.GetComponent<Upd_PlayerControl>().ChangeEnergy(10);
            cooldownTimer = 0;
        }
    }
    public void Rotate(int _direction)
    {   
            gameObject.transform.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
               initScale.y, initScale.z);
    }

    public void SpawnFireball()
    {
        var bulletObject = GameObject.Instantiate(bulletPrefab, actualShootPoint.transform.position, Quaternion.identity);

        var bullet = bulletObject.GetComponent<EnemyBullet>();
        if (strongAttack) bullet.Init(player.transform.position, bulletVelocity, enemyData.maxDamage * 2);
        else bullet.Init(player.transform.position, bulletVelocity, enemyData.maxDamage);
    }
    public void Attack()
    {
        if (enemyData.mosquito)
        {
            Rotate(direct);
        }
        if (cooldownTimer >= enemyData.attackCooldown && !enemyData.mosquito)
        {
            if (enemyData.rooted && strongAttack) Upd_PlayerControl.Instance.DamagePlayer(enemyData.maxDamage*2);
            else Upd_PlayerControl.Instance.DamagePlayer(enemyData.maxDamage);
            cooldownTimer = 0;
        }
    }
    public bool CheckGround() // перевірка чи є колайдер під ногами
    {
        return Physics2D.OverlapBox(checkPosition.position, checkSize, 0, Ground);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (enemyData.demon)
            {
                attack = true;
                isRunning = false;
            }
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!enemyData.spirit)
            {
                attack = true;
                isPatroling = false;
            }
            if (enemyData.spirit)
            {
                ChangeAttack();
                isPatroling = false;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (!enemyData.spirit)
        {
            attack = false;
            isPatroling = true;
        }
        if (enemyData.spirit) {
            isPraying = false;
            isDing = false;
            isPatroling = true;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0, enemyData.maxHealth);
        if (currentHP > 0) isHitted = true;
        if(isHitted && enemyData.spirit)
        {
            Rotate(direct);
        }
        if (currentHP <= 1) isDead = true;
    }
    public void Respawn()
    {
        GameObject enemyCopy = (GameObject)Instantiate(enemyRef);
        enemyCopy.transform.position = new Vector3(Random.Range(spawnPos.x - 3, spawnPos.x + 3), spawnPos.y, spawnPos.z);
        enemyCopy.name = gameObject.name;
        enemyCopy.SetActive(true);
        isDead = false;
        Destroy(gameObject);
    }
    IEnumerator WaitAfterHit()
    {
        if (!enemyData.spirit)
        {
            yield return new WaitForSeconds(0.55f);
        }
        else if (enemyData.mosquito)
        {
            yield return new WaitForSeconds(1f);
        }
        else
        {
            yield return new WaitForSeconds(0.6f);
        }

        isHitted = false;
    }

    IEnumerator Res()
    {
        if (!enemyData.spirit)
        {
            yield return new WaitForSeconds(0.8f);
        }
        else if (enemyData.mosquito)
        {
            yield return new WaitForSeconds(1f);
        }
        else
        {
            yield return new WaitForSeconds(0.6f);
        }
        gameObject.SetActive(false);
        Invoke("Respawn", 300f);
    }
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

    public void Patroler()
    {
        if (movingLeft)
        {
            if (gameObject.transform.position.x >= leftEdge.position.x) MoveInDirection(-1);
            else DirectionChange();
        }
        else
        {
            if (gameObject.transform.position.x <= rightEdge.position.x) MoveInDirection(1);
            else DirectionChange();
        }
       
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;

        //Make enemy face direction
        gameObject.transform.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
            initScale.y, initScale.z);


        //Move in that direction
        gameObject.transform.position = new Vector3(gameObject.transform.position.x + Time.deltaTime * _direction * speed,
            gameObject.transform.position.y, gameObject.transform.position.z);
    }

    private void DirectionChange()
    {
        idleTimer += Time.deltaTime;
        isFlipped = !isFlipped;
        if (idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }

    public void Run()
    {
        Rotate(direct);
      
        Vector2 target = new Vector2(player.position.x, gameObject.transform.position.y);

        if (Vector2.Distance(target, gameObject.transform.position) <= stoppingDistance && !attack && !isOnEdge)
        {
            //SoundManager.Instance.PlayMusic(BattleMusic);
            if((gameObject.transform.position.x >= rightEdge.position.x || gameObject.transform.position.x <= leftEdge.position.x) && enemyData.demon)
            {
                //speed = 0;
                isOnEdge = true;
                isPatroling= true;
                stateMachine.stateChange(patrol);
            }
            else
            {
                Vector2 newPos = Vector2.MoveTowards(gameObject.transform.position, target, speed * 3f * Time.fixedDeltaTime);
                gameObject.GetComponent<Rigidbody2D>().MovePosition(newPos);
            }
        }
        else
        {
            isRunning = false;
        }
    }
}
