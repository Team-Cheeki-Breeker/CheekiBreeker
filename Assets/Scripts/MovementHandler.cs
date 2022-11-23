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
    private Animator animator;                  //Animator of object
    public float AttackCooldown;
    private float AttackCurrent = 0;
    [SerializeField] private List<Collider2D> SEnableCollider = new List<Collider2D>();
    [SerializeField] private List<Collider2D> SDisableCollider = new List<Collider2D>();
    [SerializeField] private List<Collider2D> WEnableCollider = new List<Collider2D>();
    [SerializeField] private List<Collider2D> WDisableCollider = new List<Collider2D>();

    /**
     * Start function of handler
     */
    void Start()
    {
        movementMotor = GetComponent<MovementMotor>();
        CanMove = true;
        animator = this.GetComponentInChildren<Animator>(); ;
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
            if (Input.GetButton("Fire2"))
            {
                Slash();
            }
            if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                foreach(var collider in SEnableCollider)
                {
                    collider.enabled = true;
                }
                foreach (var collider in SDisableCollider)
                {
                    collider.enabled = false;
                }
            }
            if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                foreach (var collider in SEnableCollider)
                {
                    collider.enabled = false;
                }
                foreach (var collider in SDisableCollider)
                {
                    collider.enabled = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                foreach (var collider in WEnableCollider)
                {
                    collider.enabled = true;
                }
                foreach (var collider in WDisableCollider)
                {
                    collider.enabled = false;
                }
            }
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                foreach (var collider in WEnableCollider)
                {
                    collider.enabled = false;
                }
                foreach (var collider in WDisableCollider)
                {
                    collider.enabled = true;
                }
            }
        }
        if (AttackCooldown <= AttackCurrent)
        {
            inAttack = false;
            AttackCurrent = 0;
        }
        if (inAttack)
        {
            AttackCurrent += 0.1f;
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
    private bool inAttack = false;
    private void Slash()
    {
        if (!inAttack)
        {
            animator.SetTrigger("Slash");
            inAttack = true;
        }
    }
}
