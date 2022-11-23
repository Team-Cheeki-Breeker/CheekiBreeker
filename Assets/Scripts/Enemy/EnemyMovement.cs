using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
     private Transform enemy;
    private Vector3 initScale;
    private Rigidbody2D rigidbody;
   


    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private bool movingLeft;
    private bool leftPatrol = false;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy Animator")]
     private Animator animator;

    [Header("Player detection")]
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;

    [Header("Jump")]
    private bool isGrounded = true;
    [SerializeField] private float jumpHeight;
    private bool isJumping = false;
    [SerializeField] LayerMask layerMask;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private BoxCollider2D wallCheckBox;

    private bool movementLimited = false;
    [SerializeField] private BoxCollider2D feetBox;
    [SerializeField] Transform Feet;

    private int jumpCooldown = 1;
    private float jumpTimer = 1;
    private GameObject playerObj = null;
    private void Awake()
    {

        rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        enemy = GetComponent<Transform>();
        initScale = enemy.localScale;
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        StartCoroutine(CheckGrounded());
       
        movementLimited = Physics2D.OverlapBox(wallCheck.position, wallCheckBox.size, 0, layerMask);

        if (PlayerInSight())
        {
            animator.SetBool("alerted", true);
        }
        else
        {
            animator.SetBool("alerted", false);
        }



        if (animator.GetBool("alerted"))
        {
            if (movementLimited)
            {
                if(isGrounded)
                {
                   
                    float distanceFromPlayer = (playerObj.transform.position.x - enemy.position.x);
                    animator.SetBool("jumping", true);
                    rigidbody.AddForce(new Vector2(distanceFromPlayer, jumpHeight), ForceMode2D.Impulse);
                    animator.SetBool("jumping", false);

                }
            } 
            if(!animator.GetBool("jumping"))
            {
                if (enemy.position.x > playerObj.transform.position.x)
                {
                    MoveInDirection(-1);
                }
                else
                {
                    MoveInDirection(1);
                }
            }
            if (enemy.position.x < leftEdge.position.x || enemy.position.x >= rightEdge.position.x)
            {
                leftPatrol = true;
                leftEdge.position = new Vector3(enemy.position.x - 3, leftEdge.position.y, leftEdge.position.z);
                rightEdge.position = new Vector3(enemy.position.x + 3, rightEdge.position.y, rightEdge.position.z);
            }
        } else
        {
            if (!leftPatrol)
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
        }
   
    }
    private bool PlayerInSight()
    {
        RaycastHit2D inSight = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y* 3, boxCollider.bounds.size.z),
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
            boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y *3, boxCollider.bounds.size.z)
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
        idleTimer = 0;
        animator.SetBool("moving", true);
        rigidbody.velocity = new Vector2(_direction * speed, rigidbody.velocity.y);
      
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
            initScale.y,
            initScale.z);
    }

    private IEnumerator CheckGrounded()
    {

        bool lastGrounded = isGrounded;
        bool currentGrounded = Physics2D.OverlapBox(Feet.position, feetBox.size, 0, layerMask);
        if (!currentGrounded && lastGrounded)
        {
            yield return new WaitForSeconds(0.02f);
        }
        isGrounded = currentGrounded;
    }



}
