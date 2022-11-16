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

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy Animator")]
    [SerializeField] private Animator animator;

    [Header("Player detection")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;

    private GameObject playerObj = null;
    private void Awake()
    {
        initScale = enemy.localScale;
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {

        if (PlayerInSight())    
        {
            animator.SetBool("alerted", true);
            if (enemy.position.x > playerObj.transform.position.x) {
                MoveInDirection(-1);
            } else
            {
                MoveInDirection(1);
            }
            leftEdge.position = new Vector3(enemy.position.x - 3, leftEdge.position.y, leftEdge.position.z);
            rightEdge.position = new Vector3(enemy.position.x + 3, rightEdge.position.y, rightEdge.position.z);
        }
        else
        {
            animator.SetBool("alerted", false);
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

        idleTimer += Time.deltaTime;
        if (idleTimer > idleDuration)
            movingLeft = !movingLeft;
       
    }
    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        animator.SetBool("moving", true);
        //Make enemy face direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
            initScale.y,
            initScale.z);
        //Move in that direction
        enemy.position = new Vector3(
            enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }

}
