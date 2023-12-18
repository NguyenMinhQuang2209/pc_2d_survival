using TMPro;
using UnityEngine;

public class WarningController : MonoBehaviour
{
    public static WarningController instance;
    public GameObject warningObject;

    public TextMeshProUGUI warningTxt;
    public float showWarningTxtTime = 2f;

    public GameObject dieShowObject;
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
        HidenWarningObject();
        dieShowObject.SetActive(false);
    }

    public void ShowWarning(string v)
    {
        warningObject.SetActive(true);
        warningTxt.text = v;
        Invoke(nameof(HidenWarningObject), showWarningTxtTime);
    }
    public void ShowWarning(string v, float customWarningTime)
    {
        warningObject.SetActive(true);
        warningTxt.text = v;
        Invoke(nameof(HidenWarningObject), customWarningTime);

    }
    private void HidenWarningObject()
    {
        warningObject.SetActive(false);
    }

    public void PlayerDie()
    {
        dieShowObject.SetActive(true);
        Time.timeScale = 0f;
    }
    public void BackToSelectScene()
    {
        if (GameSceneManager.instance != null)
        {
            Time.timeScale = 1f;
            GameSceneManager.instance.LoadNewScene(SceneName.SelectCharacter);
        }
    }
}
