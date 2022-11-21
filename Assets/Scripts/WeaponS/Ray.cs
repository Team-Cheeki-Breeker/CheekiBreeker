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
    private GameObject damager;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0f, 0f, -90f);
        //Instantiate(damager, transform.position, transform.rotation);
        Invoke("DestroyProjectile", lifeTime);
    }

    // Update is called once per frame
    void Update()
    {

        /*RaycastHit2D hitInfo = Physics2D.Raycast(damager.transform.position, damager.transform.up, distance, solidMask);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.GetComponent<HealthController>().takeDamage(damage);
            }
        }
        //damager.transform.Translate(Vector2.up * speed * Time.deltaTime);*/
        transform.localScale += Vector3.up * speed * Time.deltaTime;
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

}
