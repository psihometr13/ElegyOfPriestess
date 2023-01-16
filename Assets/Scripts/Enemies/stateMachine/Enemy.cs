using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;

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

    public bool isHitted = false;
    public bool isDead = false;
    

    private void Awake()
    {
        stateMachine = new StateMachine();
        idle = new Idle(this, stateMachine, enemyData, "Idle");
        ground = new Ground(this, stateMachine, enemyData, "Ground");
        simpleAttack = new SimpleAtack(this, stateMachine, enemyData, "Attack");
        longAttack = new LongAttack(this, stateMachine, enemyData, "LongAttack");
        hit = new Hit(this, stateMachine, enemyData, "Hit");
        dead = new Dead(this, stateMachine, enemyData, "Dead");
        player = GameObject.Find("Main_Hero_NoWeapon").transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        spawnPos = transform.position;
        anim = GetComponent<Animator>();
        stateMachine.Inizialization(idle);
        currentHP = enemyData.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.currentState.LogicUpdate();
        cooldownTimer += Time.deltaTime;
        if (enemyData.rooted) {
           // Debug.Log("see");
            LookAtPlayer();
        } 
        
    }
     public void Attack()
    {
        if (cooldownTimer >= enemyData.attackCooldown)
        {
            Upd_PlayerControl.Instance.DamagePlayer(enemyData.maxDamage);
            cooldownTimer = 0;
        }
    }
    public bool CheckGround() // перевірка чи є колайдер під ногами
    {

        return Physics2D.OverlapBox(checkPosition.position, checkSize, 0, Ground);

    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attack = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
            attack = false;  
    }

    public void TakeDamage(float damage)
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0, enemyData.maxHealth);
        if (currentHP > 0) isHitted = true;
        if(currentHP <= 1) isDead = true;
    }
    public void Respawn()
    {
        GameObject enemyCopy = (GameObject)Instantiate(enemyRef);
        enemyCopy.transform.position = new Vector3(Random.Range(spawnPos.x - 3, spawnPos.x + 3),
           spawnPos.y, spawnPos.z);
        enemyCopy.name = gameObject.name; 
        enemyCopy.SetActive(true);
        isDead = false;
        Destroy(gameObject);
    }
    IEnumerator WaitAfterHit()
    {
        yield return new WaitForSeconds(0.65f);

        isHitted = false;
    }
    IEnumerator Res()
    {
        yield return new WaitForSeconds(0.8f);

        gameObject.SetActive(false);
        Invoke("Respawn", 3f);
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
}
