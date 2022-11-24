using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthController : MonoBehaviour
{

    private int Hp;
    public int HpMax;
    private int HpModified;
    public GameObject hpBar;
    public Animator animator;
    private Vector3 initialscale;

    public void Start()
    {
        float diffMod = PlayerPrefs.GetFloat("DifficultyValue");
        HpModified = HpMax + (int)Math.Round(diffMod*diffMod * 100);
        Hp = HpModified;
        initialscale = hpBar.transform.localScale;
    }

    public void takeDamage(int dmg)
    {
        Hp -= dmg;
        if(Hp <= 0)
        {
           hpBar.transform.localScale = new Vector3((0 * initialscale.x), initialscale.y, initialscale.z);
        } else
        {
           hpBar.transform.localScale = new Vector3((float)((1.0 * Hp / HpModified) * initialscale.x),initialscale.y,initialscale.z) ;
        }
    }

    public void Update()
    {
        if(Hp <= 0)
        {
            animator.SetTrigger("died");
        }
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
