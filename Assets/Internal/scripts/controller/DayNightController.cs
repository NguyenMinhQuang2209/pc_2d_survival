using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightController : MonoBehaviour
{
    public static DayNightController instance;

    [SerializeField] private TextMeshProUGUI dateTxt;

    [Space(10)]
    [Header("Time config")]
    [SerializeField] private float timeSpeed = 0f;
    [SerializeField] private float startTimeHour = 0f;
    [SerializeField] private float sunRiseHour = 7f;
    [SerializeField] private float sunSetHour = 7f;

    DateTime startDate;
    DateTime currentTime;
    TimeSpan sunRiseTime;
    TimeSpan sunSetTime;

    bool isNight = false;

    [SerializeField] private GameObject nightImageObject;
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
        if (currentTime.TimeOfDay >= sunRiseTime && currentTime.TimeOfDay < sunSetTime)
        {
            nightImageObject.SetActive(false);
            isNight = false;
        }
        else
        {
            nightImageObject.SetActive(true);
            isNight = true;
        }
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
            if (isNight)
            {
                nightImageObject.SetActive(false);
                isNight = false;
            }
        }
        else
        {
            if (!isNight)
            {
                nightImageObject.SetActive(true);
                isNight = true;
                LogController.instance.Log("Đêm rồi!");
            }
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
