using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHandler : MonoBehaviour
{
    private MovementMotor movementMotor;        //Movement Motor
    private bool CanMove;                       //Player can move with user input 
    public float KnockbackTime;                 //How long the player is unable to move, when hit
    public float KnockbackForce;                //How strong the knockback is
    public Vector2 KnockbackDirection;          //Direction, where the player is thrown

    /**
     * Start function of handler
     */
    void Start()
    {
        movementMotor = GetComponent<MovementMotor>();
        CanMove = true;
    }

    /**
     * FixedUpdate function of handler
     * Takes input of the user to move the player if the player can move
     */
    private void FixedUpdate()
    {
        if (CanMove)
        {
            movementMotor.Move(Input.GetAxisRaw("Horizontal"));
        }
    }

    /**
     * Update function of handler
     * Takes input of the user to make the player jump if the player can move
     */
    void Update()
    {
        if (CanMove)
        {
            if (Input.GetButtonDown("Jump"))
            {
                movementMotor.StartJump();
            }
            if (Input.GetButton("Jump"))
            {
                movementMotor.KeepJump();
            }
            if (Input.GetButtonUp("Jump"))
            {
                movementMotor.StopJump();
            }
        }
    }

    /**
     * Stops, starts player movement for all player input
     * @param bool canMove
     */
    public void ToggleMove(bool canMove)
    {
        CanMove = canMove;
    }

    public IEnumerator TurnOffMovement(float seconds)
    {
        ToggleMove(false);
        yield return new WaitForSeconds(seconds);
        ToggleMove(true);
    }

    /**
     * Knocks back the player, takes away movement for the set time
     * Direction depends on the object the player collided with
     * @param Transform colider
     */
    public IEnumerator Knockback(Transform colider)
    {
        ToggleMove(false);
        Vector2 direction;
        if(colider.position.x < transform.position.x)
        {
            direction = new Vector2(Mathf.Abs(KnockbackDirection.x), KnockbackDirection.y);
        }
        else
        {
            direction = new Vector2(-Mathf.Abs(KnockbackDirection.x), KnockbackDirection.y);
        }
        movementMotor.ThrowPlayer(direction, KnockbackForce);
        yield return new WaitForSeconds(KnockbackTime);
        ToggleMove(true);
    }
}
