using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1;
    protected int plusHealth = 0;
    int currentHealth = 0;
    bool objectDie = false;
    public void MyInitialised()
    {
        currentHealth = maxHealth + plusHealth;
    }
    public virtual void TakeDamage(int damage)
    {
        if (!objectDie)
        {

        }
        currentHealth = Mathf.Max(0, currentHealth - damage);
        if (currentHealth == 0)
        {
            objectDie = true;
        }
    }
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    public int GetMaxHealth()
    {
        return maxHealth + plusHealth;
    }
}
