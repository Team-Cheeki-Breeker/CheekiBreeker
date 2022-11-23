using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float AttackCooldown;
    private float AttackTime = 0;
    [SerializeField] private int damage;
    private bool Attacked = false;
    // Update is called once per frame
    void Update()
    {
        if (Attacked)
        {
            AttackTime += Time.deltaTime;
            if (AttackTime >= AttackCooldown)
            {
                AttackTime = 0;
                Attacked = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemy") return;
        //if (!Attacked)
        //{
            collision.gameObject.GetComponent<HealthController>().takeDamage(damage);
            //Attacked = true;
        //}
    }
   
}
