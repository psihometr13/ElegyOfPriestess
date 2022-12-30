using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using DialogueEditor;
using System;
using UnityEngine.Diagnostics;
using Unity.VisualScripting;
using System.IO;
using Unity.Burst.Intrinsics;
using System.Linq;
using UnityEngine.SocialPlatforms;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(Rigidbody2D))]

[Serializable]
public class PlayerControl : MonoBehaviour
{
	public static PlayerControl Instance { get; private set; }

    //Move
    [Header("Move Parameters")]
    public float speed = 5; // швидкість руху
	public float acceleration = 1; // прискорення
	public float runSpeed = 1; // run
	public float jumpForce = 15; // сила стрибка
	public float jumpDistance = 2f; // відстань від центру об'єкта до поверхні
	public bool facingRight = true; // в яку сторону дивиться
	public KeyCode jumpButton = KeyCode.Space; // кнопка для стрибка
	//bool canJump = false;
	public GameObject spawnPoint; //spawn
    private Vector3 direction;
	public static Rigidbody2D rb;

    //Health
    [Header("Health Parameters")]
    [SerializeField] public float curHealth = 0;
	[SerializeField] public float maxHealth = 100;
	public HealthBar healthBar;
	private bool isDead = true;
    [SerializeField] private int restoreHealthCount = 10;
    [SerializeField] private int restoreHealthEnergyUsage = 50;
    [SerializeField] public float maxDamage = 100;

    //Energy
    [Header("Energy Parameters")]
    [SerializeField] public float curEnergy = 0;
	[SerializeField] public float maxEnergy = 100;
	public EnergyBar energyBar;
    [SerializeField] private int restoreEnergyCount = 10;


    //Achimevents
    public int countOfDeaths = 0;
	public int countOfNotes = 0;
	public int countOfKilledBosses = 0;
	public int countOfVisitedLoc = 0;
	public int countOfUsedHeals = 0;
	public int countOfMagic = 0;
	public int countOfChests = 0;

	//Exp
	[SerializeField] public float curExp = 0;
	[SerializeField] public float maxExp = 100;

	//Final
	public bool isPontiffHelped = false;

    //Attack
    [Header("Attack Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    private float cooldownTimer = 0;

    [SerializeField] public Transform firepoint;
    [SerializeField] private GameObject[] fireballs;

    [SerializeField] private float range;
    [SerializeField] private float attackCooldown;
    [SerializeField] private int damage;

    private void Awake()
	{
		// If there is an instance, and it's not me, delete myself.

		if (Instance != null && Instance != this)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
		}
    }

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		rb.freezeRotation = true;
		curHealth = maxHealth;
		curEnergy = maxEnergy;

		if (MenuController.Instance.newGame == false && File.Exists(Path.Combine(@"C:\Users\ПК\AppData\LocalLow\Dream\ElegyOfPriestess\", "Profile.bin")))
			LoadGame();

	}

	bool GetJump() // перевірка чи є колайдер під ногами
	{
		bool result = false;

		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down * jumpDistance);
		//UnityEngine.Debug.Log(hit.collider.tag);
		if (hit.collider)
		{
			result = true;
		}

		return result;
	}

	void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.LeftShift))
		{
			//Debug.Log("run");
			rb.AddForce(direction * rb.mass * speed * acceleration * runSpeed);

		}
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
		{
			//Debug.Log("walk");
			//float move = Input.GetAxis("Horizontal");
			//GetComponent<Rigidbody2D>().velocity = new Vector2(move * speed * acceleration, GetComponent<Rigidbody2D>().velocity.y);
			rb.AddForce(direction * rb.mass * speed * acceleration);
		}

