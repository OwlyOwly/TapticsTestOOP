using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Enemy CurrentEnemy { get; private set; }
    public int KillsToWin { get; private set; } = 10;
    public int EnemyCount { get; private set; } = 0;
    public int DamagePerClick { get; private set; } = 10;

    public event Action OnEnemySpawnedEvent;

    private Vector2 spawnPosition = new Vector2(-3.5f, 4f);

    [SerializeField] GameObject victoryScreen;
    [SerializeField] private GameObject loseScreen;
    [SerializeField] Timer timer;

    private void Start()
    {
        ActivateNextEnemy();
        timer.OnTimeIsOver += LaunchLoseSequence;
    }

    private void ActivateNextEnemy()
    {
        if (EnemyCount == KillsToWin)
        {
            LaunchVictorySequence();
        }
        else
        {
            CurrentEnemy = ObjectPool.SharedInstance.GetNextEnemy(EnemyCount).GetComponent<Enemy>();
            CurrentEnemy.OnEnemyDiedEvent += ActivateNextEnemy;
            CurrentEnemy.transform.position = spawnPosition;
            CurrentEnemy.gameObject.SetActive(true);
            OnEnemySpawnedEvent?.Invoke();
            EnemyCount++;
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
        CurrentEnemy.GetDamage(DamagePerClick);
    }
}
