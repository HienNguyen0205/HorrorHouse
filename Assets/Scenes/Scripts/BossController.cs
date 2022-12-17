using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class BossController : MonoBehaviour
{

    [SerializeField] private GameObject player;
    private NavMeshAgent agent;
    private readonly float distanceRun = 15.0f;
    private readonly float distanceAttack = 3.0f;
    private Animation anim;
    public static bool isAttack = false;
    [SerializeField] private float radius;
    private Vector3 pos;

    void Start()
    {
        anim = GetComponent<Animation>();
        agent = GetComponent<NavMeshAgent>();
        pos = transform.position;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance < distanceRun)
        {
            Vector3 dirToPlayer = transform.position - player.transform.position;
            Vector3 newPos = transform.position - dirToPlayer;
            if (distance <= distanceAttack)
            {
                anim.Play("Attack1");
                isAttack = true;
            }
            else
            {
                anim.Play("Walk");
            }
            agent.SetDestination(newPos);
        }
        else
        {
            if(Vector3.Distance(transform.position, pos) <= 7.0f || distance < distanceRun)
            {
                Vector3 randPos = RandomNavSphere(transform.position, radius, -1);
                agent.SetDestination(randPos);
                pos = randPos;
            }
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, dist, layermask);

        return navHit.position;
    }
}
