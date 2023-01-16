using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public float startingHealth;
    [SerializeField] public float currentHealth { get; private set; }
    //private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    public UnityEngine.Object enemyRef;

    [SerializeField] float timeDestroy = 5f;
    Vector3 spawnPos;
    Animator animator;

    private void Start()
    {
        spawnPos = transform.position;
        //enemyRef = Resources.Load(gameObject.tag);
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        currentHealth = startingHealth;
        //anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //Debug.Log(currentHealth);
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        Debug.Log(currentHealth);
        if (currentHealth > 0)
        {
            animator.SetTrigger("Hurt");
            //StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                animator.SetTrigger("Die");

                //Deactivate all attached component classes
                foreach (Behaviour component in components)
                    component.enabled = false;

                dead = true;

                EnemyDie();
            }
        }
    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    //private IEnumerator Invunerability()
    //{
    //    invulnerable = true;
    //    Physics2D.IgnoreLayerCollision(10, 11, true);
    //    //for (int i = 0; i < numberOfFlashes; i++)
    //    //{
    //    //    spriteRend.color = new Color(1, 0, 0, 0.5f);
    //    //    yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
    //    //    spriteRend.color = Color.white;
    //    //    yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
    //    //}
    //    Physics2D.IgnoreLayerCollision(10, 11, false);
    //    invulnerable = false;
    //}

    IEnumerator PlayDieAnim()
    {
        yield return new WaitForSeconds(0.8f);

        gameObject.SetActive(false);
        Invoke("Respawn", timeDestroy);
    }

    void EnemyDie()
    {
        if(dead == true)
        {
            StartCoroutine("PlayDieAnim");
        }
    }

    void Respawn()
    {
        GameObject enemyCopy = (GameObject)Instantiate(enemyRef);
        enemyCopy.transform.position = new Vector3(Random.Range(spawnPos.x - 3, spawnPos.x + 3), spawnPos.y, spawnPos.z);
        enemyCopy.name = gameObject.name;
        enemyCopy.SetActive(true);
        
        Destroy(gameObject);
    }
}
