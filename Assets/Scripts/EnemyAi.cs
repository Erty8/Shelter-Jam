
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAi : MonoBehaviour
{
    public enum AIState
    {
        Idle,
        Chasing,
        Forgetting,
        Shooting
    }
    public Animator anim;
    [SerializeField] bool sawPlayer = false;
    public float forgetTime = 1.5f;
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    public float rotateVelocity;
    public float rotateSpeedMovement;

    //patrolling
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;
    
    //attacking
    public float timeBetweenAttacks = 0.2f;
    
    public GameObject projectile;
    public Transform bulletTransform;
    public bool attackBool=false;
    public bool alreadyAttacked;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private Coroutine shootingRoutine;

    public AIState state = AIState.Idle;
    private void Start()
    {
        anim = gameObject.transform.Find("Enemy Body").GetComponent<Animator>();
    }
    private void Awake()
    {
        player = GameObject.Find("Player").transform;  //playerobj player ismi check
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        animstate();
        Debug.Log("already attack");
        
        //check sight or attack
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hitinfo, 20f))
        {
            if (hitinfo.transform.CompareTag("Player"))
            {
                state = AIState.Chasing;
                //playerInSightRange = true;
                //sawPlayer = true;
                //Debug.Log("enemy saw player");
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitinfo.distance, Color.red);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitinfo.distance, Color.green);
                //Debug.Log("wont see");
                state = AIState.Idle;
                attackBool = false;
                //StopCoroutine("shootingRoutine");
                //Invoke("forgetPlayer", forgetTime);
            }

        }
        
      
        //Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        //playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if(Physics.CheckSphere(transform.position, attackRange, whatIsPlayer))
        {
            state = AIState.Shooting;
            //attackBool = true;
        }

        //if (!playerInAttackRange && !playerInSightRange) Patrolling(); //
        if (state is AIState.Idle) Patrolling();
        if (state is AIState.Chasing) ChasePlayer(); //walkPointSet
        if (state is AIState.Shooting) AttackPlayer();
        

    }
    void animstate()
    {
        switch (state)
        {
            case (AIState.Shooting):
                anim.SetBool("Attack", true);
                agent.SetDestination(transform.position);
                break;
            case (AIState.Chasing):
                anim.SetBool("Chase", true);
                anim.SetBool("Attack", false);
                agent.speed = 4;
                break;
            case (AIState.Forgetting):
                anim.SetBool("Walk", true);
                anim.SetBool("Chase", false);
                agent.speed = 1.5f;
                break;
            case (AIState.Idle):
                anim.SetBool("Idle", true);
                Debug.Log("AgentSpeed" + agent.speed);
                break; 

        }
    }

    private void Patrolling()   
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            Debug.Log("destination set"+ walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        StartCoroutine (wait());
        //walkppoint Reached
        if (distanceToWalkPoint.magnitude < 1f)
        { walkPointSet = false; }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(2);
        anim.SetBool("Walk", false);
        agent.SetDestination(transform.position);
        anim.SetBool("Idle", true);
        yield return new WaitForSeconds(5);
        anim.SetBool("Walk", true);
        anim.SetBool("Idle", false);

    }

    private void SearchWalkPoint()
    {
        //calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
            //state = AIState.Idle;
            //NavMeshHit hit;
            //if (NavMesh.SamplePosition(walkPoint, out hit, 1.0f, NavMesh.AllAreas))
            
                //walkPoint = hit.position;
                //walkPointSet = true;
            
        }
       


    }
    IEnumerator shooting()
    {
        if(attackBool) {
            Rigidbody rb = Instantiate(projectile, bulletTransform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            attackBool = false;
            
            //alreadyAttacked = true;
        }
        yield return new WaitForSeconds(timeBetweenAttacks);
        shootingRoutine = null;

    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        Invoke("forgetPlayer", forgetTime);
    }
    void forgetPlayer()
    {
        state = AIState.Idle;
        
        /*if (!playerInSightRange)
        {
            sawPlayer = false;
            playerInSightRange = false;
        }*/

    }
    
    private void AttackPlayer()
    {
        Quaternion rotationToLookAt = Quaternion.LookRotation(new Vector3(player.transform.position.x, 0, player.transform.position.z) - new Vector3(transform.position.x, 0, transform.position.z));
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y,rotationToLookAt.eulerAngles.y, ref rotateVelocity, rotateSpeedMovement * (Time.deltaTime * 1));
        transform.eulerAngles = new Vector3(0, rotationY, 0);
        ///Attack code here

        //rb.AddForce(transform.up * 8f, ForceMode.Impulse);
        ///End of attack code
        //make sure enemy doesnt move
        agent.SetDestination(transform.position);
        Debug.Log("enemy stopped");

        transform.LookAt(player);

        Debug.Log("routine öncessi");
        Debug.Log(shootingRoutine);
        if(shootingRoutine == null)
        {
            attackBool = true;
            shootingRoutine = StartCoroutine(shooting());

        }
        

        if (!alreadyAttacked)
        {
            //alreadyAttacked = true;
            //Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        
    }
    

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
