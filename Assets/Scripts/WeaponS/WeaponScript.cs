using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{

    public float Offset;//{ get; set; }

    public GameObject Bullet; //{ get; set; }
    public Transform BarrelPos; //{ get; set; }

    private float timeBtwShots;
    private float reloadCD;
    private int magazineCurr;

    public int startMag;
    public float startTimeBtwShots;
    public float startReloadTime;

    public int CurrentMagazine { get => magazineCurr;  }

    public bool isRealoading()
    {
        return reloadCD > 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        magazineCurr = startMag;
        reloadCD = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + Offset);
        

        if (timeBtwShots <= 0 && reloadCD <= 0)
        {
            
            if (Input.GetMouseButton(0))
            {
                if(magazineCurr <= 0)
                {
                    reloadCD = startReloadTime;
                    magazineCurr = startMag;
                }
                Instantiate(Bullet, BarrelPos.position, transform.rotation);
                magazineCurr -= 1;
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
            reloadCD -= Time.deltaTime;
        }
        
    }
}
