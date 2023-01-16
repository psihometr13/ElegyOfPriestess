using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "data", menuName = "data/enemy")]
public class EnemyData : ScriptableObject
{
    [Header("Type of enemy")]
    public bool rooted;
    public bool mosquito;
    public bool demon;
    public bool spirit;

    [Header("Stats")]
    public float maxHealth;
    public float maxDamage;
    public float attackCooldown;


}
