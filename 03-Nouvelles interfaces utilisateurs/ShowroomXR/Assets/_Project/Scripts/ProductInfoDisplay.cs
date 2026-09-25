using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRBaseInteractable))]
public class ProductInfoDisplay : MonoBehaviour
{
    [SerializeField] private CanvasGroup infoPanel;
    [SerializeField] private float fadeSpeed = 5f;
    [SerializeField] private float hideDelay = 0.6f;
    [SerializeField] private bool faceUser = true;

    private XRBaseInteractable interactable;
    private Transform userHead;
    private float lastHoverTime = -10f;

    private void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
        userHead = Camera.main.transform;

        infoPanel.alpha = 0f;
        infoPanel.interactable = false;
        infoPanel.blocksRaycasts = false;
    }

    private void Update()
    {
        if (interactable.isHovered)
            lastHoverTime = Time.time;

        bool visible = Time.time - lastHoverTime < hideDelay;
        infoPanel.alpha = Mathf.MoveTowards(infoPanel.alpha, visible ? 1f : 0f, fadeSpeed * Time.deltaTime);

        if (faceUser && infoPanel.alpha > 0f)
        {
            Vector3 direction = infoPanel.transform.position - userHead.position;
            direction.y = 0f;
            if (direction.sqrMagnitude > 0.001f)
                infoPanel.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}