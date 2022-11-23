using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class radation : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float attackCooldown;
    private float cooldownTimer = Mathf.Infinity;
    private bool CanDamage = false;
    private List<Collider2D> colliders = new List<Collider2D>();
    public AudioClip fireCLip;


    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (CanDamage)
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                Damage(colliders);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Enemy")
        {
            CanDamage = true;
            this.colliders.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Enemy")
        {
            CanDamage = false;
            this.colliders.Remove(collision);
        }
    }

    private void Damage(List<Collider2D> colliders)
    {
        foreach(Collider2D collision in colliders)
        {
            if(collision.tag == "Player")
            {
                collision.GetComponent<PlayerHandler>().TakeDamage(damage);
                if (!GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().PlayOneShot(fireCLip);
            }
            else if(collision.tag == "Enemy")
            {
                collision.GetComponent<HealthController>().takeDamage(damage);
            }
        }
    }
}
