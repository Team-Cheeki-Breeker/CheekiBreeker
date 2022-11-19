using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class radation : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float attackCooldown;
    private bool PlayerInTrigger = false;
    private float cooldownTimer = Mathf.Infinity;
    private Collider2D collision = null;

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (PlayerInTrigger)
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                DamagePlayer(collision);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerInTrigger = true;
            this.collision = collision;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerInTrigger = false;
            collision = null;
        }
    }

    private void DamagePlayer(Collider2D collision)
    {
        Debug.Log("Damage");
        collision.GetComponent<PlayerHandler>().TakeDamage(damage);
    }
}
