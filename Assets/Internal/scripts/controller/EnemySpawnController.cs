using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] private Vector2 spawnXAxisTop = Vector2.zero;
    [SerializeField] private Vector2 spawnXAxisDown = Vector2.zero;
    [SerializeField] private Vector2 spawnYAxisTop = Vector2.zero;
    [SerializeField] private Vector2 spawnYAxisDown = Vector2.zero;
    [SerializeField] private float commonTimeBwtSpawn = 1f;
    float currentTimeBwtSpawn = 0f;

    [SerializeField] private List<EnemyAutoSpawn> enemiesConfig = new();

    private List<EnemyAutoSpawnItem> enemySpawns = new();

    private List<EnemyAutoSpawnTemp> enemySpawnTemp = new();

    private List<EnemySpawnTimeControl> enemySpawnConfig = new();

    public struct EnemySpawnTimeControl
    {
        public float timeBtwSpawn;
        public int currentIndex;
        public EnemySpawnTimeControl(float timeBtwSpawn, int currentIndex)
        {
            this.timeBtwSpawn = timeBtwSpawn;
            this.currentIndex = currentIndex;
        }
    }

    bool wasReload = false;
    private void Update()
    {
        if (!DayNightController.instance.IsNight())
        {
            wasReload = false;
            currentTimeBwtSpawn += Time.deltaTime;
            int currentHour = DayNightController.instance.GetCurrentHour();
            if (enemySpawnTemp != null && enemySpawnTemp.Count > 0)
            {
                foreach (var enemyTemp in enemySpawnTemp)
                {
                    if (enemyTemp.spawnAtHour < currentHour)
                    {
                        enemySpawnTemp.Remove(enemyTemp);
                        enemySpawns.Add(new(enemyTemp.enemyName, enemyTemp.amount, enemyTemp.timeBwtSpawn));
                        enemySpawnConfig.Add(new(enemyTemp.timeBwtSpawn, 0));
                    }
                }
            }

            if (enemySpawnConfig != null && enemySpawnConfig.Count > 0)
            {
                for (int i = 0; i < enemySpawnConfig.Count; i++)
                {
                    EnemySpawnTimeControl item = enemySpawnConfig[i];
                    EnemyAutoSpawnItem tempEnemy = enemySpawns[i];
                    if (tempEnemy.amount > 0)
                    {
                        bool canSpawn = item.timeBtwSpawn * item.currentIndex <= currentTimeBwtSpawn;
                        item.currentIndex += 1;
                        if (canSpawn)
                        {
                            Enemy enemy = EnemyPrefab.instance.GetEnemy(tempEnemy.enemyName);
                            EnemySpawn(enemy.gameObject);
                            tempEnemy.amount -= 1;
                        }
                    }
                }
            }
        }
        else
        {
            if (!wasReload)
            {
                wasReload = true;
                currentTimeBwtSpawn = 0f;
                int day = DayNightController.instance.GetDay();
                enemySpawnConfig?.Clear();
                enemySpawns?.Clear();
                enemySpawnTemp?.Clear();
                foreach (var enemy in enemiesConfig)
                {
                    if ((day + 1) >= enemy.startSpawnDay)
                    {
                        if (enemy.endSpawnDay >= 0 && (day + 1) > enemy.endSpawnDay)
                        {
                            return;
                        }
                        if (enemy.spawnAtHour < 0)
                        {
                            foreach (var enemyItem in enemy.enemies)
                            {
                                float timeBwtSpawnTemp = enemyItem.timeBwtSpawn > 0f ? enemyItem.timeBwtSpawn : commonTimeBwtSpawn;
                                enemySpawns.Add(new(enemyItem.enemyName, enemyItem.amount, timeBwtSpawnTemp));
                                enemySpawnConfig.Add(new(timeBwtSpawnTemp, 0));
                            }
                        }
                        else
                        {
                            foreach (var enemyItem in enemy.enemies)
                            {
                                float timeBwtSpawnTemp = enemyItem.timeBwtSpawn > 0f ? enemyItem.timeBwtSpawn : commonTimeBwtSpawn;
                                enemySpawnTemp.Add(new(enemyItem.enemyName, enemyItem.amount, timeBwtSpawnTemp, enemy.spawnAtHour));
                            }
                        }
                    }
                }
            }
        }
    }
    private void EnemySpawn(GameObject enemySpawn)
    {
        int ranPos = Random.Range(0, 4);
        Vector2 randomX = Vector2.zero;
        Vector2 randomY = Vector2.zero;

        switch (ranPos)
        {
            case 0:
                randomX = spawnXAxisTop;
                randomY = spawnYAxisTop;
                break;
            case 1:
                randomX = spawnXAxisDown;
                randomY = spawnYAxisTop;
                break;
            case 2:
                randomX = spawnXAxisTop;
                randomY = spawnYAxisDown;
                break;
            case 3:
                randomX = spawnXAxisDown;
                randomY = spawnYAxisDown;
                break;
        }

        float ranX = Random.Range(Mathf.Min(randomX.x, randomX.y), Mathf.Max(randomX.x, randomX.y));
        float ranY = Random.Range(Mathf.Min(randomY.x, randomY.y), Mathf.Max(randomY.x, randomY.y));
        Instantiate(enemySpawn, new(ranX, ranY, 0f), Quaternion.identity);
    }
}
[System.Serializable]
public class EnemyAutoSpawn
{
    public int startSpawnDay = 0;
    public int endSpawnDay = -1;
    public float spawnAtHour = -1;
    public List<EnemyAutoSpawnItem> enemies = new();
}
[System.Serializable]
public class EnemyAutoSpawnItem
{
    public EnemyName enemyName;
    public int amount = 0;
    public float timeBwtSpawn = -1f;
    public EnemyAutoSpawnItem(EnemyName enemyName, int amount, float timeBwtSpawn)
    {
        this.enemyName = enemyName;
        this.amount = amount;
        this.timeBwtSpawn = timeBwtSpawn;
    }
}

public class EnemyAutoSpawnTemp
{
    public EnemyName enemyName;
    public float timeBwtSpawn = -1f;
    public int amount = 0;
    public float spawnAtHour = 0f;
    public EnemyAutoSpawnTemp(EnemyName enemyName, int amount, float timeBwtSpawn, float spawnAtHour)
    {
        this.enemyName = enemyName;
        this.amount = amount;
        this.timeBwtSpawn = timeBwtSpawn;
        this.spawnAtHour = spawnAtHour;
    }
}