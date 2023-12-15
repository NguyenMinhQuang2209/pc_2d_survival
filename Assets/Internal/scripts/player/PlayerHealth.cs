using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health
{
    [SerializeField] private Slider healthSlider;
    private void Start()
    {
        MyInitialised();
        healthSlider.minValue = 0;
        healthSlider.maxValue = GetMaxHealth();
        healthSlider.value = GetCurrentHealth();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        healthSlider.value = GetCurrentHealth();
    }
}
