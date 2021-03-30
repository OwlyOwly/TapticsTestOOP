using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;

    private List<GameObject> pooledEnemies;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int amountToPool = 10;

    private void Awake()
    {
        SharedInstance = this;
        pooledEnemies = new List<GameObject>();
        GameObject tempEnemyGO;
        Enemy tempEnemy;
        for (int i = 0; i < amountToPool; i++)
        {
            tempEnemyGO = Instantiate(enemyPrefab);
            tempEnemy = tempEnemyGO.GetComponent<Enemy>();
            tempEnemy.SetStats(i + 1);
            tempEnemyGO.SetActive(false);
            pooledEnemies.Add(tempEnemyGO);
        }
    }

    public GameObject GetNextEnemy(int index)
    {
        return pooledEnemies[index];
    }
}