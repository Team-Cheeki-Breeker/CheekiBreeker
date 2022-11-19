using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dynamo : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private CircleCollider2D center;
    [SerializeField] private BoxCollider2D outside;
    private Animator animator;

    private float cooldownTimer = Mathf.Infinity;
    private bool Usable = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (!Usable)
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                Usable = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (center.IsTouching(collision) && collision.tag == "Bulllet")
        {
            if (Usable)
            {
                Explode();
            }
        }

        if (outside.IsTouching(collision) && collision.tag == "Player")
        {
            if (Usable)
            {
                Explode();
            }
        }
    }

    private void Explode()
    {
        animator.SetBool("Explode", true);
        DamagePlayer();
    }

    private void DamagePlayer()
    {
        Debug.Log("Damage");
    }

    public void AlertObservers(string message)
    {
        if (message.Equals("LightningAnimationEnded"))
        {
            animator.SetBool("Explode", false);
        }
    }
}
