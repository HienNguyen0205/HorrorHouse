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

    private bool isSceneLoading = false;

    void Start()
    {
        isAttack = false;
        anim = GetComponent<Animation>();
        agent = GetComponent<NavMeshAgent>();
        scream = GetComponent<AudioSource>();
        pos = transform.position;
    }

    private float nextPathUpdateTime = 0f;
    private Vector3 lastTargetPos;

    void Update()
    {
        time += Time.deltaTime;
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= distanceRun && !CheckInRoom())
        {
            if (!scream.isPlaying && !warning)
            {
                warning = true;
                if (scream != null) scream.Play();
            }
            if (distance <= distanceAttack)
            {
                if (anim != null) anim.Play("Attack1");
                isAttack = true;
            }
            else
            {
                if (anim != null) anim.Play("Walk");
            }
            
            if (Time.time >= nextPathUpdateTime || Vector3.Distance(lastTargetPos, player.transform.position) > 0.5f)
            {
                nextPathUpdateTime = Time.time + 0.15f;
                lastTargetPos = player.transform.position;
                if (agent != null && agent.enabled) agent.SetDestination(player.transform.position);
            }
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
        if (isAttack && !isSceneLoading)
        {
            isSceneLoading = true;
            StartCoroutine(LoadSceneAsync(2));
        }
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        while (!operation.isDone)
        {
            yield return null;
        }
    }

    private GameObject[] centerRooms;

    bool CheckInRoom()
    {
        if (centerRooms == null || centerRooms.Length == 0)
        {
            centerRooms = GameObject.FindGameObjectsWithTag("Room_Center");
        }
        foreach(GameObject check in centerRooms)
        {
            if(check != null && player != null && Vector3.Distance(check.transform.position, player.transform.position) <= 8.0f)
            {
                return true;
            }
        }
        return false;
    }
}
