using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;
    [SerializeField] private Transform enemyWeapon;
    [SerializeField] private bool hasWeapon;
    private Vector3 initScale;
    private new Rigidbody2D rigidbody;


    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private bool movingLeft;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy Animator")]
     private Animator animator;

    [Header("Player detection")]
    [SerializeField] BoxCollider2D enemyBoxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;

    [Header("Jump")]
    [SerializeField] private bool canJump;
    [SerializeField] private float jumpHeight;
    [SerializeField] LayerMask layerMask;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private BoxCollider2D wallCheckBox;
    private bool isGrounded = true;
    private float jumpTimer = 1;
    private float jumpCooldown = 1;
    private bool movementLimited = false;
    private bool isUnderPlayer = false;
    [SerializeField] private BoxCollider2D feetBox;
    [SerializeField] Transform Feet;


    private GameObject playerObj = null;
    private void Awake()
    {

        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        initScale = enemy.localScale;
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate()
    {
        if(!animator.GetBool("died"))
        {

            if (PlayerInSight())
            {
                animator.SetBool("alerted", true);
            }
            else
            {
                animator.SetBool("alerted", false);
            }

            if (canJump)
            {
                float yDistanceFromPlayer =  playerObj.transform.position.y - enemy.position.y;
                float xDistanceFromPlayer = enemy.position.x - playerObj.transform.position.x;
                isGrounded = Physics2D.OverlapBox(Feet.position, feetBox.size, 0, layerMask);
                movementLimited = Physics2D.OverlapBox(wallCheck.position, wallCheckBox.size, 0, layerMask);
                 if(yDistanceFromPlayer > 1 && Mathf.Abs(xDistanceFromPlayer) < 3 )
                {
                    isUnderPlayer = true;
                } else
                {
                    isUnderPlayer = false;
                }

                if (isGrounded && jumpTimer >= jumpCooldown)
                {
                    animator.SetBool("jumping", false);
                }

            }
          


            if (animator.GetBool("alerted"))
            {
                if(canJump)
                {
                    jumpTimer += Time.deltaTime;
                    if (movementLimited || isUnderPlayer)
                    {

                        if (jumpTimer > jumpCooldown)
                        {
                            JumpTowardsPlayer();
                        }
                    }
                }
            
            
                
                if (enemy.position.x > playerObj.transform.position.x)
                {
                    MoveInDirection(-1);
                }
                else
                {
                    MoveInDirection(1);
                }
               
                if (enemy.position.x < leftEdge.position.x || enemy.position.x >= rightEdge.position.x)
                {
                    leftEdge.position = new Vector3(enemy.position.x - 3, leftEdge.position.y, leftEdge.position.z);
                    rightEdge.position = new Vector3(enemy.position.x + 3, rightEdge.position.y, rightEdge.position.z);
                }
            } else
            {
             
                if (movingLeft)
                {
                    if (enemy.position.x >= leftEdge.position.x)
                        MoveInDirection(-1);
                    else
                        DirectionChange();
                }
                else
                {
                    if (enemy.position.x <= rightEdge.position.x)
                        MoveInDirection(1);
                    else
                        DirectionChange();
                }
            
            }

        } else
        {
            StopMovement();
        }
    }
    private bool PlayerInSight()
    {
        RaycastHit2D inSight = Physics2D.BoxCast(
            enemyBoxCollider.bounds.center + enemy.right * range * enemy.localScale.x * colliderDistance,
            new Vector3(enemyBoxCollider.bounds.size.x * range, enemyBoxCollider.bounds.size.y* 8, enemyBoxCollider.bounds.size.z),
            0,
            Vector2.left,
            0,
            playerLayer
            );
        return inSight.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(
            enemyBoxCollider.bounds.center + enemy.right * range * enemy.localScale.x * colliderDistance,
            new Vector3(enemyBoxCollider.bounds.size.x * range, enemyBoxCollider.bounds.size.y *8, enemyBoxCollider.bounds.size.z)
            );
    }

    private void OnDisable()
    {
        animator.SetBool("moving", false);
    }
    private void DirectionChange()
    {
      
        animator.SetBool("moving", false);
        rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);

        idleTimer += Time.deltaTime;
        if (idleTimer > idleDuration)
            movingLeft = !movingLeft;
       
    }
    private void MoveInDirection(int _direction)
    {
        if(!animator.GetBool("died"))
        {

            idleTimer = 0;
            animator.SetBool("moving", true);
            rigidbody.velocity = new Vector2(_direction * speed, rigidbody.velocity.y);

            FlipDirection(_direction);
        }
    }

    private void FlipDirection(int _direction)
    {
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
            initScale.y,
            initScale.z);
        if(hasWeapon)
        {
            enemyWeapon.localScale = new Vector3(Mathf.Abs(enemyWeapon.localScale.x) * _direction, Mathf.Abs(enemyWeapon.localScale.y) * _direction, enemyWeapon.localScale.z);
        }
    }

    private void JumpTowardsPlayer()
    {
        float distanceFromPlayer = (playerObj.transform.position.x - enemy.position.x);
        float xForce = distanceFromPlayer;
        int direction = (distanceFromPlayer < 0) ? -1 : 1;
        if(distanceFromPlayer < 6)
        {
            xForce /= 2;
        }
        if(isGrounded)
        {
            animator.SetBool("jumping", true);
            rigidbody.AddForce(new Vector2(xForce, jumpHeight), ForceMode2D.Impulse);
            jumpTimer = 0;
            FlipDirection(direction);
        }
    }

    //Stops the movement, switches enemys boxcollider to a deathboxcollider
    public void SwitchColliders()
    {
        animator.SetBool("moving", false);
        enemyBoxCollider.size = new Vector2(6.5f, 1.4f);
        Destroy(feetBox);
    }


    public void StopMovement()
    {
       rigidbody.velocity = Vector3.zero;
    }
}
