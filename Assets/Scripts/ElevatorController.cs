using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    public float speed = 0.5f; //швидк≥сть
    public Transform bottomPoint = null, topPoint = null; //початкова ≥ к≥нцева зупинка

    private bool isMovingDown = true;

    void CheckPosition()
    {
        if (transform.position.y <= bottomPoint.position.y + 3)
        {
            isMovingDown = false;
        }
        else if (transform.position.y > topPoint.position.y + 2)
        {
            isMovingDown = true;
        }
    }

    void Update()
    {
        CheckPosition();
    }

    private void FixedUpdate()
    {
        if (isMovingDown == true)
        {
            if (bottomPoint != null)
            {
                if (transform.position != bottomPoint.position)
                {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, (float)(bottomPoint.position.y)), speed * Time.deltaTime);
                }
            }
        }
        else
        {
            if (topPoint != null)
            {
                if (transform.position != topPoint.position)
                {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, topPoint.position.y + 10), speed * Time.deltaTime);
                }
            }
        }
    }

    //робимо гравц€ доч≥рн≥м об'Їктом платформи, коли рухаЇмос€
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.parent = this.transform;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.parent = null;
    }
}
