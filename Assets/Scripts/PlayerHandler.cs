using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerHandler : MonoBehaviour
{
    public float health;            //current health of the player
    public float maxHealth;
    private float ModifiedMaxHP; //maximum health of the player
    private MovementHandler movementHandler;
    public AudioClip[] damageYells;
    public AudioClip[] shootingYells;
    public AudioSource audioSource;

    public GameObject WeaponsHandler;
    public Image HpBar;
    public Image RELOADING_WARN;
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
        audioSource.loop = false;
        float diffMod = PlayerPrefs.GetFloat("DifficultyValue");
        ModifiedMaxHP = maxHealth - (int)Math.Round(diffMod * diffMod * 200);
        health = ModifiedMaxHP;
    }

    public void Update()
    {
        Transform firstActiveWeapon = WeaponsHandler.transform.GetChild(1); 

        for (int i = 0; i < WeaponsHandler.transform.childCount; i++)
        {
            if (WeaponsHandler.transform.GetChild(i).gameObject.activeSelf == true)
            {
                firstActiveWeapon = WeaponsHandler.transform.GetChild(i);
            }
        }

        firstActiveWeapon.GetComponent<WeaponScript>().onShooting -= yellShooting;

        firstActiveWeapon.GetComponent<WeaponScript>().onShooting += yellShooting;

        if (firstActiveWeapon.GetComponent<WeaponScript>().isRealoading())
        {
            RELOADING_WARN.enabled = true;
        } else
        {
            RELOADING_WARN.enabled = false;
        }

        AmmoText.text = (firstActiveWeapon.GetComponent<WeaponScript>().isRealoading() ? 0 : firstActiveWeapon.GetComponent<WeaponScript>().CurrentMagazine + 1)  
                         + " / " + firstActiveWeapon.GetComponent<WeaponScript>().startMag;
        HpText.text = health + " / " + ModifiedMaxHP;
    }

    /**
     * reduces the player's health
     * @param float damage
     */
    public void TakeDamage(float damage, Transform collider = null)
    {

        health -= damage;
        HpBar.fillAmount = health / ModifiedMaxHP;
        if (!audioSource.isPlaying && damage >= 10)
        {
            audioSource.PlayOneShot(damageYells[Random.Range(0, damageYells.Length)]);
        }
            

        if (collider != null)
        {
            StartCoroutine(movementHandler.Knockback(collider));
                    
        }
        if(health <= 0)
        {
            //TODO: Death
        }
        
    }

    public void yellShooting()
    {
        if (!audioSource.isPlaying && Random.Range(0,7) >=6 )
        {
            audioSource.PlayOneShot(shootingYells[Random.Range(0, damageYells.Length)]);
        }
    }


    
}
