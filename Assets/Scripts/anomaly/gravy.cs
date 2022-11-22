using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravy : MonoBehaviour
{
    [SerializeField] private Collider2D center;
    [SerializeField] private Collider2D outside;
    [SerializeField] private int PullForce;
    [SerializeField] private int ThrowForce;
    [SerializeField] private int CenterTime;
    [SerializeField] private int BarrierResetTime;
    [SerializeField] private int Damage;
    private bool WaitingForThrow = false;
    private bool InCenter = false;
    private void Update()
    {
       
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        MovementHandler movementHandler = collision.gameObject.GetComponent<MovementHandler>();
        movementHandler.ToggleMove(false);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        MovementMotor playerMotor = collision.gameObject.GetComponent<MovementMotor>();
        if (center.IsTouching(collision))
        {
            Debug.Log("Kilövés");
            if (!InCenter)
            {
                StartCoroutine(Launch(collision));
            }
        }

        if (outside.IsTouching(collision)&& !WaitingForThrow)
        {
            Debug.Log("Behúzás");

            playerMotor.ThrowPlayer(transform.position - collision.transform.position, PullForce);
        }
    }

    private IEnumerator Launch(Collider2D collision)
    {
        InCenter = true;
        yield return new WaitForSeconds(CenterTime/100.0f);
        WaitingForThrow = true;
        collision.gameObject.GetComponent<MovementMotor>().ThrowPlayer(collision.transform.up, ThrowForce);
        collision.gameObject.GetComponent<PlayerHandler>().TakeDamage(Damage);
        collision.gameObject.GetComponent<MovementHandler>().ToggleMove(true);
        yield return new WaitForSeconds(BarrierResetTime/100.0f);
        InCenter = false;
        WaitingForThrow = false;
    }

    


}
