using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patroler : MonoBehaviour
{
    public float speed;

    public int positionOfPatrol; //відстань патрулювання
    public Transform point;
    bool movingRight;

    Transform player;
    public float stoppingDistance;

    bool chill = false;
    bool chase = false;
    bool goBack = false;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerControl.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, point.position) < positionOfPatrol && chase == false)
        {
            chill = true;
        }
        if (Vector2.Distance(transform.position, player.position) < stoppingDistance)
        {
            chase = true;
            chill = false;
            goBack = false;
        }  
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            goBack = true;
            chase = false;
        }

        if(chill == true)
        {
            Chill();
        }
        else if (chase == true)
        {
            Chase();
        }
        else if (goBack == true)
        {
            GoBack();
        }
    }

    void Chill()
    {
        if (transform.position.x > point.position.x + positionOfPatrol)
        {
            movingRight = false;
        }
        else if (transform.position.x < point.position.x - positionOfPatrol)
        {
            movingRight = true;
        }

        if (movingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }

    void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        speed = 7;
    }

    void GoBack()
    {
        transform.position = Vector2.MoveTowards(transform.position, point.position, speed * Time.deltaTime);
    }
}
