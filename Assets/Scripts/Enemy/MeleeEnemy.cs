using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;

    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;

    private float cooldownTimer = Mathf.Infinity;

    private Animator animator;
    private EnemyPatrolling enemyPatrolling;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyPatrolling = GetComponentInParent<EnemyPatrolling>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if(PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown) { 
                cooldownTimer = 0;
                animator.SetTrigger("meleeAttack");
            }
        }
        if(enemyPatrolling != null) {
            enemyPatrolling.enabled = !PlayerInSight();
        }
    }
    private bool PlayerInSight() {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right* range *transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0,
            Vector2.left,
            0,
            playerLayer
            );
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            boxCollider.bounds.center +transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z)
            );
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            //Damage player hp
        }
    }
}
