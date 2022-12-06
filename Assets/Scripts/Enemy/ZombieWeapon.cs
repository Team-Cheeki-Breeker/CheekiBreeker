using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieWeapon : MonoBehaviour
{

    public float Offset;//{ get; set; }
    public AudioClip fireCLip;
    public AudioClip reloadClip;
    private GameObject player;
   // private float spreadWindow;
    public Animator zombieAnimator;

   // public float spreadTimeDampen = 1000.0f;
   // public float spreadIncrease = 1.0f;
   // private const float MAX_SPREAD = 20.0f;
    public GameObject Bullet; //{ get; set; }
    public Transform BarrelPos; //{ get; set; }

    private float timeBtwShots;
    private float reloadCD;
    private int magazineCurr;

    public int startMag;
    public int alertScope;
    public float startTimeBtwShots;
    private int modifiedstartMag;
    public float startReloadTime;
    //public delegate void OnShooting();
    // public event OnShooting onShooting;
    

    public int CurrentMagazine { get => magazineCurr;  }

    public bool isRealoading()
    {
        return reloadCD > 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        float diffValue = PlayerPrefs.GetFloat("DifficultyValue");
        modifiedstartMag = startMag + (int)System.Math.Round(diffValue / 0.25f);
        player = GameObject.FindGameObjectWithTag("Player");
        magazineCurr = startMag;
        reloadCD = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!zombieAnimator.GetBool("died"))
        {

            //if (spreadWindow <= 0) spreadWindow = 0; else spreadWindow -= spreadTimeDampen * Time.deltaTime;
            Vector3 difference = player.transform.position - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + Offset);
        

            if (timeBtwShots <= 0 && reloadCD <= 0)
            {
            
                if (difference.magnitude <= alertScope)
                {
                    if(magazineCurr <= 0)
                    {
                        reloadCD = startReloadTime;
                        magazineCurr = modifiedstartMag;
                        //StartCoroutine(PlayReload(reloadCD * 1.0f - 0.4f));
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
        
    } 

    public void FireAway(/*float spread*/)
    {
        Instantiate(Bullet, BarrelPos.position, transform.rotation /* Quaternion.Euler(0f, 0f, Random.Range(-spread * 1.5f, spread*0.5f))*/);
        //onShooting?.Invoke();    
    }
}
