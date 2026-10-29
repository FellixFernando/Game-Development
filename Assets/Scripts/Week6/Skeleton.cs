using UnityEngine;

public class Skeleton : MonoBehaviour
{
    [SerializeField] private Transform[] point;
    [SerializeField] private float kec = 2f;
    [SerializeField] private float returnDelay = 0.5f;
    [SerializeField] private float patrolPause = 3f;
    Animator anim;

    private enum State
    {
        Patrol,
        PatrolPause,
        Chase,
        Return
    }

    private State state = State.Patrol;
    private int idxPoint;
    private int returnPoint;
    private int returnTargetPoint;
    private float returnTimer;
    private Transform hero;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    private void Update()
    {
        if (state == State.Chase)
        {
            if (hero == null)
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isAttacking", false);
        
                returnTimer = returnDelay;
                state = State.Return;
                return;
            }
        
            float distance = Vector3.Distance(
                transform.position,
                hero.position
            );
        
            if (distance <= 1.5f)
            {
                // Close enough to attack
                anim.SetBool("isWalking", false);
                anim.SetBool("isAttacking", true);
            }
            else
            {
                // Chase hero
                anim.SetBool("isWalking", true);
                anim.SetBool("isAttacking", false);
        
                MoveTo(hero.position, false);
            }
        }
        else if (state == State.Return)
        {
            returnTimer -= Time.deltaTime;
    
            if (returnTimer > 0f)
            {
                anim.SetBool("isWalking", false);
                return;
            }
    
            MoveTo(point[returnTargetPoint].position, true);
    
            if (AtPoint(point[returnTargetPoint]))
            {
                if (returnTargetPoint != returnPoint)
                {
                    returnTargetPoint = returnPoint;
                    return;
                }
    
                idxPoint = (returnPoint + 1) % point.Length;
                returnTimer = patrolPause;
                state = State.PatrolPause;
    
                anim.SetBool("isWalking", false);
            }
        }
        else if (state == State.PatrolPause)
        {
            returnTimer -= Time.deltaTime;
    
            if (returnTimer <= 0f)
            {
                state = State.Patrol;
            }
        }
        else
        {
            MoveTo(point[idxPoint].position, true);
    
            if (AtPoint(point[idxPoint]))
            {
                idxPoint = (idxPoint + 1) % point.Length;
                returnTimer = patrolPause;
                state = State.PatrolPause;
    
                anim.SetBool("isWalking", false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Hero"))
        {
            return;
        }
    
        hero = other.transform.root;
    
        returnPoint = idxPoint;
        returnTargetPoint = returnPoint == 3 ? 2 : returnPoint;
    
        state = State.Chase;
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Hero"))
        {
            return;
        }
    
        hero = other.transform.root;
        state = State.Chase;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Hero"))
        {
            return;
        }
    
        if (other.transform != hero)
        {
            return;
        }
    
        hero = null;
    
        anim.SetBool("isWalking", false);
        anim.SetBool("isAttacking", false);
    
        returnTimer = returnDelay;
        state = State.Return;
    }

    private void MoveTo(Vector3 target, bool useTargetHeight)
    {
        anim.SetBool("isWalking", true);

        Vector3 destination = target;

        if (!useTargetHeight)
        {
            destination.y = transform.position.y;
        }

        Vector3 nextPosition = Vector3.MoveTowards(
            rb.position,
            destination,
            kec * Time.deltaTime
        );

        rb.MovePosition(nextPosition);

        Vector3 direction = destination - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(
                direction,
                Vector3.up
            );
        }
    }

    private bool AtPoint(Transform target)
    {
        Vector3 position = transform.position;
        position.y = target.position.y;

        return Vector3.Distance(position, target.position) < 0.1f;
    }
}