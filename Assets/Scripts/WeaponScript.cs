using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{

    public float Offset;//{ get; set; }

    public GameObject Bullet; //{ get; set; }
    public Transform BarrelPos; //{ get; set; }

    private float timeBtwShots;
    public float startTimeBtwShots;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + Offset);

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                
                //Instantiate(shotEffect, shotPoint.position, Quaternion.identity);
                //camAnim.SetTrigger("shake");
                Instantiate(Bullet, BarrelPos.position, transform.rotation);                
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
        
    }
}
