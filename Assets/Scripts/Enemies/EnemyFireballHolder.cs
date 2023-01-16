using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireballHolder : MonoBehaviour
{
    [SerializeField] private Transform enemy;
    [SerializeField] private Transform firepoint;

    private void Update()
    {
        //transform.localScale = enemy.localScale;
        if (enemy.GetComponent<SpriteRenderer>().flipX)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            firepoint.position = new Vector3(enemy.position.x +2, enemy.position.y, enemy.position.z);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            firepoint.position = new Vector3(enemy.position.x -2, enemy.position.y, enemy.position.z);
        }
    }
}
