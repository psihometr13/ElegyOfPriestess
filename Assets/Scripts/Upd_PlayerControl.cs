using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine.UI;
using System;
using System.IO;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(Rigidbody2D))]

public class Upd_PlayerControl : MonoBehaviour
{
	//private Animations _animations;
	//private bool _isMoving;

	[SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private Text HP;
    [SerializeField] private Text Mana;
    public static Upd_PlayerControl Instance { get; private set; }
	//Move
	public float speed = 4; // швидкість руху
	public float acceleration = 1; // прискорення
	public float runSpeed = 0.8F; // run
	public float jumpForce = 15; // сила стрибка
	public float jumpDistance = 0.75f; // відстань від центру об'єкта до поверхні
	public bool facingRight = false; // в яку сторону дивиться
	public KeyCode jumpButton = KeyCode.Space; // кнопка для стрибка
	[SerializeField] GameObject spawnPoint;

	public Transform checkPosition;
	public Vector2 checkSize;
	public LayerMask Ground;

	public bool isRingUsed = false;
	public bool isRosaryUsed = false;

    [SerializeField] List<GameObject> rooms;
	public int currentRoom;

	private Vector3 direction;
	public static Rigidbody2D body;
	public static CapsuleCollider2D capsuleCollider;

	[Header("Save")]
	public Image Save1;
	public Image Save2;
	public Image Save3;
	public GameObject star;
	public GameObject weapon;
    public GameObject doorTeleport;
	public GameObject sister;
	public bool gameSaved;
    GameObject finalDoor;

    //Health
    [Header("Health Parameters")]
	[SerializeField] public float curHealth = 0;
	[SerializeField] public float maxHealth = 100;
	public HealthBar healthBar;
	private bool isDead = true;
	[SerializeField] private int restoreHealthCount = 6;
	[SerializeField] private int restoreHealthEnergyUsage = 50;
	[SerializeField] public float maxDamage = 100;

	//Energy
	[Header("Energy Parameters")]
	[SerializeField] public float curEnergy = 0;
	[SerializeField] public float maxEnergy = 100;
	public EnergyBar energyBar;
	[SerializeField] private int restoreEnergyCount = 6;
    
    //Achimevents
    public int countOfDeaths = 0;
	public int countOfNotes = 0;
	public int countOfKilledBosses = 0;
	public int countOfVisitedLoc = 0;
	public int countOfUsedHeals = 0;
	public int countOfMagic = 0;
	public int countOfChests = 0;
	public int countOfDeathsTotal = 0;
	public bool gameCompleted = false;
	public int secretRoom1 = 0;
	public int secretRoom2 = 0;

	//Exp
	[SerializeField] public float curExp = 0;
	[SerializeField] public float maxExp = 100;

	//Final
	public bool isPontiffHelped = false;

	//Attack
	[Header("Attack Parameters")]
	[SerializeField] private float colliderDistance;
	[SerializeField] private CapsuleCollider2D boxCollider;
	private float cooldownTimer = 0;

	[SerializeField] public Transform firepoint;
	[SerializeField] private GameObject fireball1;
	[SerializeField] private GameObject fireball2;
	[SerializeField] private GameObject fireball3;

	[SerializeField] private float range;
	[SerializeField] private float attackCooldown;
	[SerializeField] private int damage;


	public Animator _anim;
	public Animator _anim2;
	public LayerMask groundMask;
	private bool grounded;
	public bool IsMoving;
	public float distanceToGround = 3;
    public RuntimeAnimatorController anim1;
    public RuntimeAnimatorController anim2;
    public RuntimeAnimatorController anim3;
	bool newAtck = false;

    [SerializeField] Image energyDebuff;
    [SerializeField] Image healthDebuff;

    [Header("Music parameters")]
    public AudioClip locationMusic;
    public AudioClip SisterMusic;
    public AudioClip PontiffMusic;
    public AudioClip MobsMusic;
    void Start()
	{
		

        gameSaved = false;
        finalDoor = GameObject.Find("finalDoor");
        UnityEngine.Debug.Log(countOfDeaths);
        GameObject.Find("SpawnPoint"); //spawn
        healthDebuff.enabled = false;
        energyDebuff.enabled = false;
        //_animations = GetComponentInChildren<Animations>();
        body = GetComponent<Rigidbody2D>();
		_anim = GetComponent<Animator>();
		_anim2 = GetComponent<Animator>();
		body.freezeRotation = true;
		curHealth = maxHealth;
		curEnergy = maxEnergy;

        if (MenuController.Instance.newGame == false && 
			File.Exists(Path.Combine(@"C:\Users\Beebo\AppData\LocalLow\Dream\ElegyOfPriestess\", "Profile.bin")))
        LoadGame(); 
		star.SetActive(true);
        star.transform.position = gameObject.transform.position;
        spawnPoint.transform.position = gameObject.transform.position;
		if(isRingUsed) GameObject.FindGameObjectsWithTag("Bathilda's-ring")[0].SetActive(false);
		if(isRosaryUsed) GameObject.FindGameObjectsWithTag("Rosary")[0].SetActive(false);
        if (!SaveSystem.GetBool("doorOpened"))
        {
            finalDoor.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
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
    IEnumerator MoveAfterAttack()
    {
        yield return new WaitForSeconds(0.5f);
        speed = 4;

    }
    IEnumerator Waitfor()
    {
        yield return new WaitForSeconds(3f);
		speed= 4;

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
			IsMoving = false;
			_anim.SetBool("IsMoving", false);
		}

		if (Input.GetKey(KeyCode.LeftShift) && grounded && IsMoving == true)
		{
			_anim.SetBool("Run", true);
			Run();
		}
		else _anim.SetBool("Run", false);

		if (Mathf.Abs(body.velocity.x) > speed)
		{
			body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * speed, body.velocity.y);
		}
       
        if (Input.GetKey(KeyCode.Q))
        {
            speed = 0;
            _anim.SetTrigger("Heal");
            StartCoroutine("MoveAfterAttack");
        }
    }
	private void Run()
	{

		body.AddForce(direction * body.mass * speed * acceleration * runSpeed);

	}
	private void GetJump() // перевірка чи є колайдер під ногами
	{
		//grounded = Physics2D.Raycast(body.position, Vector3.down, distanceToGround,
		//			 groundMask);
		grounded = Physics2D.OverlapBox(checkPosition.position, checkSize, 0, Ground);

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
        UnityEngine.Debug.Log(SaveSystem.GetBool("doorOpened"));
        //UnityEngine.Debug.Log(countOfMagic);
        countOfVisitedLoc = secretRoom1 + secretRoom2;
		//UnityEngine.Debug.Log(currentRoom);
        //UnityEngine.Debug.Log(countOfVisitedLoc);
        //UnityEngine.Debug.Log(fireball3.GetComponent<EnemyProjectile>().FindClosestEnemy().name);
        int HPVisible = (int)Math.Round(curHealth);
		int ManaVisible = (int)Math.Round(curEnergy);

		HP.text = HPVisible.ToString() + "/" + maxHealth.ToString();
		Mana.text = ManaVisible.ToString() + "/" + maxEnergy.ToString();
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

		cooldownTimer += Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.Alpha7))
		{
			if (cooldownTimer >= attackCooldown){
                speed = 0;
                _anim.SetTrigger("Attack");
                Attack(fireball1, 10);
                StartCoroutine("MoveAfterAttack");
            }
			
		}
		if(newAtck){
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                if (cooldownTimer >= attackCooldown)
                {
                    speed = 0;
                    _anim.SetTrigger("Attack");
                    Attack(fireball2, 30);
                    StartCoroutine("MoveAfterAttack");
					countOfMagic = 2;
                }
            }
        }

		if (Input.GetKeyDown(KeyCode.L))
		{
			if (Save1.enabled && Save2.enabled && Save3.enabled)
			{
                SaveGame();
				showStar();
            }
		}
	
		void showStar()
        {
            star.SetActive(true);
            star.transform.position = gameObject.transform.position;
        }
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

	void StrongAttack(GameObject fireball, int energy)
	{
        if (curEnergy >= energy && cooldownTimer >= attackCooldown)
        {
            UseEnergy(energy);
            //fireball.transform.position = new Vector3(fireball.GetComponent<EnemyProjectile>().FindClosestEnemy().transform.position.x, fireball.GetComponent<EnemyProjectile>().FindClosestEnemy().transform.position.y, 0);
            fireball.SetActive(true);
            fireball.GetComponent<EnemyProjectile>().ActivateProjectile();
            cooldownTimer = 0;
        }
    }

    void Attack(GameObject fireball, int energy)
    {
        if (curEnergy >= energy && cooldownTimer >= attackCooldown)
        {
            UseEnergy(energy);
            fireball.transform.position = firepoint.position;
            fireball.GetComponent<EnemyProjectile>().ActivateProjectile();
            cooldownTimer = 0;
        }
        //GameObject.FindGameObjectsWithTag("Boss")[0].GetComponent<BossHealth>().TakeDamage(energy);
    }

    //private int FindFireball()
    //{
    //    for (int i = 0; i < fireballs.Length; i++)
    //    {
    //        if (!fireballs[i].activeInHierarchy)
    //            return i;
    //    }
    //    return 0;
    //}

    private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "currentRooom")
		{
			currentRoom = rooms.IndexOf(collision.gameObject);
			// UnityEngine.Debug.Log(currentRoom);
			if (currentRoom == 7) 
			{
				secretRoom1 = 1;
			}
            else if(currentRoom == 9)
            {
                secretRoom2 = 1;
            }
        }

        float damagePerSecond = 20;

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

	private bool IsFlying()
	{
		if (body.velocity.y < 0 && grounded == false)
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

	public void ChangeHealth(float health)
	{
        healthDebuff.enabled = true;

        maxHealth -= health;
		if(maxHealth < curHealth)
		{
			curHealth = maxHealth;
		}
		if(curHealth <= 0) DiePlayer();
    }
	
	public void ChangeEnergy(float energy)
	{
        energyDebuff.enabled = true;

        maxEnergy -= energy;
		if(maxEnergy < curEnergy)
		{
            curEnergy = maxEnergy;
		}
    }

	public void DamagePlayer(float damage)
	{
		if (curHealth > 0 && curHealth >= damage)
		{
			curHealth -= damage;
			_anim.SetTrigger("Hit");
		}
		else
		{
			DiePlayer();
		}
	}

	public void ResetStates()
	{
        healthDebuff.enabled = false;
        energyDebuff.enabled = false;
        maxHealth = 100;
        maxEnergy = 100;
        countOfDeaths += 1;
        countOfDeathsTotal += 1;
    }
    public void gameComplete()
    {
        gameCompleted = true;
    }
    public void DiePlayer()
	{
		ResetStates();
		curHealth = 0;
		isDead = true;
		this.transform.position = spawnPoint.transform.position;
		curHealth = maxHealth;
		curEnergy = maxEnergy;
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

    public void LoadGame()
    {
		Save1.enabled = false;
		Save2.enabled = false;
		Save3.enabled = false;


		if (SaveSystem.GetBool("issister"))
		{
			Destroy(sister);
           // this.GetComponent<Animator>().runtimeAnimatorController = anim3 as RuntimeAnimatorController;
        }
		if (SaveSystem.GetBool("withWeapon"))
		{
            doorTeleport.gameObject.GetComponent<BoxCollider2D>().enabled = true;
            this.GetComponent<Animator>().runtimeAnimatorController = anim2 as RuntimeAnimatorController;
			Destroy(weapon);
           
        }
        if (SaveSystem.GetBool("NewAttack"))
        {
            newAtck = true;
        }
		else
		{
			newAtck = false;
		}
        //General
        transform.position = SaveSystem.GetVector2("PlayerPosition");
        curHealth = SaveSystem.GetFloat("CurrentHealth");
        curEnergy = SaveSystem.GetFloat("CurrentEnergy");
        curExp = SaveSystem.GetFloat("CurrentExp");
        maxDamage = SaveSystem.GetFloat("MaxDamage");
        maxHealth = SaveSystem.GetFloat("MaxHealth");
        maxEnergy = SaveSystem.GetFloat("MaxEnergy");
		isRingUsed = SaveSystem.GetBool("isRingUsed");
		isRosaryUsed = SaveSystem.GetBool("isRosaryUsed");

        //Achimenents
        countOfDeaths = SaveSystem.GetInt("countOfDeaths");
        countOfNotes = SaveSystem.GetInt("countOfNotes");
        countOfKilledBosses = SaveSystem.GetInt("countOfKilledBosses");
        countOfVisitedLoc = SaveSystem.GetInt("countOfVisitedLoc");
        countOfUsedHeals = SaveSystem.GetInt("countOfUsedHeals");
        countOfMagic = SaveSystem.GetInt("countOfMagic");
        countOfChests = SaveSystem.GetInt("countOfChests");
        countOfDeathsTotal = SaveSystem.GetInt("countOfDeathsTotal");
        gameCompleted = SaveSystem.GetBool("gameCompleted");
        secretRoom1 = SaveSystem.GetInt("secretRoom1");
        secretRoom2 = SaveSystem.GetInt("secretRoom2");
    }

    public void SaveGame()
	{
		gameSaved = true;
		Save1.enabled = false;
		Save2.enabled = false;
		Save3.enabled = false;
		
		countOfDeaths = 0;

        if (newAtck)
		{
            SaveSystem.SetBool("NewAttack", true);
        }
		if (gameSaved)
		{
            SaveSystem.SetBool("withWeapon", true);
        }
		////General
		SaveSystem.SetVector2("PlayerPosition", transform.position);
		SaveSystem.SetFloat("CurrentHealth", curHealth);
		SaveSystem.SetFloat("CurrentEnergy", curEnergy);
		SaveSystem.SetFloat("CurrentExp", curExp);
		SaveSystem.SetFloat("MaxDamage", maxDamage);
		SaveSystem.SetFloat("MaxHealth", maxHealth);
		SaveSystem.SetFloat("MaxEnergy", maxEnergy);
		SaveSystem.SetBool("isRingUsed", isRingUsed);
		SaveSystem.SetBool("isRosaryUsed", isRosaryUsed);

        //Achimenents
        SaveSystem.SetInt("countOfDeaths", countOfDeaths);
		SaveSystem.SetInt("countOfNotes", countOfNotes);
		SaveSystem.SetInt("countOfKilledBosses", countOfKilledBosses);
		SaveSystem.SetInt("countOfVisitedLoc", countOfVisitedLoc);
		SaveSystem.SetInt("countOfUsedHeals", countOfUsedHeals);
		SaveSystem.SetInt("countOfMagic", countOfMagic);
		SaveSystem.SetInt("countOfChests", countOfChests);
		SaveSystem.SetInt("countOfDeathsTotal", countOfDeathsTotal);
		SaveSystem.SetBool("gameCompleted", gameCompleted);
        SaveSystem.GetInt("secretRoom1", secretRoom1 );
        SaveSystem.GetInt("secretRoom2", secretRoom2);
        SaveSystem.SaveToDisk();
	}

    private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "currentRooom")
        {
            currentRoom = rooms.IndexOf(collision.gameObject);
            //UnityEngine.Debug.Log(currentRoom);

            if (currentRoom == 9) SoundManager.Instance.PlayMusic(MobsMusic);
            else if (currentRoom == 2) SoundManager.Instance.PlayMusic(PontiffMusic);
            else if (currentRoom == 5) SoundManager.Instance.PlayMusic(SisterMusic);
            else SoundManager.Instance.PlayMusic(locationMusic);
        }
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
            SaveSystem.SetBool("isRosaryUsed", isRosaryUsed);
            UnityEngine.Debug.Log("Rosary used");
			maxHealth = maxHealth+25;
			isRosaryUsed = true;
			countOfChests += 1;
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
			maxEnergy = maxEnergy + 25;
            isRingUsed = true;
            countOfChests += 1;
            Destroy(collision.gameObject);
		}
        if (collision.gameObject.tag == "NewAttack")
        {
			newAtck = true;
            Destroy(collision.gameObject);
			
        }
        if (collision.CompareTag("Sister"))
        {
            _anim.SetTrigger("Clear");
            speed = 0;
            StartCoroutine("Waitfor");
        }
        if (collision.CompareTag("hair"))
        {
            this.GetComponent<Animator>().runtimeAnimatorController = anim3 as RuntimeAnimatorController;
        }
       
    }

}
