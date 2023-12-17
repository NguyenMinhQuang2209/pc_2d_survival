using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 1;
    protected int plusHealth = 0;
    int currentHealth = 0;
    bool objectDie = false;
    public bool showDamageTxt = true;
    public void MyInitialised()
    {
        currentHealth = maxHealth + plusHealth;
    }
    public virtual void TakeDamage(int damage)
    {
        if (objectDie)
        {
            return;
        }
        if (showDamageTxt)
        {
            DamageShowController.instance.ShowDamageTxt(transform.position, damage + "");
        }
        currentHealth = Mathf.Max(0, currentHealth - damage);
        if (currentHealth == 0)
        {
            objectDie = true;
            ObjectDie();
        }
    }
    public virtual void ObjectDie()
    {
        Destroy(gameObject);
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
[System.Serializable]
public class PlusKey
{
    public ItemName name;
    public ItemPlusType type;
    public string GetKey()
    {
        return name.ToString() + type;
    }
    public PlusKey(ItemName name, ItemPlusType type)
    {
        this.name = name;
        this.type = type;
    }
}