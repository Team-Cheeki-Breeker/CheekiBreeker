using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravy : MonoBehaviour
{
    [SerializeField] private Collider2D center;
    [SerializeField] private Collider2D outside;
    private bool InOutside = false;

    private void Update()
    {
        if (InOutside)
        {
            Debug.Log("Behúzás");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (center.IsTouching(collision))
        {
            Debug.Log("Kilövés");
            InOutside = false;
            //Debugra
            outside.enabled = false;
        }

        if (outside.IsTouching(collision))
        {
            InOutside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debugra
        outside.enabled = true;

        InOutside = false;
    }
}
