using System.Collections;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    [SerializeField] private Transform insideCameraSpot;
    [SerializeField] private GameObject interactionText;
    [SerializeField] private AudioSource hidingAudio;
    [SerializeField] private AudioClip enterHidingSound;
    [SerializeField] private AudioClip exitHidingSound;

    private bool isPlayerNear = false;
    private bool isHiding = false;
    private PlayerController player;
    private Transform originalCameraParent;
    private Vector3 originalCameraLocalPos;
    private Quaternion originalCameraLocalRot;
    private BossController ghoulBoss;

    public bool IsPlayerHiding => isHiding;

    private void Start()
    {
        ghoulBoss = FindObjectOfType<BossController>();
        if (interactionText != null) interactionText.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            ToggleHiding();
        }

        if (isHiding)
        {
            HandleHidingBreathCheck();
        }
    }

    private void ToggleHiding()
    {
        if (player == null) return;

        isHiding = !isHiding;
        player.IsHiding = isHiding;

        Transform camTransform = Camera.main != null ? Camera.main.transform : null;

        if (isHiding)
        {
            if (camTransform != null)
            {
                originalCameraParent = camTransform.parent;
                originalCameraLocalPos = camTransform.localPosition;
                originalCameraLocalRot = camTransform.localRotation;

                if (insideCameraSpot != null)
                {
                    camTransform.position = insideCameraSpot.position;
                    camTransform.rotation = insideCameraSpot.rotation;
                }
            }

            Renderer[] renderers = player.GetComponentsInChildren<Renderer>();
            foreach (var r in renderers) r.enabled = false;

            if (hidingAudio != null && enterHidingSound != null)
                hidingAudio.PlayOneShot(enterHidingSound);
        }
        else
        {
            if (camTransform != null && originalCameraParent != null)
            {
                camTransform.SetParent(originalCameraParent);
                camTransform.localPosition = originalCameraLocalPos;
                camTransform.localRotation = originalCameraLocalRot;
            }

            Renderer[] renderers = player.GetComponentsInChildren<Renderer>();
            foreach (var r in renderers) r.enabled = true;

            if (hidingAudio != null && exitHidingSound != null)
                hidingAudio.PlayOneShot(exitHidingSound);
        }

        if (interactionText != null) interactionText.SetActive(!isHiding && isPlayerNear);
    }

    private void HandleHidingBreathCheck()
    {
        if (ghoulBoss == null) ghoulBoss = FindObjectOfType<BossController>();
        if (ghoulBoss == null) return;

        float distToBoss = Vector3.Distance(transform.position, ghoulBoss.transform.position);

        if (distToBoss <= 6.0f)
        {
            bool holdingBreath = Input.GetKey(KeyCode.Space);
            if (!holdingBreath)
            {
                // Ghoul hears breathing sound if player is not holding breath nearby!
                ghoulBoss.OnHearSound(transform.position);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            player = other.GetComponent<PlayerController>();
            if (interactionText != null && !isHiding) interactionText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (interactionText != null) interactionText.SetActive(false);
        }
    }
}
