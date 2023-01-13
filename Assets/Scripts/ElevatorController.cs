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
        if (transform.position.y <= bottomPoint.position.y)
        {
            isMovingDown = false;
        }
        else if (transform.position.y >= topPoint.position.y)
        {
            isMovingDown = true;
        }
        //Debug.Log(isMovingDown);

    }

    void Update()
    {
        CheckPosition();
        if (isMovingDown == true)
        {

                if (transform.position != bottomPoint.position)
                {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, (float)(bottomPoint.position.y)), speed * Time.deltaTime);
                }
        }
        else
        {
        if (isMovingDown == false)
            {
             if (transform.position != topPoint.position)
                {
                    transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, (float)(topPoint.position.y)), speed * Time.deltaTime);
                    
                }
            }
        }
        
    }

    private void FixedUpdate()
    {

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
