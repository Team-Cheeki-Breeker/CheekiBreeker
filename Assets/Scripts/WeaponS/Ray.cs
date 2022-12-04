using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ray : MonoBehaviour
{


    public float distance;
    public LayerMask solidMask;
    public float lifeTime;
    public float speed;
    public int damage;
    public GameObject collisionEffect;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0f, 0f, -90f);
        Invoke("DestroyProjectile", lifeTime);
        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(transform.position, transform.up, distance, solidMask);

        if(hits.Length > 0)
        {
            foreach(RaycastHit2D hitInfo in hits)
            {
                if (hitInfo.collider.CompareTag("Enemy"))
                    {
                        hitInfo.collider.GetComponent<HealthController>().takeDamage(damage);
                    }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        transform.localScale += Vector3.up * speed * Time.deltaTime;
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

}
