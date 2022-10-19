using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Crosshair : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
         Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         transform.position = MousePos;
    }
}
