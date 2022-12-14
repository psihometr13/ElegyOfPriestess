using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using DialogueEditor;
using System;
using UnityEngine.Diagnostics;

[RequireComponent(typeof(Rigidbody2D))]

[Serializable]
public class PlayerControl : MonoBehaviour
{
	public static PlayerControl Instance { get; private set; }

	//Move
	public float speed = 5; // швидкість руху
	public float acceleration = 1; // прискорення
	public float runSpeed = 1; // run
	public float jumpForce = 15; // сила стрибка
	public float jumpDistance = 0.75f; // відстань від центру об'єкта до поверхні
	public bool facingRight = true; // в яку сторону дивиться
	public KeyCode jumpButton = KeyCode.Space; // кнопка для стрибка
	public GameObject spawnPoint; //spawn

	private Vector3 direction;
	public static Rigidbody2D rb;

	//Health
	[SerializeField] public float curHealth = 0;
	[SerializeField] public float maxHealth = 100;
	public HealthBar healthBar;
	private bool isDead = true;

	//Energy
	[SerializeField] public int curEnergy = 0;
	[SerializeField] public int maxEnergy = 100;
	public EnergyBar energyBar;

	//Achimevents
	public int countOfDeaths = 0;
	public int countOfNotes = 0;
	public int countOfKilledBosses = 0;
	public int countOfVisitedLoc = 0;
	public int countOfUsedHeals = 0;
	public int countOfMagic = 0;
	public int countOfChests = 0;

	//Dialog
	public NPCConversation myConversation;

	//Exp
	[SerializeField] public float curExp = 0;
	[SerializeField] public float maxExp = 100;

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
	}

	bool GetJump() // перевірка чи є колайдер під ногами
	{
		bool result = false;

		RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, jumpDistance);
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
		//Debug.DrawRay(transform.position, Vector3.down * jumpDistance, Color.red); // підсвітка, для візуального налаштування jumpDistance

		if (Input.GetKeyDown(jumpButton) && GetJump())
		{
			rb.velocity = new Vector2(0, jumpForce);
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			Attack(10);
		}

		float h = Input.GetAxis("Horizontal");

		direction = new Vector2(h, 0);

		if (h > 0 && !facingRight) Flip(); else if (h < 0 && facingRight) Flip();

		healthBar.SetHealth(curHealth);
	}

	public void AddExp(float exp)
	{
		curExp += exp;
	}

	public void DamagePlayer(float damage)
	{
		if (curHealth > 0)
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
		curEnergy -= energy;
		energyBar.SetEnergy(curEnergy);
	}

	void Attack(int energy)
	{
		UseEnergy(energy);
		GameObject.FindGameObjectsWithTag("Boss")[0].GetComponent<BossHealth>().TakeDamage(energy);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "DeadZone")
		{
			DiePlayer();
		}
		if (collision.gameObject.tag == "Blade")
		{
			DamagePlayer(25);
			UnityEngine.Debug.Log("Damage from blade!");
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

	private void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(0))
		{
			ConversationManager.Instance.StartConversation(myConversation);
		}
	}
}
