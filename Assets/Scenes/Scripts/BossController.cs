using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class BossController : MonoBehaviour
{

    [SerializeField] private GameObject player;
    private NavMeshAgent agent;
    private readonly float distanceRun = 18.0f;
    private readonly float distanceAttack = 3.0f;
    private readonly float limitTime = 8.0f;
    private float time;
    private Animation anim;
    public static bool isAttack = false;
    [SerializeField] private float radius;
    private Vector3 pos;
    private AudioSource scream;
    private bool warning = false;

    void Start()
    {
        anim = GetComponent<Animation>();
        agent = GetComponent<NavMeshAgent>();
        scream = GetComponent<AudioSource>();
        pos = transform.position;
    }

    void Update()
    {
        time += Time.deltaTime;
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= distanceRun && !CheckInRoom())
        {
            if (!scream.isPlaying && !warning)
            {
                warning = true;
                scream.Play();
            }
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
            warning = false;
            if(Vector3.Distance(transform.position, pos) <= 5.0f || time > limitTime)
            {
                time = 0.0f;
                Vector3 randDirection = Random.insideUnitSphere * radius;
                randDirection += transform.position;
                NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, radius, -1);
                pos = navHit.position;
                Debug.Log(pos);
                agent.SetDestination(navHit.position);
            }else if((distance > distanceRun && Vector3.Distance(transform.position, pos) > 5.0f) || CheckInRoom())
            {
                agent.SetDestination(pos);
            }
        }
        if (isAttack)
        {
            StartCoroutine(LoadSceneAsync(2));
        }
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        while (!operation.isDone)
        {
            yield return null;
        }
    }

    bool CheckInRoom()
    {
        GameObject[] center = GameObject.FindGameObjectsWithTag("Room_Center");
        foreach(GameObject check in center)
        {
            if(Vector3.Distance(check.transform.position, player.transform.position) <= 8.0f)
            {
                return true;
            }
        }
        return false;
    }
}
