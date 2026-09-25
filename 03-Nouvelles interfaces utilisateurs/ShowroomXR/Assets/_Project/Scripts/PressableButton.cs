using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

[RequireComponent(typeof(XRBaseInteractable))]
public class PressableButton : MonoBehaviour
{
    [SerializeField] private float pressDepth = 0.015f;
    [SerializeField] private float pressDuration = 0.15f;
    [SerializeField] private float pressSpeed = 0.3f;
    [SerializeField] private UnityEvent onPressed;

    private XRBaseInteractable interactable;
    private Vector3 restPosition;
    private float pressTimer;

    private void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
        restPosition = transform.localPosition;
    }

    private void Update()
    {
        if (WasPressedByHoveringHand())
        {
            onPressed.Invoke();
            pressTimer = pressDuration;
        }

        if (pressTimer > 0f)
            pressTimer -= Time.deltaTime;

        Vector3 target = pressTimer > 0f ? restPosition + Vector3.down * pressDepth : restPosition;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, target, pressSpeed * Time.deltaTime);
    }

    private bool WasPressedByHoveringHand()
    {
        foreach (var hoveringInteractor in interactable.interactorsHovering)
        {
            if (hoveringInteractor is XRBaseInputInteractor hand &&
                hand.activateInput.ReadWasPerformedThisFrame())
                return true;
        }
        return false;
    }
}