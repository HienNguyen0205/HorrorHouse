using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

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
    [SerializeField] private float radius = 20.0f;
    private Vector3 pos;
    private AudioSource scream;
    private bool warning = false;
    private bool isSceneLoading = false;

    private float nextPathUpdateTime = 0f;
    private Vector3 lastTargetPos;

    // Advanced AI Fields
    private Vector3? noiseInvestigateTarget = null;
    private float noiseInvestigateTimer = 0f;
    private bool isStunned = false;
    private float stunTimer = 0f;
    private PlayerController playerCtrl;
    private ElectricTorchOnOff flashlight;

    void Start()
    {
        isAttack = false;
        anim = GetComponent<Animation>();
        agent = GetComponent<NavMeshAgent>();
        scream = GetComponent<AudioSource>();
        pos = transform.position;

        if (player == null) player = GameObject.FindWithTag("Player");
        if (player != null) playerCtrl = player.GetComponent<PlayerController>();
        flashlight = FindObjectOfType<ElectricTorchOnOff>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (player == null) player = GameObject.FindWithTag("Player");
        if (player == null) return;
        if (playerCtrl == null) playerCtrl = player.GetComponent<PlayerController>();

        // Handle Stun state from Flashlight/UV
        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
            if (agent != null && agent.enabled) agent.isStopped = true;
            if (stunTimer <= 0f)
            {
                isStunned = false;
                if (agent != null && agent.enabled) agent.isStopped = false;
            }
            return;
        }

        CheckFlashlightIllumination();

        bool isPlayerHiding = playerCtrl != null && playerCtrl.IsHiding;
        Vector3 bossPosXZ = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 playerPosXZ = new Vector3(player.transform.position.x, 0f, player.transform.position.z);
        float distance = Vector3.Distance(bossPosXZ, playerPosXZ);
        float heightDiff = Mathf.Abs(transform.position.y - player.transform.position.y);

        float effectiveAttackDist = distanceAttack;
        if (agent != null)
        {
            effectiveAttackDist = Mathf.Max(distanceAttack, agent.stoppingDistance + 0.6f);
        }

        // Noise Investigation Priority
        if (noiseInvestigateTarget.HasValue)
        {
            noiseInvestigateTimer -= Time.deltaTime;
            if (agent != null && agent.enabled) agent.SetDestination(noiseInvestigateTarget.Value);

            if (Vector3.Distance(transform.position, noiseInvestigateTarget.Value) <= 2.0f || noiseInvestigateTimer <= 0f)
            {
                noiseInvestigateTarget = null;
            }
        }
        else if (distance <= distanceRun && heightDiff <= 3.0f && !CheckInRoom() && !isPlayerHiding)
        {
            if (scream != null && !scream.isPlaying && !warning)
            {
                warning = true;
                scream.Play();
            }

            if (distance <= effectiveAttackDist)
            {
                TriggerPlayerAttack();
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
            if (Vector3.Distance(transform.position, pos) <= 5.0f || time > limitTime)
            {
                time = 0.0f;
                Vector3 randDirection = Random.insideUnitSphere * (radius > 0 ? radius : 20.0f);
                randDirection += transform.position;
                if (NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, 20.0f, -1))
                {
                    pos = navHit.position;
                    if (agent != null && agent.enabled) agent.SetDestination(navHit.position);
                }
            }
            else if ((distance > distanceRun && Vector3.Distance(transform.position, pos) > 5.0f) || CheckInRoom() || isPlayerHiding)
            {
                if (agent != null && agent.enabled) agent.SetDestination(pos);
            }
        }

        if (isAttack && !isSceneLoading)
        {
            isSceneLoading = true;
            if (agent != null && agent.enabled) agent.isStopped = true;
            StartCoroutine(LoadSceneAsync(2));
        }
    }

    public void OnHearSound(Vector3 soundPosition)
    {
        noiseInvestigateTarget = soundPosition;
        noiseInvestigateTimer = 6.0f;
        if (agent != null && agent.enabled) agent.SetDestination(soundPosition);
    }

    public void OnDetectFlashlight()
    {
        if (!isStunned)
        {
            isStunned = true;
            stunTimer = 1.0f; // Stunned for 1 second
            if (anim != null) anim.Play("Damage");
        }
    }

    private void CheckFlashlightIllumination()
    {
        if (flashlight == null) flashlight = FindObjectOfType<ElectricTorchOnOff>();
        if (flashlight != null && flashlight.IsFlashlightOn)
        {
            Vector3 dirToBoss = (transform.position - flashlight.transform.position).normalized;
            float angle = Vector3.Angle(flashlight.transform.forward, dirToBoss);
            float dist = Vector3.Distance(flashlight.transform.position, transform.position);

            if (angle < 30.0f && dist <= 12.0f)
            {
                OnDetectFlashlight();
            }
        }
    }

    private void TriggerPlayerAttack()
    {
        if (anim != null) anim.Play("Attack1");
        isAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.CompareTag("Player") && !CheckInRoom())
        {
            if (playerCtrl != null && playerCtrl.IsHiding) return;
            TriggerPlayerAttack();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null && collision.gameObject != null && collision.gameObject.CompareTag("Player") && !CheckInRoom())
        {
            if (playerCtrl != null && playerCtrl.IsHiding) return;
            TriggerPlayerAttack();
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
        foreach (GameObject check in centerRooms)
        {
            if (check != null && player != null && Vector3.Distance(check.transform.position, player.transform.position) <= 8.0f)
            {
                return true;
            }
        }
        return false;
    }
}
