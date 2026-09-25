using TMPro;
using UnityEngine;

public class LampController : MonoBehaviour
{
    [SerializeField] private Light lampLight;
    [SerializeField] private float onIntensity = 5f;
    [SerializeField] private float fadeSpeed = 10f;
    [SerializeField] private TMP_Text stateLabel;
    [SerializeField] private string labelWhenOff = "ALLUMER";
    [SerializeField] private string labelWhenOn = "ÉTEINDRE";

    private bool isOn;

    private void Start()
    {
        lampLight.intensity = 0f;
        UpdateLabel();
    }

    private void Update()
    {
        float target = isOn ? onIntensity : 0f;
        lampLight.intensity = Mathf.MoveTowards(lampLight.intensity, target, fadeSpeed * Time.deltaTime);
    }

    public void Toggle()
    {
        isOn = !isOn;
        UpdateLabel();
    }

    private void UpdateLabel()
    {
        if (stateLabel != null)
            stateLabel.text = isOn ? labelWhenOn : labelWhenOff;
    }
}