using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerControl : MonoBehaviour
{
	//Move
	public float speed = 5; // швидкість руху
	public float acceleration = 1; // прискорення
	public float runSpeed = 1; // run
	public float jumpForce = 15; // сила стрибка
	public float jumpDistance = 0.75f; // відстань від центру об'єкта до поверхні
	public bool facingRight = true; // в яку сторону дивиться
	public KeyCode jumpButton = KeyCode.Space; // кнопка для стрибка
	public GameObject spawnPoint;

	private Vector3 direction;
	private Rigidbody2D body;

	//Health
	public int curHealth = 0;
	public int maxHealth = 100;
	public HealthBar healthBar;
	private bool isDead = true;

	//Energy
	public int curEnergy = 0;
	public int maxEnergy = 100;
	public EnergyBar energyBar;

	void Start()
	{
		body = GetComponent<Rigidbody2D>();
		body.freezeRotation = true;
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
			Debug.Log("run");
			body.AddForce(direction * body.mass * speed * acceleration * runSpeed);

		}
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
		{
			Debug.Log("walk");
			body.AddForce(direction * body.mass * speed * acceleration);
		}

		if (Mathf.Abs(body.velocity.x) > speed)
		{
			body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * speed, body.velocity.y);
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
		Debug.DrawRay(transform.position, Vector3.down * jumpDistance, Color.red); // підсвітка, для візуального налаштування jumpDistance

		if (Input.GetKeyDown(jumpButton) && GetJump())
		{
			body.velocity = new Vector2(0, jumpForce);
		}

		if (Input.GetKeyDown(KeyCode.Q))
		{
			DamagePlayer(10);
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			UseEnergy(10);
		}

		float h = Input.GetAxis("Horizontal");

		direction = new Vector2(h, 0);

		if (h > 0 && !facingRight) Flip(); else if (h < 0 && facingRight) Flip();
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DeadZone")
        {
			isDead = true;
			this.transform.position = spawnPoint.transform.position;
		}
    }
	public void DamagePlayer(int damage)
	{
		if(curHealth > 0)
        {
			curHealth -= damage;

			healthBar.SetHealth(curHealth);
		}
        else
        {
			isDead = true;
        }
	}
	public void UseEnergy(int energy)
	{
		curEnergy -= energy;

		energyBar.SetEnergy(curEnergy);
	}
}
