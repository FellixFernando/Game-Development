using UnityEngine;

public class EnemyBL : MonoBehaviour
{
    [SerializeField] private Transform[] point;
    [SerializeField] private float kec = 2f;
    [SerializeField] private float returnDelay = 0.5f;
    [SerializeField] private float patrolPause = 3f;

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

    private void Update()
    {

        if (state == State.Chase)
        {
            if (hero == null)
            {
                returnTimer = returnDelay;
                state = State.Return;
                return;
            }

            MoveTo(hero.position, false);
        }
        else if (state == State.Return)
        {
            returnTimer -= Time.deltaTime;
            if (returnTimer > 0f)
            {
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
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Hero"))
        {
            return;
        }

        hero = other.transform;
        returnPoint = idxPoint;
        returnTargetPoint = returnPoint == 3 ? 2 : returnPoint;
        state = State.Chase;
    }

    private void OnTriggerExit(Collider other)
    {
        if (hero == null || other.transform != hero)
        {
            return;
        }

        hero = null;
        returnTimer = returnDelay;
        state = State.Return;
    }

    private void MoveTo(Vector3 target, bool useTargetHeight)
    {
        Vector3 destination = target;
        if (!useTargetHeight)
        {
            destination.y = transform.position.y;
        }

        Vector3 nextPosition = Vector3.MoveTowards(rb.position, destination, kec * Time.deltaTime);
        rb.MovePosition(nextPosition);

        Vector3 direction = destination - transform.position;
        direction.y = 0f;
        if (direction.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }

    private bool AtPoint(Transform target)
    {
        Vector3 position = transform.position;
        position.y = target.position.y;
        return Vector3.Distance(position, target.position) < 0.1f;
    }
}
