using System.Collections.Generic;
using UnityEngine;

public class EnemyPrefab : MonoBehaviour
{
    public static EnemyPrefab instance;

    [SerializeField] private List<EnemyItem> enemies = new();
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public Enemy GetEnemy(EnemyName name)
    {
        if (enemies != null)
        {
            foreach (var enemy in enemies)
            {
                if (enemy.enemyName == name)
                {
                    return enemy.enemy;
                }
            }
        }
        return null;
    }
}
[System.Serializable]
public class EnemyItem
{
    public EnemyName enemyName;
    public Enemy enemy;
}
