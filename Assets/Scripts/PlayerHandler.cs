using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public float health;            //current health of the player
    public float maxHealth;         //maximum health of the player
    private MovementHandler movementHandler;


    /**
     * Start function of handler
     * Loads area, where the player saved
     * sets the player's health to full
     */
    void Start()
    {
        movementHandler = GetComponent<MovementHandler>();

        health = maxHealth;

        
    }


    /**
     * reduces the player's health
     * @param float damage
     */
    public void TakeDamage(float damage, Transform collider = null)
    {
        health -= damage;
        if (collider != null)
        {
            StartCoroutine(movementHandler.Knockback(collider));
           
        }
        if(health <= 0)
        {
            //TODO: Death
        }
        
    }


    
}
