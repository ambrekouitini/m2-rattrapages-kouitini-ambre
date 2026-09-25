using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRBaseInteractable))]
public class InteractionFeedback : MonoBehaviour
{
    [SerializeField] private Color accentColor = new Color(0.55f, 0.08f, 0.16f);
    [SerializeField] private float hoverIntensity = 1f;
    [SerializeField] private float selectIntensity = 2f;
    [SerializeField] private float hoverScale = 1.05f;

    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");

    private XRBaseInteractable interactable;
    private readonly List<Material> materials = new List<Material>();
    private readonly List<Color> baseEmissions = new List<Color>();
    private Vector3 baseScale;

    private enum State { Normal, Hover, Selected }
    private State current = State.Normal;

    private void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
        baseScale = transform.localScale;

        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
        {
            foreach (Material mat in rend.materials)
            {
                mat.EnableKeyword("_EMISSION");
                materials.Add(mat);
                baseEmissions.Add(mat.HasProperty(EmissionColor) ? mat.GetColor(EmissionColor) : Color.black);
            }
        }
    }

    private void Update()
    {
        State next = interactable.isSelected ? State.Selected
                   : interactable.isHovered  ? State.Hover
                   : State.Normal;

        if (next == current) return;
        current = next;
        Apply(current);
    }

    private void Apply(State state)
    {
        for (int i = 0; i < materials.Count; i++)
        {
            Color emission = state switch
            {
                State.Hover    => accentColor * hoverIntensity,
                State.Selected => accentColor * selectIntensity,
                _              => baseEmissions[i],
            };
            materials[i].SetColor(EmissionColor, emission);
        }

        transform.localScale = state == State.Hover ? baseScale * hoverScale : baseScale;
    }
}