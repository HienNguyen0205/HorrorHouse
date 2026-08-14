using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoreCollector : MonoBehaviour
{
    public static LoreCollector Instance { get; private set; }

    public enum EndingType
    {
        BadEnding = 0,
        NormalEnding = 1,
        TrueEnding = 2
    }

    [Header("Collected Notes")]
    private List<string> collectedNoteIds = new List<string>();
    private List<string> collectedTitles = new List<string>();
    private List<string> collectedContents = new List<string>();

    [Header("Notebook UI")]
    [SerializeField] private GameObject notebookPanel;
    [SerializeField] private Text titleText;
    [SerializeField] private Text contentText;
    [SerializeField] private Text counterText;

    private int currentReadingIndex = 0;

    public int CollectedCount => collectedNoteIds.Count;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        collectedNoteIds.Clear();
        collectedTitles.Clear();
        collectedContents.Clear();

        if (notebookPanel != null) notebookPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            ToggleNotebookUI();
        }
    }

    public void CollectNote(string noteId, string title, string content)
    {
        if (!collectedNoteIds.Contains(noteId))
        {
            collectedNoteIds.Add(noteId);
            collectedTitles.Add(title);
            collectedContents.Add(content);
            currentReadingIndex = collectedNoteIds.Count - 1;
            ShowNoteUI(currentReadingIndex);
        }
    }

    public EndingType EvaluateEnding()
    {
        EndingType ending;
        if (CollectedCount < 4)
        {
            ending = EndingType.BadEnding;
        }
        else if (CollectedCount < 8)
        {
            ending = EndingType.NormalEnding;
        }
        else
        {
            ending = EndingType.TrueEnding;
        }

        PlayerPrefs.SetInt("FinalEndingType", (int)ending);
        PlayerPrefs.Save();
        return ending;
    }

    public void ToggleNotebookUI()
    {
        if (notebookPanel == null) return;

        bool active = !notebookPanel.activeSelf;
        notebookPanel.SetActive(active);

        if (active && CollectedCount > 0)
        {
            ShowNoteUI(currentReadingIndex);
        }
    }

    public void ShowNextNote()
    {
        if (CollectedCount == 0) return;
        currentReadingIndex = (currentReadingIndex + 1) % CollectedCount;
        ShowNoteUI(currentReadingIndex);
    }

    public void ShowPreviousNote()
    {
        if (CollectedCount == 0) return;
        currentReadingIndex = (currentReadingIndex - 1 + CollectedCount) % CollectedCount;
        ShowNoteUI(currentReadingIndex);
    }

    private void ShowNoteUI(int index)
    {
        if (notebookPanel != null && !notebookPanel.activeSelf)
        {
            notebookPanel.SetActive(true);
        }

        if (index >= 0 && index < CollectedCount)
        {
            if (titleText != null) titleText.text = collectedTitles[index];
            if (contentText != null) contentText.text = collectedContents[index];
            if (counterText != null) counterText.text = $"{index + 1} / {CollectedCount}";
        }
    }
}
