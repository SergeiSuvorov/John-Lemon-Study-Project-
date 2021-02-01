using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health;
    public int HealthCount { get{ return health; } }
    [SerializeField] private int maxHealth;

    private void Start()
    {
        if (maxHealth == 0)
            maxHealth = health;
    }
    public void GetDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Dead();
    }

    public void GetAid(int healthAid)
    {
        if ((health + healthAid) < maxHealth)
            health += healthAid;
        else
            health = maxHealth;
    }

    private void Dead()
    {
        Destroy(gameObject);
    }
}
