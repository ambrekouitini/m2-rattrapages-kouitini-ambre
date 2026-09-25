using System.Collections;
using UnityEngine;

public class ProductConfigurator : MonoBehaviour
{
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private float colorTransitionDuration = 0.4f;
    [SerializeField] private float rotationSpeed = 25f;

    private static readonly int BaseColor = Shader.PropertyToID("_BaseColor");

    private Material material;
    private Coroutine colorRoutine;
    private bool isRotating;

    private void Awake()
    {
        if (targetRenderer == null)
            targetRenderer = GetComponentInChildren<Renderer>();

        material = targetRenderer.material;
    }

    private void Update()
    {
        if (isRotating)
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }

    public void SetTint(string hex)
    {
        if (!ColorUtility.TryParseHtmlString("#" + hex, out Color target)) return;

        if (colorRoutine != null) StopCoroutine(colorRoutine);
        colorRoutine = StartCoroutine(FadeColor(target));
    }

    public void ToggleRotation()
    {
        isRotating = !isRotating;
    }

    private IEnumerator FadeColor(Color target)
    {
        Color start = material.GetColor(BaseColor);
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / colorTransitionDuration;
            material.SetColor(BaseColor, Color.Lerp(start, target, t));
            yield return null;
        }
    }
}