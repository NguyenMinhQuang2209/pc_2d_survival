using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageShowConfig : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt;
    [SerializeField] private float floatSpeed = 1f;
    [SerializeField] private float delayDieTime = 1f;
    private void Start()
    {
        Destroy(gameObject, delayDieTime);
    }
    private void Update()
    {
        transform.position += new Vector3(0f, 1f * Time.deltaTime * floatSpeed, 0f);
    }
    public void InitText(string damage)
    {
        txt.text = damage;
    }
    public void InitText(string damage, Color txtColor)
    {
        txt.text = damage;
        txt.color = txtColor;
    }
}
