using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    public static EnemySpawnController instance;

    [SerializeField] private Vector2 spawnXAxisTop = Vector2.zero;
    [SerializeField] private Vector2 spawnXAxisDown = Vector2.zero;
    [SerializeField] private Vector2 spawnYAxisTop = Vector2.zero;
    [SerializeField] private Vector2 spawnYAxisDown = Vector2.zero;

    [Header("For spawn config")]
    [SerializeField] private Vector2 xAxisDistance = Vector2.zero;
    [SerializeField] private Vector2 yAxisDistance = Vector2.zero;
    [SerializeField] private float commonTimeBwtSpawn = 1f;
    float currentTimeBwtSpawn = 0f;

    [SerializeField] private List<EnemyAutoSpawn> enemiesConfig = new();

    private List<EnemyAutoSpawnItem> enemySpawns = new();

    private List<EnemyAutoSpawnTemp> enemySpawnTemp = new();

    private List<EnemySpawnTimeControl> enemySpawnConfig = new();


    bool wasReload = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Update()
    {
        if (!DayNightController.instance.IsNight())
        {
            wasReload = false;
            currentTimeBwtSpawn += Time.deltaTime;
            int currentHour = DayNightController.instance.GetCurrentHour();
            if (enemySpawnTemp != null && enemySpawnTemp.Count > 0)
            {
                for (int i = 0; i < enemySpawnTemp.Count; i++)
                {
                    var enemyTemp = enemySpawnTemp[i];

                    if (enemyTemp.spawnAtHour <= currentHour)
                    {
                        enemySpawns.Add(new(enemyTemp.enemyName, enemyTemp.amount, enemyTemp.timeBwtSpawn));
                        int ind = (int)(currentTimeBwtSpawn / enemyTemp.timeBwtSpawn) + 1;
                        enemySpawnConfig.Add(new(enemyTemp.timeBwtSpawn, ind));
                        enemySpawnTemp.RemoveAt(i);
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
                        if (canSpawn)
                        {
                            item.UpdateCurrentIndex();
                            Enemy enemy = EnemyPrefab.instance.GetEnemy(tempEnemy.enemyName);
                            EnemySpawn(enemy, tempEnemy.enemyName);
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
                GameObject[] list = GameObject.FindGameObjectsWithTag(MessageController.DESTROY_ON_NIGHT_TAG);
                for (int i = 0; i < list.Length; i++)
                {
                    Destroy(list[i]);
                }
                wasReload = true;
                currentTimeBwtSpawn = 0f;
                int day = DayNightController.instance.GetDay();
                enemySpawnConfig?.Clear();
                enemySpawns?.Clear();
                enemySpawnTemp?.Clear();
                foreach (var enemy in enemiesConfig)
                {
                    if (day >= enemy.startSpawnDay)
                    {
                        if (enemy.endSpawnDay >= 0 && day > enemy.endSpawnDay)
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
    private void EnemySpawn(Enemy enemySpawn, EnemyName name)
    {
        int ranPos = Random.Range(0, 4);
        Vector2 randomX = Vector2.zero;
        Vector2 randomY = Vector2.zero;

        switch (ranPos)
        {
            case 0:
                randomX = spawnXAxisTop;
                randomY = yAxisDistance;
                break;
            case 1:
                randomX = spawnXAxisDown;
                randomY = yAxisDistance;
                break;
            case 2:
                randomX = xAxisDistance;
                randomY = spawnYAxisTop;
                break;
            case 3:
                randomX = xAxisDistance;
                randomY = spawnYAxisDown;
                break;
        }

        float ranX = Random.Range(Mathf.Min(randomX.x, randomX.y), Mathf.Max(randomX.x, randomX.y));
        float ranY = Random.Range(Mathf.Min(randomY.x, randomY.y), Mathf.Max(randomY.x, randomY.y));
        Enemy enemy = Instantiate(enemySpawn, new(ranX, ranY, 0f), Quaternion.identity);
        enemy?.EnemyInit(name);
    }
    public void AddEnemy(EnemyName name)
    {
        if (enemySpawnConfig != null && enemySpawnConfig.Count > 0)
        {
            for (int i = 0; i < enemySpawnConfig.Count; i++)
            {
                EnemyAutoSpawnItem tempEnemy = enemySpawns[i];
                if (tempEnemy.enemyName == name)
                {
                    tempEnemy.PlusAmount();
                    return;
                }
            }
        }
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
    public void PlusAmount()
    {
        amount += 1;
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
public class EnemySpawnTimeControl
{
    public float timeBtwSpawn = 0f;
    public int currentIndex = 0;
    public EnemySpawnTimeControl(float timeBtwSpawn, int currentIndex)
    {
        this.timeBtwSpawn = timeBtwSpawn;
        this.currentIndex = currentIndex;
    }
    public void UpdateCurrentIndex()
    {
        currentIndex += 1;
    }
}