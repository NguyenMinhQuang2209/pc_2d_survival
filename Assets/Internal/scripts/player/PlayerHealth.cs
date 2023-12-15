using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthTxt;


    private void Start()
    {
        MyInitialised();
        healthSlider.minValue = 0;
        healthSlider.maxValue = GetMaxHealth();
        healthSlider.value = GetCurrentHealth();
        healthTxt.text = GetCurrentHealth() + "/" + GetMaxHealth();

        UpgradeController.instance.OnBuyPlusItem += OnChangePlusItemEvent;
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
}
