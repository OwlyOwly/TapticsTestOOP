using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Enemy currentEnemy { get; private set; }
    public int killsToWin { get; private set; } = 10;
    public int enemyCount { get; private set; } = 0;
    public int damagePerClick { get; private set; } = 10;

    public event Action OnEnemySpawnedEvent;

    private Vector2 spawnPosition = new Vector2(-3.5f, 4f);

    [SerializeField] GameObject victoryScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] Timer timer;

    private void Start()
    {
        ActivateNextEnemy();
        timer.onTimeIsOver += LaunchLoseSequence;
    }

    private void ActivateNextEnemy()
    {
        if (enemyCount == killsToWin)
        {
            LaunchVictorySequence();
        }
        else
        {
            currentEnemy = ObjectPool.SharedInstance.GetNextEnemy(enemyCount).GetComponent<Enemy>();
            currentEnemy.OnEnemyDiedEvent += ActivateNextEnemy;
            currentEnemy.transform.position = spawnPosition;
            currentEnemy.gameObject.SetActive(true);
            OnEnemySpawnedEvent?.Invoke();
            enemyCount++;
        }
    }

    private void LaunchVictorySequence()
    {
        float lastResult = timer.GetRemainingTime();
        PlayerPrefs.SetFloat("lastResult", lastResult);

        if (PlayerPrefs.GetFloat("localRecord") <= lastResult)
        {
            PlayerPrefs.SetFloat("localRecord", lastResult);
        }

        victoryScreen.SetActive(true);
    }

    private void LaunchLoseSequence()
    {
        loseScreen.SetActive(true);
    }

    public void GiveBonusTry()
    {
        loseScreen.SetActive(false);
    }

    public void DealDamage()
    {
        currentEnemy.GetDamage(damagePerClick);
    }
}
