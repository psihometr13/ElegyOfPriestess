using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.Threading;
[RequireComponent(typeof(Rigidbody2D))]

public class Upd_PlayerControl : MonoBehaviour
{
	//private Animations _animations;
	//private bool _isMoving;
	
	[SerializeField] private SpriteRenderer _spriteRenderer;
    public static Upd_PlayerControl Instance { get; private set; }
    //Move
    public float speed = 5; // швидкість руху
	public float acceleration = 1; // прискорення
	public float runSpeed = 1; // run
	public float jumpForce = 15; // сила стрибка
	public float jumpDistance = 0.75f; // відстань від центру об'єкта до поверхні
	public bool facingRight = true; // в яку сторону дивиться
	public KeyCode jumpButton = KeyCode.Space; // кнопка для стрибка
	public GameObject spawnPoint; //spawn

    [SerializeField] List<GameObject> rooms;
	public int currentRoom;

	private Vector3 direction;
	public static Rigidbody2D body;
	public static CapsuleCollider2D capsuleCollider;
	

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


	public Animator _anim;
    public Animator _anim2;
	public LayerMask groundMask;
	private bool grounded;
	public bool IsMoving;
	public float distanceToGround = 3;
	void Start()
	{
		//_animations = GetComponentInChildren<Animations>();
		body = GetComponent<Rigidbody2D>();
		_anim = GetComponent<Animator>();
		_anim2 = GetComponent<Animator>();
		body.freezeRotation = true;
		curHealth = maxHealth;
		curEnergy = maxEnergy;
	}
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

    void FixedUpdate()
	{
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
		{
			IsMoving = true;

			//Debug.Log("walk");	
			Move();
			//_animations.IsMoving = _isMoving;
			_anim.SetBool("IsMoving", true);

            _anim.SetBool("Run", false);
        }
        else
		{
			IsMoving=false;
			_anim.SetBool("IsMoving", false);
		}

		if (Input.GetKey(KeyCode.LeftShift) && grounded && IsMoving == true)
		{
			_anim.SetBool("Run", true);
			Run();
		}else _anim.SetBool("Run", false);



		if (Mathf.Abs(body.velocity.x) > speed )
		{
			body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * speed, body.velocity.y);
		}
	}
	private void Run()
	{       

        body.AddForce(direction * body.mass * speed * acceleration * runSpeed);
		
	}
	private void GetJump() // перевірка чи є колайдер під ногами
	{
		grounded = Physics2D.Raycast(body.position, Vector3.down, distanceToGround,
					 groundMask);
	}
	private void Move()
	{
		direction = new Vector2(Input.GetAxis("Horizontal"), 0);
		float move = Input.GetAxis("Horizontal");
		//GetComponent<Rigidbody2D>().velocity = new Vector2(move * speed, GetComponent<Rigidbody2D>().velocity.y);
		transform.position += direction * speed * Time.deltaTime;
		//_isMoving = direction.x !=0 ? true : false;
		if (direction.x != 0 && grounded)
		{
			_spriteRenderer.flipX = direction.x > 0 ? true : false;
		}

	}
	//void Flip() // відобразити по горизонталі
	//{
	//	facingRight = !facingRight;
	//	Vector3 theScale = transform.localScale;
	//	theScale.x *= -1;
	//	transform.localScale = theScale;
	//}
	private void Jump()
	{
		_anim.SetTrigger("Jump");
		
		body.velocity = new Vector2(0, jumpForce);
	}
	
	void Update()
	{
		RestoreHealth();
		RestoreEnergy();
		healthBar.SetHealth(curHealth);
		energyBar.SetEnergy(curEnergy);
		
		//Debug.DrawRay(transform.position, Vector3.down * jumpDistance, Color.red); // підсвітка, для візуального налаштування jumpDistance
		GetJump();
		if (Input.GetKeyDown(jumpButton) && grounded)
        {			
			Jump();
			
		}
		IsFlying();



		//if (Input.GetKeyDown(KeyCode.F) && grounded)
		//      {
		//	body.velocity = new Vector2(0, jumpForce);
		//	_anim.SetTrigger("JumpF");
		//      }
		//if (Input.GetKeyDown(KeyCode.E))
		//{
		//	UseEnergy(10);
		//}



		//float h = Input.GetAxis("Horizontal");
		//direction = new Vector2(h, 0);
		//float h = Input.GetAxis("Horizontal");
		//direction = new Vector2(h, 0);
		//if (h > 0 && !facingRight) Flip(); else if (h < 0 && facingRight) Flip();
		//healthBar.SetHealth(curHealth);
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "currentRooom")
        {
            currentRoom = rooms.IndexOf(collision.gameObject);
           // UnityEngine.Debug.Log(currentRoom);
        }
    }

    private bool IsFlying()
	{
		if (body.velocity.y < 0 && grounded== false)
		{
			_anim.SetBool("IsFlying", true);
			//_anim.SetBool("Run", false);
			return true;
		}
		else
		{
			_anim.SetBool("IsFlying", false);
			return false;
		}
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

	public void UseEnergy(int energy)
	{
		if (curHealth > 0 && curEnergy > 0 && curEnergy >= energy)
			curEnergy -= energy;
		else
			curEnergy = 0;
	}

	void RestoreEnergy()
	{
		if (curEnergy >= 0 && curEnergy < maxEnergy)
		{
			curEnergy += Time.deltaTime * restoreEnergyCount;
		}
	}

	public void AddExp(float exp)
	{
		curExp += exp;
	}

    public void HelpPontiff()
    {
        isPontiffHelped = true;
    }


    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //       if (collision.gameObject.tag == "Spike")
    //       {
    //           DamagePlayer(25);
    //           UnityEngine.Debug.Log("Damage from spike!");
    //       }
    //       if (collision.gameObject.tag == "Blade")
    //       {
    //           DamagePlayer(25);
    //           UnityEngine.Debug.Log("Damage from blade!");
    //       }
    //   }

}
