using UnityEngine;

public class UVSecretDecal : MonoBehaviour
{
    [SerializeField] private Renderer decalRenderer;
    [SerializeField] private float fadeSpeed = 3f;

    private float currentAlpha = 0f;
    private bool isBeingIlluminated = false;

    private void Awake()
    {
        if (decalRenderer == null) decalRenderer = GetComponent<Renderer>();
        SetAlpha(0f);
    }

    private void Update()
    {
        if (isBeingIlluminated)
        {
            currentAlpha = Mathf.MoveTowards(currentAlpha, 1f, fadeSpeed * Time.deltaTime);
            isBeingIlluminated = false; // Reset frame trigger
        }
        else
        {
            currentAlpha = Mathf.MoveTowards(currentAlpha, 0f, fadeSpeed * Time.deltaTime);
        }

        SetAlpha(currentAlpha);
    }

    public void RevealDecal()
    {
        isBeingIlluminated = true;
    }

    private void SetAlpha(float alpha)
    {
        if (decalRenderer == null || decalRenderer.material == null) return;

        if (decalRenderer.material.HasProperty("_Color"))
        {
            Color c = decalRenderer.material.color;
            c.a = alpha;
            decalRenderer.material.color = c;
        }
    }
}
