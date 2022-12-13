using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

	//// точки, между которыми бот будет двигаться, в ожидании игрока
	//public Transform waypointA;
	//public Transform waypointB;

	////player
	//public Transform _player;

	//public float speed = 1.5f; // скорость движения
	//public float acceleration = 10; // ускорение
	//public float searchDistance = 3; // с какого расстояния бот сможет "увидеть" игрока
	//public float checkDistance = 1; // расстояние с которого бот проверку перед собой, на наличие обрыва
	//public float resetDistance = 50; // макс дистанция, когда бот отслеживает позицию игрока
	//public float height = 2; // маск высота, для падения (например, если надо спуститься по ступеням за игроком)

	//// действие, когда позиция игрока не отслеживается (например, он ушел далеко вперед)
	//// возврат в стартовую позицию и отключение, отключение в текущей позиции, уничтожение объекта
	//public enum Mode { WaypointsAndDisabled = 0, Disabled = 1, Destroy = 2 };
	//public Mode action = Mode.WaypointsAndDisabled;

	//public bool facingRight = true; // бот смотрит вправо?

	//private int layerMask;
	//private bool isTarget, isWait;
	//private Rigidbody2D body;
	//private Vector3 direction;
	//private Vector3 startPosition;
	//private float curDist;

	//void Awake()
	//{
	//	layerMask = 1 << gameObject.layer | 1 << 2;
	//	layerMask = ~layerMask;
	//	body = GetComponent<Rigidbody2D>();
	//	body.freezeRotation = true;
	//	startPosition = transform.position;
	//}

	//void OnCollisionEnter2D(Collision2D coll)
	//{
	//	if (coll.transform.tag == "Player")
	//	{
	//		// физический контакт с целью
	//	}

	//}

	//Vector3 SetDirection(float xPos)
	//{
	//	return new Vector3(xPos, transform.position.y, transform.position.z) - transform.position;
	//}

	//void Walk() // зацикленное движение от А к В и обратно
	//{
	//	float a = Vector3.Distance(transform.position, waypointA.position);
	//	float b = Vector3.Distance(transform.position, waypointB.position);

	//	if (a < 1)
	//	{
	//		direction = SetDirection(waypointB.position.x);
	//	}
	//	else if (b < 1)
	//	{
	//		direction = SetDirection(waypointA.position.x);
	//	}
	//	else if (body.velocity.x == 0)
	//	{
	//		direction = SetDirection(waypointA.position.x);
	//	}
	//	else if (curDist > resetDistance)
	//	{
	//		Choose();
	//	}

	//	if (SearchPlayer()) isTarget = true;
	//}

	//void Follow() // преследование игрока
	//{
	//	if (!CheckPath() && curDist > checkDistance || body.velocity.magnitude == 0 && curDist > searchDistance)
	//	{
	//		direction = Vector3.zero;
	//		body.velocity = Vector3.zero;
	//		isWait = true;
	//	}
	//	else if (curDist > resetDistance)
	//	{
	//		Choose();
	//	}
	//	else
	//	{
	//		direction = SetDirection(_player.position.x);
	//	}
	//}

	//void Wait() // режим ожидания
	//{
	//	if (curDist < searchDistance)
	//	{
	//		isWait = false;
	//	}
	//	else if (curDist > resetDistance)
	//	{
	//		Choose();
	//	}
	//}

	//void Choose() // финальное действие
	//{
	//	switch (action)
	//	{
	//		case Mode.Disabled:
	//			isWait = false;
	//			isTarget = false;
	//			gameObject.SetActive(false);
	//			break;

	//		case Mode.WaypointsAndDisabled:
	//			transform.position = startPosition;
	//			isWait = false;
	//			isTarget = false;
	//			gameObject.SetActive(false);
	//			break;

	//		case Mode.Destroy:
	//			Destroy(gameObject);
	//			break;
	//	}
	//}

	//void LateUpdate()
	//{
	//	if (!waypointA || !waypointB) return;

	//	curDist = Vector3.Distance(_player.position, transform.position);

	//	if (!isTarget)
	//	{
	//		Walk();
	//	}
	//	else if (!isWait && isTarget)
	//	{
	//		Follow();
	//	}
	//	else if (isWait && isTarget)
	//	{
	//		Wait();
	//	}

	//	if (body.velocity.x > 0 && !facingRight) Flip();
	//	else if (body.velocity.x < 0 && facingRight) Flip();
	//}

	//bool CheckPath() // проверка поверхности на пути следования
	//{
	//	Vector3 pos = new Vector3(transform.position.x + checkDistance * Mathf.Sign(body.velocity.x), transform.position.y, transform.position.z);

	//	Debug.DrawRay(pos, Vector3.down * height, Color.red);

	//	RaycastHit2D hit = Physics2D.Raycast(pos, Vector3.down, Mathf.Infinity, layerMask);

	//	if (hit.collider && hit.distance < height)
	//	{
	//		return true;
	//	}

	//	return false;
	//}

	//bool SearchPlayer() // поиск игрока на пути следования
	//{
	//	Vector3 dir = Vector3.right * searchDistance * Mathf.Sign(body.velocity.x);

	//	Debug.DrawRay(transform.position, dir, Color.blue);

	//	RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, searchDistance, layerMask);

	//	if (hit.collider && hit.transform.tag == "Player")
	//	{
	//		return true;
	//	}

	//	return false;
	//}

	//void FixedUpdate()
	//{
	//	body.AddForce(direction.normalized * body.mass * speed * acceleration);

	//	if (Mathf.Abs(body.velocity.x) > speed)
	//	{
	//		body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * speed, body.velocity.y);
	//	}
	//}

	//void Flip() // отражение по горизонтали
	//{
	//	facingRight = !facingRight;
	//	Vector3 theScale = transform.localScale;
	//	theScale.x *= -1;
	//	transform.localScale = theScale;
	//}

	//public void GenerateWaypoints(GameObject point) // вспомогательная функция для создания вейпоинтов
	//{
	//	if (!waypointA && !waypointB)
	//	{
	//		GameObject obj = new GameObject(gameObject.name + "_Waypoints");
	//		obj.transform.position = transform.position;

	//		GameObject clone = Instantiate(point) as GameObject;
	//		clone.transform.parent = obj.transform;
	//		clone.transform.localPosition = new Vector2(3, 0);
	//		clone.name = "Point_A";
	//		waypointA = clone.transform;

	//		clone = Instantiate(point) as GameObject;
	//		clone.transform.parent = obj.transform;
	//		clone.transform.localPosition = new Vector2(-3, 0);
	//		clone.name = "Point_B";
	//		waypointB = clone.transform;
	//	}
	//}
}
