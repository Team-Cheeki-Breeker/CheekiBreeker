using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : MonoBehaviour
{
    public float lifeTime;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        transform.Rotate(0f, 0f, -90f);
        Invoke("DestroyProjectile", lifeTime);
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
