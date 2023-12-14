using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogController : MonoBehaviour
{
    public static LogController instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void Log(string log)
    {
        Debug.Log(log);
    }
    public void Log(string log, GameObject obj)
    {
        Debug.Log(log, obj);
    }
}