		if (Mathf.Abs(rb.velocity.x) > speed)
		{
			rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * speed, rb.velocity.y);
		}
	}

	void Flip() // відобразити по горизонталі
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void Update()
	{
		RestoreHealth();
		RestoreEnergy();
        healthBar.SetHealth(curHealth);
        energyBar.SetEnergy(curEnergy);

        UnityEngine.Debug.DrawRay(transform.position, Vector3.down * jumpDistance, Color.red); // підсвітка, для візуального налаштування jumpDistance

        if (Input.GetKeyDown(jumpButton) && GetJump())
		{
			rb.velocity = new Vector2(0, jumpForce);
		}

        cooldownTimer += Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			Attack(10);
		}

        if (Input.GetKeyDown(KeyCode.K))
        {
			SaveGame();
        }

        float h = Input.GetAxis("Horizontal");

		direction = new Vector2(h, 0);

		if (h > 0 && !facingRight) Flip(); else if (h < 0 && facingRight) Flip();
    }

    public void AddExp(float exp)
	{
		curExp += exp;
	}

	public void DamagePlayer(float damage)
	{
		if (curHealth > 0 && curHealth >= damage)
		{
			curHealth -= damage;
		}
		else
		{
			DiePlayer();
		}
	}

	public void DiePlayer()
	{
		curHealth = 0;
		isDead = true;
		this.transform.position = spawnPoint.transform.position;
		curHealth = maxHealth;
	}

	public void UseEnergy(int energy)
	{
		if(curHealth > 0 && curEnergy > 0 && curEnergy >= energy)
			curEnergy -= energy;
		else
			curEnergy = 0;
	}

	void Attack(int energy)
	{
		if(curEnergy >= energy && cooldownTimer >= attackCooldown)
		{
            UseEnergy(energy);
            fireballs[FindFireball()].transform.position = firepoint.position;
            fireballs[FindFireball()].GetComponent<EnemyProjectile>().ActivateProjectile();
            cooldownTimer = 0;
        }
        //GameObject.FindGameObjectsWithTag("Boss")[0].GetComponent<BossHealth>().TakeDamage(energy);
    }

    private void OnDrawGizmos()
    {
		Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "DeadZone")
		{
			DiePlayer();
		}
		if (collision.gameObject.tag == "Blade")
		{
			DamagePlayer(25);
			UnityEngine.Debug.Log("Damage from blade!");
		}

		if (collision.gameObject.tag == "Rosary")
        {
			UnityEngine.Debug.Log("Rosary used");
            maxHealth = maxHealth * 1.25f;
			Destroy(collision.gameObject);
        }

		if (collision.gameObject.tag == "Blindfold")
        {
			UnityEngine.Debug.Log("Blindfold used");
            maxDamage = maxDamage * 115 / 100;
			Destroy(collision.gameObject);
        }
		
		if (collision.gameObject.tag == "Bathilda's-ring")
        {
			UnityEngine.Debug.Log("Bathilda's ring used");
            maxEnergy = maxEnergy * 1.25f;
			Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Stick")
        {
            GameObject Child = gameObject.transform.Find("stick").gameObject;
            Instantiate(collision.gameObject, Child.transform.position, Quaternion.Euler(0, 0, -90), this.gameObject.transform);
            Destroy(collision.gameObject);
			Destroy(Child);
            UnityEngine.Debug.Log("New Stick used");
        }
    }

	void RestoreHealth()
	{
		if (curHealth >= 0 && curHealth < maxHealth)
		{
			curHealth += Time.deltaTime * restoreHealthCount;
		}
		if (Input.GetKeyDown(KeyCode.H) && curHealth >= 0 && curHealth < maxHealth)
		{
			UseEnergy(restoreHealthEnergyUsage);
            curHealth = maxHealth;
        }
	}

	void RestoreEnergy()
	{
        if (curEnergy >= 0 && curEnergy < maxEnergy)
        {
            curEnergy += Time.deltaTime * restoreEnergyCount;
        }
    }

	private void OnCollisionStay2D(Collision2D collision)
	{
		float damagePerSecond = 25;

		if (collision.gameObject.tag == "Spike")
		{
			DamagePlayer(damagePerSecond * Time.deltaTime);
			UnityEngine.Debug.Log("Damage from spike!");
		}
		if (collision.gameObject.tag == "Blade")
		{
			DamagePlayer(damagePerSecond * Time.deltaTime);
			UnityEngine.Debug.Log("Damage from blade!");
		}
	}

	public void LoadGame()
	{
        //General
        transform.position = SaveSystem.GetVector2("PlayerPosition");
        curHealth = SaveSystem.GetFloat("CurrentHealth");
        curEnergy = SaveSystem.GetFloat("CurrentEnergy");
        curExp = SaveSystem.GetFloat("CurrentExp");
        maxDamage = SaveSystem.GetFloat("MaxDamage");

        //Achimenents
        countOfDeaths = SaveSystem.GetInt("countOfDeaths");
        countOfNotes = SaveSystem.GetInt("countOfNotes");
        countOfKilledBosses = SaveSystem.GetInt("countOfKilledBosses");
        countOfVisitedLoc = SaveSystem.GetInt("countOfVisitedLoc");
        countOfUsedHeals = SaveSystem.GetInt("countOfUsedHeals");
        countOfMagic = SaveSystem.GetInt("countOfMagic");
        countOfChests = SaveSystem.GetInt("countOfChests");
    }

	void SaveGame()
	{
        //General
        SaveSystem.SetVector2("PlayerPosition", transform.position);
        SaveSystem.SetFloat("CurrentHealth", curHealth);
        SaveSystem.SetFloat("CurrentEnergy", curEnergy);
        SaveSystem.SetFloat("CurrentExp", curExp);
        SaveSystem.SetFloat("MaxDamage", maxDamage);

        //Achimenents
        SaveSystem.SetInt("countOfDeaths", countOfDeaths);
        SaveSystem.SetInt("countOfNotes", countOfNotes);
        SaveSystem.SetInt("countOfKilledBosses", countOfKilledBosses);
        SaveSystem.SetInt("countOfVisitedLoc", countOfVisitedLoc);
        SaveSystem.SetInt("countOfUsedHeals", countOfUsedHeals);
        SaveSystem.SetInt("countOfMagic", countOfMagic);
        SaveSystem.SetInt("countOfChests", countOfChests);

        SaveSystem.SaveToDisk();
    }

	public void HelpPontiff()
	{
		isPontiffHelped = true;
	}
}
