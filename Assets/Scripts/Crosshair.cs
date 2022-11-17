using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Crosshair : MonoBehaviour
{
    private GameObject player;
    private enum Crosshairside { Left, Right };
    private Crosshairside side = Crosshairside.Right;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x >= transform.position.x && side == Crosshairside.Right)
        {
            player.GetComponent<MovementMotor>().FlipCharacter();
            side = Crosshairside.Left;
        }
        if (player.transform.position.x < transform.position.x && side == Crosshairside.Left)
        {
            player.GetComponent<MovementMotor>().FlipCharacter();
            side = Crosshairside.Right;
        }
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = MousePos;

    }
}
