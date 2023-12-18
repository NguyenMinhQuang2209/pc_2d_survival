using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthTxt;

    [SerializeField] private float recoverHealthPercentDefault = 30;


    int currentStoreDay = 0;

    private void Start()
    {
        MyInitialised();
        healthSlider.minValue = 0;
        healthSlider.maxValue = GetMaxHealth();
        healthSlider.value = GetCurrentHealth();
        healthTxt.text = GetCurrentHealth() + "/" + GetMaxHealth();
        UpgradeController.instance.OnBuyPlusItem += OnChangePlusItemEvent;

        currentStoreDay = DayNightController.instance.GetDay();
    }
    private void Update()
    {
        int currentDay = DayNightController.instance.GetDay();
        if (currentStoreDay != currentDay)
        {
            currentStoreDay = currentDay;

            float recoverHealthPercentPlus = PlusCommonConfig.instance.GetPlusCommon(PlusCommonItem.Recover_Health);
            int recoverHealth = (int)Mathf.Ceil(((recoverHealthPercentDefault + recoverHealthPercentPlus) / 100f) * GetMaxHealth());
            DamageShowController.instance.ShowDamageTxt(transform.position, "+" + recoverHealth, Color.green);
            RecoverHealth(recoverHealth);
            healthSlider.value = GetCurrentHealth();
            healthTxt.text = GetCurrentHealth() + "/" + GetMaxHealth();
        }
    }

    private void OnChangePlusItemEvent(object sender, EventArgs e)
    {
        plusHealth = (int)PlusCommonConfig.instance.GetPlusCommon(PlusCommonItem.Health);
        healthSlider.maxValue = GetMaxHealth();
        healthTxt.text = GetCurrentHealth() + "/" + GetMaxHealth();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        healthSlider.value = GetCurrentHealth();
        healthTxt.text = GetCurrentHealth() + "/" + GetMaxHealth();
    }

    public override void ObjectDie()
    {
        WarningController.instance.PlayerDie();
    }
}
