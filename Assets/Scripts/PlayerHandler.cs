using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandler : MonoBehaviour
{
    public float health;            //current health of the player
    public float maxHealth;         //maximum health of the player
    private MovementHandler movementHandler;

    public GameObject WeaponsHandler;
    public Image HpBar;
    public TextMeshProUGUI HpText;
    public TextMeshProUGUI AmmoText;

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

    public void Update()
    {
        Transform firstActiveWeapon = WeaponsHandler.transform.GetChild(1); ;

        for (int i = 0; i < WeaponsHandler.transform.childCount; i++)
        {
            if (WeaponsHandler.transform.GetChild(i).gameObject.activeSelf == true)
            {
                firstActiveWeapon = WeaponsHandler.transform.GetChild(i);
            }
        }

        AmmoText.text = (firstActiveWeapon.GetComponent<WeaponScript>().isRealoading() ? "Pizdéc" : firstActiveWeapon.GetComponent<WeaponScript>().CurrentMagazine + 1)  
                         + " / " + firstActiveWeapon.GetComponent<WeaponScript>().startMag;
        HpText.text = health + " / " + maxHealth;

    }

    /**
     * reduces the player's health
     * @param float damage
     */
    public void TakeDamage(float damage, Transform collider = null)
    {
        health -= damage;
        HpBar.fillAmount = health / maxHealth;

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
