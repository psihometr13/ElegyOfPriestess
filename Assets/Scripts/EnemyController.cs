using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

	//// �����, ����� �������� ��� ����� ���������, � �������� ������
	//public Transform waypointA;
	//public Transform waypointB;

	////player
	//public Transform _player;

	//public float speed = 1.5f; // �������� ��������
	//public float acceleration = 10; // ���������
	//public float searchDistance = 3; // � ������ ���������� ��� ������ "�������" ������
	//public float checkDistance = 1; // ���������� � �������� ��� �������� ����� �����, �� ������� ������
	//public float resetDistance = 50; // ���� ���������, ����� ��� ����������� ������� ������
	//public float height = 2; // ���� ������, ��� ������� (��������, ���� ���� ���������� �� �������� �� �������)

	//// ��������, ����� ������� ������ �� ������������� (��������, �� ���� ������ ������)
	//// ������� � ��������� ������� � ����������, ���������� � ������� �������, ����������� �������
	//public enum Mode { WaypointsAndDisabled = 0, Disabled = 1, Destroy = 2 };
	//public Mode action = Mode.WaypointsAndDisabled;

	//public bool facingRight = true; // ��� ������� ������?

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
	//		// ���������� ������� � �����
	//	}

	//}

	//Vector3 SetDirection(float xPos)
	//{
	//	return new Vector3(xPos, transform.position.y, transform.position.z) - transform.position;
	//}

	//void Walk() // ����������� �������� �� � � � � �������
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

	//void Follow() // ������������� ������
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

	//void Wait() // ����� ��������
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

	//void Choose() // ��������� ��������
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

	//bool CheckPath() // �������� ����������� �� ���� ����������
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

	//bool SearchPlayer() // ����� ������ �� ���� ����������
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

	//void Flip() // ��������� �� �����������
	//{
	//	facingRight = !facingRight;
	//	Vector3 theScale = transform.localScale;
	//	theScale.x *= -1;
	//	transform.localScale = theScale;
	//}

	//public void GenerateWaypoints(GameObject point) // ��������������� ������� ��� �������� ����������
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
