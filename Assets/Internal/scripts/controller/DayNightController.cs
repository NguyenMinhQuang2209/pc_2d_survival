using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightController : MonoBehaviour
{
    public static DayNightController instance;

    [SerializeField] private TextMeshProUGUI dateTxt;
    [SerializeField] private Light2D light2D;

    [Space(10)]
    [Header("Time config")]
    [SerializeField] private float timeSpeed = 0f;
    [SerializeField] private float startTimeHour = 0f;
    [SerializeField] private float sunRiseHour = 7f;
    [SerializeField] private float sunSetHour = 7f;

    [SerializeField] private float nightIntensity = 0.5f;
    [SerializeField] private float dayIntensity = 1f;
    DateTime startDate;
    DateTime currentTime;
    TimeSpan sunRiseTime;
    TimeSpan sunSetTime;

    bool isNight = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startTimeHour);
        sunRiseTime = TimeSpan.FromHours(sunRiseHour);
        sunSetTime = TimeSpan.FromHours(sunSetHour);
        startDate = DateTime.Now.Date + TimeSpan.FromHours(0f);
    }
    private void Update()
    {
        currentTime = currentTime.AddSeconds(timeSpeed * Time.deltaTime);
        if (dateTxt != null)
        {
            dateTxt.text = currentTime.ToString("HH:mm") + "\n" + "Ngày: " + ((currentTime - startDate).Days);
        }

        if (currentTime.TimeOfDay >= sunRiseTime && currentTime.TimeOfDay < sunSetTime)
        {
            light2D.intensity = dayIntensity;
            isNight = false;
        }
        else
        {
            light2D.intensity = nightIntensity;
            if (!isNight)
            {
                LogController.instance.Log("Đêm rồi!");
            }
            isNight = true;
        }
    }
    public bool IsNight()
    {
        return isNight;
    }
    public int GetDay()
    {
        return (currentTime - startDate).Days + 1;
    }
    public int GetCurrentHour()
    {
        return currentTime.TimeOfDay.Hours;
    }
}
