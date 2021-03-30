using System;
using System.Threading.Tasks;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int baseHealth = 10;
    private int hpLevelMultiplier = 10;
    private int totalHealth;
    private int currentHealth;
    private int currentLevel;
    private Animator anim;

    public event Action OnEnemyDiedEvent;
    public event Action OnEnemyDamaged;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetStats(int level)
    {
        totalHealth = baseHealth + (level * hpLevelMultiplier);
        currentHealth = totalHealth;
        currentLevel = level;
    }

    public void GetDamage(int amount)
    {
        anim.SetTrigger("isAttacked");
        currentHealth -= amount;
        OnEnemyDamaged?.Invoke();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public async void Die()
    {
        anim.SetTrigger("isDead");
        OnEnemyDiedEvent?.Invoke();
        await Task.Delay(TimeSpan.FromSeconds(1));
        gameObject.SetActive(false);
    }

    public int GetLevel()
    {
        return currentLevel;
    }

    public int GetMaxHealth()
    {
        return totalHealth;
    }
}
