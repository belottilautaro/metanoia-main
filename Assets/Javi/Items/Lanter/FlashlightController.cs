using UnityEngine;
using UnityEngine.UI;

public class FlashlightController : MonoBehaviour
{
    [Header("Flashlight")]
    [SerializeField] private Light flashlight;
    [SerializeField] private KeyCode toggleKey = KeyCode.F;
    [SerializeField] private KeyCode intensityKey = KeyCode.Mouse1;

    [Header("Intensity")]
    [SerializeField]private float highIntensity;
    [SerializeField]private float lowIntensity;
    public float LowIntensity => lowIntensity;
    public float currentIntensity { get; private set; }

    [Header("Battery")]
    [SerializeField] private float maxBattery;
    [SerializeField] private float drainRateLow;
    [SerializeField] private float drainRateHigh;
    private float currentBattery;

    [Header("UI")]
    [SerializeField] private Image batteryBarImage;

    void Start()
    {
        flashlight.enabled = false;
        currentIntensity = lowIntensity;
        flashlight.intensity = currentIntensity;
        currentBattery = maxBattery;

        UpdateBatteryUI();
    }

    void Update()
    {
        HandleInput();
        DrainBattery();
        UpdateBatteryUI();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            if (flashlight.enabled)
            {
                flashlight.enabled = false;
            }
            else if (currentBattery > 0f)
            {
                flashlight.enabled = true;
            }
        }

        if (Input.GetKeyDown(intensityKey) && flashlight.enabled)
        {
            ToggleIntensity();
        }
    }

    void ToggleIntensity()
    {
        currentIntensity = (currentIntensity == highIntensity) ? lowIntensity : highIntensity;
        flashlight.intensity = currentIntensity;
    }

    void DrainBattery()
    {
        if (!flashlight.enabled || currentBattery <= 0f) return;

        float drainRate = (currentIntensity == highIntensity) ? drainRateHigh : drainRateLow;
        currentBattery -= drainRate * Time.deltaTime;
        currentBattery = Mathf.Clamp(currentBattery, 0f, maxBattery);

        if (currentBattery <= 0f)
        {
            flashlight.enabled = false;
        }
    }

    void UpdateBatteryUI()
    {
        if (batteryBarImage != null)
        {
            batteryBarImage.fillAmount = currentBattery / maxBattery;
        }
    }

    public bool IsFlashlightOn() => flashlight.enabled;

    public void RechargeBattery(float amount)
    {
        currentBattery = Mathf.Clamp(currentBattery + amount, 0f, maxBattery);
    }
}
