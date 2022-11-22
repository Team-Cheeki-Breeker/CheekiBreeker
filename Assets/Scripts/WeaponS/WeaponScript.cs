using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{

    public float Offset;//{ get; set; }
    public AudioClip fireCLip;
    public AudioClip reloadClip;
   // private float spreadWindow;
   

   // public float spreadTimeDampen = 1000.0f;
   // public float spreadIncrease = 1.0f;
   // private const float MAX_SPREAD = 20.0f;
    public GameObject Bullet; //{ get; set; }
    public Transform BarrelPos; //{ get; set; }

    private float timeBtwShots;
    private float reloadCD;
    private int magazineCurr;

    public int startMag;
    public float startTimeBtwShots;
    public float startReloadTime;
    public delegate void OnShooting();
    public event OnShooting onShooting;
    

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
        //if (spreadWindow <= 0) spreadWindow = 0; else spreadWindow -= spreadTimeDampen * Time.deltaTime;
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
                    StartCoroutine(PlayReload(reloadCD * 1.0f - 0.4f));
                    //spreadWindow = 0;
                }
                FireAway(/*spreadWindow*/);
                //spreadWindow += spreadIncrease;  
                GetComponent<AudioSource>().PlayOneShot(fireCLip);
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

    public IEnumerator PlayReload(float delay)
    {
        yield return new WaitForSeconds(delay);
        GetComponent<AudioSource>().PlayOneShot(reloadClip);
    }

    public void FireAway(/*float spread*/)
    {
        Instantiate(Bullet, BarrelPos.position, transform.rotation /* Quaternion.Euler(0f, 0f, Random.Range(-spread * 1.5f, spread*0.5f))*/);
        onShooting?.Invoke();    
    }
}
