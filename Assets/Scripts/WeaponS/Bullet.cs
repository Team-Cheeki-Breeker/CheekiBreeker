using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed;
    public float distance;
    public LayerMask solidMask;
    public int damage;
    public GameObject collisionEffect;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0f, 0f, -90f);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, solidMask);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<HealthController>().takeDamage(damage);
            }
            DestroyProjectile();
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        //Instantiate(collisionEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
