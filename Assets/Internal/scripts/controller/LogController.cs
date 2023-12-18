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
    public void Log(string log, bool showLog = true)
    {
        Debug.Log(log);
        if (showLog)
        {
            WarningController.instance.ShowWarning(log);
        }
    }
    public void Log(string log, float showTime)
    {
        Debug.Log(log);
        WarningController.instance.ShowWarning(log, showTime);
    }
    public void Log(string log, GameObject obj, bool showLog = true)
    {
        Debug.Log(log, obj);
        if (showLog)
        {
            WarningController.instance.ShowWarning(log);
        }
    }
}
