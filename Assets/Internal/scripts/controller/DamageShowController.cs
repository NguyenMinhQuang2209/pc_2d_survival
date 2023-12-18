using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageShowController : MonoBehaviour
{
    public static DamageShowController instance;
    public DamageShowConfig showConfigObject;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void ShowDamageTxt(Vector3 pos, string damage, Color txtColor)
    {
        DamageShowConfig temp = Instantiate(showConfigObject, pos, Quaternion.identity);
        temp.InitText(damage, txtColor);
    }
}
