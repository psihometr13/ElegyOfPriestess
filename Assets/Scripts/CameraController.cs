using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        //слідувати за гравцем
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
    }
}
