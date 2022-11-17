using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public GameObject Bullet; //{ get; set; }
    public int extraShots = 5;
    public int spread = 10;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < extraShots; i++)
        {
            
            Instantiate(Bullet, transform.position, transform.rotation * Quaternion.Euler(0f,0f, Random.Range(-spread,spread)));
        }   
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
