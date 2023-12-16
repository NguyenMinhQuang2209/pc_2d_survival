using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] private Vector2 spawnXAxis = Vector2.zero;
    [SerializeField] private Vector2 spawnYAxis = Vector2.zero;

    [SerializeField] private List<EnemySpawnSetup> enemySetup = new();
    private List<Enemy> currentEnemySpawn = new();
    int currentDay = 0;
    private void Start()
    {

    }
    private void Update()
    {
        int day = DayNightController.instance.GetDay();
        if (currentDay != day)
        {
            currentDay = day;

        }
    }


}
[System.Serializable]
public class EnemySpawnSetup
{
    public EnemyName enemyName;
    public float timeBwtSpawn = 0f;
    public int spawnDay = 0;
    public bool useEndDay = false;
    public int endSpawnDay = 0;
}