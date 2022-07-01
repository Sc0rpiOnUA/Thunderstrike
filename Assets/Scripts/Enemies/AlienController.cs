using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlienController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public AlienStatus alienStatus;
    public Animator alienAnimator;

    public LayerMask whatIsGround, whatIsPlayer;

    private bool isMoving, isDying;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        isDying = false;

        agent = GetComponent<NavMeshAgent>();
        alienStatus = GetComponent<AlienStatus>();
        alienAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isDying)
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange)
            {
                Patroling();
            }
            if (playerInSightRange && !playerInAttackRange)
            {
                ChasePlayer();
            }
            if (playerInSightRange && playerInAttackRange)
            {
                AttackPlayer();
            }

            if (isMoving)
            {
                alienAnimator.SetFloat("State", 1, 0.1f, Time.fixedDeltaTime);
            }
            else
            {
                alienAnimator.SetFloat("State", 0, 0.1f, Time.fixedDeltaTime);
            }
        }
    }

    public void SetPlayer(GameObject playerObject)
    {
        player = playerObject.transform;
    }
    public void Die()
    {
        isDying = true;
        agent.enabled = false;
        alienAnimator.SetTrigger("Death");
        alienAnimator.SetInteger("DeathVariant", Random.Range(1, 5));
    }


    private void Patroling()
    {
        if(!walkPointSet)
        {
            isMoving = false;
            SearchWalkPoint();
        }
        else
        {
            agent.SetDestination(walkPoint);
            isMoving = true;
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1)
        {
            walkPointSet = false;
            isMoving = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        isMoving = true;
    }

    private void AttackPlayer()
    {
        isMoving = false;
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        alienStatus.FireShot();

        Debug.Log("ATTACKING PLAYER!!!");
    }

    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
