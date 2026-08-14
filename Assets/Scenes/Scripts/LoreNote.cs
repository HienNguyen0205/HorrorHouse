using UnityEngine;

public class LoreNote : MonoBehaviour
{
    [Header("Lore Content")]
    [SerializeField] private string noteId = "Note_1";
    [SerializeField] private string noteTitle = "Note Title";
    [TextArea(3, 8)]
    [SerializeField] private string noteContent = "Lore note text content...";

    [Header("Audio & Visual")]
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private GameObject interactionPrompt;

    private bool isPlayerNear = false;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
        if (interactionPrompt != null) interactionPrompt.SetActive(false);
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.F))
        {
            Collect();
        }
    }

    private void Collect()
    {
        if (LoreCollector.Instance != null)
        {
            LoreCollector.Instance.CollectNote(noteId, noteTitle, noteContent);
        }

        if (pickupSound != null && audioSource != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position, 1.0f);
        }

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (interactionPrompt != null) interactionPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (interactionPrompt != null) interactionPrompt.SetActive(false);
        }
    }
}
