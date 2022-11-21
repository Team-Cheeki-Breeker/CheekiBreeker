using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dynamo : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float attackCooldown;
    [SerializeField] private CircleCollider2D center;
    [SerializeField] private BoxCollider2D outside;
    public AudioClip fireCLip;
    private Animator animator;
    private List<Collider2D> colliders = new List<Collider2D>();

    private float cooldownTimer = Mathf.Infinity;
    private bool CanDamage = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (CanDamage)
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                Explode(colliders);
                if (colliders.Count <= 0)
                    CanDamage = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (center.IsTouching(collision) && collision.tag == "Bullet")
        {
            CanDamage = true;
        }

        if (outside.IsTouching(collision) && (collision.tag == "Player" || collision.tag == "Enemy"))
        {
            if (!colliders.Contains(collision))
            {
                colliders.Add(collision);
            }
            CanDamage = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "Enemy")
        {
            this.colliders.Remove(collision);
            CanDamage = false;
        }
    }

    private void Explode(List<Collider2D> colliders)
    {
        GetComponent<AudioSource>().PlayOneShot(fireCLip);
        animator.SetBool("Explode", true);
        Damage(colliders);
    }

    private void Damage(List<Collider2D> colliders)
    {
        foreach (Collider2D collision in colliders)
        {
            if (collision.tag == "Player")
            {
                collision.GetComponent<PlayerHandler>().TakeDamage(damage);
            }
            else if (collision.tag == "Enemy")
            {
                collision.GetComponent<HealthController>().takeDamage(damage);
            }
        }
    }

    public void AlertObservers(string message)
    {
        if (message.Equals("LightningAnimationEnded"))
        {
            animator.SetBool("Explode", false);
        }
    }
}
